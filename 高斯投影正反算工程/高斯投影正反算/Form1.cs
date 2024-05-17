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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            zhengsuan zhengsuan = new zhengsuan();//新建窗口
            this.Hide();//隐藏当前窗口
            zhengsuan.ShowDialog();//独立显示新窗口
            this.Show();//显示当前窗口
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fansuan fansuan = new fansuan();//新建窗口
            this.Hide();//隐藏当前窗口
            fansuan.ShowDialog();//独立显示新窗口
            this.Show();//显示当前窗口
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
