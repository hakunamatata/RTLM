using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using RTLM;

namespace RTLM.Ccrm.Dal
{
    public class Customer
    {
        #region DAL类头部，请务必保留。

        private DbHelper db;
        private DBHelperConnectionState connState;
        public Customer()
        {
            db = new DbHelper();
            connState = DBHelperConnectionState.CloseOnExit;
        }
        public Customer(DbHelper keep)
        {
            db = keep;
            connState = DBHelperConnectionState.KeepOpen;
        }

        #endregion

        public void Insert(Guid parm_cid, string parm_store_name, int? parm_city, string parm_frequent_area, int? parm_store_state, DateTime? parm_last_order_date, DateTime? parm_off_work_time, decimal? parm_frequent_loc_x, decimal? parm_frequent_loc_y)
        {
            try
            {
                string Query = @"INSERT INTO [ccrm_customer]
					([cid]
					,[store_name]
					,[city]
					,[frequent_area]
					,[store_state]
					,[last_order_date]
					,[off_work_time]
					,[frequent_loc_x]
					,[frequent_loc_y]
					,[online_state])
				VALUES
					(@cid
					,@store_name
					,@city
					,@frequent_area
					,@store_state
					,@last_order_date
					,@off_work_time
					,@frequent_loc_x
					,@frequent_loc_y)";
                SqlParameter[] Parms = {
				new SqlParameter("@cid", parm_cid),
				new SqlParameter("@store_name", parm_store_name),
				new SqlParameter("@city", parm_city),
				new SqlParameter("@frequent_area", parm_frequent_area),
				new SqlParameter("@store_state", parm_store_state),
				new SqlParameter("@last_order_date", parm_last_order_date),
				new SqlParameter("@off_work_time", parm_off_work_time),
				new SqlParameter("@frequent_loc_x", parm_frequent_loc_x),
				new SqlParameter("@frequent_loc_y", parm_frequent_loc_y),
			};
                if (parm_store_name == null) Parms[1].Value = DBNull.Value;
                if (parm_city == null) Parms[2].Value = DBNull.Value;
                if (parm_frequent_area == null) Parms[3].Value = DBNull.Value;
                if (parm_store_state == null) Parms[4].Value = DBNull.Value;
                if (parm_last_order_date == null) Parms[5].Value = DBNull.Value;
                if (parm_off_work_time == null) Parms[6].Value = DBNull.Value;
                if (parm_frequent_loc_x == null) Parms[7].Value = DBNull.Value;
                if (parm_frequent_loc_y == null) Parms[8].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("向表 ccrm_customer 中插入数据失败。\n" + ex.Message);
            }
        }

        public void DeleteBy(Guid parm_cid)
        {
            try
            {
                string Query = @"DELETE FROM [ccrm_customer]  WHERE [cid] = @cid";
                SqlParameter[] Parms = {
				new SqlParameter("@cid", parm_cid),
			};
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("从表 ccrm_customer 中删除数据失败。\n" + ex.Message);
            }
        }

        public void Update(string parm_store_name, int? parm_city, string parm_frequent_area, int? parm_store_state, DateTime? parm_last_order_date, DateTime? parm_off_work_time, decimal? parm_frequent_loc_x, decimal? parm_frequent_loc_y, Guid parm_cid)
        {
            try
            {
                string Query = @"UPDATE [ccrm_customer] 
				SET[store_name] = @store_name
				,[city] = @city
				,[frequent_area] = @frequent_area
				,[store_state] = @store_state
				,[last_order_date] = @last_order_date
				,[off_work_time] = @off_work_time
				,[frequent_loc_x] = @frequent_loc_x
				,[frequent_loc_y] = @frequent_loc_y
				 WHERE [cid] = @cid ";
                SqlParameter[] Parms = {
				new SqlParameter("@cid", parm_cid),
				new SqlParameter("@store_name", parm_store_name),
				new SqlParameter("@city", parm_city),
				new SqlParameter("@frequent_area", parm_frequent_area),
				new SqlParameter("@store_state", parm_store_state),
				new SqlParameter("@last_order_date", parm_last_order_date),
				new SqlParameter("@off_work_time", parm_off_work_time),
				new SqlParameter("@frequent_loc_x", parm_frequent_loc_x),
				new SqlParameter("@frequent_loc_y", parm_frequent_loc_y),
			};
                if (parm_store_name == null) Parms[1].Value = DBNull.Value;
                if (parm_city == null) Parms[2].Value = DBNull.Value;
                if (parm_frequent_area == null) Parms[3].Value = DBNull.Value;
                if (parm_store_state == null) Parms[4].Value = DBNull.Value;
                if (parm_last_order_date == null) Parms[5].Value = DBNull.Value;
                if (parm_off_work_time == null) Parms[6].Value = DBNull.Value;
                if (parm_frequent_loc_x == null) Parms[7].Value = DBNull.Value;
                if (parm_frequent_loc_y == null) Parms[8].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("更新表 ccrm_customer 时失败。\n" + ex.Message);
            }
        }

        public DataTable GetData()
        {
            try
            {
                string Query = @"SELECT 
				cid,
				store_name,
				city,
				frequent_area,
				store_state,
				last_order_date,
				off_work_time,
				frequent_loc_x,
				frequent_loc_y
				FROM ccrm_customer";
                return db.ExecuteDataSet(Query, connState).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 ccrm_customer 数据时失败。\n" + ex.Message);
            }
        }


        public DataTable GetDataBy(Guid id)
        {
            try
            {
                string Query = @"SELECT 
				cid,
				store_name,
				city,
				frequent_area,
				store_state,
				last_order_date,
				off_work_time,
				frequent_loc_x,
				frequent_loc_y
				FROM ccrm_customer
                WHERE cid = @id
                ";
                db.AddParameter(new SqlParameter("@id", id));
                return db.ExecuteDataSet(Query, connState).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 ccrm_customer 数据时失败。\n" + ex.Message);
            }
        }


        /// <summary>
        /// 客户是否存在
        /// </summary>
        /// <param name="param_id">用户编号|email|手机|用户名</param>
        /// <returns></returns>
        public static int Exist(Guid param_id)
        {
            DbHelper db = new DbHelper();
            try
            {
                string Query = @"SELECT COUNT(*) FROM dt_users du, ccrm_customer cc
                                WHERE du.id = cc.cid
                                AND du.id = @id OR du.email = @id OR du.[user_name]=@id OR du.mobile = @id";
                SqlParameter[] Parms = {
			                                new SqlParameter("@id",  param_id)
		                                };
                db.AddParameter(Parms);
                return (int)db.ExecuteScalar(Query);
            }
            catch (Exception ex)
            {
                throw new Exception("执行方法 Exist 时出错。\n" + ex.Message);
            }
        }


    }
}
