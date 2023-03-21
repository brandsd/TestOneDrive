$(document).ready(function () // for country Caseading
{

    GetCountry();
       // for Disabled Citydropdown if user not select State
    $('#Country').change(function () {
        $('#State').attr('disabled', false); // for Enabled Statedropdown if user not select country
        var id = $(this).val();
        $('#State').empty();
        $('#State').append('<option>--Select State--</option>');
        $.ajax({
            url: '/Product/State?id=' + id, //url for controller and select State behalf of given Country id
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#State').append('<option value=' + data.id + '>' + data.name + '</option>');
                });
            }
        });
    });
    $('#State').change(function () {
        
        var id = $(this).val();
        $('#City').empty();
        $('#City').append('<option>--Select City--</option>');
        $.ajax({
            url: '/Product/City?id=' + id, //url for controller and select State behalf of given Country id
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#City').append('<option value=' + data.id + '>' + data.name + '</option>');
                });
            }
        });
    });
});
function GetCountry() {
    $.ajax({
        url: '/Product/Country',
        success: function (result) {
            $.each(result, function (i,data) {
                $('#Country').append('<option value=' + data.id + '>' + data.name + '</option>');
            });
        }

    });
}