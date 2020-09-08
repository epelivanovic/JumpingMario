using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
namespace Igrica
{
    public partial class Form1 : Form
    {

        Timer t1, timer1,  timerKreiDrvo, timerHoda;
       public Timer timer2;
        private List<Panel> l1;
        private PictureBox slika;
        private List<Objekat> objekti;
        private List<Drvo> lista = new List<Drvo>();
        private Random r;
        private List<PictureBox> listapicbox = new List<PictureBox>();
        bool stoji;
        bool skace;
        bool upaljeno = true;
        DataProvider dp = new DataProvider();
        bool prva = true, druga = false, treca = false;
        int brzina = 0;
        public int Rezultat = 0;

        public Form1()
        {
            InitializeComponent();
            stoji = true;
            l1 = new List<Panel>();

            objekti = new List<Objekat>();
            r = new Random();
         
            this.NovaIgra();
            for (int i = 1; i < 3; i++)
            {
                string name = "panel";
                int vrednost = i;
                name += vrednost;
                Panel p1 = (this.Controls.Find(name, true)[0]) as Panel;
                l1.Add(p1);
            }
        }

        private void Hoda(object sender, EventArgs e)
        {
            //Funkcija za promenu slike tako da izgleda kao da hoda
            if (skace == false)
            {
                if (stoji)
                {
                    pictureBox1.Image = Properties.Resources.hoda;
                    stoji = false;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.stoji;
                    stoji = true;
                }
            }
        }

        private void KrerajDrvo(object sender, EventArgs e)
        {
            //kreiranje drveta
            Drvo o = new Drvo();
            panel3.Controls.Add(o);

            o.Left = panel3.Width - o.Width;
            o.Height = panel3.Height;
            o.Width = 100;
            lista.Add(o);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //pomeranje pictureboxa levo i desno
            if (upaljeno)
            {
                if (e.KeyCode == Keys.Down)
                {
                    pictureBox1.Parent = panel1;
                   
                    t1.Stop();
                }
                if (e.KeyCode == Keys.Up)
                {

                    pictureBox1.Parent = panel2;
                    
                    t1.Interval = 1000;
                    t1.Start();
                    t1.Tick += Skoci;
                    //t2.Interval = 800;
                    //t2.Start();
                    //t2.Tick += Vrati;


                }
                

            }
            if ((e.KeyCode ==  Keys.D && skace==false) ||(e.KeyCode == Keys.Right && skace == false) )
            {
                if (pictureBox1.Right<=pictureBox1.Parent.Width)
                {
                    pictureBox1.Left += 10;
                }
                
            }
            if ((e.KeyCode == Keys.A && skace == false) || (e.KeyCode == Keys.Left && skace == false))
            {
                if (pictureBox1.Left>=0 && skace==false)
                {
                    pictureBox1.Left -= 10;
                }
               
            }
        }

        private void Kreiraj(object sender, EventArgs e)
        {

            Objekat obj = null;
            //kreiranje prepreke ili coina
            if (r.Next(0, 100) >= 50)
            {
                obj = new Prepreka();
            }
            else
            {
                obj = new Coin();
            }
            //dodavanje u listi objekata
            obj.Tag = panel1;
            this.Controls.Add(obj);
            obj.Parent = panel1;
            obj.Left = panel1.Width - obj.Width;
            obj.Height = panel1.Height;
            slika.BringToFront();
            objekti.Add(obj);


        }


