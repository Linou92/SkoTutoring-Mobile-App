namespace AppDbContext.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TeacherLang")]
    public partial class TeacherLang
    {
        public int Id { get; set; }

        public string Teacher_UserId { get; set; }

        public int LangId { get; set; }

        public virtual Language Language { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
