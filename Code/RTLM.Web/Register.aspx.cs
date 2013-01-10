using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RTLM.CCRM.BLL;

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

        string email = Request.Form["txtboxEmail"],
                mobile = Request.Form["txtboxMobile"],
                password = Request.Form["txtboxPassword"],
                checkRead = Request.Form["inputVerifyCode"];


        Consumer consumer = new Consumer();
    }
}