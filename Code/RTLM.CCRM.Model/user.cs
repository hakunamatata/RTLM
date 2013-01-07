using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTLM.CCRM.Model
{

    public class User
    {

        private Guid _uid;
        private string _email;
        private string _password;
        private string _nick_name;
        private string _mobile;
        private int _gender;
        private Guid _group_id;
        private string _avatar;
        private string _safe_question;
        private string _safe_answer;
        private string _qq;
        private int _user_type;

        public Guid ID
        {
            get { return _uid; }
            set { _uid = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string NickName
        {
            get { return _nick_name; }
            set { _nick_name = value; }
        }
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        public Guid GroupID
        {
            get { return _group_id; }
            set { _group_id = value; }
        }
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
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
        public string QQ
        {
            get { return _qq; }
            set { _qq = value; }
        }
        public int UserType
        {
            get { return _user_type; }
            set { _user_type = value; }
        }
    }
}