        private void Pomeraj(object sender, EventArgs e)
        {
            //Brisanje objekata
            foreach (var item in objekti)
            {
                if (item.Left<=-20)
                {
                    objekti.Remove(item);
                    panel1.Controls.Remove(item);
                    break;
                }
            }
           //
           //Povecanje brzine
            if ((Rezultat >= 50) && (prva == true))
            {
                timer1.Interval -= 300;
                prva = false;
                druga = true;
                timer2.Interval -= 1;

                brzina = 1;
            }
            else if ((Rezultat >= 100) && (druga == true))
            {
                timer1.Interval -= 400;
                druga = false;
                timer2.Interval -= 1;
                brzina = 2;
                treca = true;
            }
            else if ((Rezultat >= 200) && (treca == true))
            {
                timer1.Interval -= 400;
                druga = false;
                timer2.Interval -= 1;
                brzina = 3;
                treca = false;
            }
            //
            //Da li skace
            if (pictureBox1.Parent == panel2)
            {
                pictureBox1.Image = Properties.Resources.skace;
                skace = true;

            }
            else
            {
                skace = false;
            }
            //
            //Pomeranje na osnovu brzine
            if (brzina != 0)
            {
                for (int i = 0; i < objekti.Count; i++)
                {

                    objekti.ElementAt(i).Pomeri(brzina);
                   


                }
            }
            else
            {
                for (int i = 0; i < objekti.Count; i++)
                {

                    objekti.ElementAt(i).Pomeri(brzina);
                   


                }
            }
            //
            //Provera kontakta i vrste objekta
            foreach (Objekat item in objekti)
            {
                if (pictureBox1.Parent == panel1)
                {
                    slika.Tag = panel1;
                }
                else
                {
                    slika.Tag = panel2;
                }
                if (item.Kontakt(slika))
                {
                    //Ima kontakt
                    if (item.Oznaka() == 1)
                    {
                        timer1.Stop();
                        timer2.Stop();
                        timerKreiDrvo.Stop();

                        timerHoda.Stop();
                        item.SendToBack();
                        var x = Rezultat;
                        Upis u = new Upis(x);
                        u.ShowDialog();
                        DialogResult dlg = MessageBox.Show("Nova igra?", "Izgubili ste", MessageBoxButtons.YesNo);
                        if (dlg == DialogResult.Yes)
                        {
                            //Brisanje elemenata zbog nove igre i pozivanje nove igre
                            while (objekti.Count!=0)
                            {
                                panel1.Controls.Remove(Controls.Find(objekti.ElementAt(0).Name, true)[0]);
                                objekti.RemoveAt(0);
                            }
                            
                            objekti.Clear();
                           
                            while (lista.Count!=0)
                            {
                                panel3.Controls.Remove(Controls.Find(lista.ElementAt(0).Name, true)[0]);
                                lista.RemoveAt(0);
                            }
                            lista.Clear();
                            this.NovaIgra();
                        }
                        //
                        else
                        {
                            this.Close();
                        }
                        
                        
                        break;
                       
                    }
                    else
                    {
                        //Udarac u coin
                        panel1.Controls.Remove(item);
                        Rezultat += 10;
                        label1.Text = Rezultat.ToString();
                        objekti.Remove(item);
                        break;
                    }
                }
            }
            //
            //Pomeranje drva
            if (lista.Count != 0)
            {
                foreach (var item in lista)
                {
                    item.Pomeri();
                    if (item.Left < -30)
                    {
                        lista.Remove(item);
                        panel3.Controls.Remove(item);
                        break;
                    }
                }
            }
        }
        private void Form1_KeyPressAsync(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
            if (upaljeno)
            {
                if (skace)
                {
                    //Vracanje u prvi panel
                    if ((e.KeyChar == 's') || (e.KeyChar == 'S'))
                    {
                        pictureBox1.Parent = panel1;

                        t1.Stop();
                    } 
                }
                if ((e.KeyChar == 'w') || (e.KeyChar == 'W'))
                {
                    //Menjanje panela zbog skoka
                    pictureBox1.Parent = panel2;
                    if (pictureBox1.Right <= pictureBox1.Parent.Width)
                    {
                        pictureBox1.Left += 30;
                    }
                    
                    t1.Interval = 1000;
                    t1.Start();
                    t1.Tick += Skoci;
                    //t2.Interval = 800;
                    //t2.Start();
                    //t2.Tick += Vrati;


                }

            }
           
        }
        private void Skoci(object o, EventArgs e)
        {
            //Vracanje panela zbog skoka
            pictureBox1.Parent = panel1;
            pictureBox1.Left = pictureBox1.Left + 30;

            t1.Stop();
            pictureBox1.Left-= 30;
        }
        public void NovaIgra()
        {
            
            tableLayoutPanel2.BringToFront();
            pictureBox1.Left = 0;
            t1 = new Timer();
            timer1 = new Timer();
            timer2 = new Timer();
            timerKreiDrvo = new Timer();
            timerHoda = new Timer();
            brzina = 0;
            Rezultat = 0;
            var lista = dp.UcitajSve();
            if (lista.Count > 0)
            {
                label4.Text = lista[0].Ime + "   " + lista[0].Score.ToString();
            }
            else
            {
                label4.Text = 0.ToString();
            }




            int a = pictureBox1.Left;
            slika = pictureBox1;
            slika.Tag = slika.Parent;
            pictureBox1.Parent = panel1;

            timer1.Interval = r.Next(1700, 1900);
            timer1.Tick += Kreiraj;
            timer1.Start();

            timer2.Interval = 10;
            timer2.Tick += Pomeraj;
            timer2.Start();
            label1.Text = "0";

            timerKreiDrvo.Interval = 1000;
            timerKreiDrvo.Tick += KrerajDrvo;
            timerKreiDrvo.Start();
            timerHoda.Interval = 200;
            timerHoda.Tick += Hoda;
            timerHoda.Start();
        }
    }
}
