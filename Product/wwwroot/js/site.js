// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



// Add Data Function

$(".button").click(function () {
    var id = $(this).attr('id');
    $.ajax({
        url: "@Url.Action("Create", "Product")",
        type: "GET",
        dataType: "html",
        data: { id: id },
        success: function (data) {
            $("#modal_" + id).html(data);//target position of modal
            $('#pqr_' + id).modal('toggle');
        }
    });
});

public ActionResult GetDataForPopUpWindow(int id)
{
    using(YourDBContext)
    {
        var data = YourData;
      pqr pqrModel = new pqr();
        pqrModel.YourModelProperty = data.CorrespondingProperty;
        return PartialView("_GridPartialView", pqrModel);
    }
}