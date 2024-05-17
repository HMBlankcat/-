using System;
using System.Windows.Forms;

namespace 高斯投影正反算
{
    public partial class 正算 : Form
    {
        public 正算()
        {
            InitializeComponent();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            //大地经度 度分秒
        public double L;
        public double Ld;
        public double Lf;
        public double Lm;
        //大地纬度 度分秒
        public double B;
        public double Bd;
        public double Bf;
        public double Bm;
        double L0;//中央子午线度数
        double N1;//带号
        double a;//长半轴
        double b;//短半轴
        double e1;//第一偏心率平方
        double e2; //第二偏心率平方
        double m0, m2, m4, m6, m8;
        double a0, a2, a4, a6, a8;
        int i;

            const double pi = 3.141592653589793238463;
            const double p0 = 206264.8062470963551564;

            // WGS84 reference ellipsoid parameters
            const double evalue = 0.081813334017;
            const double e1 = 0.0820885218266;
            const double b = 6356863.01877304;
            const double a = 6378245.0;
            
            //double n, t, n, c, v, xz, m1, m2, m3, m4, m5, m6, a0, a2, a4, a6, a8, m0, m2, m4, m6, m8, x0, y0, l;

            //int l_num;
            //double l_center;

            //l_num = (int)(l * 180 / pi / 6.0) + 1;
            //l_center = 6 * l_num - 3;

            //l = (l / pi * 180 - l_center) * 3600;

            //m0 = a * (1 - evalue);
            //m2 = 3.0 / 2.0 * evalue * m0;
            //m4 = 5.0 / 4.0 * evalue * m2;
            //m6 = 7.0 / 6.0 * evalue * m4;
            //m8 = 9.0 / 8.0 * evalue * m6;

            //a0 = m0 + m2 / 2.0 + 3.0 / 8.0 * m4 + 5.0 / 16.0 * m6 + 35.0 / 128.0 * m8;
            //a2 = m2 / 2.0 + m4 / 2 + 15.0 / 32.0 * m6 + 7.0 / 16.0 * m8;
            //a4 = m4 / 8.0 + 3.0 / 16.0 * m6 + 7.0 / 32.0 * m8;
            //a6 = m6 / 32.0 + m8 / 16.0;
            //a8 = m8 / 128.0;

            //xz = a0 * b - a2 / 2.0 * math.sin(2 * b) + a4 / 4.0 * math.sin(4 * b) - a6 / 6.0 * math.sin(6 * b) + a8 / 8.0 * math.sin(8 * b);
            //c = a * a / b;
            //v = math.sqrt(1 + e1 * math.cos(b) * math.cos(b));
            //n = c / v;
            //t = math.tan(b);
            //n = e1 * math.cos(b) * math.cos(b);

            //m1 = n * math.cos(b);
            //m2 = n / 2.0 * math.sin(b) * math.cos(b);
            //m3 = n / 6.0 * math.pow(math.cos(b), 3) * (1 - t * t + n);
            //m4 = n / 24.0 * math.sin(b) * math.pow(math.cos(b), 3) * (5 - t * t + 9 * n);
            //m5 = n / 120.0 * math.pow(math.cos(b), 5) * (5 - 18 * t * t + math.pow(t, 4) + 14 * n - 58 * n * t * t);
            //m6 = n / 720.0 * math.sin(b) * math.pow(math.cos(b), 5) * (61 - 58 * t * t + math.pow(t, 4));
            //x0 = xz + m2 * l * l / math.pow(p0, 2) + m4 * math.pow(l, 4) / math.pow(p0, 4) + m6 * math.pow(l, 6) / math.pow(p0, 6);
            //y0 = m1 * l / p0 + m3 * math.pow(l, 3) / math.pow(p0, 3) + m5 * math.pow(l, 5) / math.pow(p0, 5);

            //double x = x0;
            //double y = y0 + 500000;

        }

        private void B_TextChanged(object sender, EventArgs e)
        {

        }

        private void 正算_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
