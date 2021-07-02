using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MobileServices.Entities
{
    [Table(name: "Items")]
    public class Items
    {
        [Key]
        [Column]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        [Column]
        [Required]
        public string ItemName { get; set; }

        public virtual int CategoryId { get; set; }
       
        [ForeignKey("CategoryId")]
        public virtual Categories Categories { get; set; }


        public virtual int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brands Brands { get; set; }

        [Column]
        [Required]
        public int Price { get; set; }
        [Column]
        [Required]
        public decimal SellerMargin { get; set; }

        public virtual ICollection<Sales> Sales { get; set; }


    }
}
