using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp60
{
    internal class Person : DependencyObject
    {
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Person), new PropertyMetadata("Learning Hard", OnValueChanged));

        public String Name
        {
            get { return (string)GetValue(NameProperty); }
            set
            {
                SetValue(NameProperty, value);
            }
        }

        private static void OnValueChanged(DependencyObject dpObj, DependencyPropertyChangedEventArgs e)
        {
            //
        }
    }
}
