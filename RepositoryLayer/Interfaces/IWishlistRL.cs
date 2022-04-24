using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishlistRL
    {
        public bool AddToWishlist(WishlistModel wishlistModel);
        public bool RemoveFromWishlist(WishlistModel wishlistModel);
        public IEnumerable<WishlistEntity> GetAllWishlistBooks(long userid);
    }
}
