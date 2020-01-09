using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample.sampleClasses
{
    
    class contactClass
    {
        //Getter Setter Properties
        //Acts as Data Carrier in Our App

        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

       readonly static string myconnstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

        //selecting Data from Database

        public DataTable Select()
        {
            //step 1: DB connection
            SqlConnection conn = new SqlConnection(myconnstr);
            DataTable dt = new DataTable();
            try
            {
                //step 2: writing SQL query
                string sql = "SELECT * FROM tbl_contact";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //inserting Data into DB
        public bool Insert(contactClass c)
        {
            /* //creating default return type
             bool isSuccess = false;

             //step 1: connect DB
             SqlConnection conn = new SqlConnection(myconnstr);
             try
             {
                 //step 2: create a SQL query
                 string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 //create peremeters to add data
                 cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                 cmd.Parameters.AddWithValue("@LastName", c.LastName);
                 cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                 cmd.Parameters.AddWithValue("@Address", c.Address);
                 cmd.Parameters.AddWithValue("@Gender", c.Gender);

                 //connection Open here
                 conn.Open();
                 int rows = cmd.ExecuteNonQuery();
                 if (rows > 0)
                 {
                     isSuccess = true;
                 }
                 else
                 {
                     isSuccess = false;
                 }
             }
             catch (Exception ex)
             {

             }
             finally
             {
                 conn.Close();
             }



             return isSuccess;*/
            bool isSuccess = false;

            //STep 1: Connect DAtabase
            SqlConnection conn = new SqlConnection(myconnstr);

            try
            {
                //STep 2: Create a SQL Query to insert DAta
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //Creating SQL Command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create Parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                
                //Connection Open Here
                conn.Open();
                if ((string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName) &&
                    string.IsNullOrEmpty(ContactNo) && string.IsNullOrEmpty(Address) && string.IsNullOrEmpty(Gender)))
                {
                    conn.Close();
                }
               
                int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if (rows > 0)
                  
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                    
                }
            }
           
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //method do update Data in DB
        public bool Update(contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstr);
            try
            {
                //step 2: create a SQL query
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create peremeters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);
                //connection Open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        //method do delete from DB
        public bool Delete(contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstr);
            try
            {
                //step 2: create a SQL query
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create peremeters to add value
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);
                //connection Open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
    }
}
