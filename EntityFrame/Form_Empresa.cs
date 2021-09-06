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
using System.Threading;


namespace EntityFrame
{
    public partial class Form_Empresa : Form
    {
        public Form_Empresa()
        {
            InitializeComponent();
            Controles(false);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //usamos la dll de windows para poder mover el formulario desde un de sus objetos
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int WParam, int lParam);
        const int MinimizarVentana = 0x20000;
        const int RestaurarVentana = 0x8;

        private void pnlBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
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
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            cache_Empresa.RucActual = "";
            Controles(false);
            txbRuc.Enabled = true;
            txbRuc.Focus();
        }
        private void Controles(bool stado)
        {
            btnEliminar.Enabled = stado;
            gbBusEmpresa.Enabled = stado;
            if (stado == false)
            {
                gbDatosEmpresa.Enabled = true;
            }
            else
            {
                gbDatosEmpresa.Enabled = false;
            }
        }
        private void Limpiar()
        {
            txbRuc.Text = "";
            txbNomEmpresa.Text = "";
            txbPersona.Text = "";
            txbCelular.Text = "";
            txbDireccion.Text = "";
            txbRucBus.Text = "";
            txbNomEmpresaBus.Text = "";
            chbTieneCuenta.Checked = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Limpiar();
            cache_Empresa.RucActual = "";
            Controles(true);
            txbRucBus.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cache_Empresa.RucActual != "")
            {
                using (EmpresaEntities db = new EmpresaEntities())
                {
                    try
                    {
                        Empresa Em = db.Empresa.FirstOrDefault(x => x.RUC == cache_Empresa.RucActual);
                        db.Empresa.Remove(Em);
                        db.SaveChanges();
                        MessageBox.Show("la empresa: " + txbNomEmpresa.Text + " Se elimino correctamente");
                        btnNuevo.PerformClick();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe selecionar un cliente de la lista para Eliminar");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txbNomEmpresa.Text != "" && txbRuc.Text != "" && txbDireccion.Text != "")
            {
                try
                {
                    //Actualizar = Editar
                    if (cache_Empresa.RucActual != "")
                    {
                        using(EmpresaEntities db=new EmpresaEntities())
                        {
                            try
                            {
                                Empresa Em = db.Empresa.FirstOrDefault(x => x.RUC == cache_Empresa.RucActual);
                                Em.NomEmpresa = txbNomEmpresa.Text;
                                Em.Persona = txbPersona.Text;
                                Em.Celuar = txbCelular.Text;
                                Em.Direccion = txbDireccion.Text;
                                Em.UltMod = DateTime.Today.Date;
                                db.SaveChanges();
                                MessageBox.Show("la empresa: " + txbNomEmpresa.Text + " Se edito correctamente");
                                btnEditar.PerformClick();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    //Nuevo = Insertar
                    else
                    {
                        using(EmpresaEntities db=new EmpresaEntities())
                        {
                            var lst = from d in db.Empresa
                                      where d.RUC == txbRuc.Text
                                      select d;
                            if (lst.Count() == 0)
                            {
                                Empresa Em = new Empresa();
                                Em.RUC = txbRuc.Text;
                                Em.NomEmpresa = txbNomEmpresa.Text;
                                Em.Persona = txbPersona.Text;
                                Em.Celuar = txbCelular.Text;
                                Em.Direccion = txbDireccion.Text;
                                Em.Cuenta = chbTieneCuenta.Checked;
                                Em.FecRegistro = DateTime.Today.Date;
                                db.SaveChanges();
                                MessageBox.Show("la empresa: "+txbNomEmpresa.Text+" Se registro correctamente");
                                Limpiar();
                            }
                            else
                            {
                                MessageBox.Show("El ruc ingresado ya esta en uso. Ingrese otro ruc diferente");
                            }
                        }
                    }
                    Limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Se generó un error:" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Es necesario llenar todos los campos");
            }
        }

        private void txbRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void btnLogUsuario_Click(object sender, EventArgs e)
        {
            Form_Log abrir = new Form_Log();
            abrir.ShowDialog();
        }

        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            Form_NuevoUsuario abrir = new Form_NuevoUsuario();
            abrir.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Form_Loguin abrir = new Form_Loguin();
            abrir.Show();
            cache_Empresa.RucActual = "";
            cache_Usuario.NomUsuario = "";
            this.Close();
        }

        private void txbRuc_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txbCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void Form_Empresa_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = "Mantenimiento de empresa   |   Usuario, " + cache_Usuario.NomUsuario;
            btnNuevoUsuario.Enabled = false;
        }
        private void buscar()
        {
            if (txbRucBus.Text != "" && txbNomEmpresaBus.Text == "" || txbNomEmpresaBus.Text != "" && txbRucBus.Text == "")
            {
                using (EmpresaEntities db = new EmpresaEntities())
                {
                    IQueryable<Empresa> lst = from d in db.Empresa
                                              select d;
                    if (txbNomEmpresaBus.Text != "")
                    {
                        lst = from d in db.Empresa
                              where d.NomEmpresa == txbNomEmpresaBus.Text
                              select d;
                    }
                    if (txbRucBus.Text != "")
                    {
                        lst = from d in db.Empresa
                              where d.RUC == txbRucBus.Text
                              select d;
                    }

                    if (lst.Count() > 0)
                    {
                        txbRuc.Text = lst.ElementAt(1).ToString();
                        cache_Empresa.RucActual = lst.ElementAt(1).ToString();
                        txbNomEmpresa.Text = lst.ElementAt(2).ToString();
                        txbPersona.Text = lst.ElementAt(3).ToString();
                        chbTieneCuenta.Checked = Convert.ToBoolean(lst.ElementAt(4));
                        txbCelular.Text = lst.ElementAt(5).ToString();
                        txbDireccion.Text = lst.ElementAt(6).ToString();
                        Controles(false);
                        btnEliminar.Enabled = true;
                        txbRuc.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("La empresa en busqueda no existe");
                        txbRucBus.ResetText();
                        txbNomEmpresaBus.ResetText(); txbNomEmpresaBus.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("No se admiten busquedas por Ruc y Nombre de Empresa al mismo tiempo, bien busca por Ruc o Nombre de la empresa");
                txbRucBus.ResetText();
                txbNomEmpresaBus.ResetText();txbNomEmpresaBus.Focus();
            }
        }
    }
}
