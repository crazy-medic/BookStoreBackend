using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        public bool AddAddress(AddressEntity address);
        public bool UpdateAddress(AddressEntity address);
    }
}
