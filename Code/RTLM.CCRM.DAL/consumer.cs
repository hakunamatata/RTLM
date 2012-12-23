using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using RTLM;

namespace RTLM.CCRM.DAL{
    public class Consumer
    {
        #region DAL类头部，请务必保留。

        private DbHelper db;
        private DBHelperConnectionState connState;
        public Consumer()
        {
            db = new DbHelper();
            connState = DBHelperConnectionState.CloseOnExit;
        }
        public Consumer(DbHelper keep)
        {
            db = keep;
            connState = DBHelperConnectionState.KeepOpen;
        }

        #endregion

        public void Insert(int parm_cid, string parm_real_name, int? parm_city, DateTime? parm_first_order_date, string parm_frequent_area, int? parm_personal_state, DateTime? parm_last_order_date)
        {
            try
            {
                string Query = @"INSERT INTO [ccrm_consumer]
					([cid]
					,[real_name]
					,[city]
					,[first_order_date]
					,[frequent_area]
					,[personal_state]
					,[last_order_date])
				VALUES
					(@cid
					,@real_name
					,@city
					,@first_order_date
					,@frequent_area
					,@personal_state
					,@last_order_date)";
                SqlParameter[] Parms = {
				new SqlParameter("@cid", parm_cid),
				new SqlParameter("@real_name", parm_real_name),
				new SqlParameter("@city", parm_city),
				new SqlParameter("@first_order_date", parm_first_order_date),
				new SqlParameter("@frequent_area", parm_frequent_area),
				new SqlParameter("@personal_state", parm_personal_state),
				new SqlParameter("@last_order_date", parm_last_order_date)
			};
                if (parm_real_name == null) Parms[1].Value = DBNull.Value;
                if (parm_city == null) Parms[2].Value = DBNull.Value;
                if (parm_first_order_date == null) Parms[3].Value = DBNull.Value;
                if (parm_frequent_area == null) Parms[4].Value = DBNull.Value;
                if (parm_personal_state == null) Parms[5].Value = DBNull.Value;
                if (parm_last_order_date == null) Parms[6].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("向表 ccrm_consumer 中插入数据失败。\n" + ex.Message);
            }
        }

        public void DeleteBy(int parm_cid)
        {
            try
            {
                string Query = @"DELETE FROM [ccrm_consumer]  WHERE [cid] = @cid";
                SqlParameter[] Parms = {
				new SqlParameter("@cid", parm_cid),
			};
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("从表 ccrm_consumer 中删除数据失败。\n" + ex.Message);
            }
        }

        public void Update(string parm_real_name, int? parm_city, DateTime? parm_first_order_date, string parm_frequent_area, int? parm_personal_state, DateTime? parm_last_order_date, int parm_cid)
        {
            try
            {
                string Query = @"UPDATE [ccrm_consumer] 
				SET[real_name] = @real_name
				,[city] = @city
				,[first_order_date] = @first_order_date
				,[frequent_area] = @frequent_area
				,[personal_state] = @personal_state
				,[last_order_date] = @last_order_date
				 WHERE [cid] = @cid ";
                SqlParameter[] Parms = {
				new SqlParameter("@cid", parm_cid),
				new SqlParameter("@real_name", parm_real_name),
				new SqlParameter("@city", parm_city),
				new SqlParameter("@first_order_date", parm_first_order_date),
				new SqlParameter("@frequent_area", parm_frequent_area),
				new SqlParameter("@personal_state", parm_personal_state),
				new SqlParameter("@last_order_date", parm_last_order_date)
			};
                if (parm_real_name == null) Parms[1].Value = DBNull.Value;
                if (parm_city == null) Parms[2].Value = DBNull.Value;
                if (parm_first_order_date == null) Parms[3].Value = DBNull.Value;
                if (parm_frequent_area == null) Parms[4].Value = DBNull.Value;
                if (parm_personal_state == null) Parms[5].Value = DBNull.Value;
                if (parm_last_order_date == null) Parms[6].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("更新表 ccrm_consumer 时失败。\n" + ex.Message);
            }
        }

        public DataTable GetData()
        {
            try
            {
                string Query = @"SELECT 
				cid,
				real_name,
				city,
				first_order_date,
				frequent_area,
				personal_state,
				last_order_date
				FROM ccrm_consumer";
                return db.ExecuteDataSet(Query, connState).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 ccrm_consumer 数据时失败。\n" + ex.Message);
            }
        }
    }
}
