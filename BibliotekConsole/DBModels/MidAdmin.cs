using System;
using System.Collections.Generic;

namespace BibliotekConsole.DBModels
{
    public partial class MidAdmin
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
    }
}
