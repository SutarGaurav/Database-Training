using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Database.May092022
{    
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public ProductDAL()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);

        }
    }
}
