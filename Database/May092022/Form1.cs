using System;
using System.Windows.Forms;
using System.Data.SqlClient;  //add namespace
using System.Data;

namespace Database.May092022
{
    public partial class ProductForm : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            con = new SqlConnection(@"Server = LAPTOP-C6NB9IB2\SQLEXPRESS; database = TQTraining; Integrated Security = True"); ;
        }
        public void ClearAll()
        {
            txtProductId.Clear();
            txtName.Clear();
            txtPrice.Clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into ProductTable values (@id,@name,@price)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductId.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record Inserted");
                    txtProductId.Enabled = true;
                    ClearAll();
                }
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from ProductTable where Id= @id"; //ha sql table madhala Id aahe.
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", int.Parse(txtProductId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        txtName.Text = dr["Name"].ToString();
                        txtPrice.Text = dr["Price"].ToString();
                    }
                }
                else
                    MessageBox.Show("Record not found");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update ProductTable set Name = @name, Price = @price where Id = @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductId.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                    MessageBox.Show("Record Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                string qry = "delete from ProductTable where Id = @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductId.Text));
                /*cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));*/ //fakt ekach parameter aahe .Id delete karaycahy
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                    MessageBox.Show("Record Deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select max(Id) from ProductTable";
                cmd = new SqlCommand(qry, con);
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj == DBNull.Value)
                    txtProductId.Text = "1";
                else
                {
                    int id = Convert.ToInt32(obj);
                    id++;
                    txtProductId.Text = id.ToString();
                }
                txtProductId.Enabled = false;  //Manual change restricted.
                txtName.Clear();
                txtPrice.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        private void btnShowAllProducts_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from ProductTable";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(dr);                    
                    dataGridView1.DataSource = table;
                }
                else
                    MessageBox.Show("No record to display");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e) //row var click kela tar to data show zala pahije.
        {
            txtProductId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void Product_Load(object sender, EventArgs e)
        {

        }
    }
}
