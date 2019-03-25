using System;
using System.Collections.Generic;
using GeoCoordinatePortable;

namespace Vue24Hour.Domain.Model
{
    public class Quadrant
    {
        public double Width = 1000;
        public double Height = 1000;
        public GeoCoordinate CenterPoint { get; set; }
        public ICollection<GeoCoordinate> Border { get; set; }
        public Guid Id { get; set; }

        public Quadrant()
        {
            CenterPoint = GeoCoordinate.Unknown;
            Border = new HashSet<GeoCoordinate>();
            Id = Guid.NewGuid();
        }
    }
}