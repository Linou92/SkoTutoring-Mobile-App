namespace AppDbContext.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentSubj")]
    public partial class StudentSubj
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public int SubjId { get; set; }
    }
}
