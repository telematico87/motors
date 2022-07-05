
function solonumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

function validaEmail(email) {
    var regex = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    var rpt = regex.test(email);
    return rpt;
}


function Decimal(t, evnt) {
    var a = isValidDecimal(t, evnt);
    return a;
}

function isValidDecimal(el, evnt) {
    var char = (evnt.which) ? evnt.which : evnt.keyCode;
    if (char == 46) {
        if (el.value.indexOf('.') === -1) {
            return true;
        } else {
            return false;
        }
    } else {
        if (char > 31 && (char < 48 || char > 57))
            return false;
    }

    return true;
}



function separadorDecimales(n) {
    n = n.toString()
    while (true) {
        var n2 = n.replace(/(\d)(\d{5})($|,|\.)/g, '$1,$2$3')
        if (n == n2) break
        n = n2
    }
    return n;
}