using System.Collections.ObjectModel;
using System.Linq;
using ShoppingCart.Commands;
using ShoppingCart.Models;
using ShoppingCart.Views.Forms;
using ShoppingCart.Views.Onboarding;
using Syncfusion.SfRotator.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.ViewModels.Onboarding
{
    /// <summary>
    /// ViewModel for on-boarding gradient page with animation.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class OnBoardingAnimationViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OnBoardingAnimationViewModel" /> class.
        /// </summary>
        public OnBoardingAnimationViewModel()
        {
            SkipCommand = new DelegateCommand(Skip);
            NextCommand = new DelegateCommand(Next);
            Boardings = new ObservableCollection<Boarding>
            {
                new Boarding
                {
                    ImagePath = "ChooseGradient.svg",
                    Header = "CHOOSE",
                    Content = "Pick the item that is right for you",
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding
                {
                    ImagePath = "ConfirmGradient.svg",
                    Header = "ORDER CONFIRMED",
                    Content = "Your order is confirmed and will be on its way soon",
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding
                {
                    ImagePath = "DeliverGradient.svg",
                    Header = "DELIVERY",
                    Content = "Your item will arrive soon. Email us if you have any issues",
                    RotatorItem = new WalkthroughItemPage()
                }
            };

            // Set bindingcontext to content view.
            foreach (var boarding in Boardings) boarding.RotatorItem.BindingContext = boarding;
        }

        #endregion

        #region Fields

        private ObservableCollection<Boarding> boardings;

        private string nextButtonText = "NEXT";

        private bool isSkipButtonVisible = true;

        private int selectedIndex;

        #endregion

        #region Properties

        public ObservableCollection<Boarding> Boardings
        {
            get => boardings;

            set
            {
                if (boardings == value) return;

                boardings = value;
                OnPropertyChanged();
            }
        }

        public string NextButtonText
        {
            get => nextButtonText;

            set
            {
                if (nextButtonText == value) return;

                nextButtonText = value;
                OnPropertyChanged();
            }
        }

        public bool IsSkipButtonVisible
        {
            get => isSkipButtonVisible;

            set
            {
                if (isSkipButtonVisible == value) return;

                isSkipButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get => selectedIndex;

            set
            {
                if (selectedIndex == value) return;

                selectedIndex = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Skip button is clicked.
        /// </summary>
        public DelegateCommand SkipCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Done button is clicked.
        /// </summary>
        public DelegateCommand NextCommand { get; set; }

        #endregion

        #region Methods     

        private bool ValidateAndUpdateSelectedIndex(int itemCount)
        {
            if (SelectedIndex >= itemCount - 1) return true;

            SelectedIndex++;
            return false;
        }

        /// <summary>
        /// Invoked when the Skip button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Skip(object obj)
        {
            MoveToNextPage();
        }

        /// <summary>
        /// Invoked when the Done button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Next(object obj)
        {
            var itemCount = (obj as SfRotator).ItemsSource.Count();
            if (ValidateAndUpdateSelectedIndex(itemCount)) MoveToNextPage();
        }

        private void MoveToNextPage()
        {
            Application.Current.MainPage = new NavigationPage(new SimpleLoginPage());
        }

        #endregion
    }
}