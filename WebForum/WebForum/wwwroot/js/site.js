// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function loadDarkModeSettings() {
    let darkMode = getCookie("darkMode");
    if (darkMode == "true") {
        toggleDarkMode();
    }
}

function toggleDarkMode() {
    $('.text-light, .text-dark').toggleClass('text-light text-dark');
    $('.navbar-light, .navbar-dark').toggleClass('navbar-light navbar-dark');
    $('.bg-white, .bg-dark').toggleClass('bg-white bg-dark');
    $('.table-white, .table-dark').toggleClass('table-white table-dark');
}

function toggleDarkModeAndSave() {
    toggleDarkMode();

    let darkMode = getCookie("darkMode");
    if (darkMode == "true") {
        setCookie("darkMode", "false", 365);
    } else {
        setCookie("darkMode", "true", 365);
    }
}
