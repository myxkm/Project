namespace LY.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("B_Caregory")]
    public partial class Caregory
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(100)]
        public string CaregoryCode { get; set; }
        [StringLength(100)]
        public string CaregoryName { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(500)]
        public string Remark { get; set; }
        [Required]
        [StringLength(100)]
        public string CreateById { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
    }
}
