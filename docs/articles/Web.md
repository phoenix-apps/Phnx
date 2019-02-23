# `Phnx.Web`

This library contains tools to help serialize and de-serialize data, using high speed compression

# Quick Examples

For a full list of all things possible with `Phnx.Web`, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.Web.html)

## Fluent Requests

Creating a HTTP request involves a series of optional fields and data. This fluent API makes it easy to assign and manage these

```cs
HttpRequestService requests = new HttpRequestService();
FluentRequest request = requests.CreateRequest();

ApiResponse response = await request
    .UseUrl("https://my-api.com")
    .WithBody().Json(new
    {
        id = 1,
        value = "Sample Value"
    })
    .Send(HttpMethod.Put);

HttpStatusCode statusCode = response.StatusCode;
string message = response.Message;

/* Output (example):
200: { id: 1, value: "Sample Value" }
*/
Console.WriteLine(statusCode + ": " message);
```

Extension methods are also written to make code like the above much simpler, as it's a fairly common HTTP request. A sample of the `PutAsync` extension method is below:

```cs
HttpRequestService requests = new HttpRequestService();

ApiResponse response = await requests.PutAsync<string>("https://my-api.com", new
{
        id = 1,
        value = "Sample Value"
});

HttpStatusCode statusCode = response.StatusCode;
string message = response.Message;

/* Output (example):
200: { id: 1, value: "Sample Value" }
*/
Console.WriteLine(statusCode + ": " message);
```

## Content Type Helpers
```cs
string jsonType = ContentType.Application.Json

// Output: application/json
Console.WriteLine(jsonType);

string calendarType = ContentType.Text.Calendar

// Output: text/calendar
Console.WriteLine(calendarType);

string iconType = ContentType.Image.Icon

// Output: image/x-icon
Console.WriteLine(iconType);
```