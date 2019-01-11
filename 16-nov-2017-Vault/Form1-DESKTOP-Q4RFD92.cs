using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16_nov_2017_Vault {
    public partial class Form1 : Form {
        SerialMonitor serialMonitor = null;
        int[] password = new int[]{2,1,3};
        List<int> enteredCode = new List<int>();
        String light = "green";

        public Form1() {
            InitializeComponent();
            serialMonitor = new SerialMonitor(this.RTBLogDSTE);
            this.Text = "C# Vault";
            this.Height = 150;
            foreach(int codePos in this.password) {
                if(codePos < 1 || codePos > 3) {
                    throw new ArgumentOutOfRangeException("Code can't be lower then 0 or heiger then 3");
                }
            }
        }

        private void cbSerialMonitorDSte_CheckedChanged(object sender, EventArgs e) {
            if (cbSerialMonitorDSte.Checked == true) {
                this.Height = 500;
            } else {
                this.Height = 150;
            }
        }

        private void btnKey_Click(object sender, EventArgs e) {
            Button pressedButton = (Button)sender;
            int pressedNumber = Convert.ToInt32(pressedButton.Text);
            serialMonitor.PrintLn("Number Pressed: " + pressedNumber, "g");
            enteredCode.Add(pressedNumber);
            if(enteredCode.Count == 1) {
                this.pbIndicator1DSte.BackColor = Color.Orange;
            } else if(enteredCode.Count == 2) {
                this.pbIndicator2DSte.BackColor = Color.Orange;
            } else   if (enteredCode.Count == 3) {
                this.pbIndicator3DSte.BackColor = Color.Orange;
                liftime = 0;
                lightsOn = false;
                Boolean correctCode = enteredCode.ToArray()[0] == password[0] &&
                    enteredCode.ToArray()[1] == password[1] &&
                    enteredCode.ToArray()[2] == password[2];
                serialMonitor.PrintLn("Code correct? " + correctCode);
                tmrBlinkDSte.Enabled = true;
                enteredCode.Clear();
                serialMonitor.Print("The correct code is: ");
                serialMonitor.PrintLn("["+ String.Join(", ", password) + "]");
                light = correctCode ? "green" : "red";
            }
        }

        private void aboutBtnDSte_Click(object sender, EventArgs e) {
            String m_about = "C# Vault.\n\n" +
                "Created by:\t\tDuncan \"duncte123\" Sterken\n" +
                "Date:\t\t\t16 Nov 2017";

            MessageBox.Show(m_about, "About");
            serialMonitor.PrintLn(m_about, "B");
        }

        Boolean lightsOn = false;
        int liftime = 0;
        private void tmrBlink_Tick(object sender, EventArgs e) {
            if (liftime >= 10) {
                //Kill
                tmrBlinkDSte.Stop();
                this.pbIndicator1DSte.BackColor = DefaultBackColor;
                this.pbIndicator2DSte.BackColor = DefaultBackColor;
                this.pbIndicator3DSte.BackColor = DefaultBackColor;
            }
            switch(light.ToLower()) {
                case "green":
                    if (lightsOn) {
                        this.pbColorGreen.BackColor = Color.LightGreen;
                    } else {
                        this.pbColorGreen.BackColor = Color.Green;
                    }
                    break;

                case "red":
                    if (lightsOn) {
                        this.pbColorRed.BackColor = Color.Red;
                    } else {
                        this.pbColorRed.BackColor = Color.Maroon;
                    }
                    break;
            }
            liftime++;
            lightsOn = !lightsOn;
        }

        private void btnClearDSte_Click(object sender, EventArgs e) {
            serialMonitor.Clear();
        }
    }
}
