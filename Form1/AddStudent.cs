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
    public partial class AddStudent : Form
    {
        public AddStudent()
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
                txtEnrollment.Clear();
                txtDepartment.Clear();
                txtSemester.Clear();
                txtContact.Clear();
                //txtEmail.Clear();

                txtEmail.Text = "";
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEnrollment.Text != "" && txtDepartment.Text != "" && txtSemester.Text != "" && txtContact.Text != "" && txtEmail.Text != "")
            { 
                String stu_name = txtName.Text;
                String stu_enrollment = txtEnrollment.Text;
                String stu_department = txtDepartment.Text;
                String stu_semester = txtSemester.Text;
                Int64 stu_contact = Int64.Parse(txtContact.Text);
                String stu_email = txtEmail.Text;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source  = 225A4\\WOLVOX; database = libraryManagementSystem; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                con.Open();
                cmd.CommandText = "insert into NewStudent(stu_name, stu_enrollment, stu_department, stu_semester, stu_contact, stu_email) values ('" + stu_name + "',  '" + stu_enrollment + "',  '" + stu_department + "',  '" + stu_semester + "',  '" + stu_contact + "',  '" + stu_email + "') ";
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data Saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Clear();
                txtEnrollment.Clear();
                txtDepartment.Clear();
                txtSemester.Clear();
                txtContact.Clear();
                txtEmail.Clear();

            }

            else
            {
                MessageBox.Show("Empty Field Not Allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
