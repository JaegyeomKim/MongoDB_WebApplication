namespace MongoDB_WebApplication.Models
{
    public interface IProjectStoreDatabaseSetting
    {
        string ProjectCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
