namespace AppDbContext.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assignment")]
    public partial class Assignment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SessionId { get; set; }

        public int Type { get; set; }

        public DateTime? Datetime { get; set; }

        public virtual Session Session { get; set; }
    }
}
