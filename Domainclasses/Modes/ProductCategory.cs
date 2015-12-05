using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Domainclasses.Modes
{
    public class ProductCategory : IEnumerable
    {
         [Key]
        public int ProductCategoryID { get; set; }
        [Required]
        [DisplayName("نام گروه")]
        public string name { get; set; }
       
        [DisplayName("توضیحات")]
        [Required(ErrorMessage = "متن مطلب وارد نشده است")]
        [AllowHtml]
        public string describtion { get; set; }
        public SuperCategory? SuperCategory { get; set; } 
        public virtual ICollection<Product>Product { get; set; }
        public virtual ICollection<ProductCategoryFiles> ProductCategoryFileses { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public enum SuperCategory
    {
        [Display(Name = @"چاپ روی انواع کاشی")]
        TilePrint = 1,

        [Display(Name = @"چاپ روی انواع ظروف")]

        DishPrint = 2,

        [Display(Name = @"چاپ روی انواع لباس")]

        ClothesPrint = 3,

        [Display(Name = @"چاپ روی دیگر سطوح")]

        OtherPrint = 4

    }
}
