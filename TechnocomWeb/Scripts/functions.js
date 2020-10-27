// JavaScript Document

$(document).ready(function () {

    $('div#list1a a').each(function () {
        $(this).click(function () {
            var getCurrID = $(this).attr('rel');
            if (getCurrID != null) {
                $('div#allContentContainer div.toggleDiv').hide();
                //alert(getCurrID);
                $(getCurrID).fadeIn('fast');
            }
        });
    });
});

$(function () {
    $("#menubarMid li").each(function () {
        $(this).hover(function () {
            $(this).addClass("active");
            $(this).find("ul").show();
        }, function () {
            $(this).removeClass("active");
            $(this).find("ul").hide();
        });
    });
});
