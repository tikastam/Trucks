using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TruckBase
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("datasource=localhost;database=trucks;username=root;password=tikastam;port=3306");
        MySqlCommand cmd;
        MySqlDataReader re;
        
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string log = "select * from log1 where username= '" + txt_username.Text + "' and pass = '" + txt_password.Text + "'";
            cmd = new MySqlCommand(log, con);
            re = cmd.ExecuteReader();
            if (re.Read())
            {
                Form3 f = new Form3();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
            re.Close();
            cmd.Dispose();
            con.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("D:\\C# connect to base\\Icon\\loginIcon.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        
    }
}
