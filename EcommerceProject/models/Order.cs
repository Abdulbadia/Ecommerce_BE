using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceProject.models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime date { get; set; }
        public virtual ICollection<OrderDetials> OrderDetials { get; set; }
         = new HashSet<OrderDetials>();
    }
}
