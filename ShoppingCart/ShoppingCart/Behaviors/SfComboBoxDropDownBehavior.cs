using System;
using System.Windows.Input;
using Syncfusion.XForms.ComboBox;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace ShoppingCart.Behaviors
{
    /// <summary>
    /// This class extends the behavior of the SfComboBox control to invoke a command when an event occurs.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SfComboBoxDropDownBehavior : Behavior<SfComboBox>
    {
        #region Binable Properties

        /// <summary>
        /// Gets or sets the CommandProperty, and it is a bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(SfComboBoxDropDownBehavior));

        #endregion

        #region Public Property

        /// <summary>
        /// Gets or sets the Command.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets the comboBox.
        /// </summary>
        public SfComboBox ComboBox { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when adding comboBox to view.
        /// </summary>
        /// <param name="comboBox">The ComboBox</param>
        protected override void OnAttachedTo(SfComboBox comboBox)
        {
            base.OnAttachedTo(comboBox);
            ComboBox = comboBox;
            comboBox.BindingContextChanged += OnBindingContextChanged;
            comboBox.SelectionChanged += SelectionChanged;
        }

        /// <summary>
        /// Invoked when exit from the view.
        /// </summary>
        /// <param name="comboBox">The comboBox</param>
        protected override void OnDetachingFrom(SfComboBox comboBox)
        {
            base.OnDetachingFrom(comboBox);
            comboBox.BindingContextChanged -= OnBindingContextChanged;
            comboBox.SelectionChanged -= SelectionChanged;
            ComboBox = null;
        }

        /// <summary>
        /// Invoked when comboBox binding context is changed.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = ComboBox.BindingContext;
        }

        /// <summary>
        /// Invoked when selected item is changed.
        /// </summary>
        /// <param name="sender">The comboBox</param>
        /// <param name="e">The selection changed event args</param>
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int totalQuantity;
            int.TryParse(e.Value.ToString(), out totalQuantity);

            var bindingContext = (sender as SfComboBox).BindingContext;

            var propertyInfo = bindingContext.GetType().GetProperty("TotalQuantity");
            propertyInfo.SetValue(bindingContext, totalQuantity);

            if (Command == null) return;

            if (Command.CanExecute((sender as SfComboBox).BindingContext))
                Command.Execute((sender as SfComboBox).BindingContext);
        }

        /// <summary>
        /// Invoked when binding context is changed.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        #endregion
    }
}