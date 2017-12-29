using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SimBase;
using SpatialTypes;

namespace AutoJunctSim
{
    public class AutoJunctSimulationStream : IAutoJunctSimulationStream
    {
        public IStreamable<IList<IStreamable<IVehicleSprite>>> Vehicles => VehiclesList;
        private StreamableList<IStreamable<IVehicleSprite>> VehiclesList = new StreamableList<IStreamable<IVehicleSprite>>();

        public void Start()
        {
            var thread = new Thread(RunSimulation);
            thread.Start();
            Thread.Sleep(1000);
        }

        private void RunSimulation()
        {
            var time = TimeSpan.Zero;
            var tick = TimeSpan.FromMilliseconds(10);

            var streamables = new List<StreamableVehicleSprite>();
            var vehicles = new List<Vehicle>();
            var movers = new List<VehicleMover>();

            var vehicle = new Vehicle();
            vehicles.Add(vehicle);

            var m = new VehicleMover(vehicle);
            movers.Add(m);

            var streamableVehicle = new StreamableVehicleSprite(vehicle);
            streamables.Add(streamableVehicle);
            streamableVehicle.TakeSample(new TimeSpanMoment(time));
            VehiclesList.AddAt(streamableVehicle, new TimeSpanMoment(time));
            
            while (true)
            {
                time += tick;
                foreach(var mover in movers)
                {
                    mover.Advance(tick);
                }
                foreach(var s in streamables)
                {
                    s.TakeSample(new TimeSpanMoment(time));
                }
            }
        }

        private class Vehicle : IVehicleSprite
        {
            public Point CentrePoint { get; set; } = new Point(0, 0);
            public Size Size { get; } = new Size(2, 5);
            public Angle Heading { get; } = Angle.FromDegrees(135);
            public Guid Guid { get; } = Guid.NewGuid();

            public Velocity Velocity { get; set; } = new Velocity(0);
            public Acceleration Acceleration { get; } = new Acceleration(6);
        }

        private class VehicleMover
        {
            private Vehicle m_Vehicle;
            public VehicleMover(Vehicle vehicle)
            {
                m_Vehicle = vehicle;
            }

            public void Advance(TimeSpan tick)
            {
                var distance = Acceleration.DistanceTravelledUnderConstantAcceleration(m_Vehicle.Velocity, m_Vehicle.Acceleration, tick);
                m_Vehicle.Velocity = m_Vehicle.Velocity + Acceleration.VelocityGained(m_Vehicle.Acceleration, tick);
                m_Vehicle.CentrePoint = m_Vehicle.CentrePoint + Angle.PointOnUnitCircleAtAngle(m_Vehicle.Heading) * distance.Metres;
            }
        }
    }
}
