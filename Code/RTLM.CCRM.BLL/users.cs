using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTLM;
using System.Data;
using System.Text.RegularExpressions;

namespace RTLM.Ccrm.Bll
{
    public class Users
    {
        public Model.User GetModel(string user)
        {
            //Model.Customer = new Model.Customer();
            DbHelper db = new DbHelper();
            Dal.User dal_user = new Dal.User(db);
            Model.User model_user = new Model.User();
            DataTable tb_user = null;
            Regex regEmail = new Regex(@"^[_a-z0-9]+@([_a-z0-9]+\.)+[a-z0-9]{2,3}$");
            Regex regGuid = new Regex(@"^[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}$");
            if (regEmail.IsMatch(user))
                tb_user = dal_user.GetDataByEmail(user);
            else if (regGuid.IsMatch(user))
                tb_user = dal_user.GetDataByID(Guid.Parse(user));
            else
                tb_user = dal_user.GetDataByMobile(user);
            // 用户不存在
            if (tb_user.Rows.Count == 0)
            {
                throw new Exception(string.Format("未找到用户 {0}。", user));
            }

            // 找到多个用户
            if (tb_user.Rows.Count > 1)
            {
                throw new Exception(string.Format("找到多个用户 {0}。", user));
            }
            foreach (DataRow dr in tb_user.Rows)
            {
                model_user.Avatar = null_check(dr["avatar"]) ? null : dr["avatar"].ToString();
                model_user.Email = null_check(dr["email"]) ? null : dr["email"].ToString();
                model_user.GroupID = Guid.Parse(dr["group_id"].ToString());
                model_user.ID = Guid.Parse(dr["uid"].ToString());
                model_user.Mobile = null_check(dr["mobile"]) ? null : dr["mobile"].ToString();
                model_user.NickName = null_check(dr["nick_name"]) ? null : dr["nick_name"].ToString();
                model_user.Password = dr["password"].ToString();
                model_user.QQ = null_check(dr["qq"]) ? null : dr["qq"].ToString();
                model_user.SafeAnswer = null_check(dr["safe_answer"]) ? null : dr["safe_answer"].ToString();
                model_user.SafeQuestion = null_check(dr["safe_question"]) ? null : dr["safe_question"].ToString();
                model_user.Gender = Convert.ToInt16(dr["gender"]);
                model_user.Type = Convert.ToInt16(dr["user_type"]);
            }

            return model_user;
        }

        private bool null_check(object dr)
        {
            return (dr == DBNull.Value || dr == null || dr.ToString() == string.Empty);
        }


        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmialExist(string email)
        {
            Dal.User dal_user = new Dal.User();
            return dal_user.IsEmailExist(email);
        }

        /// <summary>
        /// 手机是否已经使用
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public bool IsMobileExist(string mobile)
        {
            Dal.User dal_user = new Dal.User();
            return dal_user.IsMobileExist(mobile);
        }
    }
}
