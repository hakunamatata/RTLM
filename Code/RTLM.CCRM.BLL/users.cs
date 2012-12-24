using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTLM;
using System.Data;
using log4net;
namespace RTLM.CCRM.BLL
{
    public class Users
    {
        private ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Model.User GetModel(string id)
        {
            //Model.Customer = new Model.Customer();
            DbHelper db = new DbHelper();
            DAL.User dal_user = new DAL.User(db);
            Model.User model_user = new Model.User();
            DataTable tb_user = dal_user.GetDataBy(id);

            // 用户不存在
            if (tb_user.Rows.Count == 0)
            {
                log.Error("message", new Exception(string.Format("未找到 id 为 {0} 的用户。", id)));
                return null;
            }

            // 找到多个用户
            if (tb_user.Rows.Count > 1)
            {
                log.Error("error", new Exception(string.Format("找到多个 id 为 {0} 的用户。", id)));
                return null;
            }
            foreach (DataRow dr in tb_user.Rows)
            {
                model_user.Address = null_check(dr["address"]) ? null : dr["address"].ToString();
                model_user.Amount = null_check(dr["amount"]) ? null : (decimal?)Convert.ToDecimal(dr["amount"]);
                model_user.Avatar = null_check(dr["avatar"]) ? null : dr["avatar"].ToString();
                model_user.Birthday = null_check(dr["birthday"]) ? null : (DateTime?)Convert.ToDateTime(dr["Birthday"]);
                model_user.Email = null_check(dr["email"]) ? null : dr["email"].ToString();
                model_user.Exp = null_check(dr["exp"]) ? null : (int?)Convert.ToInt32(dr["exp"]);
                model_user.GroupID = Convert.ToInt32(dr["group_id"]);
                model_user.ID = Convert.ToInt32(dr["id"]);
                model_user.IsLock = null_check(dr["is_lock"]) ? false : Convert.ToBoolean(dr["is_lock"]);
                model_user.Mobile = null_check(dr["mobile"]) ? null : dr["mobile"].ToString();
                model_user.NickName = null_check(dr["nick_name"]) ? null : dr["nick_name"].ToString();
                model_user.Password = dr["password"].ToString();
                model_user.Point = null_check(dr["point"]) ? null : (int?)Convert.ToInt32(dr["point"]);
                model_user.QQ = null_check(dr["qq"]) ? null : dr["qq"].ToString();
                model_user.RegIP = null_check(dr["reg_ip"]) ? null : dr["reg_ip"].ToString();
                model_user.RegTime = null_check(dr["reg_time"]) ? null : (DateTime?)Convert.ToDateTime(dr["reg_time"]);
                model_user.SafeAnswer = null_check(dr["safe_answer"]) ? null : dr["safe_answer"].ToString();
                model_user.SafeQuestion = null_check(dr["safe_question"]) ? null : dr["safe_question"].ToString();
                model_user.Sex = null_check(dr["sex"]) ? null : dr["sex"].ToString();
                model_user.Telphone = null_check(dr["telphone"]) ? null : dr["telphone"].ToString();
                model_user.UserName = dr["user_name"].ToString();
            }

            return model_user;
        }

        private bool null_check(object dr)
        {
            return (dr == DBNull.Value || dr == null || dr.ToString() == string.Empty);
        }

    }
}
