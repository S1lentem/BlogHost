// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#create_text").click(function () {
        $('#post_elements').append('<div class="form-group container">'
            + '<label> Post:</label >'
            + '<textarea class= "form-control" rows = "5" placeholder = "Enter your new post" name="texts"></textarea >'
            + '<input type="hidden" name="order" value="text">'
            + '</div>');
    })
});

$(document).ready(function () {
    $("#create_image").click(function () {
        $('#post_elements').append('<div class="form-group container">'
            + '<label> Image:</label >'
            + '<input type="file" name="files"/>'
            + '<input type="hidden" name="order" value="image">'
            + '</div>')
    })
}); 