using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Form1
{
    public partial class ReturnBook : Form
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from IRBook where stu_enrollment = '" + txtEnrollment.Text + "' and book_return_date IS NULL";
            SqlDataAdapter irbook_data_adapter = new SqlDataAdapter(cmd);
            DataSet irbook_dataset = new DataSet();
            irbook_data_adapter.Fill(irbook_dataset);

            if (irbook_dataset.Tables[0].Rows.Count != 0)
            {
                dataGridView1.DataSource = irbook_dataset.Tables[0];
            }
            else
            {
                MessageBox.Show("Invalid enrollment number or no book issued!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ReturnBook_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
            txtEnrollment.Clear();
        }

        String book_name;
        String book_date;
        Int64 row_id;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel3.Visible = true;

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                row_id = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                book_name = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                book_date = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }

            txtBookName.Text = book_name;
            txtIssueDate.Text = book_date;
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            con.Open();
            cmd.CommandText = "update IRBook set book_return_date = '" + dateTimePicker1.Text + "' where stu_enrollment = '" + txtEnrollment.Text + "' and id = '"+row_id+"' ";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Return successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReturnBook_Load(this, null);
        }

        private void txtEnrollment_TextChanged(object sender, EventArgs e)
        {
            if(txtEnrollment.Text == "")
            {
                panel3.Visible = false;
                dataGridView1.DataSource = null;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollment.Clear();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }
    }
}
