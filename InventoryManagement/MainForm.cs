using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class MainForm : Form
    {
        string sqlConString = @"Data Source=DESKTOP-V5CAK5I\SQLEXPRESS;Initial Catalog=InventoryManagement;Integrated Security=True";
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Show_GridView();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void InsetBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlConString);
            con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter(@"Select * from inventory;", con);
            DataTable dt = new DataTable();
            sda1.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int id = dt.Rows.Count;
                id++;
                SqlCommand sda2 = new SqlCommand(@"INSERT INTO inventory
                                       VALUES (" + id + ", '" + txtID.Text + "', '"+ txtName.Text+"', '"+txtPrice.Text+"', '"+txtQuantity.Text+"');", con);
                sda2.ExecuteNonQuery();
                Show_GridView();
            }
            con.Close();

        }

        private void Show_GridView()
        {
            SqlConnection con = new SqlConnection(sqlConString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(@"Select * from inventory;", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlConString);
            con.Open();
            SqlCommand sda2 = new SqlCommand(@"UPDATE inventory
                                SET product_name = ' "+ txtName.Text+" ', price = '"+txtPrice.Text+"', quantity = '"+txtQuantity.Text+@"'
                                WHERE product_id ='"+ txtID.Text+"';", con);
            sda2.ExecuteNonQuery();
            Show_GridView();
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtID.Text = row.Cells["product_id"].Value.ToString();
                txtName.Text = row.Cells["product_name"].Value.ToString();
                txtPrice.Text = row.Cells["price"].Value.ToString();
                txtQuantity.Text = row.Cells["quantity"].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlConString);
            con.Open();
            SqlCommand sda2 = new SqlCommand(@"DELETE FROM inventory WHERE product_id = '"+txtID.Text+"'; ", con);
            sda2.ExecuteNonQuery();
            Show_GridView();
            con.Close();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlConString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(@"Select * from inventory where product_name LIKE '%"+txtSearch.Text+"%';", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        

   
    }
}
