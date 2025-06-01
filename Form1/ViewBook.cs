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
    public partial class ViewBook : Form
    {
        public ViewBook()
        {
            InitializeComponent();
        }

        private void ViewBook_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from NewBook";
            SqlDataAdapter new_book_data_adapter = new SqlDataAdapter(cmd); //recordlari cektim
            DataSet new_book_dataset = new DataSet(); //recordlari atmak icin bir dataseti olusturdum
            new_book_data_adapter.Fill(new_book_dataset); //recordalari datasetine doldurudm

            ViewBookGrid.DataSource = new_book_dataset.Tables[0]; //veri paneline olusturdugum datasetindeki ilk tabloyu yani NewBook tablosundan gelen recordlari attim

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        int book_id;
        Int64 row_id;
        private void ViewBookGrid_CellClick(object sender, DataGridViewCellEventArgs e) // e argumani olay argumanidir, yaptigimiz eylemin nerede oldugunu tutmamizi saglar
        {   
            if (ViewBookGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null) //e argumanindan gelen index degerleri bos data degilse bloga girer
            {
                book_id = int.Parse(ViewBookGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                //MessageBox.Show(ViewBookGrid.Rows[e.RowIndex].Cells[0].Value.ToString()); //gezdigimiz hücreleri ekrana gösterir info box olarak
            }

            panel2.Visible = true;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from NewBook where book_id = '"+book_id+"'";
            SqlDataAdapter new_book_data_adapter = new SqlDataAdapter(cmd);
            DataSet new_book_dataset = new DataSet();
            new_book_data_adapter.Fill(new_book_dataset);

            row_id = Int64.Parse(new_book_dataset.Tables[0].Rows[0][0].ToString()); //book id yi row id olarak tutmamizi saglar update ve delete islemlerinde kullanicaz

            txtName.Text = new_book_dataset.Tables[0].Rows[0][1].ToString(); //databaseden cekilen veriyi txtname boxina yazdirir 0nci satirin 2nci hucresi
            txtAuthorName.Text = new_book_dataset.Tables[0].Rows[0][2].ToString();
            txtPublication.Text = new_book_dataset.Tables[0].Rows[0][3].ToString();
            txtPDate.Text = new_book_dataset.Tables[0].Rows[0][4].ToString();
            txtPrice.Text = new_book_dataset.Tables[0].Rows[0][5].ToString();
            txtQuantity.Text = new_book_dataset.Tables[0].Rows[0][6].ToString();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will delete your unsaved data.", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                txtName.Clear();
                txtAuthorName.Clear();
                txtPublication.Clear();
                txtPDate.Clear();
                txtPrice.Clear();
                txtQuantity.Clear();
            }
            
        }

        private void txtBokName_TextChanged(object sender, EventArgs e)
        {
            if(txtBookName.Text != "")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewBook where book_name LIKE '"+txtBookName.Text+"%' ";
                SqlDataAdapter new_book_data_adapter = new SqlDataAdapter(cmd);
                DataSet new_book_dataset = new DataSet();
                new_book_data_adapter.Fill(new_book_dataset);

                ViewBookGrid.DataSource = new_book_dataset.Tables[0];
            }

            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewBook";
                SqlDataAdapter new_book_data_adapter = new SqlDataAdapter(cmd);
                DataSet new_book_dataset = new DataSet();
                new_book_data_adapter.Fill(new_book_dataset);

                ViewBookGrid.DataSource = new_book_dataset.Tables[0];
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            txtBookName.Clear();
            txtName.Clear();
            txtAuthorName.Clear();
            txtPublication.Clear();
            txtPDate.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be updated. Confirm", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
            { 
                String book_name = txtName.Text;
                String book_author_name = txtAuthorName.Text;
                String book_publication = txtPublication.Text;
                String book_publish_date = txtPDate.Text;
                Int64 book_price = Int64.Parse(txtPrice.Text);
                Int64 book_quantity = Int64.Parse(txtQuantity.Text);

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "update NewBook set book_name = '"+book_name+ "', book_author_name = '"+book_author_name+ "', book_publication = '"+book_publication+ "', book_publish_date = '"+book_publish_date+ "', book_price = '"+book_price+ "', book_quantity = '"+book_quantity+"' where book_id = '"+row_id+"' ";
                SqlDataAdapter new_book_data_adapter = new SqlDataAdapter(cmd);
                DataSet new_book_dataset = new DataSet();
                new_book_data_adapter.Fill(new_book_dataset);
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be deleted. Confirm", "Confirmation Dialog", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) ;
            {
                
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "delete NewBook where book_id = '" + row_id + "' ";
                SqlDataAdapter new_book_data_adapter = new SqlDataAdapter(cmd);
                DataSet new_book_dataset = new DataSet();
                new_book_data_adapter.Fill(new_book_dataset);
            }
        }
    }
}
