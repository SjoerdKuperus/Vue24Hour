using System;

namespace Vue24Hour.Domain.Model
{
    public class ControlEvent
    {
        public Guid Id { get; set; }
        public Team Team { get; set; }
        public Quadrant Quadrant { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}