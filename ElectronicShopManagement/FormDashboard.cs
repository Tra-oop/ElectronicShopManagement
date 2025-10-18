using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicShopManagement.Forms
{
    public partial class FormDashboard : Form
    {
        private Form activeForm = null;

        private void OpenchildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelshowitem1.Controls.Clear();
            this.panelshowitem1.Controls.Add(childForm);
            this.panelshowitem1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            toplabel.Text = childForm.Text;
        }
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void panelshowitem1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenchildForm(new Forms.Home());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenchildForm(new Forms.Products_Stock());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenchildForm(new Forms.Sell());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenchildForm(new Forms.Report());

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            OpenchildForm(new Forms.Home());

        }

        private void panelshowitem1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
