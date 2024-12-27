using System.Collections.Generic;

namespace BlogProject.DAL;

public partial class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsActive { get; set; }
    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
