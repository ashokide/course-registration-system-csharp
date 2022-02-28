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
    public partial class courseregister : Form
    {
        login loginObj;
        string username;
        public courseregister(login o, string user)
        {
            InitializeComponent();
            loginObj = o;
            username = user;
            label1.Text = "Hello, " + username.ToUpper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logged Out Successfully","Thank You",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();
            loginObj.Show();
        }

        private void courseregister_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cRSDataSet1.new_courses' table. You can move, or remove it, as needed.
            this.new_coursesTableAdapter.Fill(this.cRSDataSet1.new_courses);
            this.coursesTableAdapter1.FillByUsername(this.cRSDataSet2.courses,username);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string CONNECTION_URL = "Data Source=ASHOK-LAPTOP\\SQLEXPRESS;Initial Catalog=CRS;Integrated Security=True";
            SqlConnection connection = new SqlConnection(CONNECTION_URL);
            SqlCommand cmd = null;
            SqlDataAdapter sda;
            SqlDataReader sdr;
            try
            {
                connection.Open();
                string sql = "SELECT * FROM COURSES WHERE username="+"\'"+username+ "\'  and courses=" + "\'" +comboBox1.SelectedValue+"\';";
                cmd = new SqlCommand(sql, connection);
                sdr =  cmd.ExecuteReader();

                if (sdr.Read())
                {
                    MessageBox.Show("Already Course Registered :"+comboBox1.SelectedValue, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sdr.Close();
                    cmd.Dispose();
                }
                else
                {
                    sdr.Close();
                    sql = "INSERT INTO COURSES (USERNAME,COURSES) VALUES(" + "\'" + username + "\'" + "," + "\'" + comboBox1.SelectedValue + "\'" + ");";
                    sda = new SqlDataAdapter();
                    sda.InsertCommand = new SqlCommand(sql, connection);
                    sda.InsertCommand.ExecuteNonQuery();
                    MessageBox.Show("Course Registered :" + comboBox1.SelectedValue, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.courseregister_Load(sender, e);
                    tabControl1.SelectedTab = tabPage1;
                    sda.Dispose();
                }
                
            }
            catch (Exception err)
            {
                MessageBox.Show("DB Connection Error" + err.Message, "Error");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
