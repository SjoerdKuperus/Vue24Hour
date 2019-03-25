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

            //Temp data.
            GameCenter = new GeoCoordinate(52.0907336, 5.1217543); //Dom van utrecht //52.0907336,5.1217543

            /*
            var center = new Quadrant();
            center.CenterPoint = new GeoCoordinate(52.0907336, 5.1217543);
            center.Border.Add(new GeoCoordinate(52.091439, 5.119906)); //Start is same as finish
            center.Border.Add(new GeoCoordinate(52.091652, 5.120291));
            center.Border.Add(new GeoCoordinate(52.091985, 5.122255));
            center.Border.Add(new GeoCoordinate(52.091912, 5.122993));
            center.Border.Add(new GeoCoordinate(52.091401, 5.123417));
            center.Border.Add(new GeoCoordinate(52.090387, 5.12399));
            center.Border.Add(new GeoCoordinate(52.090098, 5.123935));
            center.Border.Add(new GeoCoordinate(52.089591, 5.121045));
            center.Border.Add(new GeoCoordinate(52.090421, 5.12044));
            center.Border.Add(new GeoCoordinate(52.091439, 5.119906)); //Start is same as finish
            Quadrants.Add(center);

            var pieterkerk = new Quadrant();
            pieterkerk.CenterPoint = new GeoCoordinate(52.0907336, 5.1217543);
            pieterkerk.Border.Add(new GeoCoordinate(52.091913, 5.122982));
            pieterkerk.Border.Add(new GeoCoordinate(52.091413, 5.123414));
            pieterkerk.Border.Add(new GeoCoordinate(52.090389, 5.123991));
            pieterkerk.Border.Add(new GeoCoordinate(52.090089, 5.12393));
            pieterkerk.Border.Add(new GeoCoordinate(52.090061, 5.12407));
            pieterkerk.Border.Add(new GeoCoordinate(52.0906, 5.125603));
            pieterkerk.Border.Add(new GeoCoordinate(52.091384, 5.126828));
            pieterkerk.Border.Add(new GeoCoordinate(52.092125, 5.126657));
            pieterkerk.Border.Add(new GeoCoordinate(52.092845, 5.127067));
            pieterkerk.Border.Add(new GeoCoordinate(52.0932, 5.123167));
            pieterkerk.Border.Add(new GeoCoordinate(52.091913, 5.122982));
            Quadrants.Add(pieterkerk);

            var neude = new Quadrant();
            neude.CenterPoint = new GeoCoordinate(52.0907336, 5.1217543);
            neude.Border.Add(new GeoCoordinate(52.093203, 5.123148));
            neude.Border.Add(new GeoCoordinate(52.09191, 5.122984));
            neude.Border.Add(new GeoCoordinate(52.091987, 5.122233));
            neude.Border.Add(new GeoCoordinate(52.091658, 5.120295));
            neude.Border.Add(new GeoCoordinate(52.091436, 5.119911));
            neude.Border.Add(new GeoCoordinate(52.091861, 5.118884));
            neude.Border.Add(new GeoCoordinate(52.09173, 5.118444));
            neude.Border.Add(new GeoCoordinate(52.091837, 5.117286));
            neude.Border.Add(new GeoCoordinate(52.092412, 5.116992));
            neude.Border.Add(new GeoCoordinate(52.092708, 5.117091));
            neude.Border.Add(new GeoCoordinate(52.093443, 5.116979));
            neude.Border.Add(new GeoCoordinate(52.093588, 5.118848));
            neude.Border.Add(new GeoCoordinate(52.093413, 5.120532));
            neude.Border.Add(new GeoCoordinate(52.093114, 5.12136));
            neude.Border.Add(new GeoCoordinate(52.093134, 5.122161));
            neude.Border.Add(new GeoCoordinate(52.093203, 5.123148));
            Quadrants.Add(neude);
            */

            // Read of the features from geoJson file.

            // get the JSON file content
            var path = "C:\\Ontwik\\Innovation\\Vue24Hour\\Vue24Hour\\MapData\\TestDataV1.geojson";
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