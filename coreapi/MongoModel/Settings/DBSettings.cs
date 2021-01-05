namespace coreapi.MongoModel.Settings
{

    /// <summary>
    /// 数据库配置信息
    /// </summary>
    public class DBSettings
    {
        /// <summary>
        /// Mongo 的链接地址
        /// </summary>
        public string MongoConn { get; set; }
        /// <summary>
        /// 数据库名称database
        /// </summary>
        public string MongoDBName { get; set; }
        /// <summary>
        /// 数据库 下面的表名字collection
        /// </summary>
        public string TableName_Logs { get; set; }
    }
}
