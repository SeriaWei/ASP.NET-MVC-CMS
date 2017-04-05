/*!
 * http://www.zkea.net/
 * Copyright 2016 ZKEASOFT
 * Before you remove this script, please review the lincese below
 * http://www.zkea.net/licenses
 */
if (location.hostname !== "localhost" &&
    location.hostname !== "127.0.0.1" &&
    !location.hostname.startsWith("192.") &&
    !location.hostname.startsWith("10.")) {
    document.write(CryptoJS.enc.Base64.parse("PHNjcmlwdCB0eXBlPSJ0ZXh0L2phdmFzY3JpcHQiIGFzeW5jIHNyYz0iaHR0cDovL3d3dy56a2VhLm5ldC9vcGVuc3RhdGlzdGljcyI+PC9zY3JpcHQ+").toString(CryptoJS.enc.Utf8));
}