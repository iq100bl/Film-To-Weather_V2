namespace DatabaseAccess.Entities
{
    public class ConditionModel
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string Night { get; set; }
        public ICollection<WeatherModel> Weather { get; set; }
    }
}