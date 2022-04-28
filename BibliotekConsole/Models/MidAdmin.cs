using System;
using System.Collections.Generic;

namespace BibliotekConsole.Models
{
    public partial class MidAdmin
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
    }
}
