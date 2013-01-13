using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RTLM;
public partial class Login : System.Web.UI.Page
{

    string account = string.Empty;
    string passwrod = string.Empty;
    string action = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        action = Request.QueryString["action"];
        account = Request.Form["txtboxAccount"];
        passwrod = Request.Form["txtboxPassword"];

        if (action == "login")
        {
            login(account, passwrod);
        }


    }

    protected void login(string user, string pwd)
    {
       
    }
}