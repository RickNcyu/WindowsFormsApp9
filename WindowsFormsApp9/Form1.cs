
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        static string connectionString = @"Server=localhost;Database=postgres;User ID=postgres;Password=1234;";
        string path = @"D:/集點設定";
        

           
        public Form1()
        {
            
            InitializeComponent();
        }
        DataTable Table = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(path);
            //true不覆蓋內容
            StreamWriter sw = new StreamWriter(path + @"\集點.txt", true, Encoding.Default);
            sw.Close();
            //Console.WriteLine(Table);
            string SQL = @"select * from car_data";
            dataGridView1.DataSource = GetAll(SQL);

            if (Table != null) Table.Dispose();
            Table.Columns.Add("付款方式", typeof(string));
            Table.Columns.Add("集點設定值", typeof(string));
            dataGridView2.DataSource = Table;
            button4.PerformClick();
            
        }
        static DataTable GetAll(string _SQLCommand)
        {
            DataTable oDataTable = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                NpgsqlDataAdapter oAdapter = new NpgsqlDataAdapter(_SQLCommand, conn);
                oAdapter.Fill(oDataTable);
                if (oAdapter != null)
                    oAdapter.Dispose();
            }
            return oDataTable;
        }
        static int ExecNonQuery(string _SQLCommand)
        {
            int result = 0;
            string ans = "",ans2= "", ans3 = "", ans4 = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            { 
                connection.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(_SQLCommand, connection);
                cmd.Parameters.AddWithValue("@comboBox1",ans);
                cmd.Parameters.AddWithValue("@textBox1", ans2);
                cmd.Parameters.AddWithValue("@textBox2", ans3);
                cmd.Parameters.AddWithValue("@comboBox2", ans4);
                cmd.CommandType = CommandType.Text;

                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
            }

            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string SQL = @"select * from car_data";
            Console.WriteLine(GetAll(SQL).Rows.Count);
            foreach(DataRow row in GetAll(SQL).Rows)
            {
                Console.WriteLine(row[0]);
            }
            dataGridView1.DataSource = GetAll(SQL);
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string SQL = @"INSERT INTO car_data 
                (car_no,cust_id,unit_id,product_id,status,tran_time) 
                VALUES(1, 1, @comboBox1,@textBox1, 2,@textBox2,@comboBox2) ";
            Console.WriteLine("新增結果:" + ExecNonQuery(SQL));

            dataGridView1.Update();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            


            //FileStream fs2 = new FileStream(outfileName, FileMode.Create, FileAccess.Write);
            //string text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\t" +"集點設定值"+ textBox1.Text;
            using (StreamWriter sw = new StreamWriter(@"D:/集點設定/集點.txt", false, Encoding.Default))
            {
                if (!textBox3.Enabled) textBox3.Text = "";
                if (!textBox4.Enabled) textBox4.Text = "";
                if (!textBox5.Enabled) textBox5.Text = "";
                //Console.WriteLine(@textBox1);
                sw.WriteLine("901現金"+ "=" +textBox6.Text);
                sw.WriteLine("931信用卡"+ "="+textBox7.Text);
                sw.WriteLine("903簽帳"+ "="+textBox3.Text);
                sw.WriteLine("905車隊捷利卡"+ "="+textBox4.Text);
                sw.WriteLine("939CPCPay"+ "="+textBox5.Text);
            }
            //呼叫執行查詢button4
            button4.PerformClick();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked == false) textBox3.Enabled = false;
            else textBox3.Enabled = true;
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false) textBox4.Enabled = false;
            else textBox4.Enabled = true;
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox6_Click(object sender, EventArgs e)
        {

        }
        //只能輸入數字
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*{
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
            }*/
        }
        //查詢
        private void button4_Click(object sender, EventArgs e)
        {
            Table.Rows.Clear();
            //if (Table != null) Console.WriteLine("yes");//Table.Dispose();
            string[] line = File.ReadAllLines(@"D:/集點設定/集點.txt",Encoding.Default);
            // line[0] 現金=7
            // line[1] 信用卡=4
            //Console.WriteLine(line[1]);
            string[] value;
            //用[]因為要split 會變成子字串
            for(int i=0;i<line.Length;i++)
            {   //i=0~4 
                //line[0] line[1] 個別處理每行
                value = line[i].ToString().Split('=');
                //Console.WriteLine(value[0]);
                //value[0]= 現金=7裡的現金
                string[] row = new string[value.Length];
                for(int j=0;j<value.Length;j++)
                { row[j] = value[j].Trim();
                    //Console.WriteLine(row[0]+row[1]); 
                }
                Table.Rows.Add(row);
            }
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox3_Click_1(object sender, EventArgs e)
        {
            if (checkBox3.Checked == false) textBox5.Enabled = false;
            else textBox5.Enabled = true;
        }
    }
}
