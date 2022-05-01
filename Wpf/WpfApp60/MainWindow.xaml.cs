using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace WpfApp60
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCanvas_Click(object sender, RoutedEventArgs e)
        {
            new CanvasWindow().Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtRunInfo.Text = $"[.NET {Environment.Version} {(Environment.Is64BitProcess?"X64":"X86")}] {Assembly.GetEntryAssembly()?.Location}" ;
        }

        private void btnStackPanel_Click(object sender, RoutedEventArgs e)
        {
            new StackPanelWindow().Show();
        }

        private void btnWrapPanel_Click(object sender, RoutedEventArgs e)
        {
            new WrapPanelWindow().Show();
        }

        private void btnDockPanel_Click(object sender, RoutedEventArgs e)
        {
            new DockPanelWindow().Show();
        }

        private void btnGrid_Click(object sender, RoutedEventArgs e)
        {
            new GridWindow().Show();
        }

        private void btnUniformGrid_Click(object sender, RoutedEventArgs e)
        {
            new UniformGridWindow().Show();
        }

        private void btnScrollViewer_Click(object sender, RoutedEventArgs e)
        {
            new ScrollViewerWindow().Show();
        }

        private void btnCustomStackPanel_Click(object sender, RoutedEventArgs e)
        {
            new CustomStackPanelWindow().Show();
        }
    }
}
