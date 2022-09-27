"use strict";
var KTModalCoachesAdd = function () {
    var t, e, o, n, r, i;
    return {
        init: function () {
            i = new bootstrap.Modal(document.querySelector("#modal_add_coach")), r = document.querySelector("#modal_add_coach_form"), t = r.querySelector("#modal_add_coach_submit"), e = r.querySelector("#modal_add_coach_cancel"), o = r.querySelector("#modal_add_coach_close"), n = FormValidation.formValidation(r, {
                plugins: {
                    trigger: new FormValidation.plugins.Trigger,
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: ".fv-row",
                        eleInvalidClass: "",
                        eleValidClass: ""
                    })
                }
            }), $(r.querySelector('[name="country"]')).on("change", (function () {
                n.revalidateField("country")
            })), t.addEventListener("click", (function (e) {
                e.preventDefault(), n && n.validate().then((function (e) {

                    var formData = new FormData($('#modal_add_coach_form')[0]);

                    formData.append('baseImage', $('input[type=file]')[0].files[0]);
                    formData.append('name', $('#name').val());
                    formData.append('emailAddress', $('#emailAddress').val());
                    console.log(formData);



                    /*var postCoach = {
                        image: $('input[type=file]').val(),
                        name: $('#Name').val(),
                        emailaddress: $('#EmailAddress').val(),
                    };*/


                    $.ajax({
                        type: "POST",
                        url: "/Coach/AddCoach",
                        data: formData,
                        contentType: false,
                        processData: false,
                        /*dataType: "json",*/
                        success: function (response) {
                            if (response.success) {
                                console.log(response);
                            }
                        }

                    }),

                        console.log("validated!"), "Valid" == e ? (t.setAttribute("data-kt-indicator", "on"), t.disabled = !0, setTimeout((function () {
                            t.removeAttribute("data-kt-indicator"), Swal.fire({
                                text: "Form has been successfully submitted!",
                                icon: "success",
                                buttonsStyling: !1,
                                confirmButtonText: "Ok, got it!",
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then((function (e) {
                                e.isConfirmed && (i.hide(), t.disabled = !1, window.location = "/Coach/Index")
                            }))
                        }), 2e3)) : Swal.fire({
                            text: "Sorry, looks like there are some errors detected, please try again.",
                            icon: "error",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        })
                }))
            })), e.addEventListener("click", (function (t) {
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
                    t.value ? (r.reset(), i.hide()) : "cancel" === t.dismiss && Swal.fire({
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
                    t.value ? (r.reset(), i.hide()) : "cancel" === t.dismiss && Swal.fire({
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
    KTModalCoachesAdd.init()
}));