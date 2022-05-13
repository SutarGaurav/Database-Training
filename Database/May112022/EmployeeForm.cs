using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
//Concept of DAL with disconnected architecture
namespace Database.May112022
{
    public partial class EmployeeForm : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public EmployeeForm()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public void ClearAll()
        {
            txtID.Clear();
            txtName.Clear();
            txtDesignation.Clear();
            txtSalary.Clear();
        }
        public DataSet GetEmps()
        {
            da = new SqlDataAdapter("select * from EmployeeTableAA", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "emp");
            return ds;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ds = GetEmps();
            DataRow row = ds.Tables["emp"].NewRow();
            row["Name"] = txtName.Text;
            row["Designation"] = txtDesignation.Text;
            row["Salary"] = txtSalary.Text;
            ds.Tables["emp"].Rows.Add(row);
            int res = da.Update(ds.Tables["emp"]);
            if (res == 1)
            { 
                MessageBox.Show("Record Saved");
                ClearAll();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int sal = Convert.ToInt32(txtSalary.Text);
            if (string.IsNullOrEmpty(txtName.Text) && sal > 0)
            {
                MessageBox.Show("Name can't be empty and Salary should be above 0");
            }
            else
            {
                ds = GetEmps();
                DataRow row = ds.Tables["emp"].Rows.Find(Convert.ToInt32(txtID.Text));
                if (row != null)
                {
                    row["Name"] = txtName.Text;
                    row["Designation"] = txtDesignation.Text;
                    row["Salary"] = txtSalary.Text;
                    int res = da.Update(ds.Tables["emp"]);
                    if (res == 1)
                    {
                        MessageBox.Show("Record Saved");
                        ClearAll();
                    }
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds = GetEmps();
            DataRow row = ds.Tables["emp"].Rows.Find(Convert.ToInt32(txtID.Text));
            if (row != null)
            {
                row.Delete();
                int res = da.Update(ds.Tables["emp"]);
                if (res == 1)
                    MessageBox.Show("Record Deleted");
                else
                    MessageBox.Show("Cannot Delete");
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ds = GetEmps();
            DataRow row = ds.Tables["emp"].Rows.Find(Convert.ToInt32(txtID.Text));
            if (row != null)
            {
                txtName.Text = row["Name"].ToString();
                txtDesignation.Text = row["Designation"].ToString();
                txtSalary.Text = row["Salary"].ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }
    }
}
