using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace RTLM.CCRM.DAL
{

    public class User
    {
        #region DAL类头部，请务必保留。

        private DbHelper db;
        private DBHelperConnectionState connState;
        public User()
        {
            db = new DbHelper();
            connState = DBHelperConnectionState.CloseOnExit;
        }
        public User(DbHelper keep)
        {
            db = keep;
            connState = DBHelperConnectionState.KeepOpen;
        }

        #endregion

        public void Insert(Guid parm_uid, string parm_email, string parm_password, string parm_nick_name, string parm_mobile, int parm_gender, Guid parm_group_id, string parm_avatar, string parm_safe_question, string parm_safe_answer, string parm_qq, int parm_user_type)
        {
            try
            {
                string Query = @"INSERT INTO [ccrm_users]
					([uid]
					,[email]
					,[password]
					,[nick_name]
					,[mobile]
					,[gender]
					,[group_id]
					,[avatar]
					,[safe_question]
					,[safe_answer]
					,[qq]
					,[user_type])
				VALUES
					(@uid
					,@email
					,@password
					,@nick_name
					,@mobile
					,@gender
					,@group_id
					,@avatar
					,@safe_question
					,@safe_answer
					,@qq
					,@user_type)";
                SqlParameter[] Parms = {
				                    new SqlParameter("@uid", parm_uid),
				                    new SqlParameter("@email", parm_email),
				                    new SqlParameter("@password", parm_password),
				                    new SqlParameter("@nick_name", parm_nick_name),
				                    new SqlParameter("@mobile", parm_mobile),
				                    new SqlParameter("@gender", parm_gender),
				                    new SqlParameter("@group_id", parm_group_id),
				                    new SqlParameter("@avatar", parm_avatar),
				                    new SqlParameter("@safe_question", parm_safe_question),
				                    new SqlParameter("@safe_answer", parm_safe_answer),
				                    new SqlParameter("@qq", parm_qq),
				                    new SqlParameter("@user_type", parm_user_type)
			                    };
                if (parm_mobile == null) Parms[4].Value = DBNull.Value;
                if (parm_avatar == null) Parms[7].Value = DBNull.Value;
                if (parm_safe_question == null) Parms[8].Value = DBNull.Value;
                if (parm_safe_answer == null) Parms[9].Value = DBNull.Value;
                if (parm_qq == null) Parms[10].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("向表 ccrm_users 中插入数据失败。\n" + ex.Message);
            }
        }

        public void DeleteBy(Guid parm_uid)
        {
            try
            {
                string Query = @"DELETE FROM [ccrm_users]  WHERE [uid] = @uid";
                SqlParameter[] Parms = {
				                        new SqlParameter("@uid", parm_uid),
			                        };
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("从表 ccrm_users 中删除数据失败。\n" + ex.Message);
            }
        }

        public void Update(string parm_email, string parm_password, string parm_nick_name, string parm_mobile, int parm_gender, Guid parm_group_id, string parm_avatar, string parm_safe_question, string parm_safe_answer, string parm_qq, int parm_user_type, Guid parm_uid)
        {
            try
            {
                string Query = @"UPDATE [ccrm_users] 
				SET[email] = @email
				,[password] = @password
				,[nick_name] = @nick_name
				,[mobile] = @mobile
				,[gender] = @gender
				,[group_id] = @group_id
				,[avatar] = @avatar
				,[safe_question] = @safe_question
				,[safe_answer] = @safe_answer
				,[qq] = @qq
				,[user_type] = @user_type
				 WHERE [uid] = @uid ";
                SqlParameter[] Parms = {
				                        new SqlParameter("@uid", parm_uid),
				                        new SqlParameter("@email", parm_email),
				                        new SqlParameter("@password", parm_password),
				                        new SqlParameter("@nick_name", parm_nick_name),
				                        new SqlParameter("@mobile", parm_mobile),
				                        new SqlParameter("@gender", parm_gender),
				                        new SqlParameter("@group_id", parm_group_id),
				                        new SqlParameter("@avatar", parm_avatar),
				                        new SqlParameter("@safe_question", parm_safe_question),
				                        new SqlParameter("@safe_answer", parm_safe_answer),
				                        new SqlParameter("@qq", parm_qq),
				                        new SqlParameter("@user_type", parm_user_type)
			                        };
                if (parm_mobile == null) Parms[4].Value = DBNull.Value;
                if (parm_avatar == null) Parms[7].Value = DBNull.Value;
                if (parm_safe_question == null) Parms[8].Value = DBNull.Value;
                if (parm_safe_answer == null) Parms[9].Value = DBNull.Value;
                if (parm_qq == null) Parms[10].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("更新表 ccrm_users 时失败。\n" + ex.Message);
            }
        }

        public DataTable GetData()
        {
            try
            {
                string Query = @"SELECT 
				uid,
				email,
				password,
				nick_name,
				mobile,
				gender,
				group_id,
				avatar,
				safe_question,
				safe_answer,
				qq,
				user_type
				FROM ccrm_users";
                return db.ExecuteDataSet(Query, connState).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 ccrm_users 数据时失败。\n" + ex.Message);
            }
        }

        /// <summary>
        /// 根据用户ID获取用户
        /// </summary>
        /// <param name="parm_user_id"></param>
        /// <returns></returns>
        public DataTable GetDataBy(Guid parm_user_id)
        {
            try
            {
                string Query = @"SELECT 
				uid,
				email,
				password,
				nick_name,
				mobile,
				gender,
				group_id,
				avatar,
				safe_question,
				safe_answer,
				qq,
				user_type
				FROM ccrm_users
                WHERE uid=@uid";
                db.AddParameter(new SqlParameter("@uid", parm_user_id));
                return db.ExecuteDataSet(Query, connState).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 ccrm_users 数据时失败。\n" + ex.Message);
            }
        }


        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="parm_email"></param>
        /// <returns></returns>
        public bool IsUserExist(Guid user_id)
        {
            try
            {
                string Query = @"SELECT 
				uid,
				email,
				password,
				nick_name,
				mobile,
				gender,
				group_id,
				avatar,
				safe_question,
				safe_answer,
				qq,
				user_type
				FROM ccrm_users
                WHERE uid=@uid";
                db.AddParameter(new SqlParameter("@uid", user_id));
                return (int)db.ExecuteScalar(Query, connState) == 1;
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 查询邮箱是否存在 时失败。\n" + ex.Message);
            }
        }


        /// <summary>
        /// 邮箱是否已经注册
        /// </summary>
        /// <param name="parm_email"></param>
        /// <returns></returns>
        public bool IsEmailExist(string parm_email)
        {
            try
            {
                string Query = @"SELECT 
				uid,
				email,
				password,
				nick_name,
				mobile,
				gender,
				group_id,
				avatar,
				safe_question,
				safe_answer,
				qq,
				user_type
				FROM ccrm_users
                WHERE email=@email";
                db.AddParameter(new SqlParameter("@email", parm_email));
                return (int)db.ExecuteScalar(Query, connState) == 1;
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 查询邮箱是否存在 时失败。\n" + ex.Message);
            }
        }

        /// <summary>
        /// 手机号是否已经被使用
        /// </summary>
        /// <param name="parm_mobile"></param>
        /// <returns></returns>
        public bool IsMobileExist(string parm_mobile)
        {
            try
            {
                string Query = @"SELECT 
				uid,
				email,
				password,
				nick_name,
				mobile,
				gender,
				group_id,
				avatar,
				safe_question,
				safe_answer,
				qq,
				user_type
				FROM ccrm_users
                WHERE mobile=@mobile";
                db.AddParameter(new SqlParameter("@email", parm_mobile));
                return (int)db.ExecuteScalar(Query, connState) == 1;
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 查询邮箱是否存在 时失败。\n" + ex.Message);
            }
        }
    }
}

