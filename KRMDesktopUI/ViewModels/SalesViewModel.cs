using Caliburn.Micro;
using KRMDesktopUI.Library.Api;
using KRMDesktopUI.Library.Helpers;
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
        private IConfigHelper _configHelper;
        private ProductModel _selectedProduct;

        public SalesViewModel(IProductEndpoint productEndpoint,IConfigHelper configHelper) 
        {
            _productEndpoint = productEndpoint;
            _configHelper = configHelper;
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
                return calculateSubtotal().ToString("C");
            }
        }
        public decimal calculateSubtotal()
        {
            decimal subtotal = 0;
            foreach (var item in Cart)
            {
                subtotal += (item.Product.RetailPrice * item.QuantityInCart);
            }
            return subtotal;
        }

        public string Tax
        {
            get
            {
                return calculateTax().ToString("C");
            }
        }

        public decimal calculateTax()
        {
            decimal taxAmount = 0;
            decimal taxrate = _configHelper.GetTaxRate()/100;
            foreach (var item in Cart)
            {
                if(item.Product.IsTaxable)
                {
                    taxAmount += (item.Product.RetailPrice * item.QuantityInCart * taxrate);
                }
            }
            return taxAmount;
        }

        public string Total
        {
            get
            {
                decimal total = calculateSubtotal() + calculateTax();
                return total.ToString("C");
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
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
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
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
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
