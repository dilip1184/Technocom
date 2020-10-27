
function IsNumberAndDecimalKey(evt, obj) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    var value = obj.value;
    var dotcontains = value.indexOf(".") != -1;
    if (dotcontains)
        if (charCode == 46) return false;
    if (charCode == 46) return true;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function IsNumberKey(evt, obj) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    var value = obj.value;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function IsNumberAndDashKey(evt, obj) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    var value = obj.value;
    var dashcontains = value.indexOf("-") != -1;
    if (dashcontains)
        if (charCode == 45) return false;
    if (charCode == 45) return true;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function PhoneValidationKey(evt, obj) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    var value = obj.value;
    //var dashcontains = value.indexOf("-") != -1;
    //if (dashcontains)
    //    if (charCode == 45) return false;
    if (charCode == 35) return true;
    if (charCode == 40) return true;
    if (charCode == 41) return true;
    if (charCode == 43) return true;
    if (charCode == 45) return true;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}
















