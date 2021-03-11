namespace AppDbContext.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Availability")]
    public partial class Availability
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Availability()
        {
            Sessions = new HashSet<Session>();
            TeacherAvailabilities = new HashSet<TeacherAvailability>();
        }

        public int Id { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        public DateTime Date { get; set; }

        public bool IsClosed { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeacherAvailability> TeacherAvailabilities { get; set; }
    }
}
