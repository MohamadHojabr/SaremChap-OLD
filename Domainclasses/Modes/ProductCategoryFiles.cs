using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domainclasses.Enums;

namespace Domainclasses.Modes
{
    public class ProductCategoryFiles
    {
        [Key]
        public int ProductCategoryFileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public int ProductCategoryID { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

    }

}
