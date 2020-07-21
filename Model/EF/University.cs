namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("University")]
    public partial class University
    {
        public Guid UniversityID { get; set; }

        [Required]
        [StringLength(100)]
        public string UniversityName { get; set; }

        [Required]
        [StringLength(250)]
        public string UniversityLogo { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }
    }
}
