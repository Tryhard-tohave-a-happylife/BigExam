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
        public int ID { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [StringLength(250)]
        public string NameCertificate { get; set; }

        [StringLength(50)]
        public string ImageCertificate { get; set; }

        [Column(TypeName = "date")]
        public DateTime GetDate { get; set; }
    }
}
