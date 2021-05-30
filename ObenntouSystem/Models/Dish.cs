namespace ObenntouSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dish
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dish()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int dish_id { get; set; }

        public int dish_omiseid { get; set; }

        [Required]
        [StringLength(25)]
        public string dish_name { get; set; }

        public decimal dish_price { get; set; }

        public int dish_cre { get; set; }

        public DateTime dish_credate { get; set; }

        public int? dish_upd { get; set; }

        public DateTime? dish_upddate { get; set; }

        public int? dish_del { get; set; }

        public DateTime? dish_deldate { get; set; }

        [StringLength(250)]
        public string dish_pic { get; set; }

        public virtual OmiseMaster OmiseMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
