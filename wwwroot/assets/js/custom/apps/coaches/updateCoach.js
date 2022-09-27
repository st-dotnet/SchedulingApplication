
"use strict";
var ModalUpdateCoaches = function () {
    var t, e, n, o, c, r;
    return {
        init: function () {
            t = document.querySelector("#kt_modal_update_customer"), r = new bootstrap.Modal(t), c = t.querySelector("#kt_modal_update_customer_form"), e = c.querySelector("#kt_modal_update_customer_submit"), n = c.querySelector("#kt_modal_update_customer_cancel"), o = t.querySelector("#kt_modal_update_customer_close"), e.addEventListener("click", (function (t) {
                t.preventDefault(), e.setAttribute("data-kt-indicator", "on"), setTimeout((function () {

                    var formData = new FormData($('#kt_modal_update_customer_form')[0]);
                    formData.append('baseImage', $('input[type=file]')[0].files[0]);
                    formData.append('name', $('#name').val());
                    formData.append('emailAddress', $('#EmailAddress').val());
                    console.log(formData);

                    $.ajax({
                        type: "POST",
                        url: '/Coach/AddCoach',
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            console.log(data);
                        }
                    });
                    e.removeAttribute("data-kt-indicator"), Swal.fire({
                        text: "Form has been successfully submitted!",
                        icon: "success",
                        buttonsStyling: !1,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    }).then((function (t) {
                        t.isConfirmed && r.hide()
                    }))
                }), 2e3)
            })), n.addEventListener("click", (function (t) {
                t.preventDefault(), Swal.fire({
                    text: "Are you sure you would like to cancel?",
                    icon: "warning",
                    showCancelButton: !0,
                    buttonsStyling: !1,
                    confirmButtonText: "Yes, cancel it!",
                    cancelButtonText: "No, return",
                    customClass: {
                        confirmButton: "btn btn-primary",
                        cancelButton: "btn btn-active-light"
                    }
                }).then((function (t) {
                    t.value ? (c.reset(), r.hide()) : "cancel" === t.dismiss && Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: !1,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    })
                }))
            })), o.addEventListener("click", (function (t) {
                t.preventDefault(), Swal.fire({
                    text: "Are you sure you would like to cancel?",
                    icon: "warning",
                    showCancelButton: !0,
                    buttonsStyling: !1,
                    confirmButtonText: "Yes, cancel it!",
                    cancelButtonText: "No, return",
                    customClass: {
                        confirmButton: "btn btn-primary",
                        cancelButton: "btn btn-active-light"
                    }
                }).then((function (t) {
                    t.value ? (c.reset(), r.hide()) : "cancel" === t.dismiss && Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: !1,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    })
                }))
            }))
        }
    }
}();
KTUtil.onDOMContentLoaded((function () {
    ModalUpdateCoaches.init()
}));