namespace AppDbContext.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TeacherAvailability")]
    public partial class TeacherAvailability
    {
        public int Id { get; set; }

        [ForeignKey("Teacher")]
        public string Teacher_UserId { get; set; }

        [ForeignKey("Availability")]
        public int AvailabilityId { get; set; }

        public virtual Availability Availability { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
