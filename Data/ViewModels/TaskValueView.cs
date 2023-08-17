using gantt_backend.Data.ModelsDTO;

namespace gantt_backend.Data.ViewModels
{
    public class TaskValueView
    {
        public string field { get; set; }
        public string field_id { get; set; }
        public string value { get; set; }
        public TypeJsonDto type {get;set;}
    }
}