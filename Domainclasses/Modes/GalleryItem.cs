using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domainclasses.Modes
{
    public class GalleryItem
    {
        [Key]
        public int GalleryItemId { get; set; }
        public int GalleryId { get; set; }
        [Required]
        [DisplayName("نام ")]

        public string name { get; set; }
        [DisplayName("توضیحات")]
        public string describtion { get; set; }
        [Required]
        [DisplayName("تصویر")]

        public string url { get; set; }
        public virtual Gallery Gallery { get; set; }

    }
}
