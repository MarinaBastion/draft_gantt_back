namespace gantt_backend.Data.ViewModels
{
    public class GanttViewModel
    {
        public IEnumerable<TaskViewModel> tasks { get; set; }
        public IEnumerable<LinkViewModel> links { get; set; }
    }
}