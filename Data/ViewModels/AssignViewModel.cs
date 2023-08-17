namespace gantt_backend.Data.ViewModels
{
    public class AssignViewModel
    {
        public string Id { get; set; }
        public string Resource_id { get; set; }
        public string Value { get; set; }
        public string? Start_date { get; set; }
        public int? Delay {get; set;}
        public int? Duration {get; set;}
        public string? Mode {get;set;}
        public string? Unit {get;set;}
    }
}

        