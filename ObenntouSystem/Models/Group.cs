namespace ObenntouSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Group()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int group_id { get; set; }

        public int group_userid { get; set; }

        public int group_omiseid { get; set; }

        [Required]
        [StringLength(15)]
        public string group_name { get; set; }

        public int group_cre { get; set; }

        public DateTime group_credate { get; set; }

        public int? group_upd { get; set; }

        public DateTime? group_upddate { get; set; }

        public int? group_del { get; set; }

        public DateTime? group_deldate { get; set; }

        [StringLength(250)]
        public string group_pic { get; set; }

        [Required]
        [StringLength(3)]
        public string group_type { get; set; }

        public virtual OmiseMaster OmiseMaster { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
