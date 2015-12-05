using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domainclasses.Modes
{
    public class Price
    {
         [Key]
        public int priceID { get; set; }
         [Required]
         [DisplayName("محصول")]
         public int ProductID { get; set; }
         [Required]
         [DisplayName("نوع")]
        public string neme { get; set; }
         [Required]
         [DisplayName("مقدار")]

         public int Quantity { get; set; }
         [Required]
         [DisplayName("قیمت")]
         [DisplayFormat(DataFormatString = "{0:n0}")]
         public decimal value { get; set; }
        public virtual Product product { get; set; }
    }
}
