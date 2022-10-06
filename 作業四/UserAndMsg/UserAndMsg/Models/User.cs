using System;
using System.Collections.Generic;

namespace UserAndMsg.Models
{
    public partial class User
    {
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Permission { get; set; } = null!;
    }
}
