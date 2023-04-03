namespace WebBanBanhMi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BanhMi")]
    public partial class BanhMi
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public decimal? Price { get; set; }

        public string Description { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
