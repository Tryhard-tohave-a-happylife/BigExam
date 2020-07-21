namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCertificate")]
    public partial class UserCertificate
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string NameCertificate { get; set; }

        [StringLength(50)]
        public string ImageCertificate { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime GetDate { get; set; }
    }
}
