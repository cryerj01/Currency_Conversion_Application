using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Currency_Conversion_Application
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            
            getCurrencys();
          
        }

        static string path1 = System.IO.Directory.GetCurrentDirectory();
        static string path2 = Directory.GetParent(path1).FullName;
        static string path3 = Directory.GetParent(path2).FullName;

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename= " + path3 + "\\Properties\\Database1.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader Dr;

       
        

        private void getCurrencys()
        {
            con.Open();
            string cmdtext = "SELECT CurrencyName FROM Currencys";
            cmd = new SqlCommand(cmdtext, con);
            Dr = cmd.ExecuteReader();

            while (Dr.Read())
            {
                string line = Dr["CurrencyName"].ToString();
                if(line.Trim() == "GBP")
                {

                }
                else {
                comboBox1.Items.Add(line);
            } }
            Dr.Close();
            con.Close();

        }

        private void ConvertBtn_Click(object sender, EventArgs e)
        {
            double rate;
           rate = getChosenRate();
            if (rate == -1)
            {
                Value.Text = "PLEASE SELCT A CURRENCY FROM THE DROP DOWN";
                return;
            }
            else
            {
                string combo = comboBox1.Text;
                string symble = getRateSymble(combo);
                string convert = input.Text;
                double result = calculate(convert, rate);
                Value.Text = $"Value is: {symble}{result}";

                storeConvertion(convert, result, combo);
            
            
            }
        
        
        
        
        
        
        
        }

        private void storeConvertion(string convert, double result, string combo)
        {

            int.TryParse(input.Text, out int entered);
            cmd.Connection = con;
            cmd.CommandText = "InsertIntoCoversions";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@input", entered);
            cmd.Parameters.AddWithValue("@convertedTo", combo);
            cmd.Parameters.AddWithValue("@output", result);
            

            con.Open();
            
            int saved = cmd.ExecuteNonQuery();
            con.Close();
                
            if (saved<0 )
            {
                MessageBox.Show("Not Saved");
            }
        }

        private string getRateSymble(string text)
        {
            con.Open();
            string cmdtext = "SELECT * FROM Currencys Where CurrencyName = '" + text + "'";
            cmd = new SqlCommand(cmdtext, con);
            Dr = cmd.ExecuteReader();
            Dr.Read();
            string symble = Dr["CurrencySymble"].ToString();

            con.Close();
            Dr.Close();
            return symble;

        }

        private double calculate(string convert, double rate)
        {
            Double inPut;
            Double.TryParse(convert, out inPut);
            Double result = (inPut * rate);

            double output = Math.Round(result, 2);

           
            
            return output;
        }

        private double getChosenRate()
        {
            try
            {
                double rate;
                con.Open();
                string cmdtext = "SELECT CurrencyValue FROM Currencys Where CurrencyName = '" + comboBox1.Text + "'";
                cmd = new SqlCommand(cmdtext, con);
                Dr = cmd.ExecuteReader();
                Dr.Read();
                // Console.WriteLine(cmdtext);
                string result = Dr["CurrencyValue"].ToString();
                Double.TryParse(result, out rate);
                Dr.Close();
                con.Close();

                string rateVal = rate.ToString();
                ratevalue.Text = rateVal;
                return rate;
            }
            catch (Exception e)
            {
                Dr.Close();
                con.Close();
                Console.WriteLine(e);
               
                
                return -1;
            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getChosenRate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 search = new Form2();
            search.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 addnewCurrency = new Form3();
            addnewCurrency.Show();
            if (addnewCurrency.IsDisposed)
            {
                getCurrencys();
            }
        }
    }
}
