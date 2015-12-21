using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domainclasses.Modes
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [DisplayName("گروه محصول")]
        public int ProductCategoryID { get; set; }
        [Required]
        [DisplayName("نام محصول")]
        public string name { get; set; }
       
        [DisplayName("توضیحات")]
        [Required(ErrorMessage = "متن مطلب وارد نشده است")]
        [AllowHtml]
        public string describtion { get; set; }
        [DisplayName("کلید واژه")]
        public string Tags { get; set; }

        public virtual ICollection<Price> Price { get; set; }
        public virtual ICollection<ProductFiles> Files  { get; set; }
        public virtual Form Form { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
