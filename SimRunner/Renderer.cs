using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AutoJunctSim;
using SpatialTypes;

namespace SimRunner
{
    public class Renderer
    {
        private ICanvasWriter m_Canvas;
        private IAutoJunctSimulation m_Simulation;
        private Timer m_Looper;
        public Renderer(ICanvasWriter canvas, IAutoJunctSimulation simulation)
        {
            m_Canvas = canvas;
            m_Simulation = simulation;
            m_SimulationToCanvasMapping = new CoordinateConverter(10, new SpatialTypes.Point(50,50));
        }

        public void Start()
        {
            m_LastRenderStart = DateTime.UtcNow;
            m_Looper = new Timer(TimeSpan.FromSeconds(1.0 / 30).Milliseconds);
            m_Looper.Elapsed += new ElapsedEventHandler(OnTick);
            m_Looper.Start();
        }

        private bool m_Rendering = false;
        private DateTime m_LastRenderStart;

        private CoordinateConverter m_SimulationToCanvasMapping;

        private void OnTick(object s, ElapsedEventArgs e)
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
            Render(m_Canvas);
        }

        private void Render(ICanvasWriter canvasWriter)
        {
            canvasWriter.DrawVehicles(m_Simulation.Vehicles.Select(v => new VehicleDisplay
            {
                Guid = v.Guid,
                CentrePoint = m_SimulationToCanvasMapping.Convert(v.CentrePoint),
                Size = m_SimulationToCanvasMapping.Convert(v.Size),
                Heading = v.Heading
            }).ToList());
        }
    }
}
