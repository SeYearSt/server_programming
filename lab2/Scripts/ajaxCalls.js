//Home - DownloadExamples
$('.OrderBook_Item_Add').on('click', function (e) {
    /*
    $.ajax({
        type: "GET",
        url: ajaxUrl + "/?id=" + e.currentTarget.getAttribute("data-BookId")
    }).done(function (a) {
        console.log("Book added to the Cart...");
        $("#Nav_Item_CartPartial a").text("Cart (" + a + ")");
    }).fail(function (a) {
        console.log("Cannot add the book to Cart...", a);
    }).always(function () {
        console.log("Adding the book to Cart...");
    });
    //*/
    $.ajax({
        type: "POST",
        data: JSON.stringify({ BookId: e.currentTarget.getAttribute("data-BookId"), Quantity: 1 }),
        url: ajaxUrl,
        contentType: "application/json; charset=utf-8",
        dataType: 'json'
    }).done(function (a) {
        console.log("Book added to the Cart...");
        $("#Nav_Item_CartPartial a").text("Cart (" + a.Quantity + ")");
    }).fail(function (a) {
        console.log("Cannot add the book to Cart...", a);
    }).always(function () {
        console.log("Adding the book to Cart...");
    });
    return false;
});