using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace RTLM.CCRM.Model
{
    public class Customer
    {

        private int _cid;
        private string _store_name;
        private int? _city;
        private string _frequent_area;
        private int? _store_state;
        private DateTime? _last_order_date;
        private DateTime? _off_work_time;
        private decimal? _frequent_loc_x;
        private decimal? _frequent_loc_y;
        private int? _online_state;

        public int ID
        {
            get { return _cid; }
            set { _cid = value; }
        }
        public string StoreName
        {
            get { return _store_name; }
            set { _store_name = value; }
        }
        public int? City
        {
            get { return _city; }
            set { _city = value; }
        }
        public string FrequentArea
        {
            get { return _frequent_area; }
            set { _frequent_area = value; }
        }
        public int? StoreState
        {
            get { return _store_state; }
            set { _store_state = value; }
        }
        public DateTime? LastOrderDate
        {
            get { return _last_order_date; }
            set { _last_order_date = value; }
        }
        public DateTime? OffWorkTime
        {
            get { return _off_work_time; }
            set { _off_work_time = value; }
        }
        public decimal? FrequentLocationX
        {
            get { return _frequent_loc_x; }
            set { _frequent_loc_x = value; }
        }
        public decimal? FrequentLocationY
        {
            get { return _frequent_loc_y; }
            set { _frequent_loc_y = value; }
        }
        public int? OnlineState
        {
            get { return _online_state; }
            set { _online_state = value; }
        }
    }
}