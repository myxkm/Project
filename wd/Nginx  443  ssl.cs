using System;
using System.Collections.Generic;
using System.Text;

namespace wd
{
    class Nginx__443__ssl
    {
        #region Nginx__443__ssl
#if false


        参考地址 https://jingyan.baidu.com/article/ed2a5d1fbae45e09f6be1714.html

            1. 下载Openssl  http://slproweb.com/products/Win32OpenSSL.html
            2. 设置Openssl 的环境变量 变量名：OPENSSL_HOME 变量值：~\bin
            3. 到Nginx 的conf 文件夹 内
            4. cmd   
            5. 执行 openssl genrsa -des3 -out server.key
            6. 需要你设置一个密码
            7.创建csr证书，命令：openssl req -new -key server.key -out server.csr
            8.拷贝server.key并重命名为server.key.org
            9.去除密码，命令：openssl rsa -in server.key.org -out server.key
            10.生成证书文件server.crt，命令：openssl x509 -req -days 365 -in server.csr -signkey server.key -out server.crt



        用记事本或者任意文本编辑器打开D:\nginx\conf\nginx.conf文件，找到“http”标签，在其中添加以下配置：

                    server{
                      使用了443端口
                        listen 443 default ssl;
                      证书(公钥.发送到客户端的)
                        ssl_certificate server.crt;
                      私钥,
                        ssl_certificate_key server.key;
                        location / {
                            root   html;
                            index  index.html index.htm;

                        }        

}


        nginx 80端口重定向到443端口

  server {
    listen 80;
    server_name localhost：4433;
    rewrite ^(.*)$ https://${server_name}$1 permanent; 
}

#endif
        #endregion

    }
}
