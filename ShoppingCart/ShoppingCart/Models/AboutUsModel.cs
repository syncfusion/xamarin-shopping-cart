using System.ComponentModel;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Models
{
    /// <summary>
    /// Model for About us templates.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AboutUsModel : INotifyPropertyChanged
    {
        #region EventHandler

        /// <summary>
        /// EventHandler of property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">The PropertyName</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Fields

        /// <summary>
        /// The employee name field.
        /// </summary>
        private string employeeName;

        /// <summary>
        /// The designation field.
        /// </summary>
        private string designation;

        /// <summary>
        /// The Image field.
        /// </summary>
        private string image;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of an employee.
        /// </summary>
        /// <value>The name.</value>
        public string EmployeeName
        {
            get => employeeName;

            set
            {
                employeeName = value;
                OnPropertyChanged("EmployeeName");
            }
        }

        /// <summary>
        /// Gets or sets the designation of an employee.
        /// </summary>
        /// <value>The designation.</value>
        public string Designation
        {
            get => designation;

            set
            {
                designation = value;
                OnPropertyChanged("Designation");
            }
        }

        /// <summary>
        /// Gets or sets the image of an employee.
        /// </summary>
        /// <value>The image.</value>
        public string Image
        {
            get => image;

            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        #endregion
    }
}