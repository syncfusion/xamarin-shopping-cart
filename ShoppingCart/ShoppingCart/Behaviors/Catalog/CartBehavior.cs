using System;
using ShoppingCart.ViewModels.Bookmarks;
using ShoppingCart.ViewModels.Catalog;
using ShoppingCart.ViewModels.Detail;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Behaviors.Catalog
{
    /// <summary>
    /// This class extends the behavior of the catalog page and detail page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CartBehavior : Behavior<ContentPage>
    {
        #region Fields

        private ContentPage contentPage;

        #endregion

        #region Method

        /// <summary>
        /// Invoked when adding catalog page and detail page.
        /// </summary>
        /// <param name="bindableContentPage">Bindable ContentPage</param>
        protected override void OnAttachedTo(ContentPage bindableContentPage)
        {
            base.OnAttachedTo(bindableContentPage);
            contentPage = bindableContentPage;
            bindableContentPage.Appearing += Bindable_Appearing;
        }

        /// <summary>
        /// Invoked when exit from the page.
        /// </summary>
        /// <param name="bindableContentPage">Content Page</param>
        protected override void OnDetachingFrom(ContentPage bindableContentPage)
        {
            base.OnDetachingFrom(bindableContentPage);
            bindableContentPage.Appearing -= Bindable_Appearing;
        }

        /// <summary>
        /// Invoked when appearing the page.
        /// </summary>
        /// <param name="sender">Content Page</param>
        /// <param name="e">Event Args</param>
        private void Bindable_Appearing(object sender, EventArgs e)
        {
            if (contentPage != null)
            {
                if (contentPage.BindingContext is DetailPageViewModel detailPageViewModel &&
                    detailPageViewModel != null)
                    detailPageViewModel.UpdateCartItemCount();
                else if (contentPage.BindingContext is CatalogPageViewModel catalogPageViewModel &&
                         catalogPageViewModel != null)
                    catalogPageViewModel.UpdateCartItemCount();
                else if (contentPage.BindingContext is WishlistViewModel wishlistViewModel && wishlistViewModel != null)
                    wishlistViewModel.UpdateCartItemCount();
                else if (contentPage.BindingContext is CartPageViewModel cartPageViewModel && cartPageViewModel != null)
                    cartPageViewModel.FetchCartProducts();
            }
        }

        #endregion
    }
}