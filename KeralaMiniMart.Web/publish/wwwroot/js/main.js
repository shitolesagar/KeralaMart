$(".toggle-password").click(function () {

    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});


$('#menu-btn').click(function () {
    if ($(this).prop("checked") == true) {
        $('.left_col').css('margin-left', '0px');
        $('.top_nav,.right_col').css({ 'margin-left': '200px', 'width': 'calc(100% - 200px)' });
        $('.hamburger-menu-icon').css({ 'position': 'relative', 'right': '0' });
        $('.hideMobile-txt').css('display', 'inline-block');
        $('.signOut-section').css({ 'left': '20px', 'right': 'auto' });
        $('.dashboard-logo').css('visibility', 'visible');
    }
    else if ($(this).prop("checked") == false) {
        $('.left_col').css('margin-left', '-150px');
        $('.top_nav,.right_col').css({ 'margin-left': '50px', 'width': 'calc(100% - 50px)' });
        $('.hamburger-menu-icon').css({ 'position': 'absolute', 'right': '0' });
        $('.signOut-section').css({ 'left': 'auto', 'right': '5px' });
        $('.hideMobile-txt').css('display', 'none');
        $('.dashboard-logo').css('visibility', 'hidden');


    }
});

$(window).resize(function () {

    if ($(window).width() <= 1024) {
        $("#menu-btn").prop("checked", false);

    }

});

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

function ShowSlackbarSucessMessage(alertMsg) {
    $("#alertPopup").html("<strong>" + alertMsg + "</strong>")
        .removeAttr("hidden")
        .fadeTo(2000, 500).slideUp(500, function () {
            $("#alertPopup").slideUp(500);
        });
}
function ShowSlackbarErrorMessage(alertMsg) {
    $("#alertErrorPopup").html("<strong>" + alertMsg + "</strong>")
        .removeAttr("hidden")
        .fadeTo(2000, 500).slideUp(500, function () {
            $("#alertErrorPopup").slideUp(500);
            $("#alertErrorPopup").text('');
        });
}


function redirectToDetails(url) {
    event.stopPropagation();
    window.location.href = url;
}
function redirectToEdit(url) {
    event.stopPropagation();
    window.location.href = url;
}
function OpenDeleteConfirmBox(id) {
    event.stopPropagation();
    $('#myModal').modal('show');
    $("#delete-id").val(id)
}

function goToPreviousPage() {
    window.history.back();
}


// show or hide filters 
function showHideFilters(elementId) {
    if ($("#indexPartialContent > table").length <= 0) {
        hideElement(elementId);
    } else {
        showElement(elementId);
    }
}

function showElement(elementId) {
    if ($("#" + elementId).not(":visible")) {
        $("#" + elementId).show();
    }
}

function hideElement(elementId) {
    if ($("#" + elementId).is(":visible")) {
        $("#" + elementId).hide();
    }
}
// show or hide filters end
