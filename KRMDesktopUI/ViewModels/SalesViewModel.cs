using Caliburn.Micro;
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
        private BindingList<string> _products;
        private string _itemQuantity;
        private BindingList<string> _cart;

        public BindingList<string>Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => _products);
            }
        }
        public BindingList<string> Cart
        {
            get { return _cart; }
            set 
            {
                _cart = value;
                NotifyOfPropertyChange(() => _cart) ;
            }
        }

        public string ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => Products);
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
