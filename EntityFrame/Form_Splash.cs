using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace EntityFrame
{
    public partial class Form_Splash : Form
    {
        int Segundos = 0;
        int stado = 0;
        public Form_Splash()
        {
            InitializeComponent();
            test_Conect();
        }
        private void test_Conect()
        {
            using(EmpresaEntities db=new EmpresaEntities())
            {
                try
                {
                    db.Empresa.ToList();
                    stado = 1;
                }
                catch (Exception ex)
                {
                    stado = 0;
                }
            }
        }
        private void timerTest_Tick(object sender, EventArgs e)
        {
            if (Segundos == 0)
            {
                lblStado.Text = "Inicializando sistema";
            }
            if (Segundos == 1)
            {
                lblStado.Text = "Iniciando el test de conexion con el servidor";
            }
            if (Segundos == 2)
            {
                if (stado == 1)
                {
                    lblStado.Text = "Servidor en Linea";
                }
                else
                {
                    lblStado.Text = "Servidor Fuera de linea";
                }
            }
            if (Segundos == 3)
            {
                if (stado == 1)
                {
                    lblStado.Text = "Cargando datos del sistema";
                }
                else
                {
                    lblStado.Text = "El sistema se cerrara en seguida";
                }
            }
            if (Segundos == 4)
            {
                if (stado == 1)
                {
                    lblStado.Text = "Abriendo sistema";
                }
                else
                {
                    timerTest.Stop();
                    Application.Exit();
                }
            }
            if (Segundos == 5)
            {
                if (stado == 1)
                {
                    timerTest.Stop();
                    Form_Loguin abrir = new Form_Loguin();
                    abrir.Show();
                    this.Hide();
                }
            }
            Segundos += 1;
        }

        private void Form_Splash_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            timerTest.Start();
        }
    }
}
