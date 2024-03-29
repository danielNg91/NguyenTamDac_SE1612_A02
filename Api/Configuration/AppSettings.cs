﻿using Api.Configuration;

namespace Api;

public class AppSettings
{
    public DbConfig ConnectionStrings { get; set; }
    public AdminAccount AdminAccount { get; set; }
    public string FrontendUrl { get; set; }
}
