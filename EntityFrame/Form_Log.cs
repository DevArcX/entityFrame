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
    public partial class Form_Log : Form
    {
        public Form_Log()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Log_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {
            lblTitulo.Text = "¡Registro del Usuario, " + cache_Usuario.NomUsuario + "!";
            try
            {
                EmpresaEntities db = new EmpresaEntities();
                dgvLog.DataSource = db.Log_Usuario.ToList();
                ordenarDGV();
                dgvLog.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ordenarDGV()
        {
            dgvLog.Columns[0].Visible = false;
            dgvLog.Columns["NomUsuario"].HeaderText = "Usuario";
            dgvLog.Columns["Ruc"].HeaderText = "Ruc";
            dgvLog.Columns["Nomempresa"].HeaderText = "Empresa";
            dgvLog.Columns["FecAccion"].HeaderText = "Fecha";
        }
        private void Form_Log_Paint(object sender, PaintEventArgs e)
        {
            int AnchoBorde = 2;
            Color ColorBorde = Color.FromArgb(244, 187, 19);
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid);
        }
    }
}
