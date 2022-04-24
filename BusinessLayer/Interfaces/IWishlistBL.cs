using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishlistBL
    {
        public bool AddToWishlist(WishlistModel wishlistModel);
        public bool RemoveFromWishlist(WishlistModel wishlistModel);
        public IEnumerable<WishlistEntity> GetAllWishlistBooks(long userid);
    }
}
