﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LY.Framework.Email
{
    public class MailSmtp
    {
        private void Demo()
        {
            {
                //QQ邮件发送
                MailSmtp ms = new MailSmtp("smtp.qq.com", "1477165313", "nwmklrnfduhwfhgh", 25);
                ms.SetCC("1477165313@qq.com");//抄送可以多个
                ms.SetBC("480672885@qq.com");//暗送可以多个
                ms.SetIsHtml(true);//默认:true
                ms.SetEncoding(System.Text.Encoding.UTF8);//设置格式 默认utf-8
                ms.SetIsSSL(true);//是否ssl加密 默认为false
                //bool isSuccess = ms.Send("1477165313@qq.com", "谢昆明", "1477165313@qq.com", "哈哈", "哈哈", new string[] { });
            }
            {

                //企业邮件发送
                MailSmtp ms = new MailSmtp("smtp.exmail.qq.com", "kunming.xie@lv-yan.com", "5XffdXygF6Cepvq8", 25);
                //bool isSuccess = ms.Send("kunming.xie@lv-yan.com", "谢昆明", "1477165313@qq.com", "哈哈", "哈哈", new string[] { });
            }
        }


        /// <summary>
        /// 设置邮件编码类型
        /// </summary>
        /// <param name="contentEncoding"></param>
        public void SetEncoding(Encoding contentEncoding)
        {
            this._encoding = contentEncoding;

        }
        /// <summary>
        ///设置邮件正文是否为 Html 格式
        /// </summary>
        /// <param name="isHtml"></param>
        public void SetIsHtml(bool isHtml)
        {
            this._isHtml = isHtml;
        }
        /// <summary>
        /// 抄送
        /// </summary>
        /// <param name="cc"></param>
        public void SetCC(params string[] cc)
        {
            this._cc = cc;
        }
        /// <summary>
        /// 暗送
        /// </summary>
        /// <param name="cc"></param>
        public void SetBC(params string[] bc)
        {
            this._bcc = bc;
        }
        /// <summary>
        /// 是否ssl加密
        /// </summary>
        /// <param name="isSSL"></param>
        public void SetIsSSL(bool isSSL)
        {
            this._smtp.EnableSsl = isSSL;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="host"></param>
        /// <param name="username">邮件账号</param>
        /// <param name="password">密码</param>
        /// <param name="Port">端口</param>
        public MailSmtp(string host, string username, string password, int Port = 0x19)
        {
            this._smtp.Host = host;
            this._smtp.Port = Port;
            this._smtp.EnableSsl = false;
            this._isHtml = true;
            this._encoding = Encoding.UTF8;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                this._smtp.UseDefaultCredentials = false;
            }
            else
            {
                this._smtp.Credentials = new NetworkCredential(username, password);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">发件人邮件地址</param>
        /// <param name="sender">发件人显示名称</param>
        /// <param name="to">收件人地址</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="file">附件地址数组</param>
        /// <returns>bool 是否成功 </returns>
        public bool Send(string from, string sender, string to, string subject, string body, params string[] file)
        {
            return Send(from, sender, new string[] { to }, subject, body, file);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">发件人邮件地址</param>
        /// <param name="sender">发件人显示名称</param>
        /// <param name="to">收件人地址</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="file">附件地址数组</param>
        /// <returns>bool 是否成功 </returns>
        public bool Send(string from, string sender, string[] to, string subject, string body, params string[] file)
        {
            string mailReg = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            if (to == null)
            {
                throw new ArgumentNullException("MailSmtp.Send.to");
            }

            if (to.Any(oit => !Regex.IsMatch(oit + "", mailReg)))
            {
                this.Result = "收件人地址不合法";
                return false;
            }
            if (_bcc != null && _bcc.Length > 0)
            {
                if (_bcc.Any(oit => !Regex.IsMatch(oit + "", mailReg)))
                {
                    this.Result = "暗送人地址不合法";
                    return false;
                }
            }
            if (_cc != null && _cc.Length > 0)
            {
                if (_cc.Any(oit => !Regex.IsMatch(oit + "", mailReg)))
                {
                    this.Result = "抄送人地址不合法";
                    return false;
                }
            }
            MailMessage message = new MailMessage(from, to.FirstOrDefault(), subject, body);
            // 创建一个附件对象
            foreach (var r in file)
            {
                Attachment objMailAttachment;
                objMailAttachment = new Attachment(r);//发送邮件的附件
                message.Attachments.Add(objMailAttachment);
            }
            message.From = new MailAddress(from, sender);
            message.Subject = subject;
            message.SubjectEncoding = this._encoding;
            message.Body = body;
            message.BodyEncoding = this._encoding;
            message.IsBodyHtml = this._isHtml;
            message.Priority = MailPriority.Normal;
            foreach (string str in to)
            {
                message.To.Add(str);
            }
            if (this._bcc != null && this._bcc.Length > 0)
            {
                foreach (string b in this._bcc)
                {
                    message.Bcc.Add(b);
                }
            }
            if (this._cc != null && this._cc.Length > 0)
            {
                foreach (string c in this._cc)
                {
                    message.CC.Add(c);
                }
            }

            try
            {
                this._smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return false;
        }

        private SmtpClient _smtp = new SmtpClient();
        private Encoding _encoding { get; set; }
        private bool _isHtml { get; set; }
        private string[] _cc { get; set; }
        private string[] _bcc { get; set; }
        /// <summary>
        /// 获取发送结果，成功则为空
        /// </summary>
        public string Result { get; private set; }
    }
}
