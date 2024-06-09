using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_SP_Winform
{
    public partial class frm_Main : Form
    {
        private SqlConnection con = new SqlConnection(Connection.ConnectSQL.GetConnectionString());
        public frm_Main()
        {
            InitializeComponent();
        }
        private void frm_Main_Load(object sender, EventArgs e)
        {
            GetEmpList();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        void Clear()
        {
            txt_EmpId.Text = txt_Age.Text = "0";
            txt_Name.Text = txt_Contact.Text = cb_City.Text = null;
            rdo_Male.Checked = true;
            dt_JoiningDate.Value = DateTime.Now;  
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                int empId = int.Parse(txt_EmpId.Text);
                string empName = txt_Name.Text, city = cb_City.Text, contact = txt_Contact.Text;
                float age = float.Parse(txt_Age.Text);
                DateTime joinDate = DateTime.Parse(dt_JoiningDate.Text);
                string sex = rdo_Male.Checked ? "Male" : "Female";

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("InsertEmp_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters and their values
                    cmd.Parameters.AddWithValue("@EmpID", empId);
                    cmd.Parameters.AddWithValue("@EmpName", empName);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@Sex", sex);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@JoiningDate", joinDate);
                    cmd.Parameters.AddWithValue("@Contact", contact);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Inserted...", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                Clear();
                GetEmpList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }


        void GetEmpList()
        {
            SqlCommand cmd = new SqlCommand("ListEmp_SP", con);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dgv_Employee.DataSource = dt;
        }

        private void pb_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                int empId = int.Parse(txt_EmpId.Text);
                string empName = txt_Name.Text, city = cb_City.Text, contact = txt_Contact.Text;
                float age = float.Parse(txt_Age.Text);
                DateTime joinDate = DateTime.Parse(dt_JoiningDate.Text);
                string sex = rdo_Male.Checked ? "Male" : "Female";

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("UpdateEmp_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters and their values
                    cmd.Parameters.AddWithValue("@EmpID", empId);
                    cmd.Parameters.AddWithValue("@EmpName", empName);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@Sex", sex);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@JoiningDate", joinDate);
                    cmd.Parameters.AddWithValue("@Contact", contact);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated...", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                Clear();
                GetEmpList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                int empId = int.Parse(txt_EmpId.Text);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (MessageBox.Show("Are you sure to Delete this Record", "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteEmp_SP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpID", empId);

                        cmd.ExecuteNonQuery();
                    }
                }
                Clear();
                GetEmpList();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dgv_Employee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_Employee.Rows[e.RowIndex];   

                txt_EmpId.Text = row.Cells[0].Value.ToString();
                txt_Name.Text = row.Cells[1].Value.ToString();
                cb_City.Text = row.Cells[2].Value.ToString();
                txt_Age.Text = row.Cells[3].Value.ToString();
                string sex = row.Cells[4].Value.ToString();
                dt_JoiningDate.Text = row.Cells[5].Value.ToString();
                txt_Contact.Text = row.Cells[6].Value.ToString();

                rdo_Male.Checked = sex == "Male";
                rdo_Female.Checked = sex == "Female";

                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnInsert.Enabled = false;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            int empId = int.Parse(txt_EmpId.Text);
            SqlCommand cmd = new SqlCommand("LoadEmp_SP '" + empId + "'", con);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dgv_Employee.DataSource = dt;
        }
    }
}
