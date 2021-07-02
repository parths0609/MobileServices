using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MobileServices.Entities
{
    [Table(name: "Categories")]
    public class Categories
    {
        [Key]
        [Column]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Column]
        [Required]
        public string CategoryName { get; set; }


        public virtual ICollection<Brands> Brands { get; set; }

        public virtual ICollection<Items> Items { get; set; }
    }
}
