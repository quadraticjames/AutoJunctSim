using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Renderer(Canvas canvas, AutoJunctSimulation simulation)
        {
            m_Canvas = canvas;
            m_Simulation = simulation;
        }

        public void Start()
        {

        }
    }
}
