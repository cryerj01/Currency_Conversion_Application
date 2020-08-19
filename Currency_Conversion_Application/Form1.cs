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
            // stoping edit of the combobox
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
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

       
        
        //gets the currencys form the database and shows them in the combo box 
        private void getCurrencys()
        {
            //opening a connection to the db, creating a string for the sql query and then using the data reader to exicute the commmand at the db
            con.Open();
            string cmdtext = "SELECT CurrencyName FROM Currencys";
            cmd = new SqlCommand(cmdtext, con);
            Dr = cmd.ExecuteReader();
            //whiel the data reader has a ressult it will check if the vale of the currency is GBP or not and if not assing the valueto the combo box 
            while (Dr.Read())
            {
                string line = Dr["CurrencyName"].ToString();
                if(line.Trim() == "GBP")
                {

                }
                else {
                comboBox1.Items.Add(line);
            } }
            //Closing the datareazder and the connection to the db 
            Dr.Close();
            con.Close();

        }

        //converts the value in the textbox against the seleced currency in the combo box
        private void ConvertBtn_Click(object sender, EventArgs e)
        {
            // creating a local variable to assine the valuse that is pulled out of the db using the getChosenRate()
            double rate;
           rate = getChosenRate();
            //checking if the rate was seleced form the combo box
            if (rate == -1)
            {
                //informing the user to select form the drop down
                Value.Text = "PLEASE SELCT A CURRENCY FROM THE DROP DOWN";
                return;
            }
            else
            {
                // assinging the data in the fform to loac variables and the using the calculate() to retun and answer
                //the displaying the answer
                string combo = comboBox1.Text;
                string symble = getRateSymble(combo);
                string convert = input.Text;
                double result = calculate(convert, rate);
                Value.Text = $"Value is: {symble}{result}";

                // staring the prosiduyer to store the conversion in the db
                storeConvertion(convert, result, combo);
            
            
            }
        
        
        
        
        
        
        
        }

        //storing the conversion into the db
        private void storeConvertion(string convert, double result, string combo)
        {
            //checking to see if the valuse entered is a number if not a message box is displayed asking the user to enter a number
            int.TryParse(input.Text, out int entered);
            if (entered != 0)
            {
                //if the value entered is number the comand connection is set to sqlConnection con and the command text is set to the stroed prosiduer
                //and then the paramiteres delared in the prosiduer are are assined values 
                cmd.Connection = con;
                cmd.CommandText = "InsertIntoCoversions";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@input", entered);
                cmd.Parameters.AddWithValue("@convertedTo", combo);
                cmd.Parameters.AddWithValue("@output", result);

                //the connecting to the db is opened and the cmd is exicuted and the connedction is closed
                //the cmd retuns a value if that valuse is -1 the not save d message is shown
                con.Open();

                int saved = cmd.ExecuteNonQuery();
                con.Close();

                if (saved < 0)
                {
                    MessageBox.Show("Not Saved");
                }
            }
            else
            {
                MessageBox.Show("Enter a Value such as 1 or 10");
            }
        }

        //gets the symble of the currencys
        private string getRateSymble(string text)
        {
            // A connection to the DB is made and the sql query is generted and the data reader is used to to get any results
            // the connection and data reader are closed then resutls are tehhn reurned 
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

        //calculating the conversion value 
        private double calculate(string convert, double rate)
        {
            // the value input and the chosen currency rate are multiplyed and then the answer is rounded to 2 decimal places
            // and the anser is retuned
            Double inPut;
            Double.TryParse(convert, out inPut);
            Double result = (inPut * rate);

            double output = Math.Round(result, 2);

           
            
            return output;
        }

        //getting the rate of the currency selected int he combo box
        private double getChosenRate()
        {
            // tryign to get the information form the db if the try is thrown the date reader and connetion is closed and the error is printed to console as well as retuning -1 
            try
            {// connection to the db is opened and the sql query is generated using the combo box selected the sql query is 
                // exicuted and the data reader is used to get the rate as a string which is tehn converted to a double 
                // called rate the connection and data reader are then closed and the rate vlause lable is updeted to show the selected rate 
                //befor the rate is retuned
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

        //getting the rate when the combo box is changed
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getChosenRate();
        }

        //openign the history page
        private void button1_Click(object sender, EventArgs e)
        {
            // creating a new insence of form2 called search and diplayingit
            Form2 search = new Form2();
            search.Show();
        }

        //open the add new currency page
        private void button2_Click(object sender, EventArgs e)
        {
            //creating a insence of form3 called addnewcurrency and displaying it
            Form3 addnewCurrency = new Form3();
            addnewCurrency.Show();
            // when addnewcurrency is closed refresh the currencys in the combo box
            if (addnewCurrency.IsDisposed)
            {
                getCurrencys();
            }
        }
    }
}
