using System;
using System.Collections.Generic;
using System.Text;

namespace Domen.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<UserUseCase> UserUseCases { get; set; } = new List<UserUseCase>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
