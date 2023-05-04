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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("D:\\C# connect to base\\Icon\\volvo.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox2.Image = Image.FromFile("D:\\C# connect to base\\Icon\\tdr.jpg");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void lbl_Drivers_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();
            
        }

        private void lbl_Drivers_MouseHover(object sender, EventArgs e)
        {
            lbl_Drivers.BackColor = Color.Gold;
        }

        private void lbl_Drivers_MouseLeave(object sender, EventArgs e)
        {
            lbl_Drivers.BackColor = panel2.BackColor;
        }

        private void lbl_Trucks_MouseClick(object sender, MouseEventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
            
        }

        private void lbl_Trucks_MouseHover(object sender, EventArgs e)
        {
            lbl_Trucks.BackColor = Color.Gold;
        }

        private void lbl_Trucks_MouseLeave(object sender, EventArgs e)
        {
            lbl_Trucks.BackColor = panel2.BackColor;
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.BackColor = Color.Gold;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = panel2.BackColor;
        }

        private void label3_MouseClick(object sender, MouseEventArgs e)
        {
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }
    }
}
