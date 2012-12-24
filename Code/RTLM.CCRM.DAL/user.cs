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

        public void Insert(int parm_id, int parm_group_id, string parm_user_name, string parm_password, string parm_email, string parm_nick_name, string parm_avatar, string parm_sex, DateTime? parm_birthday, string parm_telphone, string parm_mobile, string parm_qq, string parm_address, string parm_safe_question, string parm_safe_answer, decimal? parm_amount, int? parm_point, int? parm_exp, int? parm_is_lock, DateTime? parm_reg_time, string parm_reg_ip)
        {
            try
            {
                string Query = @"INSERT INTO [dt_users]
					([id]
					,[group_id]
					,[user_name]
					,[password]
					,[email]
					,[nick_name]
					,[avatar]
					,[sex]
					,[birthday]
					,[telphone]
					,[mobile]
					,[qq]
					,[address]
					,[safe_question]
					,[safe_answer]
					,[amount]
					,[point]
					,[exp]
					,[is_lock]
					,[reg_time]
					,[reg_ip])
				VALUES
					(@id
					,@group_id
					,@user_name
					,@password
					,@email
					,@nick_name
					,@avatar
					,@sex
					,@birthday
					,@telphone
					,@mobile
					,@qq
					,@address
					,@safe_question
					,@safe_answer
					,@amount
					,@point
					,@exp
					,@is_lock
					,@reg_time
					,@reg_ip)";
                SqlParameter[] Parms = {
				new SqlParameter("@id", parm_id),
				new SqlParameter("@group_id", parm_group_id),
				new SqlParameter("@user_name", parm_user_name),
				new SqlParameter("@password", parm_password),
				new SqlParameter("@email", parm_email),
				new SqlParameter("@nick_name", parm_nick_name),
				new SqlParameter("@avatar", parm_avatar),
				new SqlParameter("@sex", parm_sex),
				new SqlParameter("@birthday", parm_birthday),
				new SqlParameter("@telphone", parm_telphone),
				new SqlParameter("@mobile", parm_mobile),
				new SqlParameter("@qq", parm_qq),
				new SqlParameter("@address", parm_address),
				new SqlParameter("@safe_question", parm_safe_question),
				new SqlParameter("@safe_answer", parm_safe_answer),
				new SqlParameter("@amount", parm_amount),
				new SqlParameter("@point", parm_point),
				new SqlParameter("@exp", parm_exp),
				new SqlParameter("@is_lock", parm_is_lock),
				new SqlParameter("@reg_time", parm_reg_time),
				new SqlParameter("@reg_ip", parm_reg_ip)
			};
                if (parm_email == null) Parms[4].Value = DBNull.Value;
                if (parm_nick_name == null) Parms[5].Value = DBNull.Value;
                if (parm_avatar == null) Parms[6].Value = DBNull.Value;
                if (parm_sex == null) Parms[7].Value = DBNull.Value;
                if (parm_birthday == null) Parms[8].Value = DBNull.Value;
                if (parm_telphone == null) Parms[9].Value = DBNull.Value;
                if (parm_mobile == null) Parms[10].Value = DBNull.Value;
                if (parm_qq == null) Parms[11].Value = DBNull.Value;
                if (parm_address == null) Parms[12].Value = DBNull.Value;
                if (parm_safe_question == null) Parms[13].Value = DBNull.Value;
                if (parm_safe_answer == null) Parms[14].Value = DBNull.Value;
                if (parm_amount == null) Parms[15].Value = DBNull.Value;
                if (parm_point == null) Parms[16].Value = DBNull.Value;
                if (parm_exp == null) Parms[17].Value = DBNull.Value;
                if (parm_is_lock == null) Parms[18].Value = DBNull.Value;
                if (parm_reg_time == null) Parms[19].Value = DBNull.Value;
                if (parm_reg_ip == null) Parms[20].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("向表 dt_users 中插入数据失败。\n" + ex.Message);
            }
        }

        public void DeleteBy(int parm_id)
        {
            try
            {
                string Query = @"DELETE FROM [dt_users]  WHERE [id] = @id";
                SqlParameter[] Parms = {
				new SqlParameter("@id", parm_id),
			};
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("从表 dt_users 中删除数据失败。\n" + ex.Message);
            }
        }

        public void Update(int parm_group_id, string parm_user_name, string parm_password, string parm_email, string parm_nick_name, string parm_avatar, string parm_sex, DateTime? parm_birthday, string parm_telphone, string parm_mobile, string parm_qq, string parm_address, string parm_safe_question, string parm_safe_answer, decimal? parm_amount, int? parm_point, int? parm_exp, int? parm_is_lock, DateTime? parm_reg_time, string parm_reg_ip, int parm_id)
        {
            try
            {
                string Query = @"UPDATE [dt_users] 
				SET[group_id] = @group_id
				,[user_name] = @user_name
				,[password] = @password
				,[email] = @email
				,[nick_name] = @nick_name
				,[avatar] = @avatar
				,[sex] = @sex
				,[birthday] = @birthday
				,[telphone] = @telphone
				,[mobile] = @mobile
				,[qq] = @qq
				,[address] = @address
				,[safe_question] = @safe_question
				,[safe_answer] = @safe_answer
				,[amount] = @amount
				,[point] = @point
				,[exp] = @exp
				,[is_lock] = @is_lock
				,[reg_time] = @reg_time
				,[reg_ip] = @reg_ip
				 WHERE [id] = @id ";
                SqlParameter[] Parms = {
				new SqlParameter("@id", parm_id),
				new SqlParameter("@group_id", parm_group_id),
				new SqlParameter("@user_name", parm_user_name),
				new SqlParameter("@password", parm_password),
				new SqlParameter("@email", parm_email),
				new SqlParameter("@nick_name", parm_nick_name),
				new SqlParameter("@avatar", parm_avatar),
				new SqlParameter("@sex", parm_sex),
				new SqlParameter("@birthday", parm_birthday),
				new SqlParameter("@telphone", parm_telphone),
				new SqlParameter("@mobile", parm_mobile),
				new SqlParameter("@qq", parm_qq),
				new SqlParameter("@address", parm_address),
				new SqlParameter("@safe_question", parm_safe_question),
				new SqlParameter("@safe_answer", parm_safe_answer),
				new SqlParameter("@amount", parm_amount),
				new SqlParameter("@point", parm_point),
				new SqlParameter("@exp", parm_exp),
				new SqlParameter("@is_lock", parm_is_lock),
				new SqlParameter("@reg_time", parm_reg_time),
				new SqlParameter("@reg_ip", parm_reg_ip)
			};
                if (parm_email == null) Parms[4].Value = DBNull.Value;
                if (parm_nick_name == null) Parms[5].Value = DBNull.Value;
                if (parm_avatar == null) Parms[6].Value = DBNull.Value;
                if (parm_sex == null) Parms[7].Value = DBNull.Value;
                if (parm_birthday == null) Parms[8].Value = DBNull.Value;
                if (parm_telphone == null) Parms[9].Value = DBNull.Value;
                if (parm_mobile == null) Parms[10].Value = DBNull.Value;
                if (parm_qq == null) Parms[11].Value = DBNull.Value;
                if (parm_address == null) Parms[12].Value = DBNull.Value;
                if (parm_safe_question == null) Parms[13].Value = DBNull.Value;
                if (parm_safe_answer == null) Parms[14].Value = DBNull.Value;
                if (parm_amount == null) Parms[15].Value = DBNull.Value;
                if (parm_point == null) Parms[16].Value = DBNull.Value;
                if (parm_exp == null) Parms[17].Value = DBNull.Value;
                if (parm_is_lock == null) Parms[18].Value = DBNull.Value;
                if (parm_reg_time == null) Parms[19].Value = DBNull.Value;
                if (parm_reg_ip == null) Parms[20].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("更新表 dt_users 时失败。\n" + ex.Message);
            }
        }

        public DataTable GetData()
        {
            try
            {
                string Query = @"SELECT 
				id,
				group_id,
				user_name,
				password,
				email,
				nick_name,
				avatar,
				sex,
				birthday,
				telphone,
				mobile,
				qq,
				address,
				safe_question,
				safe_answer,
				amount,
				point,
				exp,
				is_lock,
				reg_time,
				reg_ip
				FROM dt_users";
                return db.ExecuteDataSet(Query, connState).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 dt_users 数据时失败。\n" + ex.Message);
            }
        }


        public DataTable GetDataBy(string id)
        {
            try
            {
                string Query = @"SELECT 
				id,
				group_id,
				user_name,
				password,
				email,
				nick_name,
				avatar,
				sex,
				birthday,
				telphone,
				mobile,
				qq,
				address,
				safe_question,
				safe_answer,
				amount,
				point,
				exp,
				is_lock,
				reg_time,
				reg_ip
				FROM dt_users
                WHERE id=@id or user_name=@id or mobile=@id or email=@id
                ";
                SqlParameter param = new SqlParameter("@id", id);
                db.AddParameter(param);
                return db.ExecuteDataSet(Query, connState).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 dt_users 数据时失败。\n" + ex.Message);
            }
        }

    }

}
