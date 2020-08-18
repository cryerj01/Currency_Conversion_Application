using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\turne\\Desktop\\Currency_Conversion_Application\\Currency_Conversion_Application\\Properties\\Database1.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader Dr;

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
                searchCombo.Items.Add(line);
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
            DateTime start = this.startdate.Value.Date;
            DateTime end = this.enddate.Value.Date;
            

            searchCombo(searchValue, combo, start, end);

            
        }

        private void searchCombo(int searchValue, string combo, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
