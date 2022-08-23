$(document).ready(function() {
    LoadCkEditor4();
});

function LoadCkEditor4() {
    if (!document.getElementById("ckEditor4"))
        return;
    $("body").append("<script src='/ckeditor4/ckeditor.js'></script>");

    CKEDITOR.replace('ckEditor4',
        {
            costumConfig: '/ckeditor4/config.js'
        });
}