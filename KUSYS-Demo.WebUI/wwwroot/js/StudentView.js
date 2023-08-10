$(document).ready(function () {
    $('#usersTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "",
            "sInfo": "Showing 1 to 1 of 1 entries",
            "sInfoEmpty": "No records",
            "sInfoFiltered": "(MAX data inside)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "",
            "sLoadingRecords": "Loading...",
            "sProcessing": "Processing...",
            "sSearch": "Search:",
            "sZeroRecords": "No matching records found",
            "oPaginate": {
                "sFirst": "First",
                "sLast": "Last",
                "sNext": "Next",
                "sPrevious": "Previous"
            },
            "oAria": {
                "sSortAscending": ": Sort Asc active",
                "sSortDescending": ": Sort Desc active"
            },
            "select": {
                "rows": {
                    "_": "%d co seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        }
    });
});