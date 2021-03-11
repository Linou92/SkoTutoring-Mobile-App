namespace AppDbContext.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentLang")]
    public partial class StudentLang
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public int LangId { get; set; }
    }
}
