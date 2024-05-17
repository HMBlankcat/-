using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 高斯投影正反算
{

    public partial class 反算 : Form
    {
        const double pi = 3.141592653589793238463;
        const double p0 = 206264.8062470963551564;

        // WGS84 reference ellipsoid parameters
        const double e = 0.00669438002290;
        const double e1 = 0.00673949677548;
        const double b = 6356752.3141;
        const double a = 6378137.0;
        public 反算()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 反算_Load(object sender, EventArgs e)
        {

        }
    }
}
