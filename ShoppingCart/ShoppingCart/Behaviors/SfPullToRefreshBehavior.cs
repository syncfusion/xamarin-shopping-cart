using System;
using ShoppingCart.ViewModels.Bookmarks;
using ShoppingCart.ViewModels.Catalog;
using Syncfusion.SfPullToRefresh.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Behaviors
{
    /// <summary>
    /// This class extends the behavior of the SfPullToRefresh control to refreshing a page when an event occurs.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SfPullToRefreshBehavior : Behavior<SfPullToRefresh>
    {
        #region Private

        private SfPullToRefresh pullToRefresh;

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when added sfpulltorefresh to the page.
        /// </summary>
        /// <param name="bindableSfPullToRefresh">The SfPullToRefresh</param>
        protected override void OnAttachedTo(SfPullToRefresh bindableSfPullToRefresh)
        {
            base.OnAttachedTo(bindableSfPullToRefresh);
            pullToRefresh = bindableSfPullToRefresh;
            bindableSfPullToRefresh.Refreshing += BindableSfPullToRefresh_Refreshing;
        }

        /// <summary>
        /// Invoked wheh pulling the page.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event args</param>
        private void BindableSfPullToRefresh_Refreshing(object sender, EventArgs e)
        {
            (sender as SfPullToRefresh).IsRefreshing = true;

            if ((sender as SfPullToRefresh).BindingContext is WishlistViewModel wishlistViewModel)
            {
                if (wishlistViewModel != null) wishlistViewModel.FetchWishlist();
            }
            else if ((sender as SfPullToRefresh).BindingContext is ProductHomePageViewModel productHomePageViewModel)
            {
                if (productHomePageViewModel != null)
                {
                    productHomePageViewModel.IsBusy = true;
                    productHomePageViewModel.FetchBannerImage();
                    productHomePageViewModel.FetchOfferProducts();
                    productHomePageViewModel.FetchRecentProducts();
                    productHomePageViewModel.IsBusy = false;
                }
            }
            else if ((sender as SfPullToRefresh).BindingContext is CategoryPageViewModel categoryPageViewModel)
            {
                if (categoryPageViewModel != null) categoryPageViewModel.FetchCategories(string.Empty);
            }
            else if ((sender as SfPullToRefresh).BindingContext is CartPageViewModel cartPageViewModel)
            {
                if (cartPageViewModel != null)
                {
                    cartPageViewModel.FetchCartProducts();
                    cartPageViewModel.UpdatePrice();
                }
            }

            (sender as SfPullToRefresh).IsRefreshing = false;
        }

        /// <summary>
        /// Invoked when exit from the page.
        /// </summary>
        /// <param name="bindableSfPullToRefresh">The SfPullToRefresh</param>
        protected override void OnDetachingFrom(SfPullToRefresh bindableSfPullToRefresh)
        {
            base.OnDetachingFrom(bindableSfPullToRefresh);
            bindableSfPullToRefresh.Refreshing -= BindableSfPullToRefresh_Refreshing;
        }

        #endregion
    }
}