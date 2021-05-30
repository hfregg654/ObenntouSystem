namespace ObenntouSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Groups = new HashSet<Group>();
            Orders = new HashSet<Order>();
        }

        [Key]
        public int user_id { get; set; }

        [Required]
        [StringLength(15)]
        public string user_name { get; set; }

        [Required]
        [StringLength(10)]
        public string user_phone { get; set; }

        [Required]
        [StringLength(100)]
        public string user_mail { get; set; }

        public int user_cre { get; set; }

        public DateTime user_credate { get; set; }

        public int? user_upd { get; set; }

        public DateTime? user_upddate { get; set; }

        public int? user_del { get; set; }

        public DateTime? user_deldate { get; set; }

        [Required]
        [StringLength(50)]
        public string user_acc { get; set; }

        [Required]
        [StringLength(50)]
        public string user_pwd { get; set; }

        [Required]
        [StringLength(15)]
        public string user_pri { get; set; }

        [StringLength(250)]
        public string user_pic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> Groups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
