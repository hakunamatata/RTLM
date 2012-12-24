using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using log4net;

namespace RTLM.CCRM.BLL
{
    public class Consumer
    {
        private ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Model.Consumer GetModel(string id)
        {
            Users bll_user = new Users();
            Model.Consumer model_consumer = new Model.Consumer();
            Model.User model_user = bll_user.GetModel(id);

            if (model_user == null)
            {
                log.Error("message", new Exception(string.Format("未找到 id 为 {0} 的用户。", id)));
                return null;
            }

            DAL.Consumer dal_customer = new DAL.Consumer();
            DataTable tb_customer = dal_customer.GetDataBy(id);

            if (tb_customer.Rows.Count == 0)
            {
                log.Error("message", new Exception(string.Format("未找到 id 为 {0} 的用户。", id)));
                return null;
            }
            if (tb_customer.Rows.Count > 1)
            {
                log.Error("error", new Exception(string.Format("找到多个 id 为 {0} 的用户。", id)));
                return null;
            }



            foreach (DataRow dr in tb_customer.Rows)
            {

                model_consumer.Address = model_user.Address;
                model_consumer.Amount = model_user.Amount;
                model_consumer.Avatar = model_user.Avatar;
                model_consumer.Birthday = model_user.Birthday;
                model_consumer.Email = model_user.Email;
                model_consumer.Exp = model_user.Exp;
                model_consumer.GroupID = model_user.GroupID;
                model_consumer.ID = model_user.ID;
                model_consumer.IsLock = model_user.IsLock;
                model_consumer.Mobile = model_user.Mobile;
                model_consumer.NickName = model_user.NickName;
                model_consumer.Password = model_user.Password;
                model_consumer.Point = model_user.Point;
                model_consumer.QQ = model_user.QQ;
                model_consumer.RegIP = model_user.RegIP;
                model_consumer.RegTime = model_user.RegTime;
                model_consumer.SafeAnswer = model_user.SafeAnswer;
                model_consumer.SafeQuestion = model_user.SafeQuestion;
                model_consumer.Sex = model_user.Sex;
                model_consumer.Telphone = model_user.Telphone;
                model_consumer.UserName = model_user.UserName;

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
            return DAL.Consumer.Exist(id) == 1;
        }


        /// <summary>
        /// 创建客户
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public int CreateCustomer(Model.Consumer Consumer)
        {
            // 1: 用户已存在
            if (IsConsumerExist(Consumer.ID.ToString()) || IsConsumerExist(Consumer.Email) || IsConsumerExist(Consumer.UserName) || IsConsumerExist(Consumer.Mobile))
            {
                return 0;
            }

            DbHelper db = new DbHelper();
            try
            {
                db.BeginTransaction();
                DAL.User dal_user = new DAL.User(db);
                dal_user.Insert(
                            Consumer.ID,
                            Consumer.GroupID,
                            Consumer.UserName,
                            Consumer.Password,
                            Consumer.Email,
                            Consumer.NickName,
                            Consumer.Avatar,
                            Consumer.Sex,
                            Consumer.Birthday,
                            Consumer.Telphone,
                            Consumer.Mobile,
                            Consumer.QQ,
                            Consumer.Address,
                            Consumer.SafeQuestion,
                            Consumer.SafeAnswer,
                            Consumer.Amount,
                            Consumer.Point,
                            Consumer.Exp,
                            Consumer.IsLock ? 1 : 0,
                            Consumer.RegTime,
                            Consumer.RegIP
                           );
                DAL.Consumer dal_consumer = new DAL.Consumer(db);
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
                log.Error("error", e);
                db.RollbackTransaction();
                return -1; // 执行发生错误。
            }
            finally
            {
                db.Dispose();
            }
        }

    }
}
