namespace LY.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("B_User")]
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        public int CaregoryId { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
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
