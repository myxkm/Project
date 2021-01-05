namespace EFCodeFrist
{
    using System.Data.Entity;

    public partial class CodeFristModel : DbContext
    {
        public CodeFristModel()
            : base("name=CodeFristModel")
        {
        }
        public virtual DbSet<OJT_Examination_Paper_Master> OJT_Examination_Paper_Master { get; set; }
        public virtual DbSet<OJT_Examination_Paper_Type> OJT_Examination_Paper_Type { get; set; }
        public virtual DbSet<OJT_Question_Master> OJT_Question_Master { get; set; }
        public virtual DbSet<OJT_Question_Option> OJT_Question_Option { get; set; }
        public virtual DbSet<OJT_Question_Type> OJT_Question_Type { get; set; }
        public virtual DbSet<OJT_User_Score_Details> OJT_User_Score_Details { get; set; }
        public virtual DbSet<OJT_User_Score_Master> OJT_User_Score_Master { get; set; }
        public virtual DbSet<OJTMaster> OJTMaster { get; set; }
        public virtual DbSet<OJTDetails> OJTDetails { get; set; }
        
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<CodeFristModel>());  //默认的 
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CodeFristModel>());  //删除并创建 
        }
    }
}
