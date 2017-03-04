using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BusinessLayer
{
    public class CustomerDataAccessLayer
    {
       public void addCustomer(Customer customer)
        {
            string connection = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("spAddCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@customerName";
                paramName.Value = customer.customerName;
                cmd.Parameters.Add(paramName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@customerEmail";
                paramEmail.Value = customer.customerEmail;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@customerPassword";
                paramPassword.Value = customer.customerPassword;
                cmd.Parameters.Add(paramPassword);


                if(customer.customerType == "Individual")
                {
                    SqlParameter paramType = new SqlParameter();
                    paramType.ParameterName = "@customerType";
                    paramType.Value = 1;
                    cmd.Parameters.Add(paramType);
                }

                else
                {
                    SqlParameter paramType = new SqlParameter();
                    paramType.ParameterName = "@customerType";
                    paramType.Value = 2;
                    cmd.Parameters.Add(paramType);
                }
                

                SqlParameter paramAddress = new SqlParameter();
                paramAddress.ParameterName = "@customerAddress";
                paramAddress.Value = customer.customerAddress;
                cmd.Parameters.Add(paramAddress);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int authenticateCustomer(CustomerLoginClass customer)
        {
            string connection = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("spLoginCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@customerEmail";
                paramEmail.Value = customer.customerEmail;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@customerPassword";
                paramPassword.Value = customer.customerPassword;
                cmd.Parameters.Add(paramPassword);

                con.Open();
                int flag = Convert.ToInt32(cmd.ExecuteScalar());

                return flag;
            }
        }

        public Customer CustomerDetails(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("select customerName, customerEmail, customerPassword, customerType, customerAddress from tblCustomer where customerID='"+id+"'", con);
                cmd.CommandType = CommandType.Text;
                Customer cust = new Customer();

                con.Open();
                SqlDataReader rdr =  cmd.ExecuteReader();
                rdr.Read();

                cust.customerName = rdr["customerName"].ToString();
                cust.customerEmail = rdr["customerEmail"].ToString();
                cust.customerPassword = rdr["customerPassword"].ToString();
                if (Convert.ToInt32(rdr["customerType"]) == 1)
                {
                    cust.customerType = "Individual";
                }
                else
                {
                    cust.customerType = "Supplier";
                }
                cust.customerAddress = rdr["customerAddress"].ToString();

                return cust;
            }
        }

    }
}
