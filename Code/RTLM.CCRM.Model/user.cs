using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTLM.CCRM.Model
{

    public class User
    {

        private int _id;
        private int _group_id;
        private string _user_name;
        private string _password;
        private string _email;
        private string _nick_name;
        private string _avatar;
        private string _sex;
        private DateTime? _birthday;
        private string _telphone;
        private string _mobile;
        private string _qq;
        private string _address;
        private string _safe_question;
        private string _safe_answer;
        private decimal? _amount;
        private int? _point;
        private int? _exp;
        private bool _is_lock;
        private DateTime? _reg_time;
        private string _reg_ip;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public int GroupID
        {
            get { return _group_id; }
            set { _group_id = value; }
        }
        public string UserName
        {
            get { return _user_name; }
            set { _user_name = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string NickName
        {
            get { return _nick_name; }
            set { _nick_name = value; }
        }
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        public DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        public string Telphone
        {
            get { return _telphone; }
            set { _telphone = value; }
        }
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        public string QQ
        {
            get { return _qq; }
            set { _qq = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string SafeQuestion
        {
            get { return _safe_question; }
            set { _safe_question = value; }
        }
        public string SafeAnswer
        {
            get { return _safe_answer; }
            set { _safe_answer = value; }
        }
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public int? Point
        {
            get { return _point; }
            set { _point = value; }
        }
        public int? Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }
        public bool IsLock
        {
            get { return _is_lock; }
            set { _is_lock = value; }
        }
        public DateTime? RegTime
        {
            get { return _reg_time; }
            set { _reg_time = value; }
        }
        public string RegIP
        {
            get { return _reg_ip; }
            set { _reg_ip = value; }
        }
    }

}
