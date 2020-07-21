namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OfferJobMajor")]
    public partial class OfferJobMajor
    {
        [Key]
        [Column(Order = 0)]
        public Guid OfferID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ParentMajor { get; set; }

        public int? ChildMajor { get; set; }
    }
}
