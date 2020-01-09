using Sample.sampleClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class Sample : Form
    {
        contactClass c = new contactClass();
        public Sample()
        {
            InitializeComponent();
        }
        
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            //Get the value from the input fields
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            //Inserting Data into DAtabase uing the method we created in previous episode
            bool success = c.Insert(c);
            if (success == true)
            {
                //Successfully Inserted
                MessageBox.Show("New Contact Successfully Inserted");
                Clear();
                //Call the Clear Method Here
                //Clear();
            }
            else
            {
                //FAiled to Add Contact
                MessageBox.Show("Failed to add New Contact. Try Again.");
            }
            //load data on grid
            DataTable dt = c.Select();
            dgv.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Sample_Load(object sender, EventArgs e)
        {
            //load data on grid
            DataTable dt = c.Select();
            dgv.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void Clear()
        {
            txtBoxAddress.Text = "";
            txtBoxContactId.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxFirstName.Text = "";
            txtBoxLastName.Text = "";
            txtBoxSearch.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the Data from textboxes
            c.ContactID = int.Parse(txtBoxContactId.Text);
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;
            //Update DAta in Database
            bool success = c.Update(c);
            if (success == true)
            {
                //Updated Successfully
                MessageBox.Show("Contact has been successfully Updated.");
                //Load Data on Data GRidview
                DataTable dt = c.Select();
                dgv.DataSource = dt;
                //Call Clear Method
                Clear();
            }
            else
            {
                //Failed to Update
                MessageBox.Show("Failed to Update Contact.Try Again.");
            }
        }

        private void dgv_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtBoxContactId.Text = dgv.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxFirstName.Text = dgv.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxLastName.Text = dgv.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgv.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgv.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgv.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the Contact ID fromt eh Application
            c.ContactID = Convert.ToInt32(txtBoxContactId.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                //Successfully Deleted
                MessageBox.Show("Contact successfully deleted.");
                //Refresh Data GridView
                //Load Data on Data GRidview
                DataTable dt = c.Select();
                dgv.DataSource = dt;
                //CAll the Clear Method Here
                Clear();
            }
            else
            {
                //FAiled to dElte
                MessageBox.Show("Failed to Delete Dontact. Try Again.");
            }
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgv.DataSource = dt;
        }
    }
}
