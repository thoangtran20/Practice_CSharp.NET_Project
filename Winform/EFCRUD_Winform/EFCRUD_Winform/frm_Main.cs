using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFCRUD_Winform
{
    public partial class frm_Main : Form
    {
        Customer model = new Customer();
        public frm_Main()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to Delete this Record", "Message", 
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (CustomerDBEntities db = new CustomerDBEntities())
                {
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Detached)
                    {
                        db.Customers.Attach(model);
                        db.Customers.Remove(model);
                        db.SaveChanges();
                        LoadData();
                        Clear();
                        MessageBox.Show("Delete Successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            model.FirstName = txtFirstName.Text.Trim();
            model.LastName = txtLastName.Text.Trim();
            model.City = txtCity.Text.Trim();
            model.Address = txtAddress.Text.Trim();
            using (CustomerDBEntities db = new CustomerDBEntities())
            {
                if (model.CustomerID == 0) //insert
                    db.Customers.Add(model);
                else
                    db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            Clear();
            LoadData();
            MessageBox.Show("Submitted Successfully!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void Clear()
        {
            txtFirstName.Text = txtLastName.Text = txtCity.Text 
                = txtAddress.Text = null;
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            model.CustomerID = 0;
        }

        void LoadData()
        {
            dgrvCustomer.AutoGenerateColumns = false;
            using (CustomerDBEntities db = new CustomerDBEntities())
            {
                dgrvCustomer.DataSource = db.Customers.ToList<Customer>();
            }
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            Clear();
            this.ActiveControl = txtFirstName;
            LoadData();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgrvCustomer_DoubleClick(object sender, EventArgs e)
        {
            if (dgrvCustomer.CurrentRow.Index != -1)
            {
                model.CustomerID = Convert.ToInt32(dgrvCustomer.CurrentRow.Cells["CustomerID"].Value);
                using (CustomerDBEntities db = new CustomerDBEntities())
                {
                    model = db.Customers.Where(x => x.CustomerID == model.CustomerID).FirstOrDefault();
                    txtFirstName.Text = model.FirstName;
                    txtLastName.Text = model.LastName;
                    txtCity.Text = model.City;
                    txtAddress.Text = model.Address;
                }
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }
    }
}
