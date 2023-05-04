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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("datasource=localhost;database=trucks;port=3306;username=root;password=tikastam");
        MySqlCommand cmd;
        MySqlDataAdapter ada;
        MySqlDataReader re;
        private void Form2_Load(object sender, EventArgs e)
        {
            string query = "select * from mark";
            comb(comboBox2, query, "mark", "id");
            comboBox2_SelectedIndexChanged(null, null);
            
        }
        public void comb(ComboBox com,string query,string display,string valme)
        {
            cmd = new MySqlCommand(query,con);
            ada = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ada.Fill(table);
            com.DataSource = table;
            com.DisplayMember = display;
            com.ValueMember = valme;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int val;
            int.TryParse(comboBox2.SelectedValue.ToString(), out val);
            string query = "select * from model where model.markid = " + val;
            comb(comboBox1, query, "model", "markid");
            lbl_mark.Text = comboBox2.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            lbl_model.Text = comboBox1.Text;
            con.Open();
            string query = "select * from model where model = '" + comboBox1.Text + "'";
            cmd = new MySqlCommand(query, con);
            re = cmd.ExecuteReader();


            while (re.Read())
            {
                if (int.Parse(re.GetString("hp")) == 0)
                {
                    lbl_horse.Text = "Choose Model";
                    lbl_engine.Text = "Choose Model";
                    lbl_rating.Text = "Choose Model";
                    lbl_generation.Text = "Choose Model";
                }
                else
                {
                    lbl_engine.Text = re.GetString("engine");
                    lbl_horse.Text = re.GetString("hp");
                    lbl_rating.Text = re.GetString("rating");
                    lbl_generation.Text = re.GetString("ty");
                }
            }
            con.Close();
        }
    }
}
