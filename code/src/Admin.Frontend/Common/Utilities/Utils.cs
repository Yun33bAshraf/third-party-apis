using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;
using IApply.Frontend.Common.Enums;

namespace IApply.Frontend.Common.Utilities;

public static class Utils
{
    public static string GetDisplayName<TModel>(string propertyName)
    {
        var property = typeof(TModel).GetProperty(propertyName);
        if (property != null)
        {
            var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
        }
        return propertyName; // Fallback to property name if no Display attribute found
    }
    public static async void DisplayErrorMessage(IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("alert", message);
        await js.InvokeVoidAsync("console.log", message);
    }

    // capitalazie first words 
    public static string CapitalizeWord(string word)
    {
        if (string.IsNullOrEmpty(word))
            return word;

        return char.ToUpper(word[0]) + word.Substring(1).ToLower();
    }

    public static string FormatTitle(string title)
    {
        var words = title.Split('-')
                         .Select(word => CapitalizeWord(word))
                         .ToArray();

        return string.Join(" ", words);
    }

    public static bool HasRight(ClaimsPrincipal? userState, SystemRights Right)
    {
        if (userState != null)
        {
            var (_, _, rightName, _) = Right.ToRightAttribute();
            var rsaClaims = userState.FindAll(ClaimTypes.Rsa);

            if (rsaClaims != null && rsaClaims.Any(x => x.Value == rightName))
            {
                return true;
            }
        }
        return false;
    }
    
    public static bool HasUserRight(ClaimsPrincipal? userState, UserRights Right)
    {
        if (userState != null)
        {
            var right = ((int)Right).ToString();
            var rsaClaims = userState.FindAll(ClaimTypes.Rsa);

            if (rsaClaims != null && rsaClaims.Any(x => x.Value == right))
            {
                return true;
            }
        }
        return false;
    }

    public static bool HasUserRole(ClaimsPrincipal? userState, RoleEnum Role)
    {
        if (userState != null)
        {
            var role = ((int)Role).ToString();
            var roleClaims = userState.FindAll(ClaimTypes.Role);

            if (roleClaims != null && roleClaims.Any(x => x.Value == role))
            {
                return true;
            }
        }
        return false;
    }

    public static bool HasRole(ClaimsPrincipal? userState, ApplicationRole Role)
    {
        if (userState != null)
        {
            var role = ((int)Role).ToString();
            var roleClaims = userState.FindAll(ClaimTypes.Role);

            if (roleClaims != null && roleClaims.Any(x => x.Value == role))
            {
                return true;
            }
        }
        return false;
    }

    public static int GetUserId(ClaimsPrincipal? userState)
    {
        if (userState != null)
        {
            var userIdClaim = userState.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
        }
        return 0;
    }


    public static string AddSpacesToWords(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        string result = Regex.Replace(input, @"([a-z])([A-Z])|([A-Za-z])(\d)", "$1$3 $2$4");

        return result;
    }

    public static string GetDescription(this Enum genericEnum) //Hint: Change the method signature and input paramter to use the type parameter T
    {
        Type genericEnumType = genericEnum.GetType();
        MemberInfo[] memberInfo = genericEnumType.GetMember(genericEnum.ToString());

        if (memberInfo != null && memberInfo.Length > 0)
        {
            var attributes = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (attributes != null && attributes.Count() > 0)
            {
                return ((System.ComponentModel.DescriptionAttribute)attributes.ElementAt(0)).Description;
            }
        }

        return genericEnum.ToString();
    }

    //Local Time Conversion
 public static async Task<string> ConvertToLocalTimeWithZoneAbbreviation(
    DateTime utcDate, 
    IJSRuntime jsRuntime = null)
{
    TimeZoneInfo timeZone;
    
    // Try to get client timezone 
    if (jsRuntime != null)
    {
        try
        {
            var timeZoneId = await jsRuntime.InvokeAsync<string>("getClientTimezone");
            timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        catch
        {
            // Fallback to server timezone
            timeZone = TimeZoneInfo.Local;
        }
    }
    else
    {
        // Use server timezone
        timeZone = TimeZoneInfo.Local;
    }
    
    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, timeZone);
    string abbreviation = GetTimeZoneAbbreviation(timeZone, timeZone.IsDaylightSavingTime(localTime));
    return $"{localTime:yyyy-MM-dd hh:mm tt} {abbreviation}";
}

    //Time Zone Description
    private static string GetTimeZoneAbbreviation(TimeZoneInfo timeZone, bool isDaylight)
    {
        string name = isDaylight ? timeZone.DaylightName : timeZone.StandardName;

        var abbreviations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        // North America
        { "Eastern Standard Time", "EST" },
        { "Eastern Daylight Time", "EDT" },
        { "Central Standard Time", "CST" },
        { "Central Daylight Time", "CDT" },
        { "Mountain Standard Time", "MST" },
        { "Mountain Daylight Time", "MDT" },
        { "Pacific Standard Time", "PST" },
        { "Pacific Daylight Time", "PDT" },
        { "Alaskan Standard Time", "AKST" },
        { "Alaskan Daylight Time", "AKDT" },
        { "Hawaiian Standard Time", "HST" },

        // South America
        { "Argentina Standard Time", "ART" },
        { "E. South America Standard Time", "BRST" },
        { "Pacific SA Standard Time", "CLT" },

        // Europe
        { "GMT Standard Time", "GMT" },
        { "Greenwich Mean Time", "GMT" },
        { "UTC", "UTC" },
        { "Central European Standard Time", "CET" },
        { "Central European Daylight Time", "CEST" },
        { "W. Europe Standard Time", "WET" },
        { "Romance Standard Time", "CET" },
        { "Russian Standard Time", "MSK" },

        // Asia
        { "India Standard Time", "IST" },
        { "Pakistan Standard Time", "PKT" },
        { "Bangladesh Standard Time", "BST" },
        { "Myanmar Standard Time", "MMT" },
        { "SE Asia Standard Time", "ICT" },
        { "China Standard Time", "CST" },
        { "Tokyo Standard Time", "JST" },
        { "Korea Standard Time", "KST" },
        { "Singapore Standard Time", "SGT" },

        // Oceania
        { "AUS Eastern Standard Time", "AEST" },
        { "AUS Eastern Daylight Time", "AEDT" },
        { "West Australia Standard Time", "AWST" },
        { "New Zealand Standard Time", "NZST" },
        { "New Zealand Daylight Time", "NZDT" },

        // Middle East
        { "Arab Standard Time", "AST" },
        { "Iran Standard Time", "IRST" },
        { "Israel Standard Time", "IST" },
        { "Arabian Standard Time", "AST" },

        // Africa
        { "South Africa Standard Time", "SAST" },
        { "E. Africa Standard Time", "EAT" },
        { "W. Central Africa Standard Time", "WAT" }
    };

        if (abbreviations.TryGetValue(name, out var abbr))
            return abbr;

        var fallback = string.Concat(name
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(w => char.ToUpper(w[0])));

        return fallback;
    }

}
