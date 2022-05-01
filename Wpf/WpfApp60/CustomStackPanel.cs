using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp60
{
    public class CustomStackPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            Size panelDesiredSize = new Size();
            foreach (UIElement child in this.InternalChildren)
            {
                child.Measure(availableSize);

                // 子元素的期望大小
                panelDesiredSize.Width += child.DesiredSize.Width;
                panelDesiredSize.Height += child.DesiredSize.Height;
            }

            return panelDesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = 10;
            double y = 10;
            foreach (UIElement child in this.InternalChildren)
            {
                // 排列子元素的位置
                child.Arrange(new Rect(new Point(x, y), new Size(finalSize.Width - 10, child.DesiredSize.Height)));
                y += child.RenderSize.Height + 5;
            }

            return finalSize;
        }
    }
}
