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
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            getCurrencys();
          
        }

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\turne\\Desktop\\Currency_Conversion_Application\\Currency_Conversion_Application\\Properties\\Database1.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader Dr;

        ComboBox CurrencySelector = new ComboBox();
        

        private void getCurrencys()
        {
            con.Open();
            string cmdtext = "SELECT CurrenctName FROM Currencys";
            cmd = new SqlCommand(cmdtext, con);
            Dr = cmd.ExecuteReader();
            Dr.Read();
            int i = 0;
            while (Dr[i] != null)
            {
                CurrencySelector.Items.Add(Dr[i]);
                i++;
            }

           


        }


    }
}
