﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("ProductCategory")]
        public int ProductCategoryID { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

    }

}
