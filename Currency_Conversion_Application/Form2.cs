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

namespace Currency_Conversion_Application
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            getCurrencys();
        }

        
        static string path1 = System.IO.Directory.GetCurrentDirectory();
        static string path2 = Directory.GetParent(path1).FullName;
        static string path3 = Directory.GetParent(path2).FullName;

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename= " + path3 + "\\Properties\\Database1.mdf;Integrated Security=True");
        SqlCommand cmd;
        
        SqlDataReader Dr;
        private string cmdtext;

        private void getCurrencys()
        {
            searchCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            con.Open();
            string cmdtext = "SELECT CurrencyName FROM Currencys";
            cmd = new SqlCommand(cmdtext, con);
            Dr = cmd.ExecuteReader();

            while (Dr.Read())
            {
                string line = Dr["CurrencyName"].ToString();
                if (line.Trim() == "GBP" )
                {

                }
                else
                {
                    searchCombo.Items.Add(line);
                }
            }
            searchCombo.Items.Add("All");
            Dr.Close();
            con.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //get all info for the search
           int.TryParse(searchinput.Text, out int searchValue);
            string combo = searchCombo.Text;
            if (checkBox1.Checked)
            {
                DateTime start = this.startdate.Value.Date;
                DateTime end = this.enddate.Value.Date;
                search(searchValue, combo, start, end);
            }
            else { 
            search(searchValue, combo);
               }



        }

        private void search(int searchValue, string combo)
        {

            string search = combo.Trim();
            switch (combo.Trim())
            {

                case "":
                    cmdtext = "SELECT * FROM Coversions";
                    if (searchinput.ToString().Trim() != null)
                    {
                        if (int.TryParse(searchinput.Text, out int find))
                        {

                            cmdtext += " Where input = '" + find + "'";
                        }
                    }
                    break;

                    
                default:
                    cmdtext = "SELECT * FROM Coversions WHERE convertedTo  = '" + search + "'";
                    if (searchinput.ToString().Trim() != null)
                    {
                        if (int.TryParse(searchinput.Text, out int find))
                        {

                            cmdtext += " AND  input = '" + find + "'";
                        }
                    }
                    

                    break;
          
            }

           
            cmd = new SqlCommand(cmdtext, con);
            SqlDataAdapter adpt =  new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Update();
            
            
        }

        private void search(int searchValue, string combo, DateTime start, DateTime end)
        {
            string search = combo.Trim();

            switch (combo.Trim())
            {
                case "":
                    cmdtext = "SELECT * FROM Coversions ";
                  
                    if (searchinput.ToString().Trim() != null)
                    {
                       
                        if (int.TryParse(searchinput.Text, out int find))
                        {

                            cmdtext += "WHERE  input = '" + find + "'";
                        }
                    }
                    break;
                
                default:
                    cmdtext = "SELECT * FROM Coversions WHERE convertedTo  = '" + search + "'";

                    if (searchinput.ToString().Trim() != null)
                    {
                        if (int.TryParse(searchinput.Text, out int find))
                        {

                            cmdtext += " Where input = '" + find + "'";
                        }
                    }

                    break;
                    
            }
            if (combo == "" && searchinput.Text == "")
            {
                cmdtext += " Where  conversiondate BETWEEN CONVERT(DATE,'" + start.ToShortDateString() + "',103) AND CONVERT(DATE,'" + end.ToShortDateString() + "',103)";
            }
            else
            {
                cmdtext += "  AND  conversiondate BETWEEN CONVERT(DATE,'" + start.ToShortDateString() + "',103) AND CONVERT(DATE,'" + end.ToShortDateString() + "',103)";
            }

            cmdtext += " ORDER BY conversionDate";
            
            cmd = new SqlCommand(cmdtext, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            con.Open();
           
            adpt.Fill(dt);
            ds.Tables.Add(dt);
            con.Close();

          
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();




        }


    }
}
