"use strict";
var KTUsersUpdateDetails = function () {
    const t = document.getElementById("kt_modal_update_details"),
        e = t.querySelector("#kt_modal_update_user_form"),
        n = new bootstrap.Modal(t);
    return {
        init: function () {
            (() => {
                t.querySelector('[data-kt-users-modal-action="close"]').addEventListener("click", (t => {
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
                        t.value ? (e.reset(), n.hide()) : "cancel" === t.dismiss && Swal.fire({
                            text: "Your form has not been cancelled!.",
                            icon: "error",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        })
                    }))
                })), t.querySelector('[data-kt-users-modal-action="cancel"]').addEventListener("click", (t => {
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
                        t.value ? (e.reset(), n.hide()) : "cancel" === t.dismiss && Swal.fire({
                            text: "Your form has not been cancelled!.",
                            icon: "error",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        })
                    }))
                }));
                const o = t.querySelector('[data-kt-users-modal-action="submit"]');
                o.addEventListener("click", (function (t) {
                    if ($('#IsClubPassPlayer').prop('checked')) {
                        var isclub = true;
                    }
                    else {
                        isclub = false;
                    }

                    

                    var formData = new FormData($('#kt_modal_update_user_form')[0]);// form Id - formUploadInvoice
                    formData.append('baseImage', $('input[type=file]')[0].files[0]);
                    formData.append('id', $('#Id').val());
                    formData.append('playerName', $('#PlayerName').val());
                    formData.append('emailAddress', $('#EmailAddress').val());
                    formData.append('teamId', $('#TeamId').val());
                    formData.append('age', $('#Age').val());
                    formData.append('isclubpassPlayer', isclub);

                    console.log(formData);
                    $.ajax({
                        type: "POST",
                        url: '/Player/AddPlayer',
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            
                        }
                    });


                    t.preventDefault(), o.setAttribute("data-kt-indicator", "on"), o.disabled = !0, setTimeout((function () {
                        o.removeAttribute("data-kt-indicator"), o.disabled = !1, Swal.fire({
                            text: "Form has been successfully submitted!",
                            icon: "success",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then((function (t) {
                            t.isConfirmed && n.hide(),
                            window.location = "/Player/Index";
                        }))
                    }), 2e3)
                }))
            })()
        }
    }
}();
KTUtil.onDOMContentLoaded((function () {
    KTUsersUpdateDetails.init()
}));