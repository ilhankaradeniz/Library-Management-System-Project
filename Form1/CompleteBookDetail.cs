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
    public partial class CompleteBookDetail : Form
    {
        public CompleteBookDetail()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void CompleteBookDetail_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from IRBook where book_return_date is null";
            SqlDataAdapter irbook_data_adapter = new SqlDataAdapter(cmd);
            DataSet irbook_dataset = new DataSet();
            irbook_data_adapter.Fill(irbook_dataset);
            dataGridView1.DataSource = irbook_dataset.Tables[0];

            cmd.CommandText = "select * from IRBook where book_return_date is not null";
            SqlDataAdapter irbook_data_adapter_return_panel = new SqlDataAdapter(cmd);
            DataSet irbook_dataset_return_book = new DataSet();
            irbook_data_adapter_return_panel.Fill(irbook_dataset_return_book);
            dataGridView2.DataSource = irbook_dataset_return_book.Tables[0];
        }
    }
}
