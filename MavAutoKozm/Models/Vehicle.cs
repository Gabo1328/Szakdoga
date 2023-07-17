namespace MavAutoKozm.Models
{
    public class Vehicle
    {
        //PK
        public int Id { get; set; }

        //FK
        public int UserId { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public string Color { get; set; }

        public string NumberPlate { get; set; }
    }
}
