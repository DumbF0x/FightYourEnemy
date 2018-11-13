using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UEA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UEA ovire = new UEA(10);

            List<List<char>> o = ovire.Ustvari(5, 1, 10, 10, 10, 0.6);

            string s = "";
            for(int i=0; i<o.Count; i++)
            {
                for(int j=0; j<o[i].Count; j++)
                {
                    if (o[i][j] == ' ')
                        s += '_';
                    else
                        s += o[i][j];
                    s += " ";
                }
                    
                s += "\n";
            }

            label1.Text = s;
        }
    }
}
