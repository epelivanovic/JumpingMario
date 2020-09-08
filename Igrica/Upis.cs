using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Igrica
{
    public partial class Upis : Form
    {
        int rezultat;
        public Upis( int x)
        {
            InitializeComponent();
            rezultat = x;
        }
        DataProvider dp = new DataProvider();
        Form1 f1 = new Form1();
        
       
        private void Upis_Load(object sender, EventArgs e)
        {
            var lista = dp.UcitajSve();
            List<Rezultat> lista1 = new List<Rezultat>();
            label3.Text = rezultat.ToString();
            f1.timer2.Stop();
            for (int i = 0; i < lista.Count; i++)
            {
                if (i<5)
                {
                    lista1.Add(lista[i]);
                }
                else
                {
                    break;
                }
            }
            dataGridView1.DataSource = lista1;

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            int score = Convert.ToInt32(label3.Text) ;
            string ime = textBox1.Text;
            if (dp.Upisi(score,ime))
            {
                
                var lista = dp.UcitajSve();
                List<Rezultat> lista1 = new List<Rezultat>();
                
                for (int i = 0; i < lista.Count; i++)
                {
                    if (i < 6)
                    {
                        lista1.Add(lista[i]);
                    }
                    else
                    {
                        break;
                    }
                }
                dataGridView1.DataSource = lista1;
                MessageBox.Show("Upisani ste");
                this.Close();
            }
            else
            {
                MessageBox.Show("Doslo je do greske,niste upisani");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
