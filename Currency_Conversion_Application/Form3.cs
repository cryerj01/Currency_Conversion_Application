using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Currency_Conversion_Application
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        // creating a new currency in the db from the data in the form
        private void button1_Click(object sender, EventArgs e)
        {
            //setting local variable to have the values of the textboxes
            string name = textBox1.Text;
            float.TryParse(textBox2.Text, out float value);
            string sysmble = textBox3.Text;
            // creating a new object of currency called add with the inputed data
            Currency add = new Currency(name, value, sysmble);
            //addeding the currency to the db and then if it is added closing the form
              bool added = add.storeCurrency(add);
            if(added == true)
            {
                this.Close();
            }
        }
         
        
    }
}
