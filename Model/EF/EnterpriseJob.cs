namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnterpriseJob")]
    public partial class EnterpriseJob
    {
        [Key]
        [Column(Order = 0)]
        public Guid EnterpriseID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ParentJob { get; set; }

        public int? ChildJob { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool Important { get; set; }
    }
}
