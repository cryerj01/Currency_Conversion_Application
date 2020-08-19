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

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            float.TryParse(textBox2.Text, out float value);
            string sysmble = textBox3.Text;

            Currency add = new Currency(name, value, sysmble);
        bool added = add.storeCurrency(add);
            if(added == true)
            {
                this.Close();
            }
        }
         
        
    }
}
