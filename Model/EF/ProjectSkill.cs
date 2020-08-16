namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProjectSkill")]
    public partial class ProjectSkill
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectID { get; set; }

        [Key]
        [Column("ProjectSkill", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectSkill1 { get; set; }
    }
}
