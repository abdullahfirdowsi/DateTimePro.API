namespace DateTimePro.API.Models
{
    public class TimeZoneInfoModel
    {
        public required string Id { get; set; }
        public required string DisplayName { get; set; }
        public required string StandardName { get; set; }
        public required string CurrentTime { get; set; }
    }
}