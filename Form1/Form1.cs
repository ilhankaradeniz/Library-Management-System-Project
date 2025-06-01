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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    
     

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textUsername_MouseClick(object sender, MouseEventArgs e)
        {
            if (textUsername.Text == "Username")
                textUsername.Clear();
        }

        private void textPassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (textPassword.Text == "Password")
            { 
                textPassword.Clear();
                textPassword.PasswordChar ='*';
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = 225A4\\WOLVOX ; database = libraryManagementSystem ; integrated security =  True";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from loginTable where username = '" + textUsername.Text + "' and pass = '" + textPassword.Text + "' ";
            SqlDataAdapter login_table_data_adapter = new SqlDataAdapter(cmd);
            DataSet login_table_dataset = new DataSet();
            login_table_data_adapter.Fill(login_table_dataset);

            if(login_table_dataset.Tables[0].Rows.Count != 0)  //girdigim veriler olusturulan datasetin birinci tablosunda degerlere sahipse bloga gir
            {
                this.Hide();
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
            else //degilse hata mesaji ver, bu boyle bir kullanicinin olmadigi anlamina gelmektedir
            {
                MessageBox.Show("Wrong Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
