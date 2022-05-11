using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            string constr = ConfigurationManager.ConnectionStrings["DefalutConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public DataSet GetEmps()
        {
            da = new SqlDataAdapter("select * from EmployeeTable", con);
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
                MessageBox.Show("Record Saved");
        }
    }
}
