using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RTLM;

namespace RTLM.Ccrm.Bll
{
    public class Group
    {
        public Model.Group GetModel(Guid group_id)
        {
            Model.Group model_group = new Model.Group();
            Dal.Group dal_group = new Dal.Group();

            DataTable dt = dal_group.GetDataBy(group_id);
            if (dt.Rows.Count == 0)
            {
                throw new Exception(string.Format("未找到 ID 为 {0} 的组。", group_id.ToString()));
            }

            DataRow dr = dt.Rows[0];

            model_group.ID = Guid.Parse(dr["group_id"].ToString());
            model_group.IsDefault = Convert.ToInt16(dr["is_default"]) == 1;
            model_group.Title = dr["title"].ToString();
            model_group.ParentGroupID = null;

            return model_group;
        }
    }
}
