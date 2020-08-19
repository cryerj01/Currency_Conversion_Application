using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;

public class Currency
{
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

    // basic currency with no input paramiters
    public Currency()
    {
        CurrencyName = "Unkown";
        CurrencyValue = 0;
        CurrencySymble = "Unkown";
    }
    // Currency wth input peramiters 
	public Currency(string name, float Value, string symble)
    {
        CurrencyName = name;
        CurrencyValue = Value;
        CurrencySymble = symble;
    }
    // delaring Currency properties and making getters and setter for each
    public string CurrencyName { get; set; }
    public float CurrencyValue { get; set; }
    public string CurrencySymble { get; set; }

    
    // save new currency to db methord
    internal Boolean storeCurrency(Currency newCurrency)
    {
        bool result = false;
        //opening the connedtion to the db and creating the query to check if the currency is alread in the db 
        con.Open();
        cmdtext = "SELECT * FROM Currencys WHERE CurrencyName = '" + newCurrency.CurrencyName + "'";

        cmd = new SqlCommand(cmdtext, con);
        Dr = cmd.ExecuteReader();
        Dr.Read();
        // if the currency isnt in the db the ne wcurrency is then added to the db using a stored procider.
        if (!Dr.HasRows)
        {
            Dr.Close();
         
            // setting up the command to add the data using the stored prociduer 
            cmd.Connection = con;
            cmd.CommandText = "storeCurrency";
            cmd.CommandType = CommandType.StoredProcedure;
            // assing the values of the stored prosiduer valuses of the object newCurrency
            cmd.Parameters.AddWithValue("@CurrencyName", newCurrency.CurrencyName);
            cmd.Parameters.AddWithValue("@CurrencyValue",  newCurrency.CurrencyValue);
            cmd.Parameters.AddWithValue("@CurrencySymble", newCurrency.CurrencySymble);

           // checking if the query is successfull or not if not showing a messag box saying so else retung true
            int saved = cmd.ExecuteNonQuery();
        con.Close();
            if (saved < 0)
            {
                MessageBox.Show("Not Saved");
            }
            else
            {
                result = true;
            }
            
        }
        return result;
    }
}


