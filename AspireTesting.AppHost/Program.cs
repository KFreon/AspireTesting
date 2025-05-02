var builder = DistributedApplication.CreateBuilder(args);

// Can also do builder.AddConnectionString without the sql + db step to use an existing database.

// Build sql container called "thedatabase" containing a database called "mydatabase"
var sql = builder.AddSqlServer("thedatabase")
    //.WithImageTag("2019-latest") // Allows easily changing the desired image
    .WithLifetime(ContainerLifetime.Persistent); // will not destroy "thedatabase" container on exit. Should reuse any existing container with that name.
var db = sql.AddDatabase("mydatabase");

var apiService = builder.AddProject<Projects.AspireTesting_ApiService>("apiservice")
    .WithReference(db) // Inject connection string from db reference
    .WaitFor(db);  // wait for it's health checks to be healthy

// Spin up the UI project (uses CommunityToolkit)
var ui = builder.AddNpmApp("frontend", "../client", scriptName: "dev") // Run `npm run dev` in the ../client directory
    .WithNpmPackageInstallation() // Do npm install
    .WithHttpEndpoint(env: "VITE_PORT")  // Add an env var called "VITE_PORT" and set it to some random port (used to config vite proxy in vite.config.ts)
    .WithEnvironment("VITE_BACKEND_URL", apiService.GetEndpoint("http"))  // Get the http endpoint of apiService and set that env var to it's value
    .WithExternalHttpEndpoints() // Allow external access (outside containers, allows host access)
    .WaitFor(apiService);

builder.Build().Run();
