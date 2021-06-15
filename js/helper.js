var _validImageExtensions = [".jpg", ".jpeg", ".png"];

function ValidateImageExt(oInput) {
    if (oInput.type == "file") {
        var sFileName = oInput.value;
        if (sFileName.length > 0) {
            var blnValid = false;
            for (var j = 0; j < _validImageExtensions.length; j++) {
                var sCurExtension = _validImageExtensions[j];
                if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                    blnValid = true;
                    break;
                }
            }

            if (!blnValid) {
                document.getElementById("image_message").innerHTML = "Invalid image format, please select .jpg, .jpeg or .png file";
                oInput.value = "";
                return false;
            }
        }
    }

    document.getElementById("image_message").innerHTML = "";
    return true;
}

var _validFileExtensions = [".pdf", ".docx"];

function ValidateFileExt(oInput, id) {
    if (oInput.type == "file") {
        var sFileName = oInput.value;
        if (sFileName.length > 0) {
            var blnValid = false;
            for (var j = 0; j < _validFileExtensions.length; j++) {
                var sCurExtension = _validFileExtensions[j];
                if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                    blnValid = true;
                    break;
                }
            }

            if (!blnValid) {
                document.getElementById(id).innerHTML = "Invalid file format, please select a .pdf or .docx file";
                oInput.value = "";
                return false;
            }
        }
    }
    document.getElementById(id).innerHTML = "";
    return true;
}

function changeSrc(url) {
    var frame = document.getElementById("portal");
    frame.src = url;
    return;
}

function openForm() {

    document.getElementById("myForm").style.display = "block";
    document.getElementById("blurry_content").style.display = "none";

}

function closeForm() {

    document.getElementById("myForm").style.display = "none";
    document.getElementById("blurry_content").style.display = "block";
}

function change() {

    var sidebar = document.getElementById("sidebar");
    if (sidebar.classList.contains('active')) {
        sidebar.classList.remove('active');

    } else {
        sidebar.classList.add('active');
    }

    console.log("Blur");
    return;

}

function expand(id) {

    var ul = document.getElementById(id);
    if (ul.getAttribute('area-expanded') == 'true') {
        ul.setAttribute('area-expanded', 'false');
    }
    else {
        ul.setAttribute('area-expanded', 'true');
    }

    console.log("Blur");
    return;
}

function validateRadios() {
    var police = document.getElementById("Police");
    alert(police.value);
    return;
}

function checkSoudyNumber(txtNum, id) {

    if (txtNum.value.length == 10) {
        var regex = /^0(6|7|8){1}[0-9]{1}[0-9]{7}$/;

        if (regex.test(txtNum.value) === false) {
            document.getElementById(id).innerText = 'please enter a valid south african number';
        }
        else {
            document.getElementById(id).innerText = '';
        }
    }

}

function checkPasswordStrength(txtPass, id) {

    if (txtPass.value.length < 6) {
        document.getElementById(id).innerText = 'minimum of 6 characters required for password';
    }
    else {
        document.getElementById(id).innerText = '';

        var strongRegex = /(?=.*[!@#$%^&*])/;

        if (strongRegex.test(txtPass.value) === false) {
            document.getElementById(id).innerText = 'password must contain at least one special character';
        }
        else {

            document.getElementById(id).innerText = '';
            document.getElementById('hiddenpassword').innerText = txtPass.value;
        }

    }
}

function confirmPasswordCheck(txtConfirmPass, id) {

    if (txtConfirmPass.value.length >= 6) {
        var pass = document.getElementById('hiddenpassword').innerText;
        var confPass = txtConfirmPass.value;
        if (pass == confPass) {
            document.getElementById(id).innerText = '';
        }
        else {
            document.getElementById(id).innerText = "passwords don't match";
        }
    }
}

function validateEmail(emailField, id) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if (reg.test(emailField.value) === false) {
        document.getElementById(id).innerText = 'please enter valid email address';
    }
    else {
        document.getElementById(id).innerText = '';
    }

}

function validateAge(txtAge, id) {

    if (txtAge.value < 14) {
        document.getElementById(id).innerText = 'please enter valid age';

    }
    else {
        document.getElementById(id).innerText = '';
    }
}
