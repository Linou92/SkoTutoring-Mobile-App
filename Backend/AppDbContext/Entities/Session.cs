namespace AppDbContext.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Session")]
    public partial class Session
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Session()
        {
            Assignments = new HashSet<Assignment>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        [ForeignKey("Level")]
        public int LevelId { get; set; }

        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }

        [ForeignKey("Student")]
        public string StudentId { get; set; }

        [ForeignKey("Language")]
        public int LangId { get; set; }

        public int Purpose { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Availability")]
        public int AvailabilityId { get; set; }

        public int Status { get; set; }

        [Column(TypeName = "text")]
        public string Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual Availability Availability { get; set; }

        public virtual Language Language { get; set; }

        public virtual Level Level { get; set; }

        public virtual Student Student { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
