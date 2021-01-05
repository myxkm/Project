namespace LY.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OJT_Details")]
    public partial class OJTDetails
    {
        [Key]
        public int Id { get; set; }

        [Column("OJT_Master_Id")]
        public int OJTMasterId { get; set; }

        public string Log { get; set; }
    }
}
