namespace EFCodeFrist
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OJT_User_Score_Details
    {
        public int Id { get; set; }

        public int User_Score_Master_Id { get; set; }

        public int Question_Master_Id { get; set; }

        public int Question_Option_Id { get; set; }

        public int Examination_Paper_Master_Id { get; set; }
    }
}
