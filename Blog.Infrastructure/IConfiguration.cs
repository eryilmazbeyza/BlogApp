namespace Blog.Persistence
{
    public interface IConfiguration
    {
        object GetConnectionString(string v);
    }
}