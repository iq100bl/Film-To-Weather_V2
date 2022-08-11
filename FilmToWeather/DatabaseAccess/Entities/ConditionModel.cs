namespace DatabaseAccess.Entities
{
    public class ConditionModel
    {
        public int Id { get; set; }
        public string? ConditionsDay { get; set; }
        public string? ConditionsNight { get; set; }
        public ICollection<WeatherModel> Weather { get; set; }
    }
}