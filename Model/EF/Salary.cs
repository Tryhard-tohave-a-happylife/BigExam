namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Salary")]
    public partial class Salary
    {
        public int ID { get; set; }

        [Column("Salary")]
        [StringLength(20)]
        public string Salary1 { get; set; }

        [StringLength(20)]
        public string Bonus { get; set; }
    }
}
