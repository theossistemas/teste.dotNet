export function SoNumeros(evt: any) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    var controlKey = (evt.ctrlKey) ? evt.ctrlKey : evt.ctrlKey;
    if (charCode >= 48 && charCode <= 57 || charCode <= 46 || controlKey) {

    } else {
        evt.cancelBubble = true;
        evt.returnValue = false;
        evt.preventDefault();
    }
}