namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryArticle")]
    public partial class CategoryArticle
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string NameCategory { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public int? ParentID { get; set; }

        public int? Amount { get; set; }
    }
}
