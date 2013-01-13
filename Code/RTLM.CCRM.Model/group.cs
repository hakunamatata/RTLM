using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTLM.Ccrm.Model
{
    public class Group
    {

        private Guid _group_id;
        private string _title;
        private int? _parent_group_id;
        private int _is_default;

        public Guid ID
        {
            get { return _group_id; }
            set { _group_id = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public int? ParentGroupID
        {
            get { return _parent_group_id; }
            set { _parent_group_id = value; }
        }
        public bool IsDefault
        {
            get { return _is_default == 1; }
            set { _is_default = value ? 1 : 0; }
        }

        public Group()
        {
            this.ID = Guid.NewGuid();
            this.Title = string.Empty;
            this. ParentGroupID = null;
            this.IsDefault = false;
        }
    }

}
