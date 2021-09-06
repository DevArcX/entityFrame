using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EntityFrame
{
    public partial class Form_Loguin : Form
    {
        Usuario_val oUsuario = new Usuario_val();
        VerOculPass oPass = new VerOculPass();
        public Form_Loguin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            ValidarUsuario();
        }
        private void Form_Loguin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //usamos la dll de windows para poder mover el formulario desde un de sus objetos
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int WParam, int lParam);
        const int MinimizarVentana = 0x20000;
        const int RestaurarVentana = 0x8;

        // para minimizar a restaurar ventana desde icno de barra de estado
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= MinimizarVentana;
                cp.ClassStyle |= RestaurarVentana;
                return cp;
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnOcultar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolOcultar = new ToolTip();
            toolOcultar.SetToolTip(btnMostrar, "Clic para Ocultar la contraseña");
        }

        private void btnMostrar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolMostrar = new ToolTip();
            toolMostrar.SetToolTip(btnOcultar, "Clic para Mostrar la contraseña");
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            oPass.OcultarOMostrarPass(txbContraseña, btnOcultar, btnMostrar);
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            oPass.OcultarOMostrarPass(txbContraseña, btnOcultar, btnMostrar);
        }
        private void ValidarUsuario()
        {
            if (txbUsuario.Text != "" && txbContraseña.Text != "")
            {
                try
                {
                    int stado = oUsuario.ValidarUsuario(txbUsuario.Text, txbContraseña.Text);
                    if (stado == 1)
                    {
                        Form_Empresa abrir = new Form_Empresa();
                        abrir.Show();
                        this.Dispose();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
