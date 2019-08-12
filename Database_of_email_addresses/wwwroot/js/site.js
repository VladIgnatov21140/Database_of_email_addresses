// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.filtertext').keypress(function (event) {
    if (event.keyCode == 13) {
        document.location.href = 'https://localhost:5001/Home/SetFilters?SNewParameters='
            + document.getElementById('SelectedCountry').value + ","
            + document.getElementById('SelectedArea').value + ","
            + document.getElementById('SelectedCity').value + ","
            + document.getElementById('SelectedStreet').value + ","
            + document.getElementById('SelectedHousing').value + ","
            + document.getElementById('SelectedHouse').value + ","
            + document.getElementById('SelectedPostCode').value;
    }
});