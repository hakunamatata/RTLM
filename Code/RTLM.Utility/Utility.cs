using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LitJson;

namespace RTLM
{
    public partial class Utility
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ReturnMessage(bool success, string message)
        {
            JsonData returnJson = new JsonData();
            returnJson["success"] = success;
            returnJson["message"] = message;
            return returnJson.ToJson();
        }

        /// <summary>
        /// 返回成功信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ReturnSuccessMessage(string message)
        {
            return ReturnMessage(true, message);
        }

        /// <summary>
        /// 返回成功信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ReturnSuccessMessage()
        {
            return ReturnMessage(true, string.Empty);
        }

        /// <summary>
        /// 返回失败信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ReturnFailedMessage(string message)
        {
            return ReturnMessage(false, message);
        }

        /// <summary>
        /// 输出信息
        /// </summary>
        /// <param name="message"></param>
        public static void Show(string message, Page page)
        {
            JsonData json = new JsonData();
            //json.
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "ScriptOutput", "alert('123')", true);
        }
    }
}

