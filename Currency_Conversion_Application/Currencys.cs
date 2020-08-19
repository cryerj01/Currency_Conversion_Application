using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;

public class Currency
{
    static string path1 = System.IO.Directory.GetCurrentDirectory();
    static string path2 = Directory.GetParent(path1).FullName;
    static string path3 = Directory.GetParent(path2).FullName;

    SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename= " + path3 + "\\Properties\\Database1.mdf;Integrated Security=True");
    SqlCommand cmd;

    SqlDataReader Dr;
    private string cmdtext;
    public Currency()
    {
        CurrencyName = "Unkown";
        CurrencyValue = 0;
        CurrencySymble = "Unkown";
    }
	public Currency(string name, float Value, string symble)
    {
        CurrencyName = name;
        CurrencyValue = Value;
        CurrencySymble = symble;
    }
    
    public string CurrencyName { get; set; }
    public float CurrencyValue { get; set; }
    public string CurrencySymble { get; set; }

    

    internal Boolean storeCurrency(Currency newCurrency)
    {
        bool result = false;
        con.Open();
        cmdtext = "SELECT * FROM Currencys WHERE CurrencyName = '" + newCurrency.CurrencyName + "'";

        cmd = new SqlCommand(cmdtext, con);
        Dr = cmd.ExecuteReader();
        
        if (Dr.Read() != null)
        {
            Dr.Close();
         
            cmd.Connection = con;
            cmd.CommandText = "storeCurrency";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CurrencyName", newCurrency.CurrencyName);
            cmd.Parameters.AddWithValue("@CurrencyValue",  newCurrency.CurrencyValue);
            cmd.Parameters.AddWithValue("@CurrencySymble", newCurrency.CurrencySymble);
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


