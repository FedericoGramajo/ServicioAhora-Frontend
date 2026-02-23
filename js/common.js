window.showToastr = function (type, message) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    if (type == "success") {
        toastr.success(message);
    }
    else if (type == "error") {
        toastr.error(message);
    }
    else if (type == "warning") {
        toastr.warning(message);
    }
    else if (type == "info") {
        toastr.info(message);
    }
}
window.showSwal = function (type, message) {
    if (type == "success") {
        Swal.fire({
            title: "Good job!",
            text: message,
            icon: "success"
        });
    }
    if (type == "error") {
        Swal.fire({
            title: "Task failed!",
            text: message,
            icon: "error"
        });
    }
}

window.ShowConfirmationModal = function () {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('bsConfirmationModal')).show();
}

window.HideConfirmationModal = function () {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('bsConfirmationModal')).hide();
}

window.ShowModal = function (id) {
    var element = document.getElementById(id);
    if (element) {
        bootstrap.Modal.getOrCreateInstance(element).show();
    }
}

window.HideModal = function (id) {
    var element = document.getElementById(id);
    if (element) {
        bootstrap.Modal.getOrCreateInstance(element).hide();
    }
}
