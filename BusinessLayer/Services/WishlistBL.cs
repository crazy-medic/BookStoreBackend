using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishlistBL : IWishlistBL
    {
        private readonly IWishlistRL wishlistRL;

        public WishlistBL(IWishlistRL WishlistRL)
        {
            this.wishlistRL = WishlistRL;
        }

        public bool AddToWishlist(WishlistModel wishlistModel)
        {
            try
            {
                return this.wishlistRL.AddToWishlist(wishlistModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<WishlistEntity> GetAllWishlistBooks(long userid)
        {
            try
            {
                return this.wishlistRL.GetAllWishlistBooks(userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveFromWishlist(WishlistModel wishlistModel)
        {
            try
            {
                return this.wishlistRL.RemoveFromWishlist(wishlistModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
