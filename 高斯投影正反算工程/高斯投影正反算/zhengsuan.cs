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
    public partial class zhengsuan : Form
    {
        //大地经度 度分秒
        public double L;
        public double Ld;
        public double Lf;
        public double Lm;
        //大地纬度 度分秒
        public double B,B1;
        public double Bd;
        public double Bf;
        public double Bm;
        double L0;//中央子午线度数
        double N1;//带号
        double a = 6378245.0;//长半轴
        double b = 6356863.01877304;//短半轴
        double e1 = 0.0818133340198;//第一偏心率平方
        double e2 = 0.0820885218235; //第二偏心率平方
        double m0, m2, m4, m6, m8;

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {
            L0 = double.Parse(textBox7.Text.ToString());
        }

        double a0, a2, a4, a6, a8,a3,a5;

        private void zhengsuan_Load(object sender, EventArgs e)
        {

        }


        public zhengsuan()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {

            //将大地经度转化为度数
            Ld = double.Parse(textBox5.Text);
            Lf = double.Parse(textBox4.Text);
            Lm = double.Parse(textBox3.Text);
            L = Ld + Lf / 60 + Lm / 3600;
            //将大地纬度转化为度数
            Bd = double.Parse(TextBox6.Text);
            Bf = double.Parse(textBox1.Text);
            Bm = double.Parse(textBox2.Text);
            B = Bd + Bf / 60 + Bm / 3600;
            B1 = Bd * 3600+ Bf * 60 + Bm;
            if (comboBox1.Text == "三度带")
            {
                N1 = Math.Truncate((L + 1.5) / 3);//带号
                //L0 = 3 * N1;

            }
            if (comboBox1.Text == "六度带")
            {
                N1 = Math.Truncate(L / 6) + 1;
                //L0 = 6 * N1 - 3;

            }

            //求椭球面上某点到赤道的子午线弧长，精度至0.001m
            double radB = (Math.PI / 180) * B; //将纬度转换为弧度
            double Δ1, Δ2, Δ3, Δ4, Δ5;
            a0 = 32140.404 - Math.Cos(radB) * Math.Cos(radB) * (135.3302 - (0.7092 - 0.0040 * Math.Cos(radB) * Math.Cos(radB)) * Math.Cos(radB) * Math.Cos(radB));
            double N = 6399698.902 - (21562.267 - (108.973 - 0.612 * Math.Cos(radB) * Math.Cos(radB)) * Math.Cos(radB) * Math.Cos(radB)) * Math.Cos(radB) * Math.Cos(radB);
            a4 = (0.25 + 0.00252 * Math.Cos(radB) * Math.Cos(radB)) * Math.Cos(radB) * Math.Cos(radB) - 0.04166;
            a6 = (0.166 * Math.Cos(radB) * Math.Cos(radB) - 0.084) * Math.Cos(radB) * Math.Cos(radB);
            a3 = (0.3333333 + 0.001123 * Math.Cos(radB) * Math.Cos(radB)) * Math.Cos(radB) * Math.Cos(radB) - 0.1666667;
            a5 = 0.0083 - (0.1667 - (0.1968 + 0.0040 * Math.Cos(radB) * Math.Cos(radB)) * Math.Cos(radB) * Math.Cos(radB)) * Math.Cos(radB) * Math.Cos(radB);

            //double X = Math.Round(Δ1 - Δ2 + Δ3 - Δ4 + Δ5, 8);
            //卯酉圈曲率半径N
            //double N = a / Math.Sqrt(1 - e1 * Math.Sin(radB) * Math.Sin(radB));
            // tan(B)的值
            double t = Math.Tan(radB);
            // η的值
            double η = Math.Sqrt(e2) * Math.Cos(radB);
            //l的值
            double ρm = 180*3600/Math.PI;
            double l = ((L - L0) * 3600) / ρm;
            //高斯坐标中的x值和y值
            //double x = Math.Round(X + (N / 2) * t * Math.Cos(radB) * Math.Cos(radB) * l * l + (N / 24) * t * (5 - t * t + 9 * η * η + 4 * Math.Pow(η, 4)) * Math.Pow(Math.Cos(radB), 4) * Math.Pow(l, 4) + (N / 720) * t * (61 - 58 * t * t + Math.Pow(t, 4)) * Math.Pow(l, 6) * (Math.Pow(Math.Cos(radB), 6)), 3);
            //double y = Math.Round(N * Math.Cos(radB) * l + (N / 6.0) * (1 - t * t + η * η) * Math.Pow(Math.Cos(radB), 3) * Math.Pow(l, 3) + (N / 120.0) * (5 - 18 * t * t + Math.Pow(t, 4) + 14 * η * η - 58 * η * η * t * t) * Math.Pow(Math.Cos(radB), 5) * Math.Pow(l, 5), 3);
            double x = Math.Round((6367558.4969 * B1 / ρm) - (a0 - (0.5 + (a4 + a6 * l * l) * l * l) * l * l * N) * Math.Sin(radB) * Math.Cos(radB), 3);
            double y = Math.Round((1 + (a3 + a5 * l * l) * l * l) * l * N * Math.Cos(radB), 3);
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.Text = "大地经度为L:" + Ld + "°" + Lf + "′" + Lm + "″" + "        " + "大地经度为B:" + Bd + "°" + Bf + "′" + Bm + "″" + "\r\n";
            richTextBox1.Text += "大地坐标转高斯平面坐标后：" + "\r\n";
            richTextBox1.Text += "X坐标：" + x + "米" + "        " + "Y坐标：" + "" + y  + "米" + "\r\n";
            richTextBox1.Text += " l N a0 a4 a6 a3 a5分别为：" + l + " " + N + " " + a0 + " " + a4 + " " + a6 + " " + a3 + " " + a5 ;

        }
    }
}
