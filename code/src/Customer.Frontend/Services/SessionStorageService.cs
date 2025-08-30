using Blazored.LocalStorage;
using System.Text;
using System.Text.Json;

namespace OpenProfileAPI.Frontend.Services;

public static class LocalStorageService
{
    public static async Task SaveItemEncryptedAsync<T>(this ILocalStorageService localStorageService, string key, T item)
    {
        var itemJson = JsonSerializer.Serialize(item);
        var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);
        var base64Json = Convert.ToBase64String(itemJsonBytes);
        await localStorageService.SetItemAsync(key, base64Json);
    }

    public static async Task<T?> ReadItemEncryptedAsync<T>(this ILocalStorageService localStorageService, string key) where T : class
    {
        var base64Json = await localStorageService.GetItemAsync<string>(key);
        if (!string.IsNullOrEmpty(base64Json))
        {
            var itemJsonBytes = Convert.FromBase64String(base64Json);
            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);
            var item = JsonSerializer.Deserialize<T>(itemJson);
            return item;
        }
        return null;
    }

}
