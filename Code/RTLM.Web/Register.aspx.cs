using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using RTLM;

public partial class Register : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];

        if (action == "register")
        {
            register();

        }
    }


    protected void register()
    {


        // 有效性验证

        string email = Request.Form["txtboxEmail"],
                mobile = Request.Form["txtboxMobile"],
                password = Request.Form["txtboxPassword"],
                verifycode = Request.Form["inputVerifyCode"],
                readcheck = Request.Form["checkboxRead"];

        try
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            RTLM.Ccrm.Model.Consumer model_consumer = new RTLM.Ccrm.Model.Consumer();
            RTLM.Ccrm.Bll.Consumer bll_consumer = new RTLM.Ccrm.Bll.Consumer();
            model_consumer.Email = email;
            model_consumer.Mobile = mobile;
            model_consumer.Password = Utility.Encrypt(password);
            bll_consumer.CreateConsumer(model_consumer);
            Response.Redirect("Default.aspx");
        }
        catch (Exception ex)
        {

        }

    }
}