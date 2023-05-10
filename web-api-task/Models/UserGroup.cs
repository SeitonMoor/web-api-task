namespace web_api_task.Models
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
    }
}
