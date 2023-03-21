$(document).ready(function () {
    ShowProductData();
});
function ShowProductData() {
    $.ajax({
        url: 'Ajax/ProductList',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result, statu, xhr) {
            var object = '';
            $.each(result, function (index, item) {
                object += '<tr>'
                object += '<td>' + item.id + '</td>';
                    object += '</tr>'
            });
            $('#table_data').html(object);

        },
        error: function () {
            alert("Data can't get");
        }

    });
};