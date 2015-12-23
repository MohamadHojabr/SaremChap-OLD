using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domainclasses.Modes
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "گروه خبر وارد نشده است")]
        [DisplayName("گروه خبر")]
        public int ChapterId { get; set; }

        [Required(ErrorMessage = "نویسنده خبر وارد نشده است")]
        [DisplayName("نویسنده خبر")]
        public string Authors { get; set; }
        [Required(ErrorMessage = "تیتر خبر وارد نشده است")]
        [DisplayName("تیتر خبر")]
        public string SubjectLead { get; set; }
        
        [Required(ErrorMessage = "متن خبر وارد نشده است")]
        [DisplayName("متن خبر")]
        [AllowHtml]
        public string SubjectContent { get; set; }
        [DisplayName("وضعیت خبر")]
        public Status Status { get; set; }
        [DisplayName("تاریخ مطلب")]

        public DateTime SubjectDate { get; set; }
        [DisplayName("کلید واژه")]
        public string Tags { get; set; }

        public virtual Chapter Chapter { get; set; }
        public virtual ICollection<SubjectFiles> SubjectFiles { get; set; }
    }

    public enum Status
    {
        [Display(Name = @"بخش اسلایدشو")]
        Slideshow = 1,

        [Display(Name = @"بخش محصول ویژه")]

        Special = 2,

        [Display(Name = @"بخش محصولات")]

        Products = 3,

        [Display(Name = @" منوی اصلی")]

        MainMenu = 4,

        [Display(Name = @"غیر فعال")]

        Disable = 5,


    }
}
