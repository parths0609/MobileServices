using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MobileServices.Entities
{
    [Table(name: "Brands")]
    public class Brands
    {
        [Key]
        [Column]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }
        [Column]
        [Required]
        public string BrandName { get; set; }

        //foreign key
        [ForeignKey(name:"Categories")]
        public int CategoryId { get; set; }

        public Categories Categories { get; set; }

        

        public virtual ICollection<Sales> Sales { get; set; }
    }
}
