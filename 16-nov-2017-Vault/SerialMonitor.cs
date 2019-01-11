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
    public partial class SerialMonitor {
        RichTextBox theBox;

        /// <summary>
        /// This is the constructor
        /// </summary>
        /// <param name="box">Where to print the text</param>
        public SerialMonitor(RichTextBox box) {
            this.theBox = box;
        }

        /// <summary>
        /// This is the constructor
        /// </summary>
        public SerialMonitor() {
            this.theBox = this.RTBLogDSTEHIDDEN;
        }

        /// <summary>
        /// This prints a message with a new line in the monitor
        /// </summary>
        /// <param name="a_text">the text</param>
        /// <param name="a_color">the color</param>
        public void PrintLn(Object a_text, String a_color) {

            switch (a_color.ToUpper()) {
                case "R": theBox.SelectionColor = Color.Red; break;
                case "G": theBox.SelectionColor = Color.Green; break;
                case "B": theBox.SelectionColor = Color.Blue; break;
                default: theBox.SelectionColor = Color.White; break;
            }

            theBox.AppendText(a_text + "\n");
            theBox.ScrollToCaret();
        }

        /// <summary>
        /// This prints a message in the monitor
        /// </summary>
        /// <param name="a_text">the text</param>
        /// <param name="a_color">the color</param>
        public void Print(Object a_text, String a_color) {

            switch (a_color.ToUpper()) {
                case "R": theBox.SelectionColor = Color.Red; break;
                case "G": theBox.SelectionColor = Color.Green; break;
                case "B": theBox.SelectionColor = Color.Blue; break;
                default: theBox.SelectionColor = Color.White; break;
            }

            theBox.AppendText(Convert.ToString(a_text));
            theBox.ScrollToCaret();
        }

        /// <summary>
        /// This prints a message with a new line in the monitor
        /// </summary>
        /// <param name="a_text">the text to print</param>
        public void PrintLn(Object a_text) {
            PrintLn(a_text, "W");
        }

        /// <summary>
        /// This prints a message in the monitor
        /// </summary>
        /// <param name="a_text">the text to print</param>
        public void Print(Object a_text) {
            Print(a_text, "W");
        }

        /// <summary>
        /// This clears the monitor
        /// </summary>
        public void Clear() {
            theBox.Clear();
            theBox.ScrollToCaret();
            PrintLn("Cleared");
            theBox.ScrollToCaret();
        }

    }
}
