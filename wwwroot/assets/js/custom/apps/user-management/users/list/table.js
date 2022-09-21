"use strict";
var KTUsersList = function () {
    var datatable, t,
        n,
        r,
        modal = document.getElementById("kt_modal_add_user"),
        bootstrapModal = new bootstrap.Modal(modal),
        table = document.getElementById("kt_table_users"),
        c = () => {
            table.querySelectorAll('[data-kt-users-table-filter="delete_row"]').forEach((t) => {
                t.addEventListener("click", function (t) {   
                    t.preventDefault();
                    const n = t.target.closest("tr"),
                          r = n.querySelectorAll("td")[2].innerText;
                    var id = $(this).attr("data-id")
                    Swal.fire({
                        text: "Are you sure you want to delete " + r + "?",
                        icon: "warning",
                        showCancelButton: !0,
                        buttonsStyling: !1,
                        confirmButtonText: "Yes, delete!",
                        cancelButtonText: "No, cancel",
                        customClass: { confirmButton: "btn fw-bold btn-danger", cancelButton: "btn fw-bold btn-active-light-primary" },
                    }).then(function (t) {
                        debugger;
                        $.ajax({
                            url: '/Player/DeletePlayer' + "?id=" + id,
                            type: 'Delete',
                            data : id,
                            success: function (data) {
                                if (data.success) {
                                    debugger;
                                    
                                }
                            },
                        });
                        t.value
                            ? Swal.fire({ text: "You have deleted " + r + "!.", icon: "success", buttonsStyling: !1, confirmButtonText: "Ok, got it!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                .then(function () {
                                    window.location = "/Player/Index/";
                                })
                                .then(function () {
                                    a();
                                })
                            : "cancel" === t.dismiss && Swal.fire({ text: customerName + " was not deleted.", icon: "error", buttonsStyling: !1, confirmButtonText: "Ok, got it!", customClass: { confirmButton: "btn fw-bold btn-primary" } });
                    });
                });
            });
        },
        d = () => {
            table.querySelectorAll('[data-kt-users-modal-action="edit"]').forEach((t) => {
                debugger;
                t.addEventListener("click", function (e) {
                    console.log('started');
                    e.preventDefault();
                    bootstrapModal.show();
                    debugger;
                    $.ajax({
                        url: '/Player/Edit/'+row.id,
                        type: 'GET',
                        success: function (data) {
                            document.getElementsByClassName("image-input-wrapper")[0].style = 'background-image: url(/media/users/' + data.id + '.jpg);';
                            $("#Id").val(data.id);
                            $("#FirstName").val(data.firstName);
                            $("#LastName").val(data.lastName);
                            $("#Email").val(data.email);
                            $("#Alias").val(data.alias);
                            $("#QuarterlyGoal").val(data.quarterlyGoal);
                            document.getElementById("Role" + data.role).checked = true;
                        },
                        cache: false,
                        contentType: false,
                        processData: false
                    });
                });
            });
        },
        l = () => {
            const c = table.querySelectorAll('[type="checkbox"]');
            (t = document.querySelector('[data-kt-user-table-toolbar="base"]')), (n = document.querySelector('[data-kt-user-table-toolbar="selected"]')), (r = document.querySelector('[data-kt-user-table-select="selected_count"]'));
            const s = document.querySelector('[data-kt-user-table-select="delete_selected"]');
            c.forEach((e) => {
                e.addEventListener("click", function () {
                    debugger;

                    $("#checkAll").click(function () {
                        $(".checkBox").prop('checked',
                            $(this).prop('checked'));
                    });

                    setTimeout(function () {
                        a();
                    }, 50);
                });
            }),
                s.addEventListener("click", function () {
                    Swal.fire({
                        text: "Are you sure you want to delete selected customers?",
                        icon: "warning",
                        showCancelButton: !0,
                        buttonsStyling: !1,
                        confirmButtonText: "Yes, delete!",
                        cancelButtonText: "No, cancel",
                        customClass: { confirmButton: "btn fw-bold btn-danger", cancelButton: "btn fw-bold btn-active-light-primary" },
                    }).then(function (t) {
                        debugger;

                        var selectedIDs = new Array();
                        $('input:checkbox.checkBox').each(function () {
                            if ($(this).prop('checked')) {
                                selectedIDs.push($(this).attr('data-id'));
                            }
                        });
                        debugger;
                        var postData = { values: selectedIDs }
                        console.log(postData);
                        $.ajax({
                            "url": "/Player/DeleteMultiplePlayer/",
                            "type": "POST",
                            "data": postData,
                            "dataType": "json",
                            success: function (data) {
                                if (data.success) {
                                    debugger;

                                }
                            },
                            "traditional": true,
                        }),

                        t.value
                            ? 
                            Swal.fire({ text: "You have deleted all selected customers!.", icon: "success", buttonsStyling: !1, confirmButtonText: "Ok, got it!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                .then(function () {
                                    c.forEach((t) => {
                                        t.checked &&
                                            datatable
                                                .row($(t.closest("tbody tr")))
                                                .remove()
                                                .draw();
                                    });
                                    table.querySelectorAll('[type="checkbox"]')[0].checked = !1;
                                })
                                .then(function () {
                                    a(), l();
                                })
                            : "cancel" === t.dismiss &&
                            Swal.fire({ text: "Selected customers was not deleted.", icon: "error", buttonsStyling: !1, confirmButtonText: "Ok, got it!", customClass: { confirmButton: "btn fw-bold btn-primary" } });
                    });
                });
        };
    const a = () => {
        const e = table.querySelectorAll('tbody [type="checkbox"]');
        let c = !1,
            l = 0;
        e.forEach((e => {
            e.checked && (c = !0, l++)
        })), c ? (r.innerHTML = l, t.classList.add("d-none"), n.classList.remove("d-none")) : (t.classList.remove("d-none"), n.classList.add("d-none"))
    };
    return {
        init: function () {
            table &&
                (
                    (
                        datatable = $(table).DataTable({
                            "processing": true,
                            "serverSide": true,
                            "filter": true,
                            "paging": true,
                            "pageLength": 10,
                            "ajax": {
                                "url": "/player/GetPlayers",
                                "type": "POST",
                                "dataType": "json",
                                "data": function (d) {
                                    // d.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]').val();
                                    return d;
                                }
                            },
                            "columnDefs": [
                                { orderable: !1, targets: 0 },
                                { orderable: !1, targets: 6 },
                            ],
                            "columns": [
                                {
                                    "data": "id",
                                    "render": function (data, type, row, meta) { 
                                        return `<div class='form-check form-check-sm form-check-custom form-check-solid'><input id="checkBoxId" class='form-check-input checkBox' type='checkbox' data-id=${row.id}></div>`;
                                    }
                                }, 
                                {
                                    "data": "emailAddress",
                                    "render": function (data, type, row, meta) {
                                        return `<div class='symbol symbol-circle symbol-50px overflow-hidden me-3' >
		                                                <div class='symbol-label'>
                                                            <img src="data:image/png;base64, ${row.image}" alt="${row.playerName}" />
		                                                </div>
                                                </div>
                                                <div class='d-flex flex-column'>
	                                                <a href='#' class='text-gray-800 text-hover-primary mb-1'>${row.playerName}</a>
	                                                <span>${data}</span>
                                                </div>`
                                    }
                                },
                                { "data": "playerName", "name": "Name", "autoWidth": true },
                                { "data": "age", "name": "Age", "autoWidth": true },
                                { "data": "team", "name": "Team", "autoWidth": true },
                                { "data": "isClubPassPlayer", "name": "isClubPassPlayer", "autoWidth": true },
                                { 
                                    "render": function (data, type, row, meta) {
                                        return `<a href='#'  class='btn btn-light btn-active-light-primary btn-sm' data-bs-toggle="dropdown" aria-expanded="false">
		                                            Actions
	                                                <span class='svg-icon svg-icon-5 m-0'>
			                                                <svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none'>
				                                                <path d='M11.4343 12.7344L7.25 8.55005C6.83579 8.13583 6.16421 8.13584 5.75 8.55005C5.33579 8.96426 5.33579 9.63583 5.75 10.05L11.2929 15.5929C11.6834 15.9835 12.3166 15.9835 12.7071 15.5929L18.25 10.05C18.6642 9.63584 18.6642 8.96426 18.25 8.55005C17.8358 8.13584 17.1642 8.13584 16.75 8.55005L12.5657 12.7344C12.2533 13.0468 11.7467 13.0468 11.4343 12.7344Z' fill='black'></path>
			                                                </svg>
	                                                </span>
                                                </a>
                                                <div class='dropdown-menu menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-bold fs-7 w-125px py-4' data-kt-menu='true'>
	                                                <div class='menu-item px-3'>
		                                                <a href="/Player/Edit/${row.id}" class="menu-link px-3" data-id=data>Edit</a>
	                                                </div> 
	                                                <div class='menu-item px-3'>
		                                               <a href="#" class="menu-link px-3" data-kt-users-table-filter="delete_row" data-id=${row.id}>Delete</a>
	                                                </div>
                                                </div>`
                                    }
                                }
                            ]
                        })
                    ).on("draw", function () {

                        table.querySelectorAll("tbody tr").forEach((e) => {
                        }),

                            l(), c(), d(), a();
                    }),
                    l(),
                    document.querySelector('[data-kt-user-table-filter="search"]').addEventListener("keyup", function (t) {
                       debugger;
                        datatable.search(t.target.value).draw();
                    }),
                    document.querySelector('[data-kt-user-table-filter="reset"]').addEventListener("click", function () {
                        document
                            .querySelector('[data-kt-user-table-filter="form"]')
                            .querySelectorAll("select")
                            .forEach((e) => {
                                $(e).val("").trigger("change");
                            }),
                            datatable.search("").draw();
                    }),
                    c(), d(),  
                    (() => {
                        const t = document.querySelector('[data-kt-user-table-filter="form"]'),
                            n = t.querySelector('[data-kt-user-table-filter="filter"]'),
                            r = t.querySelectorAll("select");
                        n.addEventListener("click", function () {
                            var t = "";
                            r.forEach((e, n) => {
                                e.value && "" !== e.value && (0 !== n && (t += " "), (t += e.value));
                            }),
                                datatable.search(t).draw();
                        });
                    })());
        },
    };
}();
KTUtil.onDOMContentLoaded((function () {
    KTUsersList.init()
}));