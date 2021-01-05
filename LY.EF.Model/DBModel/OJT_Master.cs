namespace LY.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OJT_Master")]
    public partial class OJTMaster
    {
        [Key]
        public int Id { get; set; }
        public string Log { get; set; }

    }
}
