using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEYENCE_inkjet_printing_control_DEMO.UserControls
{
    public partial class ucAdd : UserControl
    {
        public event EventHandler ItemAdded;
        public ucAdd()
        {
            InitializeComponent();
        }

        private void btnAddInkjet_Click(object sender, EventArgs e)
        {
            using (frmAddEdit frmAddEdit = new frmAddEdit())
            {
                if (frmAddEdit.ShowDialog() == DialogResult.OK)
                {
                    ItemAdded?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
