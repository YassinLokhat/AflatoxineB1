using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AflatoxineB1
{
    public class Echantillon
    {
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox tAbs;
        private System.Windows.Forms.Label lNumEchantillon;
        private System.Windows.Forms.Label lPpb;
        private System.Windows.Forms.Panel gEchantillons;

        public int Num { get; private set; }
        public static int LastNumber = 0;

        Form1 parent;

        public Echantillon(Form1 parent)
        {
            // 
            // gEchantillons
            // 
            this.gEchantillons = parent.panel;
            this.Num = LastNumber++;

            this.parent = parent;
            parent.bAdd.Location = new System.Drawing.Point(parent.bAdd.Location.X, parent.bAdd.Location.Y + 25);

            // 
            // lNumEchantillon
            // 
            this.lNumEchantillon = new Label();
            this.lNumEchantillon.AutoSize = true;
            this.lNumEchantillon.Location = new System.Drawing.Point(12, parent.bAdd.Location.Y - 25);
            this.lNumEchantillon.Name = "lNumEchantillon";
            this.lNumEchantillon.Size = new System.Drawing.Size(101, 13);
            this.lNumEchantillon.Text = "n° " + (Num + 1) + " - Absorbance  :";
            // 
            // tAbs
            // 
            this.tAbs = new TextBox();
            this.tAbs.Location = new System.Drawing.Point(119, parent.bAdd.Location.Y - 25 - 2); 
            this.tAbs.Name = "tAbs";
            this.tAbs.Size = new System.Drawing.Size(100, 20);
            this.tAbs.TabIndex = Num + parent.bAdd.TabIndex + 1;
            this.tAbs.TextChanged += tAbs_TextChanged;
            // 
            // label
            // 
            this.label = new Label();
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(225, parent.bAdd.Location.Y - 25);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(28, 13);
            this.label.Text = "----->";
            // 
            // lPpb
            // 
            this.lPpb = new Label();
            this.lPpb.AutoSize = true;
            this.lPpb.Location = new System.Drawing.Point(259, parent.bAdd.Location.Y - 25);
            this.lPpb.Name = "lPpb";
            this.lPpb.Size = new System.Drawing.Size(46, 13);
            this.lPpb.Text = "";

            this.gEchantillons.Controls.Add(this.lPpb);
            this.gEchantillons.Controls.Add(this.label);
            this.gEchantillons.Controls.Add(this.tAbs);
            this.gEchantillons.Controls.Add(this.lNumEchantillon);
        }

        void tAbs_TextChanged(object sender, EventArgs e)
        {
            double x;
            if (!double.TryParse(tAbs.Text.Replace(".", ","), out x))
                return;

            x = Math.Round(x / parent.absorbance[0] * parent.B_B0[0]);

            //int ppb = (int)Math.Round(Math.Exp(parent.a * x) * parent.b);
            int ppb = (int)Math.Round(Math.Pow(parent.a, x) * parent.b);
            lPpb.Text = "ppb : " + ppb;
        }
    }
}
