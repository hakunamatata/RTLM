using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace RTLM.CCRM.Model
{
    public class Consumer
    {

        private int _cid;
        private string _real_name;
        private int? _city;
        private DateTime? _first_order_date;
        private string _frequent_area;
        private int? _personal_state;
        private DateTime? _last_order_date;

        public int ID
        {
            get { return _cid; }
            set { _cid = value; }
        }
        public string RealName
        {
            get { return _real_name; }
            set { _real_name = value; }
        }
        public int? City
        {
            get { return _city; }
            set { _city = value; }
        }
        public DateTime? FirstOrderDate
        {
            get { return _first_order_date; }
            set { _first_order_date = value; }
        }
        public string FrequentArea
        {
            get { return _frequent_area; }
            set { _frequent_area = value; }
        }
        public int? State
        {
            get { return _personal_state; }
            set { _personal_state = value; }
        }
        public DateTime? LastOrderDate
        {
            get { return _last_order_date; }
            set { _last_order_date = value; }
        }
    }
}