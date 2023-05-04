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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("datasource=localhost;database=trucks;port=3306;username=root;password=tikastam");
        MySqlCommand cmd;
        MySqlDataAdapter ada;
        


        private void Form5_Load(object sender, EventArgs e)
        {
            string com = "select id, concat(DriverName,' ',DriverLastName) as Driver from drivers";
            filcom(comboBox1, com, "Driver", "id");
            displayd();
        }

        private void filcom(ComboBox combo, string query, string display, string valmem)
        {
            cmd = new MySqlCommand(query, con);
            ada = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            ada.Fill(table);
            combo.DataSource = table;
            combo.DisplayMember = display;
            combo.ValueMember = valmem;
        }
        private void displayd()
        {
            
            string query = "select idtr as Id, driver as Driver,stpoint as 'Start Point',destpoint as Destination,stdate as 'Start Date',deadline as Deadline from driving";
            cmd = new MySqlCommand(query, con);
            ada = new MySqlDataAdapter(cmd);
            DataTable tab = new DataTable();
            ada.Fill(tab);
            dataGridView1.DataSource = tab;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
            comboBox1.Text = "Select driver";

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Bisque;
            dataGridView1.EnableHeadersVisualStyles = false;
            
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 100;
            

            for(int i = 0; i < dataGridView1.Rows.Count; i+=2)
            {
                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Brown;
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.BurlyWood;
            }
            for(int i = 1;i<dataGridView1.Rows.Count; i += 2)
            {
                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Aquamarine;
            }

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            lbl_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string addload = "insert into driving(driver,stpoint,destpoint,stdate,deadline) values('" + comboBox1.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "')";
                cmd = new MySqlCommand(addload, con);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Successfully added load");
                }
                else
                {
                    MessageBox.Show("Not successfully added load", "Add Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                displayd();
                btn_clear.PerformClick();
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            lbl_id.Text = "Choose Load";
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();
            comboBox1.Text = "Select driver";
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            con.Open();
            string edit = "update driving set driver = '" + comboBox1.Text + "', stpoint = '" + textBox1.Text + "', destpoint = '" + textBox2.Text + "', stdate = '" + dateTimePicker1.Text + "', deadline = '" + dateTimePicker2.Text + "' where idtr =" + int.Parse(lbl_id.Text);
            cmd = new MySqlCommand(edit, con);

            if(cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Successfully changed", "Edit load",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Not successfully changed", "Change load parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            displayd();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            con.Open();
            string remove = "delete from driving where idtr = " + int.Parse(lbl_id.Text);
            cmd = new MySqlCommand(remove, con);

            if(cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Removed Successfully", "Remove driver", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Not removed successfully", "Remove driver", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();
            displayd();
        }
    }
}
