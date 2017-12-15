using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AutoJunctSim;

namespace SimRunner
{
    public class Renderer
    {
        private Canvas m_Canvas;
        private AutoJunctSimulation m_Simulation;
        private Timer m_Looper;
        public Renderer(Canvas canvas, AutoJunctSimulation simulation)
        {
            m_Canvas = canvas;
            m_Simulation = simulation;
        }

        public void Start()
        {
            m_LastRenderStart = DateTime.UtcNow;
            m_Looper = new Timer(s => OnTick(s), null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1.0 / 30));
        }

        private bool m_Rendering = false;
        private DateTime m_LastRenderStart;

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
            Render();
        }

        private void Render()
        {

        }
    }
}
