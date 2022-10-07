"use strict";
var KTSigninGeneral = function () {
    var t, e, i;
    return {
        init: function () {
            t = document.querySelector("#kt_sign_in_form"), e = document.querySelector("#kt_sign_in_submit"), i = FormValidation.formValidation(t, {
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
                    },
                    password: {
                        validators: {
                            notEmpty: {
                                message: "The password is required"
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger,
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: ".fv-row"
                    })
                }
            }), e.addEventListener("click", (function (n) {
                n.preventDefault(), i.validate().then((function (i) {

                    var signInformData = {
                        email: $('#Email').val(),
                        password: $('#Password').val(),
                    };
                    console.log(signInformData);
                    $.ajax({
                        url: '/Account/LogInUser/',
                        type: 'POST',
                        data: signInformData,
                        success: function (response) {
                            if (response.result.success === true) {
                                (e.setAttribute("data-kt-indicator", "on"), e.disabled = !0, setTimeout((function () {
                                    e.removeAttribute("data-kt-indicator"), e.disabled = !1, Swal.fire({
                                        text: "You have successfully logged in!",
                                        icon: "success",
                                        buttonsStyling: !1,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    }).then((function (e) {
                                        e.isConfirmed && (t.querySelector('[name="email"]').value = "", t.querySelector('[name="password"]').value = "")
                                        if (response.result.message == "Admin") {
                                            window.location = "/Dashboard/Index"
                                        } else if (response.result.message == "Coach") {
                                            window.location = "/GameSchedule/ScheduleGames"
                                        } else {
                                            window.location = "/PlayerCorner/Index"
                                        }
                                    }))
                                }), 2e3))
                            }
                            else {
                                Swal.fire({
                                    text: "Sorry, You have entered wrong UserName and Password, please try again.",
                                    icon: "error",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                })
                            }
                        },
                    });
                }))
            }))
        }
    }
}();
KTUtil.onDOMContentLoaded((function () {
    KTSigninGeneral.init()
}));