function checkValidEmail(inputTag) {
    var emailReg = /^([\w\.])+@([a-zA-Z0-9\-])+\.([a-zA-Z]{2,4})(\.[a-zA-Z]{2,4})?$/;
    var pThongbao = document.getElementById("pthongbao");

    if (inputTag.value.match(emailReg)) {
        return true;
    } else {
        return false;
    }
}

$('#frmNewsletter').submit(function (e) {
    var email = document.getElementById('input-emailNewsLetter');

    if (!checkValidEmail(email)) {
        e.preventDefault()
        $(this).get(0).reset()
        alertify.error('Email không hợp lệ')
    } else {
        e.preventDefault()
        $(this).get(0).reset()
        alertify.success('Đã đăng ký thành công')
    }
})