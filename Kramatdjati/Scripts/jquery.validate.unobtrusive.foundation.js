(function ($) {

    //#region Unobtrusive Validation Foundation Integration

    $.validator.setDefaults({
        highlight: function (element, errorClass, validClass) {
            $(element).removeClass(validClass).addClass("error").addClass(errorClass);
            $(element.form).find("label[for=" + element.id + "]").removeClass(validClass).addClass("error").addClass(errorClass);
            $(element.form).find("span[data-valmsg-for=" + element.id + "]").removeClass(validClass).addClass("error").addClass(errorClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("error").removeClass(errorClass).addClass(validClass);
            $(element.form).find("label[for=" + element.id + "]").removeClass("error").removeClass(errorClass).addClass(validClass);
            $(element.form).find("span[data-valmsg-for=" + element.id + "]").removeClass("error").removeClass(errorClass).addClass(validClass);
        }
    });

    $("form span.field-validation-error").addClass("error");
    $("form div.row").has("span.field-validation-error").find("label, input, select, textarea, span.field-validation-error").addClass("error");
    $("form div.validation-summary-errors").has("li:visible").addClass("alert-box alert");

    //#endregion

})(window.jQuery);
