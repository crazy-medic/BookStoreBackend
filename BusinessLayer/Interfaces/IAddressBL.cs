using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBL
    {
        public bool AddAddress(AddressEntity address);
        public bool UpdateAddress(AddressEntity address);
        public bool DeleteAddress(AddressEntity address);
        public IEnumerable<AddressEntity> GetAllAddress(long userid);
    }
}
