using System;
using System.Collections.Generic;
using System.Text;

namespace wd
{
    class 数据库迁移
    {
        //        https://www.cnblogs.com/loong-hon/p/10791610.html

        //EXEC master.dbo.sp_addlinkedserver
        //@server = N'MYSQL_dev',
        //@srvproduct= N'MySQL',
        //@provider= N'MSDASQL',
        //@provstr= N'DRIVER={MySQL ODBC 8.0 ANSI Driver}; SERVER=rm-uf69u30v6qe8i8g82to.mysql.rds.aliyuncs.com; _ 
        //DATABASE= lvdev; USER=rooti; PASSWORD=G34oodztf1#; OPTION=3' 


        //   SELECT* INTO LY_DB.dbo.cm_attachment_attachmentfile
        //   FROM openquery(MYSQL_dev, 'SELECT  *  FROM lvdev.cm_attachment_attachmentfile')

 //       SELECT* into LY_DB.dbo.cm_attachment_attachmentfile FROM openquery(MYSQL_Attach, 'SELECT  *  FROM lvyantest.cm_attachment_attachmentfile limit 10,10')
 //insert into  LY_DB.dbo.cm_attachment_attachmentfile select * FROM openquery(MYSQL_Attach, 'SELECT  *  FROM lvyantest.cm_attachment_attachmentfile limit 10,10')



    }
}
