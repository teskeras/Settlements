// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let table = $('#settlementsTable')
if (table.length > 0) {
    let baseUrl = window.location.origin
    let pageNumber = 1
    let isFormValid = false
    $('#paging').on('change', function () {
        pageNumber = 1
        getTable()
    })
    $('#previousPage').on('click', function () {
        pageNumber--
        getTable()
    })
    $('#nextPage').on('click', function () {
        pageNumber++
        getTable()
    })

    function getTable() {
        let pageSize = $('#paging').val()

        $.getJSON(baseUrl + '/api/settlements/getsettlements?pagenumber=' + pageNumber + '&pagesize=' + pageSize, function (data, status, request) {
            let headers = JSON.parse(request.getResponseHeader('X-Pagination'))
            let items = []
            $.each(data, function (key, val) {
                items.push('<tr class="tableRow" data-toggle="modal" data-target="#modal" data-id="' + val.id + '"><td>' + val.settlementName + '</td><td>' + val.postalCode + '</td><td>' + val.country.countryName + '</td></tr>')
            })
            $('tbody').remove()
            $('<tbody/>', {
                html: items.join('')
            }).appendTo(table)
            if (headers.HasPrevious) {
                $('#previousPage').prop('disabled', false)
            }
            else {
                $('#previousPage').prop('disabled', true)
            }
            if (headers.HasNext) {
                $('#nextPage').prop('disabled', false)
            }
            else {
                $('#nextPage').prop('disabled', true)
            }
            $('#totalCount').html('Total results: ' + headers.TotalCount)
        })
    }
    

    getTable()

    function delay(callback, ms) {
        let timer = 0;
        return function () {
            let context = this, args = arguments;
            clearTimeout(timer);
            timer = setTimeout(function () {
                callback.apply(context, args);
            }, ms || 0);
        };
    }

    $('#countryName').on('keyup', delay(function (e) {
        let search = $('#countryName').val().trim()
        if (search == "") {
            $('#countries').remove()
            let countriesDiv = $('#countriesDiv')
            $('<datalist id="countries">').appendTo(countriesDiv)
        }
        if (search) {
            $.getJSON(baseUrl + '/api/settlements/getcountries/' + search, function (data) {
                $('#countries').remove()
                let countriesDiv = $('#countriesDiv')
                $('<datalist id="countries">').appendTo(countriesDiv)

                $.each(data, function (key, val) {
                    $('#countries').append('<option data-value=' + val.id + ' value="' + val.countryName + '">')
                })
            })
        }
    }, 500))

    $('#modal').on('show.bs.modal', function (event) {
        let modal = $(this)
        let clicked = $(event.relatedTarget)
        let save = $('#saveModal')
        save.unbind('click')
        let forms = document.getElementsByClassName('needs-validation')
        let validation = Array.prototype.filter.call(forms, function (form) {
            save.on('click', function (event) {
                let countryName = $('#countryName').val()
                if (form.checkValidity() === false || !$('#countries option[value="' + countryName + '"]').length) {
                    isFormValid = false
                    event.preventDefault()
                    event.stopPropagation()
                }
                else {
                    isFormValid = true
                }
                form.classList.add('was-validated')
            })
        })
        if (clicked.data('id') == 'addSettlement') {
            modal.find('.modal-title').text('Add new settlement')
            $('#deleteSettlement').hide()
            $('#settlementName').val('')
            $('#postalCode').val('')
            $('#countryName').val('')
            save.on('click', function () {
                if (!isFormValid) {
                    return
                }
                let settlementName = $('#settlementName').val()
                let postalCode = $('#postalCode').val()
                let countryName = $('#countryName').val()
                let countryId = Number($('#countries option[value="' + countryName + '"]').attr('data-value'))

                $.ajax({
                    type: "POST",
                    url: baseUrl + '/api/settlements/createsettlement',
                    data: JSON.stringify({ SettlementName: settlementName, PostalCode: postalCode, CountryId: countryId }),
                    success: function (data) {
                        location.reload();
                    },
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8'
                })
            })
        }
        else {
            let id = clicked.data('id')
            modal.find('.modal-title').text('Edit settlement')
            $('#deleteSettlement').show()
            $.getJSON(baseUrl + '/api/settlements/getsettlement/' + id, function (data) {
                $('#settlementName').val(data.settlementName)
                $('#postalCode').val(data.postalCode)
                $('#countryName').val(data.country.countryName)
            })
            isFormValid = true
            $('#countryName').trigger('keyup')
            save.on('click', function () {
                if (!isFormValid) {
                    return
                }
                let settlementName = $('#settlementName').val()
                let postalCode = $('#postalCode').val()
                let countryName = $('#countryName').val()
                let countryId = Number($('#countries option[value="' + countryName + '"]').attr('data-value'))

                $.ajax({
                    type: "PUT",
                    url: baseUrl + '/api/settlements/editsettlement/' + id,
                    data: JSON.stringify({ Id: id, SettlementName: settlementName, PostalCode: postalCode, CountryId: countryId }),
                    success: function (data) {
                        location.reload();
                    },
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8'
                })
            })
            $('#deleteSettlement').on('click', function () {
                $.ajax({
                    type: 'DELETE',
                    url: baseUrl + '/api/settlements/deleteSettlement/' + id,
                    success: function (data) {
                        location.reload();
                    }
                })
            })
        }
    })

    $('#countryName').on('change', function (e) {
        let countryName = $('#countryName').val()
        if (!$('#countries option[value="' + countryName + '"]').length) {
            this.setCustomValidity('false')
        }
        else {
            this.setCustomValidity('')
        }
    })
}
