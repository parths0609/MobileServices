using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MobileServices.Entities
{
    [Table(name:"Sales")]
    public class Sales
    {
        [Required]
        [Key]
        [Column]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }
        
        [Required]
        [Column]
        public DateTime DateOfSale { get; set; }

        //foreign keys
        [ForeignKey(name: "Brands")]
        public int BrandId { get; set; }
       
        public Brands Brands { get; set; }

    }

   
}
