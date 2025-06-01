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
    public partial class IssueBooks : Form
    {
        public IssueBooks()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void IssueBooks_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd = new SqlCommand("select book_name from NewBook", con);
            SqlDataReader sql_data_reader_new_book = cmd.ExecuteReader();

            while(sql_data_reader_new_book.Read())
            {
                for(int i = 0; i < sql_data_reader_new_book.FieldCount; i++)
                {
                    comboBoxBooks.Items.Add(sql_data_reader_new_book.GetString(i));
                }
            }

            sql_data_reader_new_book.Close();
            con.Close();

        }
        int count;
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if(txtEnrollment.Text != "")
            {
                string eid = txtEnrollment.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewStudent where stu_enrollment = '" + eid + "' ";
                SqlDataAdapter new_student_data_adapter = new SqlDataAdapter(cmd);
                DataSet new_student_dataset = new DataSet();
                new_student_data_adapter.Fill(new_student_dataset);

                
                //Code to count how many books has been issues on this enrollment number

                cmd.CommandText = "select count(stu_enrollment) from IRBook where stu_enrollment = '" + eid + "' and book_return_date is null ";
                SqlDataAdapter irbook_data_adapter = new SqlDataAdapter(cmd);
                DataSet irbook_dataset = new DataSet();
                irbook_data_adapter.Fill(irbook_dataset);

                count = int.Parse(irbook_dataset.Tables[0].Rows[0][0].ToString());
               

                if (new_student_dataset.Tables[0].Rows.Count != 0)
                {
                    txtName.Text = new_student_dataset.Tables[0].Rows[0][1].ToString();
                    txtDepartment.Text = new_student_dataset.Tables[0].Rows[0][3].ToString();
                    txtSemester.Text = new_student_dataset.Tables[0].Rows[0][4].ToString();
                    txtContact.Text = new_student_dataset.Tables[0].Rows[0][5].ToString();
                    txtEmail.Text = new_student_dataset.Tables[0].Rows[0][6].ToString();

                }

                else
                {
                    txtName.Clear();
                    txtDepartment.Clear();
                    txtSemester.Clear();
                    txtContact.Clear();
                    txtEmail.Clear();

                    MessageBox.Show("Invalid Enrollment No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollment.Clear();
            
        }

        private void buttonIssue_Click(object sender, EventArgs e)
        {
            if(txtName.Text != "")
            {
                if(comboBoxBooks.SelectedIndex != -1 && count <=3)
                {
                    String stu_enrollment = txtEnrollment.Text;
                    String stu_name = txtName.Text;
                    String stu_department = txtDepartment.Text;
                    String stu_semester = txtSemester.Text;
                    Int64 stu_contact = Int64.Parse(txtContact.Text);
                    String stu_email = txtEmail.Text;
                    String book_name = comboBoxBooks.Text;
                    String book_issue_date = dateTimePicker1.Text;

                    string eid = txtEnrollment.Text;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "insert into IRBook (stu_enrollment, stu_name, stu_department, stu_semester, stu_contact, stu_email, book_name, book_issue_date) values ('"+stu_enrollment+"', '"+stu_name+ "', '"+stu_department+ "', '"+stu_semester+ "', '"+stu_contact+ "', '"+stu_email+ "', '"+book_name+ "', '"+book_issue_date+"' )";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book Issued.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else
                {
                    MessageBox.Show("Select book. Or maximum number of book has been issued.", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

            else
            {
                MessageBox.Show("Enter valid enrollment number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEnrollment_TextChanged(object sender, EventArgs e)
        {
            if(txtEnrollment.Text == "")
            {
                txtName.Clear();
                txtDepartment.Clear();
                txtSemester.Clear();
                txtContact.Clear();
                txtEmail.Clear();
            }
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Maximum 3 books can be issued to 1 student!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
