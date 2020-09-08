using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace Igrica
{
    class Drvo:PictureBox
    {
        public  void Pomeri()
        {
            this.Left -= 10;
        }
        public  int Oznaka()
        {
            return 3;
        }
        int bro = 0;
        public Drvo()
        {
            string ime = "drvo" + (++bro);
            this.Name = ime;
            this.Image = Properties.Resources.drvo;
            this.BackColor = Color.Transparent;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        }
    }
}
