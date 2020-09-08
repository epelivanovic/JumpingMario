using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Igrica
{
    class Prepreka:Objekat
    {
     
        public override void Pomeri(int Prva)
        {
            if (Prva==1)
            {
                this.Left -= 15;
            }
            else if(Prva==0)
            {
                this.Left -= 10;
            }
            else
            {
                this.Left -= 20;
            }

        }
        public override int Oznaka()
        {
            return 1;
        }
        int bro = 0;
       public  Prepreka()
        {
            string ime = "prepreka" + (++bro);
            this.Name = ime;
            //this.ImageLocation = "...Slike/prepreka.png";
            //this.ImageLocation = "../Slike/prepreka.png";
            this.Image = Properties.Resources.prepreka;
            this.BackColor = Color.Transparent;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        }
    }
}
