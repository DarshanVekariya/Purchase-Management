$(document).ready(function () {

    $("#addProduct").off("click").on("click", function (e) {
        e.preventDefault();

        $(this).prop("disabled", true);

        var productName = $("#productName").val();
        var productPrice = $("#productPrice").val();

        $.ajax({
            url: "/Product/Add",
            type: "POST",
            data: {
                Name: productName,
                Price: productPrice
            },
            success: function (response) {
                $("#productList").load(location.href + " #productList");
                $("#productName").val("");
                $("#productPrice").val("");
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            },
            complete: function () {
                $("#addProduct").prop("disabled", false);
            }
        });
    });



    $(document).on("click", ".cancelProduct", function () {
        var $tr = $(this).closest("tr");
        $tr.find(".editProduct").show();
        $tr.find(".deleteProduct").show();
        $tr.find(".saveProduct").hide();
        $tr.find(".cancelProduct").hide();

        $tr.find("#namelbl").show();
        $tr.find("#pricelbl").show();
        $tr.find("#nametext").hide();
        $tr.find("#pricetext").hide();
    });
 
    $(document).on("click", ".editProduct", function () {
        var $tr = $(this).closest("tr");
        $tr.find(".editProduct").hide();
        $tr.find(".deleteProduct").hide();
        $tr.find(".saveProduct").show();
        $tr.find(".cancelProduct").show();

        $tr.find("#namelbl").hide();
        $tr.find("#pricelbl").hide();
        $tr.find("#nametext").show();
        $tr.find("#pricetext").show();
    });

    $(document).on("click", ".saveProduct", function () {
        var $tr = $(this).closest("tr");
        var productId = $tr.data("id");
        var newName = $tr.find("#nametext").val();
        var newPrice = $tr.find("#pricetext").val();

        $.ajax({
            url: "/Product/Save",
            type: "POST",
            data: {
                Id: productId,
                Name: newName,
                Price: newPrice
            },
            success: function (response) {
              
                $("#productList").load(location.href + " #productList");
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });

        $tr.find("#namelbl").text(newName);
        $tr.find("#pricelbl").text(newPrice);

        $tr.find(".editProduct").show();
        $tr.find(".deleteProduct").show();
        $tr.find(".saveProduct").hide();
        $tr.find(".cancelProduct").hide();

        $tr.find("#namelbl").show();
        $tr.find("#pricelbl").show();
        $tr.find("#nametext").hide();
        $tr.find("#pricetext").hide();
    });
   
    $(document).on("click", ".deleteProduct", function () {
        var productId = $(this).closest("tr").data("id");
        $.ajax({
            url: "/Product/Delete?id=" + productId,
            type: "DELETE",
            success: function (response) {
              
                $("#productList").load(location.href + " #productList");
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });
    });
});

