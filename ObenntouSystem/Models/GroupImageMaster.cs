namespace ObenntouSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupImageMaster")]
    public partial class GroupImageMaster
    {
        [Key]
        public int gpic_id { get; set; }

        [Required]
        [StringLength(250)]
        public string gpic_path { get; set; }

        public int? gpic_cre { get; set; }

        public DateTime? gpic_credate { get; set; }

        public int? gpic_upd { get; set; }

        public DateTime? gpic_upddate { get; set; }

        public int? gpic_del { get; set; }

        public DateTime? gpic_deldate { get; set; }
    }
}
