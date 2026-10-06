window.showToast = function (msg, type = "success") {
    Swal.fire({
        text: msg,
        icon: type, // success/error/warning/info 自带对应颜色
        timer: 2000,
        showConfirmButton: false
    })
}