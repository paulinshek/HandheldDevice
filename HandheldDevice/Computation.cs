using System;
using Microsoft.SPOT;
using System.Collections;

namespace HandheldDevice
{
    class Computation
    {
        enum Direction {UP,DOWN,LEFT,RIGHT};
        Queue locationHistory = new Queue();
        Queue distanceHistory = new Queue();

        public void addLocation(double X, double Y, double Z)
        {
            locationHistory.Enqueue(new Coordinate(X,Y,Z));
            
        }

        public void addDistance(int distance)
        {
            distanceHistory.Enqueue(distance);
        }

        class Coordinate
        {
            double X, Y, Z;
            public Coordinate(double X, double Y, double Z)
            {
                this.X = X; this.Y = Y; this.Z = Z;
            }
        }
    }
}
