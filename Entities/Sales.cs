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

        [DateValidation(ErrorMessage = "Invalid date")]
        [Required]
        [Column]
        public DateTime DateOfSale { get; set; }
        [Required]
        [Column]
        public int SellingPrice { get; set; }



        //foreign keys
        [ForeignKey(name: "Brands")]
        public int BrandId { get; set; }
       
        public Brands Brands { get; set; }
        [ForeignKey(name: "Items")]
        public int ItemId { get; set; }

        public Items Items { get; set; }


      
    }

    public class SalesReportRequest : Sales
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class DateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
    }



}
