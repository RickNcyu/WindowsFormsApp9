
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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string SQL = @"select * from car_data";
            dataGridView1.DataSource = GetAll(SQL);
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
            string path = @"D:/集點設定";
            System.IO.Directory.CreateDirectory(path);

            string outfileName = path + @"\集點.txt";
            //FileStream fs2 = new FileStream(outfileName, FileMode.Create, FileAccess.Write);
            string text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\t" +"集點設定值"+ textBox1.Text;
            using (StreamWriter sw = new StreamWriter(outfileName,true, Encoding.Default))
            {
                //Console.WriteLine(@textBox1);
                sw.WriteLine(text);
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
