$(document).ready(function(){
    $("#checky").click(function () {
        $("#RegisterBut").attr("disabled", !this.checked);
    });
});