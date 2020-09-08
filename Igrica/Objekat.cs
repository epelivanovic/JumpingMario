using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Igrica
{
   public abstract class Objekat:PictureBox
    {
        public abstract void Pomeri(int prva);
        public abstract int Oznaka();
        public bool Kontakt(PictureBox slika)
        {
            if (this.Left<=slika.Right && this.Tag==slika.Tag && this.Right>=slika.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
           
        
    }
}
