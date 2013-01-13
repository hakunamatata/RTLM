using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RTLM.Ccrm.Bll
{
    public class Customer : Users
    {
        public Model.Customer GetModel(string user)
        {
            Model.Customer model_customer = new Model.Customer();
            Model.User model_user = base.GetModel(user);

            if (model_user == null)
            {
                throw new Exception(string.Format("未找到用户 {0}。", user));
            }

            Dal.Customer dal_customer = new Dal.Customer();
            DataTable tb_customer = dal_customer.GetDataBy(model_user.ID);

            if (tb_customer.Rows.Count == 0)
            {
                throw new Exception(string.Format("未找到用户 {0}。", user));
            }
            if (tb_customer.Rows.Count > 1)
            {
                throw new Exception(string.Format("找到用户 {0}。", user));
            }



            foreach (DataRow dr in tb_customer.Rows)
            {

                model_customer.Avatar = model_user.Avatar;
                model_customer.Email = model_user.Email;
                model_customer.GroupID = model_user.GroupID;
                model_customer.ID = model_user.ID;
                model_customer.Mobile = model_user.Mobile;
                model_customer.NickName = model_user.NickName;
                model_customer.Password = model_user.Password;
                model_customer.QQ = model_user.QQ;
                model_customer.SafeAnswer = model_user.SafeAnswer;
                model_customer.SafeQuestion = model_user.SafeQuestion;
                model_customer.Gender = model_user.Gender;
                model_customer.Type = model_user.Type;

                model_customer.StoreName = null_check(dr["store_name"]) ? null : dr["store_name"].ToString();
                model_customer.City = null_check(dr["city"]) ? null : (int?)Convert.ToInt32(dr["city"]);
                model_customer.FrequentArea = null_check(dr["frequent_area"]) ? null : dr["frequent_area"].ToString();
                model_customer.StoreState = null_check(dr["store_state"]) ? null : (int?)Convert.ToInt32(dr["store_state"]);
                model_customer.LastOrderDate = null_check(dr["last_order_date"]) ? null : (DateTime?)Convert.ToDateTime(dr["last_order_date"]);
                model_customer.OffWorkTime = null_check(dr["off_work_time"]) ? null : (DateTime?)Convert.ToDateTime(dr["off_work_time"]);
                model_customer.FrequentLocationX = null_check(dr["frequent_loc_x"]) ? null : (decimal?)Convert.ToDecimal(dr["frequent_loc_x"]);
                model_customer.FrequentLocationY = null_check(dr["frequent_loc_y"]) ? null : (decimal?)Convert.ToDecimal(dr["frequent_loc_y"]);

            }

            return model_customer;

        }

        private bool null_check(object dr)
        {
            return (dr == DBNull.Value || dr == null || dr.ToString() == string.Empty);
        }


        public bool IsCustomerExist(Guid id)
        {
            return Dal.Customer.Exist(id) == 1;
        }


        /// <summary>
        /// 创建客户
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        public int CreateCustomer(Model.Customer Customer)
        {
            // 1: 用户已存在
            if (IsEmialExist(Customer.Email) || IsMobileExist(Customer.Mobile))
            {
                return 0;
            }

            DbHelper db = new DbHelper();

            try
            {
                db.BeginTransaction();
                Dal.User dal_user = new Dal.User(db);
                dal_user.Insert(
                            Customer.ID,
                            Customer.Email,
                            Customer.Password,
                            Customer.NickName,
                            Customer.Mobile,
                            Customer.Gender,
                            Customer.GroupID,
                            Customer.Avatar,
                            Customer.SafeQuestion,
                            Customer.SafeAnswer,
                            Customer.QQ,
                            Customer.Type
                           );
                Dal.Customer dal_customer = new Dal.Customer(db);
                dal_customer.Insert(
                                    Customer.ID,
                                    Customer.StoreName,
                                    Customer.City,
                                    Customer.FrequentArea,
                                    Customer.StoreState,
                                    Customer.LastOrderDate,
                                    Customer.OffWorkTime,
                                    Customer.FrequentLocationX,
                                    Customer.FrequentLocationY
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

    }
}
