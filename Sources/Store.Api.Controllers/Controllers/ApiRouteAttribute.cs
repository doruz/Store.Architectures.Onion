using Microsoft.AspNetCore.Mvc;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ApiRouteAttribute(string template) : RouteAttribute($"api/{template}");