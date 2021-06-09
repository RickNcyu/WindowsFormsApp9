
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
        string path = @"C:\down";
        

           
        public Form1()
        {
            
            InitializeComponent();
        }
        DataTable Table = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(path);
            //true不覆蓋內容

            StreamWriter sw = new StreamWriter(path + @"\down.txt", true, Encoding.Default);
            sw.Close();
            /*
              if(File.Exists(path + @"\集點.txt")) { }
            else
            {
                File.Create(path + @"\集點.txt");
                
            }
            */
            //Console.WriteLine(Table);

            if (Table != null) Table.Dispose();
            Table.Columns.Add("付款方式", typeof(string));
            Table.Columns.Add("集點設定值", typeof(string));
            dataGridView2.DataSource = Table;
            button4.PerformClick();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
          

        }

        private void button2_Click(object sender, EventArgs e)
        {


            string temp="";
            //FileStream fs2 = new FileStream(outfileName, FileMode.Create, FileAccess.Write);
            //string text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\t" +"集點設定值"+ textBox1.Text;
            using (StreamWriter sw = new StreamWriter(@"C:/down/down.txt", false, Encoding.Default))
            {
                if (radioButton1.Checked) { temp = "以現金集點"; }
                else if (radioButton2.Checked) { temp = "以公升集點"; }
                if (!textBox3.Enabled) textBox3.Text = "";
                if (!textBox4.Enabled) textBox4.Text = "";
                if (!textBox5.Enabled) textBox5.Text = "";
                if (!textBox10.Enabled) textBox10.Text = "";
                if (!textBox9.Enabled) textBox9.Text = "";
                if (!textBox8.Enabled) textBox8.Text = "";
                //Console.WriteLine(@textBox1);
                sw.WriteLine(temp);
                sw.WriteLine("站IP" + "=" + textBox12.Text);
                sw.WriteLine("汽油");
                sw.WriteLine("901現金"+ "=" +textBox6.Text);
                sw.WriteLine("931信用卡"+ "="+textBox7.Text);
                sw.WriteLine("903簽帳"+ "="+textBox4.Text);
                sw.WriteLine("905車隊捷利卡"+ "="+textBox3.Text);
                sw.WriteLine("939CPCPay"+ "="+textBox5.Text);

                
                sw.WriteLine("\n柴油");
                sw.WriteLine("901現金" + "=" + textBox2.Text);
                sw.WriteLine("931信用卡" + "=" + textBox1.Text);
                sw.WriteLine("903簽帳" + "=" + textBox9.Text);
                sw.WriteLine("905車隊捷利卡" + "=" + textBox10.Text);
                sw.WriteLine("939CPCPay" + "=" + textBox8.Text);
                sw.WriteLine("站名" + "=" + textBox11.Text);
                
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
            string[] line = File.ReadAllLines(@"C:/down/down.txt",Encoding.Default);
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
                
                //判斷 假如設定檔裡有站名就顯示
                if(i==15)
                {
                    
                    textBox11.Text = row[1];
                }
                if (i == 1)
                {

                    textBox12.Text = row[1];
                }
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

        private void checkBox6_Click(object sender, EventArgs e)
        {
            if (checkBox6.Checked == false) textBox10.Enabled = false;
            else textBox10.Enabled = true;
        }

        private void checkBox5_Click(object sender, EventArgs e)
        {
            if (checkBox5.Checked == false) textBox9.Enabled = false;
            else textBox9.Enabled = true;
        }

        private void checkBox4_Click(object sender, EventArgs e)
        {
            if (checkBox4.Checked == false) textBox8.Enabled = false;
            else textBox8.Enabled = true;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Console.WriteLine(textBox12.Text);
            
                File.Copy(@"C:\down\down.txt", @"C:\down\1.txt", true);
                using (StreamWriter sw = new StreamWriter(@"C:\down\1.txt", true, Encoding.Default))
                {
                    sw.WriteLine(textBox12.Text + ".10");
                }

                File.Copy(@"C:\down\down.txt", @"C:\down\2.txt", true);
                using (StreamWriter sw = new StreamWriter(@"C:\down\2.txt", true, Encoding.Default))
                {
                    sw.WriteLine(textBox12.Text + ".20");
                }
                File.Copy(@"C:\down\2.txt", @"\\10.85.138.202\down\down.txt", true);
                
                /*
                File.Copy(@"C:\down\down.txt", @"C:\down\3.txt", true);
                using (StreamWriter sw = new StreamWriter(@"C:\down\3.txt", true, Encoding.Default))
                {
                    sw.WriteLine(textBox12.Text + ".30");
                }
                */  
            
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
