// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();
    connection.on("LoadProducts", function () {
        LoadProdData();
    });
    LoadProdData();

    function LoadProdData() {
        var tr = '';
        $.ajax({
            url: '/Products/GetProducts',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                        <td>${v.ProdName}</td>
                        <td>${v.Category}</td>
                        <td>${v.UnitPrice}</td>
                        <td>${v.StockQty}</td>
                        <td>
                            <a href='../Products/Edit/${v.ProdId}'>Edit</a> |
                            <a href='../Products/Details?id=${v.ProdId}'>Detail</a> |
                            <a href='../Products/Delete/${v.ProdId}'>Delete</a>
                        </td>
                    </tr>`;
                });
                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});