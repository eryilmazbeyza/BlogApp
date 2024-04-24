namespace Blog.Web.Models
{
    public class BaseEntityViewModel
    {
        public long Id { get; set; }

        /// <summary>
        /// true ise kalıcı olarak databaseden dataları siler
        /// </summary>
        public bool IsHardDelete { get; set; } = false;
    }
}
