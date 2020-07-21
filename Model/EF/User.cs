namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Column(TypeName = "date")]
        public DateTime UserBirthDay { get; set; }

        [Required]
        [StringLength(100)]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string UserMobile { get; set; }

        public int UserArea { get; set; }

        public string UserAddress { get; set; }

        public string UserImage { get; set; }

        [Column(TypeName = "text")]
        public string UserExperience { get; set; }

        [Required]
        [StringLength(10)]
        public string Sex { get; set; }

        public bool AtSchool { get; set; }

        public Guid? School { get; set; }

        public double? GPA { get; set; }

        public string Interest { get; set; }

        public int CompleteProfile { get; set; }
    }
}
