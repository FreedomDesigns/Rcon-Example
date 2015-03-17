using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MW2_RCON
{
    class DisplayBox
    {
        public void Showme(string info)
        {
            MessageBox.Show(info + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            Environment.Exit(0);
            Application.Exit();
        }
    }
}
