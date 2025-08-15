importScripts('https://www.gstatic.com/firebasejs/10.13.2/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/10.13.2/firebase-messaging-compat.js');
const firebaseConfig = {
    apiKey: "AIzaSyDsclVATsILyHYM6R9yqZcqQPeHGAS6sMI",
    authDomain: "mishwar-40829.firebaseapp.com",
    projectId: "mishwar-40829",
    storageBucket: "mishwar-40829.firebasestorage.app",
    messagingSenderId: "86840089786",
    appId: "1:86840089786:web:da7d170196a6c74caf3683",
    measurementId: "G-N8G553PJW0"
};
firebase.initializeApp(firebaseConfig);
const messaging = firebase.messaging();
//messaging.onBackgroundMessage((payload) => {
//    console.log('[firebase-messaging-sw.js] Received background message ', payload);
//    const notificationTitle = payload.notification.title;
//    const notificationOptions = {
//        body: payload.notification.body,
//    };
//    return self.registration.showNotification(notificationTitle,
//        notificationOptions);
//});
self.addEventListener('notificationclick', event => {
    console.log(event)
});