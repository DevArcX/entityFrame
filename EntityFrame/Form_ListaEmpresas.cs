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
    public partial class Form_ListaEmpresas : Form
    {
        public Form_ListaEmpresas()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_ListaEmpresas_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {
            try
            {
                EmpresaEntities db = new EmpresaEntities();
                dgvempresa.DataSource = db.Empresa.ToList();
                ordenarDGV();
                dgvempresa.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ordenarDGV()
        {
            dgvempresa.Columns[0].Visible = false;
            dgvempresa.Columns["Nomempresa"].HeaderText = "Empresa";
            dgvempresa.Columns["Persona"].HeaderText = "Responsable";
            dgvempresa.Columns["FecRegistro"].HeaderText = "Fecha_Registro";
            dgvempresa.Columns["UltMod"].HeaderText = "Ultima_Modificación";
        }

        private void Form_ListaEmpresas_Paint(object sender, PaintEventArgs e)
        {
            int AnchoBorde = 2;
            Color ColorBorde = Color.FromArgb(155, 89, 182);
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid, ColorBorde, AnchoBorde, ButtonBorderStyle.Solid);
        }
    }
}
