using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public bool AddToCart(CartModel cart)
        {
            try
            {
                return this.cartRL.AddToCart(cart);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Cart> GetCartItems(long userid)
        {
            try
            {
                return this.cartRL.GetCartItems(userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveFromCart(Cart cart)
        {
            try
            {
                return this.cartRL.RemoveFromCart(cart);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCart(Cart cart)
        {
            try
            {
                return this.cartRL.UpdateCart(cart);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
