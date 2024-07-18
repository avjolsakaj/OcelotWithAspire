var builder = DistributedApplication.CreateBuilder(args);

var orderService = builder.AddProject<Projects.OrderService>("orderservice");

var customerService = builder.AddProject<Projects.CustomerService>("customerservice");

var auth = builder.AddProject<Projects.AuthenticationWebApi>("authenticationwebapi");

var apiGateway = builder.AddProject<Projects.ApiGateway>("apigateway")
    .WithReference(orderService)
    .WithReference(customerService)
    .WithReference(auth)
    .WithExternalHttpEndpoints();

await builder.Build().RunAsync();
