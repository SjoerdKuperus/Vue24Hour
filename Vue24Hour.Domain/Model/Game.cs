using System;
using System.Collections.Generic;
using System.IO;
using BAMCIS.GeoJSON;
using GeoCoordinatePortable;
using Newtonsoft.Json;

namespace Vue24Hour.Domain.Model
{
    public sealed class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Team> Teams { get; set; }
        public GameState GameState { get; set; }
        public ICollection<Quadrant> Quadrants { get; set; }
        public GeoCoordinate GameCenter { get; set; }
        public ICollection<ControlEvent> ControlEvents { get; set; }

        public Game()
        {
            Quadrants = new HashSet<Quadrant>();
            ControlEvents = new HashSet<ControlEvent>();
            Teams = new HashSet<Team>();
        }

        public void FillFakeQuadrantData()
        {
            //Temp data.
            GameCenter = new GeoCoordinate(52.0907336, 5.1217543); //Dom van utrecht //52.0907336,5.1217543

            // Read of the features from geoJson file.

            // get the JSON file content
            var path = "C:\\Ontwik\\Innovation\\Vue24Hour\\Vue24Hour\\MapData\\TestDataV2.geojson";
            var jsonData = File.ReadAllText(path);

            GeoJson data = JsonConvert.DeserializeObject<GeoJson>(jsonData);
            FeatureCollection features = data as FeatureCollection;
            if (features != null)
            {
                foreach (var feature in features.Features)
                {
                    var geometry = feature.Geometry;
                    Polygon polygon = geometry as Polygon;
                    if (polygon != null)
                    {
                        var newQuadrant = new Quadrant();
                        newQuadrant.CenterPoint = new GeoCoordinate(52.0907336, 5.1217543); // TODO?

                        foreach (var polygonCoords in polygon.Coordinates)
                        {
                            var positions = polygonCoords.Coordinates;
                            foreach (var point in positions)
                            {
                                newQuadrant.Border.Add(new GeoCoordinate(point.Latitude, point.Longitude));
                            }
                        }

                        Quadrants.Add(newQuadrant);
                    }
                }
            }
        }
    }
}