using System;
using System.Collections.Generic;

namespace lap2_ef.Models;

public partial class Member
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<BookCheckout> BookCheckouts { get; set; } = new List<BookCheckout>();
}
