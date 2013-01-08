<%@ WebHandler Language="C#" Class="users" %>

using System;
using System.Web;
using System.Runtime.Serialization;
using RTLM.CCRM.DAL;
using RTLM.Utility;
using LitJson;
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
                if (valid_email(email))
                    context.Response.Write(Utility.ReturnSuccessMessage("邮箱验证成功。"));
                else
                    context.Response.Write(Utility.ReturnFailedMessage("邮箱验证失败。"));
                break;
            default:
                break;
        }

    }

    bool valid_email(string email)
    {
        return true;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}