// wwwroot/js/getClientInformation.js
async function getClientSystemInfo() {
    const userAgent = navigator.userAgent;
    const platform = navigator.platform;
    const language = navigator.language || navigator.userLanguage;

    let os = "Unknown OS";
    let browser = "Unknown Browser";
    let is64Bit = false;

    // Detect OS
    if (userAgent.indexOf("Win") !== -1) {
        os = "Windows";
        is64Bit = userAgent.indexOf("x64") !== -1 || userAgent.indexOf("WOW64") !== -1;
    } else if (userAgent.indexOf("Mac") !== -1) {
        os = "MacOS";
    } else if (userAgent.indexOf("Linux") !== -1) {
        os = "Linux";
    } else if (userAgent.indexOf("Android") !== -1) {
        os = "Android";
    } else if (userAgent.indexOf("like Mac") !== -1) {
        os = "iOS";
    }

    // Detect Browser
    if (userAgent.indexOf("Chrome") !== -1) {
        browser = "Chrome";
    } else if (userAgent.indexOf("Firefox") !== -1) {
        browser = "Firefox";
    } else if (userAgent.indexOf("Safari") !== -1) {
        browser = "Safari";
    } else if (userAgent.indexOf("MSIE") !== -1 || userAgent.indexOf("Trident") !== -1) {
        browser = "Internet Explorer";
    }

    // Fetch IP address
    const ipResponse = await fetch('https://api.ipify.org?format=json');
    const ipData = await ipResponse.json();

    return {
        userAgent: userAgent,
        platform: platform,
        os: os + (is64Bit ? " 64-bit" : " 32-bit"),
        language: language,
        online: navigator.onLine,
        cookiesEnabled: navigator.cookieEnabled,
        browser: browser,
        ip: ipData.ip,
    };
}
