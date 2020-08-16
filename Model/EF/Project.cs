namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Project")]
    public partial class Project
    {
        public int ProjectID { get; set; }

        public Guid MasterID { get; set; }

        public int ProjectMajor { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int Amount { get; set; }

        public int? Apply { get; set; }
    }
}
