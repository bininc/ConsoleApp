using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp60
{
    public class Program0
    { 
        [STAThread]
        public static void Main(String[] args)
        {
            App app = new App();
            app.MainWindow = new MainWindow();
            app.Run(app.MainWindow);
           
        }
    }
}
