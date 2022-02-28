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

namespace CourseRegistrationSystem
{
    public partial class adminlogin : Form
    {
        home homeObj;
        public adminlogin(home o)
        {
            InitializeComponent();
            homeObj = o;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Equals("") || textBox2.Text.Trim().Equals(""))
            {
                MessageBox.Show("Login Fields Cannot Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
            }

            string CONNECTION_URL = "Data Source=ASHOK-LAPTOP\\SQLEXPRESS;Initial Catalog=CRS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(CONNECTION_URL);
            SqlCommand cmd = null;
            string sql = "SELECT * FROM ADMIN WHERE USERNAME=" + "\'" + textBox1.Text + "\' and " + " PASSWORD=" + "\'" + textBox2.Text + "\'";
            SqlDataReader sdr = null;

            try
            {
                connection.Open();
                cmd = new SqlCommand(sql, connection);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    new admin(homeObj).Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Invalid Credentials", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("DB Connection Error", "Error");
            }
            finally
            {
                sdr.Close();
                cmd.Dispose();
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            homeObj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Really Want to Close The Application", "Close Application", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                homeObj.Close();
            }
        }
    }
}
