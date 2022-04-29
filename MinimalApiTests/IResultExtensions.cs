
using Microsoft.AspNetCore.Http;
using System;

namespace MinimalApiTests;

public static class IResultExtensions
{
    public static T? GetOkObjectResultValue<T>(this IResult result)
    {
        return (T?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?.GetProperty("Value", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(result);
    }

    public static int? GetObjectResultStatusCode(this IResult result)
    {
        return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?.GetProperty("StatusCode", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?.GetValue(result);
    }
}
