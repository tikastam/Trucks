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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("datasource=localhost;database=trucks;port=3306;username=root;password=tikastam");
        MySqlCommand cmd;
        MySqlDataAdapter ada;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            string query = "select * from mark";
            filcom(comboBox1, query, "mark", "id");
            string spec = "select * from specialskills";
            filcom(comboBox3, spec, "skills", "idskill");
            comboBox1_SelectedIndexChanged(null, null);
            display(string.Empty);

        }
        
        public void display(string valuesearch)
        {
            string query = "select id as Id,DriverName as Name,DriverLastName as 'Last Name',TruckMark as 'Truck Mark',TruckModel as 'Truck Model',special as Skills from drivers where concat(DriverName, DriverLastName) like '%"+valuesearch+"%'";
            ada = new MySqlDataAdapter(query, con);
            DataTable table = new DataTable();
            ada.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[4].Width = 60;

            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Brown;
            dataGridView1.EnableHeadersVisualStyles = false;
        }
        public void filcom(ComboBox combo, string query, string display, string valuem)
        {

            cmd = new MySqlCommand(query, con);
            ada = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            ada.Fill(tab);
            combo.DataSource = tab;
            combo.DisplayMember = display;
            combo.ValueMember = valuem;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int val;
            Int32.TryParse(comboBox1.SelectedValue.ToString(), out val);
            string query = "select * from model where model.markid = " + val;
            filcom(comboBox2, query, "model", "markid");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string insert = "insert into drivers(DriverName,DriverLastName,TruckMark,TruckModel,special) values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "')";
                cmd = new MySqlCommand(insert, con);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Inserted");
                }
                else
                {
                    MessageBox.Show("Not Inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            display(string.Empty);
            button4.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                con.Open();
                string update = "update drivers set DriverName = '" + textBox1.Text + "', DriverLastName = '" + textBox2.Text + "', TruckMark = '" + comboBox1.Text + "', TruckModel = '" + comboBox2.Text + "', special = '" + comboBox3.Text + "' where id = " + int.Parse(lbl_id.Text);
                cmd = new MySqlCommand(update, con);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Updated");
                }
                else
                {
                    MessageBox.Show("Not Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            display(string.Empty);
            button4.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string delete = "delete from drivers where id =" + int.Parse(lbl_id.Text);
                cmd = new MySqlCommand(delete, con);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Deleted");
                }
                else
                {
                    MessageBox.Show("Not Deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }
            
            display(string.Empty);
            button4.PerformClick();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            comboBox1.SelectedIndex = 0;
            lbl_id.Text = "Choose Driver";
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            lbl_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            display(txt_search.Text);
        }
    }
}
