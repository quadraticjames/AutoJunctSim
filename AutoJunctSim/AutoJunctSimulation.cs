﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpatialTypes;

namespace AutoJunctSim
{
    public class AutoJunctSimulation : IAutoJunctSimulation
    {
        public void Advance(TimeSpan deltaTime)
        {
            ((DummyVehicleSprite)Vehicles.First()).CentrePoint += new Point(0.45, 0.45);
        }

        public IList<IVehicleSprite> Vehicles
        {
            get; private set;
        } = new List<IVehicleSprite>() { new DummyVehicleSprite() };
    
        private class DummyVehicleSprite : IVehicleSprite
        {
            public Point CentrePoint { get; set; } = new Point(0, 0);
            public Size Size { get; } = new Size(2, 5);
            public Angle Heading { get; } = Angle.FromDegrees(135);
            public Guid Guid { get; } = Guid.NewGuid();
        }
    }
}
