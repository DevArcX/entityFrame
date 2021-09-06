using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrame
{
    public class VerOculPass
    {
        public void OcultarOMostrarPass(TextBox txbOculVer, PictureBox btnVer, PictureBox btnOcul)
        {
            if (txbOculVer.PasswordChar == '\0')
            {
                txbOculVer.PasswordChar = '*';
                btnOcul.Visible = false;
                btnVer.Visible = true;
            }
            else
            {
                txbOculVer.PasswordChar = '\0';
                btnOcul.Visible = true;
                btnVer.Visible = false;
            }

        }
    }
}
