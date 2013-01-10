<%@ WebHandler Language="C#" Class="RTLM.Web.Api.Handler.users" %>

using System;
using System.Web;
using System.Runtime.Serialization;
using RTLM;
using LitJson;

namespace RTLM.Web.Api.Handler
{
    public class users : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            System.Collections.Specialized.NameValueCollection Parameters = context.Request.QueryString;

            for (int i = 0; i < Parameters.Count; i++)
            {
                string[] values = Parameters.GetValues(i);
                if (values.Length == 0 || string.IsNullOrEmpty(values[0]))
                {
                    context.Response.Write(Utility.ReturnFailedMessage("传递的参数不全。"));
                    return;
                }
            }

            string action = context.Request.QueryString["action"];
            switch (action)
            {
                case "valid_email":
                    string email = context.Request.QueryString["p1"];
                    if (!valid_email(email))
                        context.Response.Write(Utility.ReturnSuccessMessage("邮箱验证成功。"));
                    else
                        context.Response.Write(Utility.ReturnFailedMessage("邮箱验证失败。"));
                    break;

                case "valid_mobile":
                    string mobile = context.Request.QueryString["p1"];
                    if (!valid_mobile(mobile))
                        context.Response.Write(Utility.ReturnSuccessMessage("手机验证成功。"));
                    else
                        context.Response.Write(Utility.ReturnFailedMessage("手机验证失败。"));
                    break;

                default:
                    break;
            }

        }


        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool valid_email(string email)
        {
            try
            {
                CCRM.DAL.User dal_user = new CCRM.DAL.User();
                return dal_user.IsEmailExist(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 手机号码是否存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        bool valid_mobile(string mobile)
        {
            try
            {
                CCRM.DAL.User dal_user = new CCRM.DAL.User();
                return dal_user.IsMobileExist(mobile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}