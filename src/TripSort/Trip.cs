namespace TripSort
{
    public class Trip
    {
        public Trip(string startPoint, string endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public Trip(string startPoint, string endPoint, string details) : this(startPoint, endPoint)
        {
            Details = details;
        }

        public Trip(string startPoint, string endPoint, string details, Vehicle vehicle) : this(startPoint, endPoint, details)
        {
            Vehicle = vehicle;
        }

        public string StartPoint { get; }

        public string EndPoint { get; }

        public string Details { get; private set; }

        public Vehicle Vehicle { get; private set; }

        public bool Completed { get; private set; }

        public void Complete(bool completed)
        {
            Completed = completed;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Trip;
            if (other == null)
            {
                return false;
            }

            return StartPoint.Equals(other.StartPoint) && EndPoint.Equals(other.EndPoint);
        }

        public override int GetHashCode()
        {
            return StartPoint.GetHashCode() ^ EndPoint.GetHashCode();
        }

        public override string ToString()
        {
            return $"From {StartPoint} to {EndPoint} on {Vehicle}. Details: {Details}. Completed: {Completed}";
        }
    }
}
