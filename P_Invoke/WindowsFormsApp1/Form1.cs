using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string Path = @"RFLOCKDLL.dll";

        [DllImport(Path, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern StringBuilder W_Card(string room, string startTime, string days, string endTime, string beep, string isAlarm, string user, string isCover, string lift);
        [DllImport(Path, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string R_Card(int i_dispaly);
        [DllImport(Path, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string Rct_Card(int i_dispaly);
        [DllImport(Path, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern string Getcardid(int IntnPort);
        [DllImport(Path, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int Woff_Card();

        private void btnTest_Click(object sender, EventArgs e)
        {
            var result = W_Card("8305", "201911201310", "1", "1400", "1", "1", "8888", "1",
                "1+2+3+E+5+6+7+8+9+10+11+12+13+14+15+16+17+18+19+20+21+22+23+24+25+26+27+28+29+30");
            textBox1.Text = result.ToString();
            
            /*string writeData = "8306,201911200800,1,1400,1,1,8888,1,1+2+3+E+5+6+7+8+9+10+11+12+13+14+15+16+17+18+19+20+21+22+23+24+25+26+27+28+29+30";
            string logFileName = @".\W-R-Card\Log" + DateTime.Now.ToString("yyyyMM");
            File.AppendAllText(logFileName, $"DllIn:{writeData}:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\n");
            string writeFileName = @".\W-R-Card\W-RCard_In.txt";
            File.WriteAllText(writeFileName, writeData + "\n");
            Process.Start(new ProcessStartInfo(@".\W-R-Card\WriteCard.exe") { CreateNoWindow = false, UseShellExecute = true});*/
        }
    }
}
