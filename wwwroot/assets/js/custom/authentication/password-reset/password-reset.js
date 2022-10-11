"use strict";
var KTPasswordResetGeneral = function () {
    var t, e, i;
    return {
        init: function () {
            t = document.querySelector("#kt_password_reset_form"), e = document.querySelector("#kt_password_reset_submit"), i = FormValidation.formValidation(t, {
                fields: {
                    email: {
                        validators: {
                            notEmpty: {
                                message: "Email address is required"
                            },
                            emailAddress: {
                                message: "The value is not a valid email address"
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger,
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: ".fv-row",
                        eleInvalidClass: "",
                        eleValidClass: ""
                    })
                }
            }), e.addEventListener("click", (function (o) {
                o.preventDefault(), i.validate().then((function (i) {
                    debugger;
                    var emailData = {
                        email: $('#Email').val()
                    };

                    console.log(emailData);
                    $.ajax({
                        url: '/Account/ForgotPassword/',
                        type: 'POST',
                        data: emailData,
                        success: function (response) {
                            debugger;
                            if (response.success == true) {
                                (e.setAttribute("data-kt-indicator", "on"), e.disabled = !0, setTimeout((function () {
                                    e.removeAttribute("data-kt-indicator"), e.disabled = !1, Swal.fire({
                                        text: "A link has been sent to your gmail Account to reset your password",
                                        icon: "success",
                                        buttonsStyling: !1,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    }).then((function (e) {
                                        e.isConfirmed && (t.querySelector('[name="email"]').value = "")
                                    }))
                                }), 1500));
                            } else {
                                Swal.fire({
                                    text: "Sorry, looks like there are some errors detected, please try again.",
                                    icon: "error",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                });
                            }
                        }
                    });
                }))
            }))
        }
    }
}();
KTUtil.onDOMContentLoaded((function () {
    KTPasswordResetGeneral.init()
}));