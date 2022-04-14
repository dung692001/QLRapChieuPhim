
    $(document).ready(function () {
        LoadDsHangSX();
        });
    $('#btnSubmitAdd').click(function () {

        let mahangsx = $('#mahangsx').val().trim();
    let tenhangsx = $('#tenhangsx').val().trim();
    let check = 1;
    if (mahangsx == "") {;
    document.getElementById("mahangsx_err").innerHTML = "Bạn chưa nhập mã hãng sản xuất!"; check = 0;
            }
    else {
        document.getElementById("mahangsx_err").innerHTML = "";
            }
    if (tenhangsx == "") {
        document.getElementById("tenhangsx_err").innerHTML = "Bạn để trống tên hãng sản xuất!"; check = 0;
            }
    else {
        document.getElementById("tenhangsx_err").innerHTML = "";
            }
    if( check ==1 )
    $.ajax({
        url: '/Admin/HangSXes/addHangSX',
    type: 'post',
    data: {
        mahangsx: mahangsx,
    tenhangsx: tenhangsx
                },
    success: function (data) {
                    if (data.code == 200) {
        $('#mahangsx').val('');
    $('#tenhangsx').val('');
    LoadDsHangSX();


                    } else {

        $('#modalHienThiLoi').modal();
    $('#mahangsx').val('');
                    }

                }
            });
        });
    $(document).on('click', "button[name='update']", function () {
        $('#btnSubmitAdd').hide();
    $('#btnSubmitEdit').show();
    $('#modaltitle').text("Sửa hãng sản xuất");
    let idHangSX = $(this).closest('tr').attr('id');
    //  alert(idHangSX);

    $.ajax({
        url: '/Admin/HangSXes/chiTietHangSX',
    type: 'get',
    data: {
        id: idHangSX
                },
    success: function (data) {

                    if (data.code == 200) {

        $('#mahangsx').val(data.da.MaHangSX);
    $('#mahangsx').prop('readonly', true);
    $('#tenhangsx').val(data.da.TenHangSX);
    //$('#tendoan1').prop('readonly', false);
    $('#modalHangSX').modal();


                    }


                }
            });
        });
    $(document).on('click', "button[name='view']", function () {
        let idHangSX = $(this).closest('tr').attr('id');
    //alert(idDoAn);
    $.ajax({
        url: '/Admin/HangSXes/chiTietHangSX',
    type: 'get',
    data: {
        id: idHangSX
                },
    success: function (data) {
        $('#tblChiTiet').empty();
    if (data.code == 200) {
        $('#modalChiTietHangSX').modal();
    let tr = '<tr>';
        tr += '<td>' + data.da.MaHangSX + '</td>';
        tr += '<td>' + data.da.TenHangSX + '</td>';


        tr += '</tr>';

    $('#tblChiTiet').append(tr);


                    }
                }
            });
        });
    $('#btnSubmitEdit').click(function () {
        let mahangsx = $('#mahangsx').val().trim();
    let tenhangsx = $('#tenhangsx').val().trim();
    //alert("edit");
    $.ajax({
        url: '/Admin/HangSXes/editHangSX',
    type: 'post',
    data: {
        mahangsx: mahangsx,
    tenhangsx: tenhangsx

                },
    success: function (data) {
                    if (data.code == 200) {
        LoadDsHangSX();
    $('#mahangsx').val('');
    $('#tenhangsx').val('');
    $('#modalHangSX').modal('hide');


                    }



                }
            });
        });
    $('#btnAdd').click(function () {
        $('#modaltitle').text("Thêm mới hãng sản xuất");
    $('#mahangsx').val('');
    $('#tenhangsx').val('');
    $('#btnSubmitEdit').hide();
    $('#btnSubmitAdd').show();
    $('#mahangsx').prop('readonly', false);
    $('#modalHangSX').modal();

        });
        //$(document).on('click', "button[name='update']", function () {
        //    $('#modalXoa').modal();
        //})
        let ma;
    $(document).on('click', "button[name='delete']", function () {

        $('#modalXoa').modal();
    ma = $(this).closest('tr').attr('id');


        });
    $('#btnDelete').click(function () {

        $.ajax({
            url: '/Admin/HangSXes/xoaHangSX',
            type: 'post',
            data: {
                id: ma
            },
            success: function (data) {
                if (data.code == 200) {

                    LoadDsHangSX();
                    $('#modalXoa').modal('hide');
                } else {
                    $('#modalHienThiLoiKhiXoa').modal();
                    $('#modalXoa').modal('hide');
                }

            }


        })
    });
    function LoadDsHangSX() {
        $.ajax({
            url: '/Admin/HangSXes/dsHangSX',
            type: 'get',
            success: function (data) {
                /*  console.log(data);*/

                $('#tblHangSX').empty();
                $.each(data.dsHangSX, function (k, v) {
                    let tr = '<tr id="' + v.MaHangSX + '">'
                    tr += '<td>' + v.MaHangSX + '</td>';
                    tr += '<td>' + v.TenHangSX + '</td>';

                    tr += '<td>';
                    tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                    tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                    tr += '<button class = "btn btn-xs btn-info" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';
                    tr += '</td>';
                    tr += '</tr>';
                    $('#tblHangSX').append(tr);

                });

            }

        });
        }
