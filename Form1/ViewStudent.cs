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
    public partial class ViewStudent : Form
    {
        public ViewStudent()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will delete your unsaved data.", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                txtName.Clear();
                txtNo.Clear();
                txtDepartment.Clear();
                txtSemester.Clear();
                txtContact.Clear();
                txtEmail.Clear();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollment.Clear();
            txtName.Clear();
            txtEnrollment.Clear();
            txtDepartment.Clear();
            txtSemester.Clear();
            txtContact.Clear();
            txtEmail.Clear();
        }

        private void ViewStudent_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from NewStudent";
            SqlDataAdapter new_student_data_adapter = new SqlDataAdapter(cmd);
            DataSet new_student_data_set = new DataSet();
            new_student_data_adapter.Fill(new_student_data_set);

            ViewStudentGrid.DataSource = new_student_data_set.Tables[0];
        }

        private void txtEnrollment_TextChanged(object sender, EventArgs e)
        {
            if(txtEnrollment.Text != "")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewStudent where stu_enrollment LIKE '"+txtEnrollment.Text+"%' ";
                SqlDataAdapter new_student_data_adapter = new SqlDataAdapter(cmd);
                DataSet new_student_dataset = new DataSet();
                new_student_data_adapter.Fill(new_student_dataset);

                ViewStudentGrid.DataSource = new_student_dataset.Tables[0];
            }

            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewStudent";
                SqlDataAdapter new_student_data_adapter = new SqlDataAdapter(cmd);
                DataSet new_student_dataset = new DataSet();
                new_student_data_adapter.Fill(new_student_dataset);

                ViewStudentGrid.DataSource = new_student_dataset.Tables[0];
            }
        }

        int stu_id;
        Int64 row_id;
        private void ViewStudentGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ViewStudentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                stu_id = int.Parse(ViewStudentGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                //MessageBox.Show(ViewBookGrid.Rows[e.RowIndex].Cells[0].Value.ToString()); //gezdigimiz hücreleri ekrana gösterir info box olarak
            }

            panel2.Visible = true;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from NewStudent where stu_id = '" + stu_id + "'";
            SqlDataAdapter new_student_data_adapter = new SqlDataAdapter(cmd);
            DataSet new_student_dataset = new DataSet();
            new_student_data_adapter.Fill(new_student_dataset);

            row_id = Int64.Parse(new_student_dataset.Tables[0].Rows[0][0].ToString());

            txtName.Text = new_student_dataset.Tables[0].Rows[0][1].ToString();
            txtNo.Text = new_student_dataset.Tables[0].Rows[0][2].ToString();
            txtDepartment.Text = new_student_dataset.Tables[0].Rows[0][3].ToString();
            txtSemester.Text = new_student_dataset.Tables[0].Rows[0][4].ToString();
            txtContact.Text = new_student_dataset.Tables[0].Rows[0][5].ToString();
            txtEmail.Text = new_student_dataset.Tables[0].Rows[0][6].ToString();

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Data will be updated. Confirm", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                String stu_name = txtName.Text;
                String stu_enrollment = txtNo.Text;
                String stu_department = txtDepartment.Text;
                String stu_semester = txtSemester.Text;
                Int64 stu_contact = Int64.Parse(txtContact.Text);
                String stu_email = txtEmail.Text;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "update NewStudent set stu_name = '" + stu_name + "', stu_enrollment = '" + stu_enrollment + "', stu_department = '" + stu_department + "', stu_semester = '" + stu_semester + "', stu_contact = '" + stu_contact + "', stu_email = '" + stu_email + "' where stu_id = '"+row_id+"' ";
                SqlDataAdapter new_student_data_adapter = new SqlDataAdapter(cmd);
                DataSet new_student_dataset = new DataSet();
                new_student_data_adapter.Fill(new_student_dataset);
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

                cmd.CommandText = "delete NewStudent where stu_id = '" + row_id + "' ";
                SqlDataAdapter new_student_data_adapter = new SqlDataAdapter(cmd);
                DataSet new_student_dataset = new DataSet();
                new_student_data_adapter.Fill(new_student_dataset);
            }
        }
    }
}
