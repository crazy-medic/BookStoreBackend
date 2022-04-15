using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        public bool AddToCart(CartModel cart);
        public bool UpdateCart(Cart cart);
        public bool RemoveFromCart(Cart cart);
    }
}
