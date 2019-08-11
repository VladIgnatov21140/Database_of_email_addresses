// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.filtertext').keypress(function (event) {
    if (event.keyCode == 13) {
        SendFilteringReport();
    }
});

$('#refreshtable').click(function (event) {
    refreshtable();
});