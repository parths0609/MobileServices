using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MobileServices.Entities
{
    public class Sales
    {
        public int SaleId { get; set; }
        [Display(Name = "Date of Sale")]
        public CustomDate DateOfSale { get; set; }

    }

    public class CustomDate
    {
        
        public DateTime SaleDate { get ; set; }

        public CustomDate()
        {
            //SaleDate = SaleDate.ToShortDateString();
        }
    }
}
