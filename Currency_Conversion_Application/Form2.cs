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
            //get all currencys from the DB
            getCurrencys();
        }

        //MAking it so the DB file path is not dependent on the been on my machine
        static string path1 = System.IO.Directory.GetCurrentDirectory();
        static string path2 = Directory.GetParent(path1).FullName;
        static string path3 = Directory.GetParent(path2).FullName;
        //declaring the db connection using the path above and then the relative path from where the path3 ends
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename= " + path3 + "\\Properties\\Database1.mdf;Integrated Security=True");
        //declaring a SqlCommand and SqlDataReader

        SqlCommand cmd;
        SqlDataReader Dr;
        private string cmdtext;


        //gets the currencys form the database and shows them in the combo box 
        private void getCurrencys()
        {   // making it so the combo box cant be edited
            searchCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            //opening a connection to the db, creating a string for the sql query and then using the data reader to exicute the commmand at the db
            con.Open();
            string cmdtext = "SELECT CurrencyName FROM Currencys";
            cmd = new SqlCommand(cmdtext, con);
            Dr = cmd.ExecuteReader();
            //whiel the data reader has a ressult it will check if the vale of the currency is GBP or not and if not assing the valueto the combo box 
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
            
            //Closing the datareazder and the connection to the db 
            Dr.Close();
            con.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //get all info for the search and check if the user is wanting to use the date to search. 
            // depenign on if the date is used or not depends on which of the seach() is used
            int.TryParse(searchinput.Text, out int searchValue);
            string combo = searchCombo.Text;
            //passing all the needed data to the relevent methord
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

        //search the db not suing the date range
        private void search(int searchValue, string combo)
        {
            // setting the valuse of the combo box to a local variable.
            string search = combo.Trim();
            //using a switch to detemin the sql query generated based off the combo box selection
            switch (combo.Trim())
            {
                //if no selection is made 
                case "":
                    cmdtext = "SELECT * FROM Coversions";
                    //checking if the user has typed a value to seach for 
                    if (searchinput.ToString().Trim() != null)
                    {
                        if (int.TryParse(searchinput.Text, out int find))
                        {
                            //adding to the sql query when needed USING WHERE not AND
                            cmdtext += " Where input = '" + find + "'";
                        }
                    }
                    break;

                    
                default:
                    // if any selection is made the sql query is created using the variable search which is the combo box text
                    cmdtext = "SELECT * FROM Coversions WHERE convertedTo  = '" + search + "'";
                    //checking if the user has typed a value to seach for 
                    if (searchinput.ToString().Trim() != null)
                    {
                        if (int.TryParse(searchinput.Text, out int find))
                        {
                            //adding to the sql query when needed using AND not WHERE
                            cmdtext += " AND  input = '" + find + "'";
                        }
                    }
                    

                    break;
          
            }

           //exicuting the query and then displaying the data in a datagride using a SqlDataAdapter and a Dataset
            cmd = new SqlCommand(cmdtext, con);
            SqlDataAdapter adpt =  new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Open();
            adpt.Fill(ds);
            
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();
            con.Close();
            
            
        }
        //search the db using the date range
        private void search(int searchValue, string combo, DateTime start, DateTime end)
        { // setting the valuse of the combo box to a local variable.
            string search = combo.Trim();
            //using a switch to detemin the sql query generated based off the combo box selection
            switch (combo.Trim())
            {
                //if no selection is made 
                case "":
                    cmdtext = "SELECT * FROM Coversions ";
                    //checking if the user has typed a value to seach for 
                    if (searchinput.ToString().Trim() != null)
                    {
                       
                        if (int.TryParse(searchinput.Text, out int find))
                        {
                            //adding to the sql query when needed USING WHERE not AND
                            cmdtext += "WHERE  input = '" + find + "'";
                        }
                    }
                    break;

                // if any selection is made the sql query is created using the variable search which is the combo box text
                default:
                    cmdtext = "SELECT * FROM Coversions WHERE convertedTo  = '" + search + "'";
                    //checking if the user has typed a value to seach for
                    if (searchinput.ToString().Trim() != null)
                    {
                        if (int.TryParse(searchinput.Text, out int find))
                        {
                            //adding to the sql query when needed using AND not WHERE
                            cmdtext += " Where input = '" + find + "'";
                        }
                    }

                    break;
                    
            }
            // adding the date part of the search to the query depenign on if the user has ented a value and a currency to look for or not
            if (combo == "" && searchinput.Text == "")
            {// where the user hasnt ented a value or seleced a currency
                cmdtext += " Where  conversiondate BETWEEN CONVERT(DATE,'" + start.ToShortDateString() + "',103) AND CONVERT(DATE,'" + end.ToShortDateString() + "',103)";
            }
            else
            {
                //where the user has ented a value or seleced a currency
                   cmdtext += "  AND  conversiondate BETWEEN CONVERT(DATE,'" + start.ToShortDateString() + "',103) AND CONVERT(DATE,'" + end.ToShortDateString() + "',103)";
            }
            // addeing a order by to sort the data by the date
            cmdtext += " ORDER BY conversionDate";

            //exicuting the query and then displaying the data in a datagride using a SqlDataAdapter and a Dataset
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
