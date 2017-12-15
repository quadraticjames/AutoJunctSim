using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AutoJunctSim;
using SpatialTypes;

namespace SimRunner
{
    public class Renderer
    {
        private Canvas m_Canvas;
        private IAutoJunctSimulation m_Simulation;
        private Timer m_Looper;
        public Renderer(Canvas canvas, IAutoJunctSimulation simulation)
        {
            m_Canvas = canvas;
            m_Simulation = simulation;
            m_SimulationToCanvasMapping = new CoordinateConverter();
        }

        public void Start()
        {
            m_LastRenderStart = DateTime.UtcNow;
            m_Looper = new Timer(s => OnTick(s), null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1.0 / 30));
        }

        private bool m_Rendering = false;
        private DateTime m_LastRenderStart;

        private CoordinateConverter m_SimulationToCanvasMapping;

        private void OnTick(object state)
        {
            if (m_Rendering) return;
            m_Rendering = true;

            var now = DateTime.UtcNow;
            var delta = now - m_LastRenderStart;
            m_LastRenderStart = now;

            AdvanceAndRender(delta);

            m_Rendering = false;
        }

        private void AdvanceAndRender(TimeSpan delta)
        {
            m_Simulation.Advance(delta);
            var renderTransaction = new TransactionCanvasWriter();
            Render(renderTransaction);
            renderTransaction.Execute();
        }

        private void Render(ICanvasWriter canvasWriter)
        {
            foreach(var vehicle in m_Simulation.Vehicles)
            {
                var centre = m_SimulationToCanvasMapping.Convert(vehicle.CentrePoint);
                var size = m_SimulationToCanvasMapping.Convert(vehicle.Size);
                var heading = vehicle.Heading;

                canvasWriter.DrawRectangle(centre, size, heading);
            }
        }
    }
}
