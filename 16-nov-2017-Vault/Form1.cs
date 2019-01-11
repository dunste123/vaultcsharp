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
        //This holds the monitor
        SerialMonitor serialMonitor = null;
        //What is the code
        int[] password = new int[]{2,1,3};
        //What did the user enter
        List<int> enteredCode = new List<int>();
        //Dummy var
        String light = "green";
        //All lights off
        Boolean lightsOn = false;
        //Timer liftime
        int liftime = 0;

        public Form1() {
            InitializeComponent();
            //Regiser the monitor
            serialMonitor = new SerialMonitor(this.RTBLogDSTE);
            //Set the name
            this.Text = "C# Vault";
            //Hide the monitor
            this.Height = 210;
            //Check if any numbers in the password are more than 3 or less than 0,
            //if one is, crash the app 😏
            foreach (int codePos in this.password) {
                if(codePos < 1 || codePos > 3) {
                    MessageBox.Show("Numbers in the code can't be lower then 1 or heiger then 3", "Error"
                        ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //This just crashes the app
                    throw new ArgumentOutOfRangeException("Numbers in the code can't be lower then 1 or heiger then 3");
                }
            }
        }

        private void cbSerialMonitorDSte_CheckedChanged(object sender, EventArgs e) {
            //This checks if we should show the monitor
            if (cbSerialMonitorDSte.Checked) {
                this.Height = 450;
            } else {
                this.Height = 210;
            }
        }

        private void btnKey_Click(object sender, EventArgs e) {
            //Get the pressed button
            Button pressedButton = (Button)sender;
            //Convert the button text to a number
            int pressedNumber = Convert.ToInt32(pressedButton.Text);
            //Add the pressed number to the history
            enteredCode.Add(pressedNumber);
            //Write a message
            serialMonitor.PrintLn("Input "+ enteredCode.Count + " has been set to " + pressedNumber);
            //Set the first indicator to orange
            if (enteredCode.Count == 1) {
                this.pbIndicator1DSte.BackColor = Color.Orange;
            //Set the second indicator to orange
            } else if(enteredCode.Count == 2) {
                this.pbIndicator2DSte.BackColor = Color.Orange;
            //Set the third indicator to orange and perform the checks
            } else if (enteredCode.Count == 3) {
                this.pbIndicator3DSte.BackColor = Color.Orange;
                //Reset the timer lifespan
                liftime = 0;
                //Turn all lights off
                lightsOn = false;
                //get the correct code
                Boolean correctCode = enteredCode.ToArray()[0] == password[0] &&
                    enteredCode.ToArray()[1] == password[1] &&
                    enteredCode.ToArray()[2] == password[2];
                //Enable the timer
                tmrBlinkDSte.Enabled = true;
                //Clear the history
                enteredCode.Clear();
                //Wat light should be pressed
                light = correctCode ? "green" : "red";
                //Write another message
                serialMonitor.Print("Code correct? ");
                if(correctCode) {
                    //Correct code
                    serialMonitor.PrintLn(correctCode, "g");
                    serialMonitor.PrintLn("The vault is.... Unlocked", "g");
                } else {
                    //Incorrect code
                    serialMonitor.PrintLn(correctCode, "R");
                    serialMonitor.PrintLn("The vault is.... Locked", "r");
                }
            }
        }

        private void aboutBtnDSte_Click(object sender, EventArgs e) {
            //Set the about message
            String m_about = "C# Vault.\n\n" +
                "Created by:\t\tDuncan \"duncte123\" Sterken\n" +
                "Thanks to:\t\tDuncan for coding\n" +
                "Date:\t\t\t16 Nov 2017";

            MessageBox.Show(m_about, "About");
            serialMonitor.PrintLn(m_about, "B");
        }

        private void tmrBlink_Tick(object sender, EventArgs e) {
            if (liftime >= 10) {
                //Kill the timer
                tmrBlinkDSte.Stop();
                //Reset the pbs
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
            //Toggle the light
            lightsOn = !lightsOn;
        }

        private void btnClearDSte_Click(object sender, EventArgs e) {
            //Clear the monitor
            serialMonitor.Clear();
        }

        private void cbCheatDSte_CheckedChanged(object sender, EventArgs e) {
            //Show the password 😱
            MessageBox.Show("The code is: [" + String.Join(", ", password) + "]", "Cheater!1!1!1");
            serialMonitor.PrintLn("Code shown", "b");
        }
    }
}
