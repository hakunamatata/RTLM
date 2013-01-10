using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTLM;
using System.Data;

namespace RTLM.CCRM.BLL
{
    public class Users
    {
        public Model.User GetModel(Guid id)
        {
            //Model.Customer = new Model.Customer();
            DbHelper db = new DbHelper();
            DAL.User dal_user = new DAL.User(db);
            Model.User model_user = new Model.User();
            DataTable tb_user = dal_user.GetDataBy(id);

            // 用户不存在
            if (tb_user.Rows.Count == 0)
            {
                throw new Exception(string.Format("未找到 id 为 {0} 的用户。", id));
                return null;
            }

            // 找到多个用户
            if (tb_user.Rows.Count > 1)
            {
                throw new Exception(string.Format("找到多个 id 为 {0} 的用户。", id));
                return null;
            }
            foreach (DataRow dr in tb_user.Rows)
            {
                model_user.Avatar = null_check(dr["avatar"]) ? null : dr["avatar"].ToString();
                model_user.Email = null_check(dr["email"]) ? null : dr["email"].ToString();
                model_user.GroupID = Guid.Parse(dr["group_id"].ToString());
                model_user.ID = Guid.Parse(dr["id"].ToString());
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
            DAL.User dal_user = new DAL.User();
            return dal_user.IsEmailExist(email);
        }

        /// <summary>
        /// 手机是否已经使用
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public bool IsMobileExist(string mobile)
        {
            DAL.User dal_user = new DAL.User();
            return dal_user.IsMobileExist(mobile);
        }
    }
}
