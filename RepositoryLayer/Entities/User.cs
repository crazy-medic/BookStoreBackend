using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public long? Phone { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime? ModifiedAt { get; internal set; }

        [ForeignKey("type")]
        public int fkAccountType { get; set; }
        public virtual AddressTypes type { get; set; }
    }
}
