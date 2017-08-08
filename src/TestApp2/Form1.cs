using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            try
            {
                ApplicationServices.RegisterServices();
                Console.WriteLine("ApplicationServices.RegisterServices -> done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
