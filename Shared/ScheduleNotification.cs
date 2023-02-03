namespace Shared
{
    public record ScheduleNotification
    {
        public DateTime DeliveryTime { get; init; }
        public HelloMessage HelloMessage { get; set; }
    }
}
