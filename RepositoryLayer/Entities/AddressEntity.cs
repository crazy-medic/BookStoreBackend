using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class AddressEntity
    {
        public long AddressId { get; set; }

        [ForeignKey("user")]
        public long fkUserId { get; set; }
        public virtual User user { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long PinCode { get; set; }

        [ForeignKey("addresstypes")]
        public int fkAddressType { get; set; }
        public virtual AddressTypes addresstypes { get; set; }
    }
}
