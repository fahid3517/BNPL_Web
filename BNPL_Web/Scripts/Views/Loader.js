function ShowLoader() {
    $("#content").append('<div id="div-loading" class="loader">Loading<span></span></div>');
}
function HideLoader() {
    let loader = $("#content").find("#div-loading");
    loader.remove();
}
function ShowModalLoader() {
    $(".modal-body").append('<div id="modalLoading" class="loader">Loading<span></span></div>');
}
function HideModalLoader() {
    let loader = $(".modal-body").find("#modalLoading");
    loader.remove();
}