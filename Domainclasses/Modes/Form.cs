using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domainclasses.Modes
{
    public class Form
    {
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "عنوان فارسی فرم وارد نشده است")]
    [DisplayName("عنوان فارسی فرم")]

    public string Fa_Title { get; set; }
    [Required(ErrorMessage = "عنوان انگلیسی فرم وارد نشده است")]
    [DisplayName("عنوان انگلیسی فرم")]

    public string En_Title { get; set; }


    public int Product_ID { get; set; }
    public virtual Product Product { get; set; }
    public virtual ICollection<Field> Fields { get; set; }
    }

}
