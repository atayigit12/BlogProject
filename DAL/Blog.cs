using System;
using System.Collections.Generic;

namespace BlogProject.DAL;

public partial class Blog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public decimal? Rating { get; set; }

    public DateTime PublishDate { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();

    public virtual User User { get; set; } = null!;
}
