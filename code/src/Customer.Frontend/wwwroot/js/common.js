// wwwroot/js/Common.js

window.addResizeListener = (dotNetHelper) => {
    window.addEventListener('resize', function () {
        dotNetHelper.invokeMethodAsync('OnScreenResize', window.innerWidth);
    });
}

function checkAndApplyDoubleScroll () {
    const tables = document.querySelectorAll('.table-responsive');
    tables.forEach(table => {
        const hasHorizontalOverflow = table.scrollWidth > table.clientWidth;
        if (hasHorizontalOverflow && typeof $(table).doubleScroll === 'function') {
            $(table).doubleScroll();
        }
    });
};


function toggleFullScreen() {
    if (!document.fullscreenElement) {
        // If not in fullscreen, request fullscreen
        document.documentElement.requestFullscreen();
    } else {
        // If already in fullscreen, exit fullscreen
        if (document.exitFullscreen) {
            document.exitFullscreen();
        }
    }
}

function getClientTimezone() {
    return Intl.DateTimeFormat().resolvedOptions().timeZone;
}
function getPageTitle() {
    return document.title;
}
function BlazorDownloadFile(fileName, contentType, base64Data) {
    const link = document.createElement("a");
    link.download = fileName;
    link.href = "data:" + contentType + ";base64," + base64Data;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function initializeTooltips(selector = '[data-bs-toggle="tooltip"]') {
    // Remove any visible tooltips without an associated button
    document.querySelectorAll('.tooltip.bs-tooltip-auto.show').forEach(tooltipElement => {
        if (!document.querySelector(`[aria-describedby="${tooltipElement.id}"]`)) {
            tooltipElement.remove();
        }
    });

    // Initialize tooltips for uninitialized elements
    Array.from(document.querySelectorAll(selector)).forEach(tooltipTriggerEl => {
        if (!bootstrap.Tooltip.getInstance(tooltipTriggerEl)) {
            new bootstrap.Tooltip(tooltipTriggerEl);
        }
    });
}

function toggleBodyScroll(add) {
    if (add) {
        document.body.classList.add('overflow-hidden');
    } else {
        document.body.classList.remove('overflow-hidden');
    }
}

async function initializeFirebase(firebaseConfig) {
    const app = firebase.initializeApp(firebaseConfig);
    // const analytics = getAnalytics(app);
    if ('serviceWorker' in navigator) {
        const register = await navigator.serviceWorker.register('firebase-messaging-sw.js');
    }
}

async function getFCMToken() {
    try {
        const messaging = firebase.messaging();

        // Get the FCM token
        const fcmToken = await messaging.getToken();
        console.log(fcmToken);

        // Set up the onMessage event listener
        messaging.onMessage((payload) => {
            console.log("onMessage event fired", payload);
            const notification = new Notification(payload.notification.title, {
                body: payload.notification.body
            });
        });

        return fcmToken; // Return the token if successful
    } catch (error) {
        console.error("Error getting FCM token:", error);
        return null; // Return null in case of an exception
    }
}


