using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

public static class CookieService
{
    public static async Task SaveItemEncryptedAsync<T>(this HttpContext httpContext, string key, T item, int expirationDays = 7)
    {
        // Serialize and encrypt the item
        var itemJson = JsonSerializer.Serialize(item);
        var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);
        var base64Json = Convert.ToBase64String(itemJsonBytes);

        // Set the cookie with expiration
        httpContext.Response.Cookies.Append(key, base64Json, new CookieOptions
        {
            HttpOnly = false,  // For security
            Secure = false,    // Ensure secure in production
            Expires = DateTime.UtcNow.AddDays(expirationDays)
        });

        await Task.CompletedTask;  // Since no actual async work is done here
    }

    public static async Task<T?> ReadItemEncryptedAsync<T>(this HttpContext httpContext, string key) where T : class
    {
        // Try to read the cookie
        if (httpContext.Request.Cookies.TryGetValue(key, out var base64Json) && !string.IsNullOrEmpty(base64Json))
        {
            // Decode and decrypt the item
            var itemJsonBytes = Convert.FromBase64String(base64Json);
            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);
            var item = JsonSerializer.Deserialize<T>(itemJson);
            return await Task.FromResult(item);
        }

        return null;  // Return null if cookie does not exist
    }
}
