var submitButtonValiation = (function () {
    $('#btnSubmit').click(function () {
        var username = $('#txtUsername').val();
        
        if (username == '') {
            $('#lblError').text('Please enter username');
        }
        
    });
});


var loginCredentialValidation = (function () {
    $('.LoginCredential').keyup(function () {
        var username = $('#txtUsername').val();
        
        if (username == '') {
            $('#btnSubmit').attr('disabled', true);
            $('#lblError').text(' ');
        }
        else {
            $('#btnSubmit').attr('disabled', false);
            $('#lblError').text(' ');
        }
    });
});

var txtUserNameValidationFocus = (function () {
    $('#txtUsername').focus(function () {
        $('#txtUsername').css('background-color', 'yellow');
    });
});

var txtUserNameValidationBlur = (function () {
    $('#txtUsername').blur(function () {
        $('#txtUsername').css('background-color', 'white');
    });
});

var hidePopup = (function () {
    $('#loginpopup').hide();
});

var buttonClose = (function () {
    $('#btnClose').click(function () {
        $('#loginpopup').show();
    });
});



$(document)
    .ready(function() {
        submitButtonValiation();
        loginCredentialValidation();
        txtUserNameValidationFocus();
        txtUserNameValidationBlur();
        
    });

$(document).ready(function () {
    $("#loginpopup").dialog({
        autoOpen: false,
        title: 'Login',
        width: 200,
        height: 180,
        resizable: false,
        modal: true
    });

});

function openPopup() {
    $("#loginpopup").dialog("open");
}


