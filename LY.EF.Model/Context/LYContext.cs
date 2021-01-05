namespace LY.EF.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LYContext : DbContext
    {
        public LYContext()
            : base("name=LYContext")
        {
        }
       
        public virtual DbSet<OJTDetails> OJTDetails { get; set; }
        public virtual DbSet<OJTMaster> OJTMaster { get; set; }
        public virtual DbSet<OJT_Examination_Paper_Master> OJT_Examination_Paper_Master { get; set; }
        public virtual DbSet<OJT_Examination_Paper_Type> OJT_Examination_Paper_Type { get; set; }
        public virtual DbSet<OJT_Question_Master> OJT_Question_Master { get; set; }
        public virtual DbSet<OJT_Question_Option> OJT_Question_Option { get; set; }
        public virtual DbSet<OJT_Question_Type> OJT_Question_Type { get; set; }
        public virtual DbSet<OJT_User_Score_Details> OJT_User_Score_Details { get; set; }
        public virtual DbSet<OJT_User_Score_Master> OJT_User_Score_Master { get; set; }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Caregory> Caregory { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }


    }
}
