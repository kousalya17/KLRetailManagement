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
        private int _itemQuantity = 1;
        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();
        private IProductEndpoint _productEndpoint;
        private ProductModel _selectedProduct;

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

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
            }
        }
        public BindingList<CartItemModel> Cart
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
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }
        public string SubTotal
        {
            get
            {
                decimal subtotal = 0;
                foreach(var item in Cart)
                {
                    subtotal += (item.Product.RetailPrice * item.QuantityInCart);
                }
                return subtotal.ToString("C");
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
                if(ItemQuantity>0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    result = true;
                }
                return result;
            }
        }

        public void AddToCart()
        {
            CartItemModel existingitem = Cart.FirstOrDefault(x=>x.Product == SelectedProduct);
            if (existingitem != null)
            {
                existingitem.QuantityInCart += ItemQuantity;
                Cart.Remove(existingitem);
                Cart.Add(existingitem);
            }
            else
            {

                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Cart);
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
            NotifyOfPropertyChange(() => SubTotal);
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
