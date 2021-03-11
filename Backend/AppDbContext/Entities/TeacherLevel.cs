namespace AppDbContext.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TeacherLevel")]
    public partial class TeacherLevel
    {
        public int Id { get; set; }

        public string Teacher_UserId { get; set; }

        public int LevelId { get; set; }

        public virtual Level Level { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
