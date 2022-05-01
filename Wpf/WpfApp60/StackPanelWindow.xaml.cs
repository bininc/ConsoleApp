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
using System.Windows.Shapes;

namespace WpfApp60
{
    /// <summary>
    /// StackPanelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StackPanelWindow : Window
    {
        public StackPanelWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (null == stackPanel) return;
            stackPanel.Orientation = Orientation.Horizontal;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if (null == stackPanel) return;
            stackPanel.Orientation = Orientation.Vertical;
        }
    }
}
