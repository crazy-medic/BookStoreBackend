using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        public bool AddToCart(CartModel cart);
        public bool UpdateCart(Cart cart);
        public bool RemoveFromCart(Cart cart);
        public IEnumerable<Cart> GetCartItems(long userid);
    }
}
