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
                #region 注册相关
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
                #endregion

                case "login":
                    string userType = context.Request.QueryString["p1"],
                           user = context.Request.QueryString["p2"],
                           password = context.Request.QueryString["p3"];
                    if (userType == "consumer")
                    {
                        if (consumerLogin(user, password))
                            context.Response.Write(Utility.ReturnSuccessMessage());
                        else
                            context.Response.Write(Utility.ReturnFailedMessage("登录失败。"));
                    }
                    break;
                default:
                    break;
            }

        }

        #region 用户注册所涉及的验证和方法
        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool valid_email(string email)
        {
            try
            {
                Ccrm.Dal.User dal_user = new Ccrm.Dal.User();
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
                Ccrm.Dal.User dal_user = new Ccrm.Dal.User();
                return dal_user.IsMobileExist(mobile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion




        #region 用户登录相关

        bool consumerLogin(string user, string pwd)
        {
            try
            {
                RTLM.Ccrm.Bll.Consumer bll_consumer = new RTLM.Ccrm.Bll.Consumer();
                return bll_consumer.ConsumerLogin(user, pwd) == 1;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}