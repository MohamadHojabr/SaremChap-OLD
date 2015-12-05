using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domainclasses.Enums;

namespace Domainclasses.Modes
{
    public class ProductFiles
    {
        [Key]
        public int FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }

}
