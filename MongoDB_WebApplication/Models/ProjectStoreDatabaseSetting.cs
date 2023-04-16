namespace MongoDB_WebApplication.Models
{
    public class ProjectStoreDatabaseSetting : IProjectStoreDatabaseSetting
    {
        public string ProjectCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
