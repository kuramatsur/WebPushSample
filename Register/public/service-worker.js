//通知への対応
self.addEventListener("push", function (event) {
    console.log("[Service Worker] Push Received.");
    console.log(`[Service Worker] Push had this data: "${event.data.text()}"`);

    const data = event.data?.json() ?? {};

    const title = data?.title;
    const options = data?.options;
    event.waitUntil(self.registration.showNotification(title, options));

});
