using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.Entities
{
    [Table("Users")]
    public class UserEntity : IdentityUser
    {
        public virtual ICollection<OrderEntity> Orders { get; set; }
    }

    public enum Role
    {
        Admin,
        Customer
    }
}
