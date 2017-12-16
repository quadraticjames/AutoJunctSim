using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shell;
using System.Windows.Media;
using System.Windows.Shapes;
using SpatialTypes;

namespace SimRunner
{
    internal class CanvasWriter : ICanvasWriter
    {
        public CanvasWriter(Canvas canvas)
        {
            m_Canvas = canvas;
        }
        private Canvas m_Canvas;
        private Dictionary<Guid, Rectangle> m_Rectangles = new Dictionary<Guid, Rectangle>();
        public void DrawVehicles(IEnumerable<VehicleDisplay> vehicles)
        {
            var vehicleGuids = vehicles.Select(v => v.Guid).ToList();
            foreach(var removedGuid in m_Rectangles.Keys.Except(vehicleGuids).ToList())
            {
                m_Canvas.Children.Remove(m_Rectangles[removedGuid]);
                m_Rectangles.Remove(removedGuid);
            }

            // Every vehicle in the dictionary is in vehicles

            foreach (var newVehicle in vehicles.Where(v => !m_Rectangles.ContainsKey(v.Guid)))
            {
                System.Windows.Application.Current.Dispatcher.Invoke(delegate {

                    var rect = new Rectangle();

                    rect.Stroke = new SolidColorBrush(Colors.Black);
                    rect.Fill = new SolidColorBrush(Colors.Blue);
                    rect.Width = newVehicle.Size.X;
                    rect.Height = newVehicle.Size.Y;

                    var rotateTransform = new RotateTransform(0);
                    rotateTransform.CenterX = rect.Width / 2;
                    rotateTransform.CenterY = rect.Height / 2;
                    rect.RenderTransform = rotateTransform;

                    m_Canvas.Children.Add(rect);
                    m_Rectangles.Add(newVehicle.Guid, rect);
                });
            }

            // Every vehicle in vehicles is in the dictionary

            foreach(var vehicle in vehicles)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(delegate
                {
                    var rect = m_Rectangles[vehicle.Guid];
                    var rotateTransform = (RotateTransform)rect.RenderTransform;
                    rotateTransform.Angle = vehicle.Heading.Degrees;

                    var topLeftPoint = vehicle.CentrePoint - new Point(rotateTransform.CenterX, rotateTransform.CenterY);
                    Canvas.SetLeft(rect, topLeftPoint.X);
                    Canvas.SetTop(rect, topLeftPoint.Y);
                });
            }
        }
    }
}
