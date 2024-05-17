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
    public partial class fansuan : Form
    {
        //大地经度 度分秒
        public double L;
        public double Ld;
        public double Lf;
        public double Lm;
        //大地纬度 度分秒
        public double B, B1;
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
        double a0, a2, a4, a6, a8, a3, a5;

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            L0 = double.Parse(textBox7.Text.ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        double β,Z,Nf,b2,b3,b4,b5,l;
        public fansuan()
        {
            InitializeComponent();
        }

        private void fansuan_Load(object sender, EventArgs e)
        {

        }
        int i;
        private void ButtonLogin_Click(object sender, EventArgs e)
        {

            //输入坐标x,y
            double x = double.Parse(TextBox6.Text);
            double y1 = double.Parse(textBox5.Text);
            double y = y1 - Math.Truncate(y1 / Math.Pow(10, 6)) * Math.Pow(10, 6) - 500000;
            double ρm = 180 * 3600 / Math.PI;
            
            β = Math.Round(ρm * x / 6367558.4969,4);
            double radβ = Math.Round((Math.PI / 180/3600) * β,9); //将纬度转换为弧度
            Bf = Math.Round(β + (50221746 + (293622 + (2350 + 22 * Math.Cos(radβ) * Math.Cos(radβ)) * Math.Cos(radβ) * Math.Cos(radβ)) * Math.Cos(radβ) * Math.Cos(radβ)) * 0.0000000001 * Math.Sin(radβ) * Math.Cos(radβ) * ρm,4);
            double RadBf = (Math.PI / 180 / 3600) * Bf;
            Z = Math.Round(y1 / (Nf * Math.Cos(RadBf)),9);
            Nf = Math.Round(6399698.902 - (21562.267 - (108.973 - 0.612 * Math.Cos(RadBf) * Math.Cos(RadBf)) * Math.Cos(RadBf) * Math.Cos(RadBf)) * Math.Cos(RadBf) * Math.Cos(RadBf),3);
            b2 = Math.Round((0.5 + 0.003369 * Math.Cos(RadBf) * Math.Cos(RadBf)) * Math.Sin(RadBf) * Math.Cos(RadBf),9);
            b3 = Math.Round(0.333333 - (0.166667 - 0.001123 * Math.Cos(Bf) * Math.Cos(Bf)) * Math.Cos(Bf) * Math.Cos(RadBf),9);
            b4 = Math.Round(0.25 + (0.161612 + 0.005617 * Math.Cos(RadBf) * Math.Cos(RadBf)) * Math.Cos(RadBf) * Math.Cos(RadBf),9);
            b5 = Math.Round(0.2 - (0.1667 - 0.0088 * Math.Cos(RadBf) * Math.Cos(RadBf)) * Math.Cos(RadBf) * Math.Cos(RadBf),9);
            B = (Bf - (1 - (b4 - 0.147 * Z * Z) * Z * Z) * Z * Z * b2 * ρm)/3600;
            l = Math.Round((1 - (b3 - b5 * Z * Z) * Z * Z) * Z * ρm,4);
            L = L0 + (l/3600);

            //转换经度
            double d1 = Math.Truncate(L);//度
            double f1 = Math.Truncate((L-d1) * 60);//分
            double m1 = Math.Round(((L - d1) * 60 - f1) * 60, 2);//秒
            if (m1 == 60)
            {
                m1 = 0;
                f1 += 1;
            }
            //求大地纬度B
            //B = Bf1[i] - (tf * y * y) / (2 * Mf * Nf) + tf * (5 + 3 * tf * tf + ηf - 9 * ηf * tf * tf) * Math.Pow(y, 4) / (24 * Mf * Math.Pow(Nf, 3)) + tf * (61 + 90 * tf * tf + 45 * Math.Pow(tf, 4)) * Math.Pow(y, 6) / (720 * Mf * Math.Pow(Nf, 5));
            double d = Math.Truncate(B);//度
            double f = Math.Truncate((B - d) * 60);//分
            double m = Math.Round(((B - d) * 60 - f) * 60, 2);//秒
            if (m == 60)
            {
                m = 0;
                f += 1;
            }
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.Text += "高斯平面坐标转大地坐标后：" + "\r\n";
            richTextBox1.Text += "大地坐标L：" + d1 + "°   " + f1 + "′  " + m1 + "″   " + "大地坐标B：" + d + "°   " + f + "′  " + m + "″   " + "\r\n";
            //richTextBox1.Text += " Bf β Z Nf b2 b3 b4 b5分别为：" + Bf + " " + β + " " + Z + " " + Nf + " " + b2 + " " + b3 + " " + b4 + " " + b5;
            //获取l的后面几位，计算后，再除以3600，再+L0

        }
    }
}
