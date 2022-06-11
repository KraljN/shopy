using System;
using System.Collections.Generic;
using System.Text;

namespace Domen.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
