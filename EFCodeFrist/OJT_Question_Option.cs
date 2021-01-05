namespace EFCodeFrist
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OJT_Question_Option
    {
        public int Id { get; set; }

        [StringLength(300)]
        public string Contents { get; set; }

        [StringLength(300)]
        public string Remark { get; set; }

        public int IsDisabled { get; set; }

        public int Examination_Paper_Master_Id { get; set; }

        public int Question_Master_Id { get; set; }

        public int IsAnswer { get; set; }

        public int IsDelete { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
