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
        private readonly SerialMonitor _serialMonitor;
        //What is the code
        private readonly int[] _password = {2,1,3};
        //What did the user enter
        private readonly List<int> _enteredCode = new List<int>();
        //Dummy var
        private string _light = "green";
        //All lights off
        private bool _lightsOn;
        //Timer lifetime
        private int _lifetime;

        public Form1() {
            InitializeComponent();
            //Regiser the monitor
            _serialMonitor = new SerialMonitor(RTBLogDSTE);
            //Set the name
            base.Text = @"C# Vault";
            //Hide the monitor
            Height = 210;
            //Check if any numbers in the password are more than 3 or less than 0,
            //if one is, crash the app 😏
            if (_password.Any(codePos => codePos < 1 || codePos > 3))
            {
                MessageBox.Show(@"Numbers in the code can't be lower then 1 or heiger then 3", @"Error"
                    ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                //This just crashes the app
                throw new ArgumentOutOfRangeException("_password", 
                    @"Numbers in the code can't be lower then 1 or heiger then 3");
            }
        }

        private void cbSerialMonitorDSte_CheckedChanged(object sender, EventArgs e)
        {
            //This checks if we should show the monitor
            Height = cbSerialMonitorDSte.Checked ? 450 : 210;
        }

        private void btnKey_Click(object sender, EventArgs e) {
            //Get the pressed button
            var pressedButton = (Button)sender;
            //Convert the button text to a number
            var pressedNumber = Convert.ToInt32(pressedButton.Text);
            //Add the pressed number to the history
            _enteredCode.Add(pressedNumber);
            //Write a message
            _serialMonitor.PrintLn("Input "+ _enteredCode.Count + " has been set to " + pressedNumber);
            switch (_enteredCode.Count)
            {
                //Set the first indicator to orange
                case 1:
                    pbIndicator1DSte.BackColor = Color.Orange;
                    //Set the second indicator to orange
                    break;
                case 2:
                    pbIndicator2DSte.BackColor = Color.Orange;
                    //Set the third indicator to orange and perform the checks
                    break;
                case 3:
                {
                    pbIndicator3DSte.BackColor = Color.Orange;
                    //Reset the timer lifespan
                    _lifetime = 0;
                    //Turn all lights off
                    _lightsOn = false;

                    var codeArray = _enteredCode.ToArray();
                    
                    //get the correct code
                    var correctCode = codeArray[0] == _password[0] &&
                                          codeArray[1] == _password[1] &&
                                          codeArray[2] == _password[2];
                    //Enable the timer
                    tmrBlinkDSte.Enabled = true;
                    //Clear the history
                    _enteredCode.Clear();
                    //Wat light should be pressed
                    _light = correctCode ? "green" : "red";
                    //Write another message
                    _serialMonitor.Print("Code correct? ");
                    if(correctCode) {
                        //Correct code
                        _serialMonitor.PrintLn(true, "g");
                        _serialMonitor.PrintLn("The vault is.... Unlocked", "g");
                    } else {
                        //Incorrect code
                        _serialMonitor.PrintLn(false, "R");
                        _serialMonitor.PrintLn("The vault is.... Locked", "r");
                    }

                    break;
                }
            }
        }

        private void aboutBtnDSte_Click(object sender, EventArgs e) {
            //Set the about message
            const string mAbout = "C# Vault.\n\n" +
                                   "Created by:\t\tDuncan \"duncte123\" Sterken\n" +
                                   "Thanks to:\t\tDuncan for coding\n" +
                                   "Date:\t\t\t16 Nov 2017";

            MessageBox.Show(mAbout, @"About");
            _serialMonitor.PrintLn(mAbout, "B");
        }

        private void tmrBlink_Tick(object sender, EventArgs e) {
            if (_lifetime >= 10) {
                //Kill the timer
                tmrBlinkDSte.Stop();
                //Reset the pbs
                pbIndicator1DSte.BackColor = DefaultBackColor;
                pbIndicator2DSte.BackColor = DefaultBackColor;
                pbIndicator3DSte.BackColor = DefaultBackColor;
            }
            switch(_light.ToLower()) {
                case "green":
                    pbColorGreen.BackColor = _lightsOn ? Color.LightGreen : Color.Green;
                    break;

                case "red":
                    pbColorRed.BackColor = _lightsOn ? Color.Red : Color.Maroon;
                    break;
            }
            
            _lifetime++;
            //Toggle the light
            _lightsOn = !_lightsOn;
        }

        private void btnClearDSte_Click(object sender, EventArgs e) {
            //Clear the monitor
            _serialMonitor.Clear();
        }

        private void cbCheatDSte_CheckedChanged(object sender, EventArgs e) {
            //Show the password 😱
            MessageBox.Show(@"The code is: [" + String.Join(", ", _password) + ']', @"Cheater!1!1!1");
            _serialMonitor.PrintLn("Code shown", "b");
        }
    }
}
