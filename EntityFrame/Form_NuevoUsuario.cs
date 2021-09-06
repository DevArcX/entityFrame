using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrame
{
    public partial class Form_NuevoUsuario : Form
    {
        Usuario_val oUsuario = new Usuario_val();
        public Form_NuevoUsuario()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            CrearUsuario();
        }
        private void CrearUsuario()
        {
            if (txbUsuario.Text != "" && txbContraseña.Text != "")
            {
                try
                {
                    oUsuario.NuevoUsuario(txbUsuario.Text.Trim(), txbContraseña.Text.Trim(), DateTime.Today.Date);
                    MessageBox.Show("Se registro el usuario: " + txbUsuario.Text + " Con exito");
                    txbContraseña.ResetText();
                    txbUsuario.ResetText();txbUsuario.Focus();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Form_NuevoUsuario_Paint(object sender, PaintEventArgs e)
        {
            int AnchoBorde = 2;
            Color ColorBorde = Color.FromArgb(46, 204, 113);
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid);
        }
    }
}
