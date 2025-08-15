using IApply.Frontend.Common.Enums;

namespace IApply.Frontend.Models.User.GetUsers
{
    public class Vehicle
    {
        public Guid? VehicleCatalogId { get; set; }
        public Guid? VehicleTypeId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Registration { get; set; }
        public VehicleType VehicleType { get; set; }
        public int SeatingCapacity { get; set; }
        public List<File> Files { get; set; }

    }
}
