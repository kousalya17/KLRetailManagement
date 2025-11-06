using Caliburn.Micro;
using KRMDesktopUI.Library.Api;
using KRMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<ProductModel> _products;
        private int _itemQuantity;
        private BindingList<string> _cart;
        private IProductEndpoint _productEndpoint;

        public SalesViewModel(IProductEndpoint productEndpoint) 
        {
            _productEndpoint = productEndpoint;
        }

        protected override async void OnViewLoaded(Object view)
        {
            base.OnViewLoaded(view);
            await Loadproducts();
        }

        public async Task Loadproducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }
        public BindingList<ProductModel>Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        public BindingList<string> Cart
        {
            get { return _cart; }
            set 
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart) ;
            }
        }

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }
        public string SubTotal
        {
            get
            {
                return "$0.00";
            }
        }
        public string Tax
        {
            get
            {
                return "$0.00";
            }
        }
        public string Total
        {
            get
            {
                return "$0.00";
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool result = false;

                return result;
            }
        }

        public void AddToCart()
        {

        }

        public bool canRemoveToCart
        {
            get
            {
                bool result = false;

                return result;
            }
        }

        public void RemoveToCart()
        {

        }

        public bool canCheckOut
        {
            get
            {
                bool result = false;

                return result;
            }
        }

        public void CheckOut()
        {

        }

    }
}
