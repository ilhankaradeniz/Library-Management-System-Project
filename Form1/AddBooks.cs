using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;  //veritabani islemleri icin gerekli olan kutuphane


namespace Form1
{
    public partial class AddBooks : Form
    {
        public AddBooks()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();   //sadece acilan menuyu kapatir
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBookName.Text != "" && textAuthorName.Text != "" && textPublication.Text != "" && textBookPrice.Text != "" && textQuantity.Text != "")
            {
                String book_name = textBookName.Text;       //textbox a girilen degeri degiskene ata
                String book_author_name = textAuthorName.Text;
                String book_publication = textPublication.Text;
                String book_publish_date = dateTimePicker1.Text;
                Int64 book_price = Int64.Parse(textBookPrice.Text);
                Int64 book_quantity = Int64.Parse(textQuantity.Text);

                SqlConnection con = new SqlConnection(); //yeni bir baglanti nesnesi olusturdum
                con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                SqlCommand cmd = new SqlCommand(); //query yapacagim nesneyi olusturdum
                cmd.Connection = con;   //cmd nesnesini veritabanina bagladim

                con.Open();
                cmd.CommandText = "insert into NewBook (book_name, book_author_name, book_publication, book_publish_date, book_price, book_quantity) values ('" + book_name + "', '" + book_author_name + "', '" + book_publication + "', '" + book_publish_date + "', '" + book_price + "', '" + book_quantity + "')";
                cmd.ExecuteNonQuery(); //insert komutunu calistirdim, sorgu sonucu dondurmeyecegi icin bu methodla cagiridm
                con.Close();

                MessageBox.Show("Data Saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBookName.Clear();
                textAuthorName.Clear();
                textPublication.Clear();
                textBookPrice.Clear();
                textQuantity.Clear();
            }
            else
            {
                MessageBox.Show("Empty Field Not Allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will delete your unsaved data.", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {           //cikan uyari mesajini onaylarsak clear methodu ile textboxlar sifirlanicak
                textBookName.Clear();
                textAuthorName.Clear();
                textPublication.Clear();
                textBookPrice.Clear();
                textQuantity.Clear();
            }
        }
    }
}
