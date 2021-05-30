namespace ObenntouSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [Key]
        public int order_id { get; set; }

        public int order_groupid { get; set; }

        public int order_userid { get; set; }

        public int order_dishesid { get; set; }

        public int order_cre { get; set; }

        public DateTime order_credate { get; set; }

        public int? order_upd { get; set; }

        public DateTime? order_upddate { get; set; }

        public int? order_del { get; set; }

        public DateTime? order_deldate { get; set; }

        public virtual Dish Dish { get; set; }

        public virtual Group Group { get; set; }

        public virtual User User { get; set; }
    }
}
