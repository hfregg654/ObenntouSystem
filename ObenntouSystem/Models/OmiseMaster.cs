namespace ObenntouSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OmiseMaster")]
    public partial class OmiseMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OmiseMaster()
        {
            Dishes = new HashSet<Dish>();
            Groups = new HashSet<Group>();
        }

        [Key]
        public int omise_id { get; set; }

        [Required]
        [StringLength(15)]
        public string omise_name { get; set; }

        public int omise_cre { get; set; }

        public DateTime omise_credate { get; set; }

        public int? omise_upd { get; set; }

        public DateTime? omise_upddate { get; set; }

        public int? omise_del { get; set; }

        public DateTime? omise_deldate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dish> Dishes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
