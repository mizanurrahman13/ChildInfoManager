var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CHILDINFOMANAGER_Web>("childinfomanager-web");

builder.Build().Run();
