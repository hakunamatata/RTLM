using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace RTLM.CCRM.Model
{
    public class Behavior
    {

        private int _bhvr_id;
        private int _csmr_id;
        private string _csmr_name;
        private string _csmr_cellphone;
        private string _csmr_destination;
        private decimal _csmr_loc_x;
        private decimal _csmr_loc_y;
        private int _cstmr_id;
        private DateTime _bhvr_date;
        private string _goods_name;
        private decimal? _csm_amount;
        private decimal? _tip_amount;
        private int? _related_salesman;
        private int _bhvr_state;
        private int? _is_failed;
        private string _failed_reason;

        public int ID
        {
            get { return _bhvr_id; }
            set { _bhvr_id = value; }
        }
        public int ConsumerID
        {
            get { return _csmr_id; }
            set { _csmr_id = value; }
        }
        public string ConsumerName
        {
            get { return _csmr_name; }
            set { _csmr_name = value; }
        }
        public string ConsumerCellphone
        {
            get { return _csmr_cellphone; }
            set { _csmr_cellphone = value; }
        }
        public string ConsumerDestination
        {
            get { return _csmr_destination; }
            set { _csmr_destination = value; }
        }
        public decimal ConsumerLocationX
        {
            get { return _csmr_loc_x; }
            set { _csmr_loc_x = value; }
        }
        public decimal ConsumerLocationY
        {
            get { return _csmr_loc_y; }
            set { _csmr_loc_y = value; }
        }
        public int CostomerID
        {
            get { return _cstmr_id; }
            set { _cstmr_id = value; }
        }
        public DateTime Date
        {
            get { return _bhvr_date; }
            set { _bhvr_date = value; }
        }
        public string GoodsName
        {
            get { return _goods_name; }
            set { _goods_name = value; }
        }
        public decimal? ConsumeAmount
        {
            get { return _csm_amount; }
            set { _csm_amount = value; }
        }
        public decimal? TipsAmount
        {
            get { return _tip_amount; }
            set { _tip_amount = value; }
        }
        public int? RelatedSalesman
        {
            get { return _related_salesman; }
            set { _related_salesman = value; }
        }
        public int State
        {
            get { return _bhvr_state; }
            set { _bhvr_state = value; }
        }
        public int? IsFailed
        {
            get { return _is_failed; }
            set { _is_failed = value; }
        }
        public string FailedReason
        {
            get { return _failed_reason; }
            set { _failed_reason = value; }
        }
    }
}