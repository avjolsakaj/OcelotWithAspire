var builder = DistributedApplication.CreateBuilder(args);

var auth = builder.AddProject<Projects.AuthenticationWebApi>("authenticationwebapi");

var customerApi = builder.AddProject<Projects.CustomerService>("customerservice");

var orderApi = builder.AddProject<Projects.OrderService>("orderservice");

var apiGateway = builder.AddProject<Projects.ApiGateway>("apigateway")
    .WithReference(customerApi)
    .WithReference(orderApi);

await builder.Build().RunAsync();
