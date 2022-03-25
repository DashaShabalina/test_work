namespace Task1.Data
{
    public class Point
    {
        public int Id { get; set; }
        public int IdEquipment { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Point(int id, int idEquipment, double latitude, double longitude)
        {
            Id = id;
            IdEquipment = idEquipment;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}