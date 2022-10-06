using System;
using System.Collections.Generic;

namespace UserAndMsg.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public string Time { get; set; } = null!;
    }
}
