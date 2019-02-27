using System;

namespace Vue24Hour.Domain.Model
{
    public class ControlEvent
    {
        public Team Team { get; set; }
        public Quadrant Quadrant { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}