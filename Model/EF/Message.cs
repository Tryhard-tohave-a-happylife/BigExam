namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public int ID { get; set; }

        public int FromUser { get; set; }

        public int ToProject { get; set; }

        [Column("Message")]
        [Required]
        [StringLength(1000)]
        public string Message1 { get; set; }

        public DateTime? Date { get; set; }
    }
}
