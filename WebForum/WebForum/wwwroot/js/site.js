// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function toggleDarkMode() {
    $('.text-light, .text-dark').toggleClass('text-light text-dark');
    $('.navbar-light, .navbar-dark').toggleClass('navbar-light navbar-dark');
    $('.bg-white, .bg-dark').toggleClass('bg-white bg-dark');
    $('.table-white, .table-dark').toggleClass('table-white table-dark');
}
