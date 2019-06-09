using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AflatoxineB1
{
    public partial class Form1 : Form
    {
        public double[] ppb = { 0, 1, 5, 20, 40 };
        public double[] absorbance = new double[5];
        public double[] B_B0 = new double[5];
        //public double a, b;
 
        public Form1()
        {
            InitializeComponent();

            B_B0[0] = 100;

            new Echantillon(this);

            bAdd.Click += bAdd_Click;
            tStd1.TextChanged += tStd_TextChanged;
            tStd2.TextChanged += tStd_TextChanged;
            tStd3.TextChanged += tStd_TextChanged;
            tStd4.TextChanged += tStd_TextChanged;
            tStd5.TextChanged += tStd_TextChanged;

            bValider.Click += bValider_Click;
        }

        void bValider_Click(object sender, EventArgs e)
        {
            absorbance[0] = double.Parse(tStd1.Text.Replace(".", ","));
            absorbance[1] = double.Parse(tStd2.Text.Replace(".", ","));
            absorbance[2] = double.Parse(tStd3.Text.Replace(".", ","));
            absorbance[3] = double.Parse(tStd4.Text.Replace(".", ","));
            absorbance[4] = double.Parse(tStd5.Text.Replace(".", ","));
            
            for (int i = 1; i < 5; i++)
                B_B0[i] = (int)Math.Round(absorbance[i] / absorbance[0] * B_B0[0]);

            CalculAB(B_B0, ppb);
            
            panel.Enabled = true;
        }

        private void CalculAB(double[] X, double[] Y)
        {
            double sX = 0, sX2 = 0, sXY = 0, sY = 0;
            int n = X.Count();

            for (int i = 1; i < n; i++)
            {
                sX += X[i];
                sY += Ln(Y[i]);
                sX2 += X[i] * X[i];
                sXY += X[i] * Ln(Y[i]);
            }

            b = (sX * sXY - sX2 * sY) / (sX * sX - (n - 1) * sX2);
            //a = (sY - (n - 1) * b) / sX;
            a = (sXY - sX * b) / sX2;

            b = Math.Exp(b);
            a = Math.Exp(a);
        }

        void tStd_TextChanged(object sender, EventArgs e)
        {
            DisableAll();

            if (AbsorbanceValide())
                bValider.Enabled = true;
        }

        void bAdd_Click(object sender, EventArgs e)
        {
            new Echantillon(this);            
        }

        private void DisableAll()
        {
            bValider.Enabled = false;
            panel.Enabled = false;
            panel.Controls.Clear();
            bAdd.Location = new System.Drawing.Point(50, 15);
            panel.Controls.Add(bAdd);
            Echantillon.LastNumber = 0;
            new Echantillon(this);
        }

        private bool AbsorbanceValide()
        {
            double i;

            if (!double.TryParse(tStd1.Text.Replace(".", ","), out i))
                return false;
            if (!double.TryParse(tStd2.Text.Replace(".", ","), out i))
                return false;
            if (!double.TryParse(tStd3.Text.Replace(".", ","), out i))
                return false;
            if (!double.TryParse(tStd4.Text.Replace(".", ","), out i))
                return false;
            if (!double.TryParse(tStd5.Text.Replace(".", ","), out i))
                return false;

            return true;
        }

        public static double Ln(double x)
        {
            return Math.Log(x, Math.E);
        }
    }
}
