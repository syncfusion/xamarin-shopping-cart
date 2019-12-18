using System.Windows.Input;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace ShoppingCart.Behaviors
{
    /// <summary>
    /// This class extends the behavior of the SfListView to invoke a command when an event occurs.
    /// </summary>
    public class SfListViewTapBehavior : Behavior<SfListView>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the CommandProperty, and it is a bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(SfListViewTapBehavior));

        /// <summary>
        /// Gets or sets the Command.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region Method

        /// <summary>
        /// Invoked when added sflistview to the page.
        /// </summary>
        /// <param name="bindableListView">The SfListView</param>
        protected override void OnAttachedTo(SfListView bindableListView)
        {
            base.OnAttachedTo(bindableListView);
            bindableListView.ItemTapped += BindableListView_ItemTapped;
        }

        /// <summary>
        /// Invoked when exit from the page.
        /// </summary>
        /// <param name="bindableListView">The SfListView</param>
        protected override void OnDetachingFrom(SfListView bindableListView)
        {
            base.OnDetachingFrom(bindableListView);
            bindableListView.ItemTapped -= BindableListView_ItemTapped;
        }

        /// <summary>
        /// Invoked when tapping the listview item.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">ItemTapped EventArgs</param>
        private void BindableListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Command == null) return;

            if (Command.CanExecute(e.ItemData)) Command.Execute(e.ItemData);
        }

        #endregion
    }
}