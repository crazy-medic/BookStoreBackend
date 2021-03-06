using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public bool AddAddress(AddressEntity address)
        {
            try
            {
                return this.addressRL.AddAddress(address);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteAddress(AddressEntity address)
        {
            try
            {
                return this.addressRL.DeleteAddress(address);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AddressTypes> GetAddressTypes()
        {
            try
            {
                return this.addressRL.GetAddressTypes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AddressEntity> GetAllAddress(long userid)
        {
            try
            {
                return this.addressRL.GetAllAddress(userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAddress(AddressEntity address)
        {
            try
            {
                return this.addressRL.UpdateAddress(address);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
