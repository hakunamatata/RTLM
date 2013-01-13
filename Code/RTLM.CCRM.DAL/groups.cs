using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using RTLM;

namespace RTLM.Ccrm.Dal
{
    public class Group
    {
        #region DAL类头部，请务必保留。

        private DbHelper db;
        private DBHelperConnectionState connState;
        public Group()
        {
            db = new DbHelper();
            connState = DBHelperConnectionState.CloseOnExit;
        }
        public Group(DbHelper keep)
        {
            db = keep;
            connState = DBHelperConnectionState.KeepOpen;
        }

        #endregion

        public void Insert(Guid parm_group_id, string parm_title, int? parm_parent_group_id, int parm_is_default)
        {
            try
            {
                string Query = @"INSERT INTO [ccrm_groups]
					([group_id]
					,[title]
					,[parent_group_id]
					,[is_default])
				VALUES
					(@group_id
					,@title
					,@parent_group_id
					,@is_default)";
                SqlParameter[] Parms = {
				new SqlParameter("@group_id", parm_group_id),
				new SqlParameter("@title", parm_title),
				new SqlParameter("@parent_group_id", parm_parent_group_id),
				new SqlParameter("@is_default", parm_is_default)
			};
                if (parm_parent_group_id == null) Parms[2].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("向表 ccrm_groups 中插入数据失败。\n" + ex.Message);
            }
        }

        public void DeleteBy(Guid parm_group_id)
        {
            try
            {
                string Query = @"DELETE FROM [ccrm_groups]  WHERE [group_id] = @group_id";
                SqlParameter[] Parms = {
				new SqlParameter("@group_id", parm_group_id),
			};
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("从表 ccrm_groups 中删除数据失败。\n" + ex.Message);
            }
        }

        public void Update(string parm_title, int? parm_parent_group_id, int parm_is_default, Guid parm_group_id)
        {
            try
            {
                string Query = @"UPDATE [ccrm_groups] 
				SET[title] = @title
				,[parent_group_id] = @parent_group_id
				,[is_default] = @is_default
				 WHERE [group_id] = @group_id ";
                SqlParameter[] Parms = {
				new SqlParameter("@group_id", parm_group_id),
				new SqlParameter("@title", parm_title),
				new SqlParameter("@parent_group_id", parm_parent_group_id),
				new SqlParameter("@is_default", parm_is_default)
			};
                if (parm_parent_group_id == null) Parms[2].Value = DBNull.Value;
                db.AddParameter(Parms);
                db.ExecuteNonQuery(Query, connState);
            }
            catch (Exception ex)
            {
                throw new Exception("更新表 ccrm_groups 时失败。\n" + ex.Message);
            }
        }

        public DataTable GetData()
        {
            try
            {
                string Query = @"SELECT 
				group_id,
				title,
				parent_group_id,
				is_default
				FROM ccrm_groups";
                return db.ExecuteDataSet(Query, connState).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 ccrm_groups 数据时失败。\n" + ex.Message);
            }
        }


        public DataTable GetDataBy(Guid parm_group_id)
        {
            try
            {
                string Query = @"SELECT 
				group_id,
				title,
				parent_group_id,
				is_default
				FROM ccrm_groups
                WHERE group_id=@group_id
                ";
                db.AddParameter(new SqlParameter("@group_id", parm_group_id));
                return db.ExecuteDataSet(Query, connState).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("获取表 ccrm_groups 数据时失败。\n" + ex.Message);
            }
        }

    }
}