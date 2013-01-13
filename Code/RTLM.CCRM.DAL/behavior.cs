using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using RTLM;

namespace RTLM.Ccrm.Dal
{
    public class Behavior
    {
        #region DAL类头部，请务必保留。

        private DbHelper db;
        private DBHelperConnectionState connState;
        public Behavior()
        {
            db = new DbHelper();
            connState = DBHelperConnectionState.CloseOnExit;
        }
        public Behavior(DbHelper keep)
        {
            db = keep;
            connState = DBHelperConnectionState.KeepOpen;
        }

        #endregion

        public void Insert(Guid parm_bhvr_id, Guid parm_csmr_id, string parm_csmr_name, string parm_csmr_cellphone, string parm_csmr_destination, decimal parm_csmr_loc_x, decimal parm_csmr_loc_y, Guid parm_cstmr_id, DateTime parm_bhvr_date, string parm_goods_name, decimal? parm_csm_amount, decimal? parm_tip_amount, int? parm_related_salesman, int parm_bhvr_state, int? parm_is_failed, string parm_failed_reason)
        {
            try
            {
                string Query = @"INSERT INTO [ccrm_consume_behavior]
					([bhvr_id]
					,[csmr_id]
					,[csmr_name]
					,[csmr_cellphone]
					,[csmr_destination]
					,[csmr_loc_x]
					,[csmr_loc_y]
					,[cstmr_id]
					,[bhvr_date]
					,[goods_name]
					,[csm_amount]
					,[tip_amount]
					,[related_salesman]
					,[bhvr_state]
					,[is_failed]
					,[failed_reason])
				VALUES
					(@bhvr_id
					,@csmr_id
					,@csmr_name
					,@csmr_cellphone
					,@csmr_destination
					,@csmr_loc_x
					,@csmr_loc_y
					,@cstmr_id
					,@bhvr_date
					,@goods_name
					,@csm_amount
					,@tip_amount
					,@related_salesman
					,@bhvr_state
					,@is_failed
					,@failed_reason)";
                SqlParameter[] Parms = {
				new SqlParameter("@bhvr_id", parm_bhvr_id),
				new SqlParameter("@csmr_id", parm_csmr_id),
				new SqlParameter("@csmr_name", parm_csmr_name),
				new SqlParameter("@csmr_cellphone", parm_csmr_cellphone),
				new SqlParameter("@csmr_destination", parm_csmr_destination),
				new SqlParameter("@csmr_loc_x", parm_csmr_loc_x),
				new SqlParameter("@csmr_loc_y", parm_csmr_loc_y),
				new SqlParameter("@cstmr_id", parm_cstmr_id),
				new SqlParameter("@bhvr_date", parm_bhvr_date),
				new SqlParameter("@goods_name", parm_goods_name),
				new SqlParameter("@csm_amount", parm_csm_amount),
				new SqlParameter("@tip_amount", parm_tip_amount),
				new SqlParameter("@related_salesman", parm_related_salesman),
				new SqlParameter("@bhvr_state", parm_bhvr_state),
				new SqlParameter("@is_failed", parm_is_failed),
				new SqlParameter("@failed_reason", parm_failed_reason)
			};
                if (parm_csmr_name == null) Parms[2].Value = DBNull.Value;
                if (parm_csmr_cellphone == null) Parms[3].Value = DBNull.Value;
                if (parm_csm_amount == null) Parms[10].Value = DBNull.Value;
                if (parm_tip_amount == null) Parms[11].Value = DBNull.Value;
                if (parm_related_salesman == null) Parms[12].Value = DBNull.Value;
                if (parm_is_failed == null) Parms[14].Value = DBNull.Value;
                if (parm_failed_reason == null) Parms[15].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("向表 ccrm_consume_behavior 中插入数据失败。\n" + ex.Message);
            }
        }

        public void DeleteBy(int parm_bhvr_id)
        {
            try
            {
                string Query = @"DELETE FROM [ccrm_consume_behavior]  WHERE [bhvr_id] = @bhvr_id";
                SqlParameter[] Parms = {
				new SqlParameter("@bhvr_id", parm_bhvr_id),
			};
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("从表 ccrm_consume_behavior 中删除数据失败。\n" + ex.Message);
            }
        }

