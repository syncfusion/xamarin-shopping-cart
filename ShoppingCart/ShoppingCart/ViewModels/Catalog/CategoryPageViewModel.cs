using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using ShoppingCart.Commands;
using ShoppingCart.DataService;
using ShoppingCart.Models;
using ShoppingCart.Views.Catalog;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ScrollToPosition = Syncfusion.ListView.XForms.ScrollToPosition;

namespace ShoppingCart.ViewModels.Catalog
{
    /// <summary>
    /// ViewModel for Category page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CategoryPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CategoryPageViewModel" /> class.
        /// </summary>
        public CategoryPageViewModel(ICategoryDataService categoryDataService, string selectedCategory)
        {
            this.categoryDataService = categoryDataService;

            Device.BeginInvokeOnMainThread(() => { FetchCategories(selectedCategory); });
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with StackLayout, which displays the categories using ComboBox.
        /// </summary>
        public ObservableCollection<Category> Categories
        {
            get => categories;
            set
            {
                if (categories == value) return;

                categories = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Fields

        private ObservableCollection<Category> categories;

        private DelegateCommand scrollToStartCommand;

        private DelegateCommand scrollToEndCommand;

        private DelegateCommand categorySelectedCommand;

        private DelegateCommand expandingCommand;

        private DelegateCommand notificationCommand;
        private bool isMainCategory;
        private readonly ICategoryDataService categoryDataService;

        private DelegateCommand backButtonCommand;

        #endregion

        #region Command       

        /// <summary>
        /// Gets or sets the command that will be executed when the ScrollToStart button is clicked.
        /// </summary>
        public DelegateCommand ScrollToStartCommand =>
            scrollToStartCommand ?? (scrollToStartCommand = new DelegateCommand(ScrollToStartClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the ScrollToEnd button is clicked.
        /// </summary>
        public DelegateCommand ScrollToEndCommand =>
            scrollToEndCommand ?? (scrollToEndCommand = new DelegateCommand(ScrollToEndClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the Category is selected.
        /// </summary>
        public DelegateCommand CategorySelectedCommand =>
            categorySelectedCommand ?? (categorySelectedCommand = new DelegateCommand(CategorySelected));

        /// <summary>
        /// Gets or sets the command that will be executed when expander is expanded.
        /// </summary>
        public DelegateCommand ExpandingCommand =>
            expandingCommand ?? (expandingCommand = new DelegateCommand(ExpanderClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the Notification button is clicked.
        /// </summary>
        public DelegateCommand NotificationCommand =>
            notificationCommand ?? (notificationCommand = new DelegateCommand(NotificationClicked));

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public DelegateCommand BackButtonCommand =>
            backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));

        #endregion

        #region Methods

        /// <summary>
        /// Gets the categories from database
        /// </summary>
        /// <param name="selectedCategory">The selectedCategory</param>
        public async void FetchCategories(string selectedCategory)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedCategory))
                {
                    //Main category
                    var categories = await categoryDataService.GetCategories();
                    if (categories != null && categories.Count > 0)
                        Categories = new ObservableCollection<Category>(categories);

                    isMainCategory = true;
                }
                else
                {
                    //Sub category
                    isMainCategory = false;

                    var subcategories = await categoryDataService.GetSubCategories(int.Parse(selectedCategory));
                    if (subcategories != null && subcategories.Count > 0)
                        Categories = new ObservableCollection<Category>(subcategories);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void ScrollToStartClicked(object attachedObject)
        {
            if (!(attachedObject is SfListView listView)) return;

            var scrollRow = listView.GetVisualContainer()?.ScrollRows;
            var firstVisibleIndex = (int) scrollRow?.ScrollLineIndex;
            var totalItemsCount = listView.DataSource.DisplayItems.Count;

            int scrollToIndex;
            if (firstVisibleIndex > 0 && firstVisibleIndex < totalItemsCount - 1)
                scrollToIndex = firstVisibleIndex - 1;
            else
                scrollToIndex = 0;

            listView.LayoutManager.ScrollToRowIndex(scrollToIndex, ScrollToPosition.Center,
                true);
        }

        /// <summary>
        /// Invoked when the items are sorted.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private static void ScrollToEndClicked(object attachedObject)
        {
            if (!(attachedObject is SfListView listView)) return;

            var scrollRow = listView.GetVisualContainer()?.ScrollRows;
            var lastVisibleIndex = (int) scrollRow?.LastBodyVisibleLineIndex;
            var totalItemsCount = listView.DataSource.DisplayItems.Count;

            int scrollToIndex;
            if (lastVisibleIndex >= 0 && lastVisibleIndex < totalItemsCount - 1)
                scrollToIndex = lastVisibleIndex + 1;
            else
                scrollToIndex = totalItemsCount - 1;

            listView.LayoutManager.ScrollToRowIndex(scrollToIndex, ScrollToPosition.Center,
                true);
        }

        /// <summary>
        /// Invoked when the Category is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void CategorySelected(object attachedObject)
        {
            var category = attachedObject as Category;
            if (category == null && attachedObject is string)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CatalogListPage(string.Empty));
                return;
            }

            if (category != null)
            {
                if (isMainCategory)
                    await Application.Current.MainPage.Navigation.PushAsync(
                        new CategoryTilePage(category.ID.ToString()));
                else
                    await Application.Current.MainPage.Navigation.PushAsync(
                        new CatalogListPage(category.ID.ToString()));
                return;
            }

            await Application.Current.MainPage.DisplayAlert("Error",
                $"Selected any one category or selected category {attachedObject} not found", "Close");
        }

        /// <summary>
        /// Invoked when the expander is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ExpanderClicked(object obj)
        {
            var objects = obj as List<object>;
            var category = objects[0] as Category;
            var listView = objects[1] as SfListView;

            if (listView == null) return;

            var itemIndex = listView.DataSource.DisplayItems.IndexOf(category);
            var scrollIndex = itemIndex + category.SubCategories.Count;
            //Expand and bring the item in the view.
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(100);
                listView.LayoutManager.ScrollToRowIndex(scrollIndex, ScrollToPosition.End,
                    true);
            });
        }

        /// <summary>
        /// Invoked when the notification button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void NotificationClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}