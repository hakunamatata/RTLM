using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RTLM;

namespace RTLM.Ccrm.Bll
{
    public class Consumer : Users
    {
        public Model.Consumer GetModel(string user)
        {
            
            Model.Consumer model_consumer = new Model.Consumer();
            Model.User model_user = base.GetModel(user);

            if (model_user == null)
            {
                throw new Exception(string.Format("未找到用户 {0}。", user));
            }

            Dal.Consumer dal_customer = new Dal.Consumer();
            DataTable tb_customer = dal_customer.GetDataBy(model_user.ID);

            if (tb_customer.Rows.Count == 0)
            {
                throw new Exception(string.Format("未找到用户 {0}", user));
            }
            if (tb_customer.Rows.Count > 1)
            {
                throw new Exception(string.Format("找到的用户 {0}。", user));
            }



            foreach (DataRow dr in tb_customer.Rows)
            {

                model_consumer.Avatar = model_user.Avatar;
                model_consumer.Email = model_user.Email;
                model_consumer.GroupID = model_user.GroupID;
                model_consumer.ID = model_user.ID;
                model_consumer.Mobile = model_user.Mobile;
                model_consumer.NickName = model_user.NickName;
                model_consumer.Password = model_user.Password;
                model_consumer.QQ = model_user.QQ;
                model_consumer.SafeAnswer = model_user.SafeAnswer;
                model_consumer.SafeQuestion = model_user.SafeQuestion;
                model_consumer.Gender = model_user.Gender;
                model_consumer.Type = model_user.Type;

                model_consumer.RealName = null_check(dr["real_name"]) ? null : dr["real_name"].ToString();
                model_consumer.City = null_check(dr["city"]) ? null : (int?)Convert.ToInt32(dr["city"]);
                model_consumer.FrequentArea = null_check(dr["frequent_area"]) ? null : dr["frequent_area"].ToString();
                model_consumer.State = null_check(dr["personal_state"]) ? null : (int?)Convert.ToInt32(dr["personal_state"]);
                model_consumer.LastOrderDate = null_check(dr["last_order_date"]) ? null : (DateTime?)Convert.ToDateTime(dr["last_order_date"]);
                model_consumer.FirstOrderDate = null_check(dr["first_order_date"]) ? null : (DateTime?)Convert.ToDateTime(dr["first_order_date"]);
            }

            return model_consumer;


        }

        private bool null_check(object dr)
        {
            return (dr == DBNull.Value || dr == null || dr.ToString() == string.Empty);
        }



        public bool IsConsumerExist(string id)
        {
            return Dal.Consumer.Exist(id) == 1;
        }


        /// <summary>
        /// 创建消费者
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public int CreateConsumer(Model.Consumer Consumer)
        {
            // 1: 用户已存在
            //BLL.Users bll_user = new 
            if (IsEmialExist(Consumer.Email) || IsMobileExist(Consumer.Mobile))
            {
                return 0;
            }

            DbHelper db = new DbHelper();
            try
            {
                db.BeginTransaction();
                Dal.User dal_user = new Dal.User(db);
                dal_user.Insert(
                            Consumer.ID,
                            Consumer.Email,
                            Consumer.Password,
                            Consumer.NickName,
                            Consumer.Mobile,
                            Consumer.Gender,
                            Consumer.GroupID,
                            Consumer.Avatar,
                            Consumer.SafeQuestion,
                            Consumer.SafeAnswer,
                            Consumer.QQ,
                            Consumer.Type
                           );
                Dal.Consumer dal_consumer = new Dal.Consumer(db);
                dal_consumer.Insert(
                                    Consumer.ID,
                                    Consumer.RealName,
                                    Consumer.City,
                                    Consumer.FirstOrderDate,
                                    Consumer.FrequentArea,
                                    Consumer.State,
                                    Consumer.LastOrderDate
                                   );

                db.CommitTransaction();
                return 1; // 执行成功。
            }
            catch (Exception e)
            {
                
                db.RollbackTransaction();
                throw e;
                //return -1; // 执行发生错误。
            }
            finally
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns> 1：登录成功  0：登录失败</returns>
        public int ConsumerLogin(string user, string password)
        {
            Consumer bll_consumer = new Consumer();
            Model.Consumer model_consumer = new Model.Consumer();
            if (bll_consumer.IsEmialExist(user) || bll_consumer.IsMobileExist(user))
            {
                model_consumer = bll_consumer.GetModel(user);
                if (Utility.Encrypt(password) == model_consumer.Password) return 1;
            }

            return 0;
        }

    }
}
