var isNS = (navigator.appName == "Netscape") ? 1 : 0; if (navigator.appName == "Netscape") document.captureEvents(Event.MOUSEDOWN || Event.MOUSEUP); function mischandler() { return false; } function mousehandler(e) { var myevent = (isNS) ? e : event; var eventbutton = (isNS) ? myevent.which : myevent.button; if ((eventbutton == 2) || (eventbutton == 3)) return false; } document.oncontextmenu = mischandler; document.onmousedown = mousehandler; document.onmouseup = mousehandler;
function doBeforePaste(control) {
    maxLength = control.attributes["maxLength"].value;
    if (maxLength) {
        event.returnValue = false;
    }
}

function doPaste(control) {
    maxLength = control.attributes["maxLength"].value;
    value = control.value;
    if (maxLength) {
        event.returnValue = false;
        maxLength = parseInt(maxLength);
        var oTR = control.document.selection.createRange();
        var iInsertLength = maxLength - value.length + oTR.text.length;
        var sData = window.clipboardData.getData("Text").substr(0, iInsertLength);
        oTR.text = sData;
    }
}

function LimitInput(control) {
    if (control.value.length > control.attributes["maxLength"].value) {
        control.value = control.value.substring(0, control.attributes["maxLength"].value);
    }
}

function CheckOnOff(rdoId, gridName) {
    //debugger
    var rdo = document.getElementById(rdoId);
    // Getting an array of all the "INPUT" controls on the form.
    var all = document.getElementsByTagName("input");
    for (i = 0; i < all.length; i++) {
        //Checking if it is a radio button, and also checking if the
        //id of that radio button is different than "rdoId" 
        if (all[i].type == "radio" && all[i].id != rdo.id) {
            var count = all[i].id.indexOf(gridName);
            if (count != -1) {
                all[i].checked = false;
            }
        }
    }
    rdo.checked = true; // Finally making the clicked radio button CHECKED
}

function CheckboxCheck1(rbid, gridName) {
    //var gv = document.getElementById("<%=GridView1.ClientID%>");
    var rb = document.getElementById(rbid);
    var rbs = gridName.getElementsByTagName("input");
    var row = rb.parentNode.parentNode;
    for (var i = 0; i < rbs.length; i++) {
        if (rbs[i].type == "check") {
            if (rbs[i].checked && rbs[i] != rb) {
                rbs[i].checked = false;
                break;
            }
        }
    }
}

function ConfirmChoice() {

    answer = confirm("Do you want to close?")

    if (answer != 0) {

        window.close();

    }
}

/*code for maintaining number only as an input*/
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}


/*Start Enh*/
var lstbox;
var isCtrl = false;

function key1(event) {

    if (event.keyCode == 17) isCtrl = false;
}

function key2(event, lst) {

    if (event.keyCode == 17) isCtrl = true;

    if (event.keyCode == 67 && isCtrl == true) {

        var selectedArray = new Array();
        var seBranchj = document.getElementById(lst);
        var i;
        var count = 0;

        for (i = 0; i < seBranchj.options.length; i++) {
            if (seBranchj.options[i].selected) {
                selectedArray[count] = seBranchj.options[i].text;
                count++;
            }
        }

        var s = '';
        for (i = 0; i < selectedArray.length; i++) {

            s = s + selectedArray[i] + '; ';

        }

        if (window.clipboardData && clipboardData.setData) {
            clipboardData.setData("Text", s);
        }

        return false;
    }
}


function copyOnDoubleClick(lstbox, txtbox) {
    var selectedArray = new Array();
    var seBranchj = document.getElementById(lstbox);
    var i;
    var count = 0;

    for (i = 0; i < seBranchj.options.length; i++) {
        if (seBranchj.options[i].selected) {
            selectedArray[count] = seBranchj.options[i].text;
            count++;
        }
    }

    var s = '';
    for (i = 0; i < selectedArray.length; i++) {
        if (i == 0)
            s = s + selectedArray[i];
        else
            s = s + '; ' + selectedArray[i];
    }

    document.getElementById(txtbox).value = document.getElementById(txtbox).value + s + '; ';
}

