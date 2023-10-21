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

namespace sms_desktop_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = 
            new SqlConnection("Data Source = DESKTOP-2R4RB09\\SQLEXPRESS; Initial Catalog = sms; User = sa; Password = sqlDb11");
        SqlCommand cmd;
        SqlDataReader read;
        string id;
        bool Mode = true;
        string sql;

        // If the Mode is true means add record to db else update record
        private void btn_save_Click(object sender, EventArgs e)
        {
            string name = txt1.Text;
            string course = txt2.Text;
            string fees = txt3.Text;

            if(Mode == true)
            {
                // sql query command
                sql = "INSERT INTO student(student_name,course,fee) VALUES(@student_name, @course, @fee)";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@student_name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fees);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added Successfully!");

                // Clear text boxes
                txt1.Clear();
                txt2.Clear();
                txt3.Clear();

                // Set txt1 focus
                txt1.Focus();
            }
            else
            {

            }

        }
    }
}
