using DatabaseAccess.Entities.Abstractions;

namespace DatabaseAccess.Entities
{
    public class WeatherModel : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime TimeUpdate { get; set; }
        public int Temperature { get; set; }
        public bool IsDay { get; set; }
        public Guid CityId { get; set; }
        public CityModel City { get; set; }
        public int CodeCondition { get; set; }
        public ConditionModel Condition { get; set; }
    }
}