function showContextMenu(lst) {
    return;
    contextMenu.setCapture()
    var ScrollTop = document.body.scrollTop;
    if (ScrollTop == 0) {
        if (window.pageYOffset)
            ScrollTop = window.pageYOffset;
        else
            ScrollTop = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
    }

    contextMenu.style.pixelTop = event.clientY + ScrollTop; //document.body.scrollTop
    contextMenu.style.pixelLeft = event.clientX + document.body.scrollLeft;
    contextMenu.style.visibility = "visible"
    event.returnValue = false
    lstbox = lst;
}
function revert() {
    document.releaseCapture()
    hideMenu()
}
function hideMenu() {
    contextMenu.style.visibility = "hidden"
}
function handleClick() {

    copyToClipboard('text');
    revert();
    event.cancelBubble = true;
}

function highlight() {
    var elem = event.srcElement
    if (elem.className == "menuItem") {
        elem.className = "menuItemOn"
    }
}
function unhighlight() {
    var elem = event.srcElement
    if (elem.className == "menuItemOn") {
        elem.className = "menuItem"
    }
}
function copyToClipboard(s) {
    var selectedArray = new Array();
    var seBranchj = document.getElementById(lstbox);
    var i;
    var count = 0;
    for (i = 0; i < seBranchj.options.length; i++) {
        if (seBranchj.options[i].selected) {
            selectedArray[count] = seBranchj.options[i].text;
            count++;
        }
    }
    s = '';

    for (i = 0; i < selectedArray.length; i++) {
        if (i == 0)
            s = s + selectedArray[i] + '; ';
        else
            s = s + selectedArray[i] + '; ';
    }
    if (window.clipboardData && clipboardData.setData) {
        clipboardData.setData("Text", s);

    }
}

/*end enh*/

//to disable the Back/Forward
function noBack() {
    window.history.forward(-1);
}
noBack();
window.onload = noBack;
setTimeout("noBack()", 0);
window.onunload = function () { null };


//to disable Refreash(F5) and new window(Cntr+N) and (Alt + <--)

document.onkeydown = function () {
    if ((event.keyCode == 78) && (event.ctrlKey)) {
        window.event.cancelBubble = true;
        window.event.returnValue = false;
        return false;
    }
    if (window.event && window.event.keyCode == 116) {
        window.event.keyCode = 37;
    }
    if (event.keyCode == 37) {
        window.event.cancelBubble = true;
        window.event.returnValue = false;
        return false;
    }
}

function CheckAll(me) {
    var index = me.id.lastIndexOf('_');
    var prefix = me.id.substr(0, index);
    for (i = 0; i < document.forms[0].length; i++) {
        var o = document.forms[0][i];
        if (o.type == 'checkbox') {
            if (me.id != o.id) {
                if (o.id.substring(0, prefix.length) == prefix) {
                    if (o.checked == true && o.isDisabled == true) {
                        o.checked = true;
                    }
                    else {
                        o.checked = !me.checked;

                    }
                    o.click();
                }
            }
        }
    }
}

function CheckRadio(rdoId, gridName) {
    var all = document.getElementsByTagName("input");
    for (i = 0; i < all.length; i++) {
        if (all[i].type == "radio" && all[i].id != rdoId.id) {
            var count = all[i].id.indexOf(gridName);
            if (count != -1) {
                all[i].checked = false;
            }
        }
    }
    rdoId.checked = true;
}

function CheckRadioGridView(rdoId, gridName) {
    var index = rdoId.id.lastIndexOf('_');
    var prefix = rdoId.id.substr(0, index);
    for (i = 0; i < document.forms[0].length; i++) {
        var o = document.forms[0][i];
        if (o.type == 'radio') {
            if (rdoId.id != o.id) {
                if (o.id.substring(0, prefix.length) == prefix) {
                    o.checked = false;
                }
            }
        }
    }
    rdoId.checked = true;
}

