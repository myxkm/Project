namespace EFCodeFrist
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OJT_Examination_Paper_Master")]  //--Ó³Éä ±í
    public partial class OJT_Examination_Paper_Master
    {
        [Key]//--Ó³Éä Ö÷¼ü
        [Column("Id")] // Ó³Éä ×Ö¶Î
        public int Id { get; set; }

        public int State { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int Examination_Paper_Type_Id { get; set; }

        [DefaultValue(0)]
        public int IsDelete { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(300)]
        public string Remark { get; set; }

        public int IsDisabled { get; set; }
    }
}
