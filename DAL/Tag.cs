using System;
using System.Collections.Generic;

namespace BlogProject.DAL;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
}