        public void Update(Guid parm_csmr_id, string parm_csmr_name, string parm_csmr_cellphone, string parm_csmr_destination, decimal parm_csmr_loc_x, decimal parm_csmr_loc_y, Guid parm_cstmr_id, DateTime parm_bhvr_date, string parm_goods_name, decimal? parm_csm_amount, decimal? parm_tip_amount, int? parm_related_salesman, int parm_bhvr_state, int? parm_is_failed, string parm_failed_reason, Guid parm_bhvr_id)
        {
            try
            {
                string Query = @"UPDATE [ccrm_consume_behavior] 
				SET[csmr_id] = @csmr_id
				,[csmr_name] = @csmr_name
				,[csmr_cellphone] = @csmr_cellphone
				,[csmr_destination] = @csmr_destination
				,[csmr_loc_x] = @csmr_loc_x
				,[csmr_loc_y] = @csmr_loc_y
				,[cstmr_id] = @cstmr_id
				,[bhvr_date] = @bhvr_date
				,[goods_name] = @goods_name
				,[csm_amount] = @csm_amount
				,[tip_amount] = @tip_amount
				,[related_salesman] = @related_salesman
				,[bhvr_state] = @bhvr_state
				,[is_failed] = @is_failed
				,[failed_reason] = @failed_reason
				 WHERE [bhvr_id] = @bhvr_id ";
                SqlParameter[] Parms = {
				                        new SqlParameter("@bhvr_id", parm_bhvr_id),
				                        new SqlParameter("@csmr_id", parm_csmr_id),
				                        new SqlParameter("@csmr_name", parm_csmr_name),
				                        new SqlParameter("@csmr_cellphone", parm_csmr_cellphone),
				                        new SqlParameter("@csmr_destination", parm_csmr_destination),
				                        new SqlParameter("@csmr_loc_x", parm_csmr_loc_x),
				                        new SqlParameter("@csmr_loc_y", parm_csmr_loc_y),
				                        new SqlParameter("@cstmr_id", parm_cstmr_id),
				                        new SqlParameter("@bhvr_date", parm_bhvr_date),
				                        new SqlParameter("@goods_name", parm_goods_name),
				                        new SqlParameter("@csm_amount", parm_csm_amount),
				                        new SqlParameter("@tip_amount", parm_tip_amount),
				                        new SqlParameter("@related_salesman", parm_related_salesman),
				                        new SqlParameter("@bhvr_state", parm_bhvr_state),
				                        new SqlParameter("@is_failed", parm_is_failed),
				                        new SqlParameter("@failed_reason", parm_failed_reason)
			                        };
                if (parm_csmr_name == null) Parms[2].Value = DBNull.Value;
                if (parm_csmr_cellphone == null) Parms[3].Value = DBNull.Value;
                if (parm_csm_amount == null) Parms[10].Value = DBNull.Value;
                if (parm_tip_amount == null) Parms[11].Value = DBNull.Value;
                if (parm_related_salesman == null) Parms[12].Value = DBNull.Value;
                if (parm_is_failed == null) Parms[14].Value = DBNull.Value;
                if (parm_failed_reason == null) Parms[15].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("更新表 ccrm_consume_behavior 时失败。\n" + ex.Message);
            }
        }

        public DataTable GetData()
        {
            try
            {
                string Query = @"SELECT 
				bhvr_id,
				csmr_id,
				csmr_name,
				csmr_cellphone,
				csmr_destination,
				csmr_loc_x,
				csmr_loc_y,
				cstmr_id,
				bhvr_date,
				goods_name,
				csm_amount,
				tip_amount,
				related_salesman,
				bhvr_state,
				is_failed,
				failed_reason
				FROM ccrm_consume_behavior";
                return db.ExecuteDataSet(Query, connState).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 ccrm_consume_behavior 数据时失败。\n" + ex.Message);
            }
        }
    }
}