using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TSP.Assets
{
    public class Point
    {
        private double _x;
        private double _y;

        public double x 
        { 
            get
            {
                return _x;
            }
            set 
            {
                _x = value;
            }
        }

        public double y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public Point (double X, double Y, Canvas cv, int index)
        {
            x = X;
            y = Y;

            Ellipse elipse = new Ellipse()
            {
                Width = 20,
                Height = 20,
                Fill = new BrushConverter().ConvertFromString("White") as Brush
            };

            Label label = new Label()
            {
                FontSize = 13,
                Content = Convert.ToString(index + 1),
                FontWeight = FontWeights.Bold
            };

            cv.Children.Add(elipse);
            cv.Children.Add(label);

            elipse.SetValue(Canvas.LeftProperty, this.x);
            elipse.SetValue(Canvas.TopProperty,  this.y);

            if(index > 9)
            {
                label.SetValue(Canvas.LeftProperty, this.x - 3);
                label.SetValue(Canvas.TopProperty, this.y - 5);
            }
            else
            {
                label.SetValue(Canvas.LeftProperty, this.x + 1);
                label.SetValue(Canvas.TopProperty, this.y - 5);
            }
            
            Canvas.SetZIndex(label, 3);
            Canvas.SetZIndex(elipse, 2);
        }

        public double Distance(Point other)
        {
            double xDistance = Math.Abs(this.x - other.x);
            double yDistance = Math.Abs(this.y - other.y);
            return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
        }
    }
}
