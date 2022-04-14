
    $(document).ready(function () {
        LoadDsNuocSX();
        });
    $('#btnSubmitAdd').click(function () {
        let manuocsx = $('#manuocsx').val().trim();
    let tennuocsx = $('#tennuocsx').val().trim();
    let check = 1;
    if (manuocsx == "") {
        document.getElementById("manuocsx_err").innerHTML = "Bạn chưa nhập mã!"; check = 0;
            }
    else {
        document.getElementById("manuocsx_err").innerHTML = "";
            }
    if (tennuocsx == "") {
        document.getElementById("tennuocsx_err").innerHTML = "Bạn chưa nhập tên"; check = 0;
            }
    else {
        document.getElementById("tennuocsx_err").innerHTML = "";
            }
    if (check == 1)
    $.ajax({
        url: '/Admin/NuocSanXuats/addNuocSanXuat',
    type: 'post',
    data: {
        manuocsx: manuocsx,
    tennuocsx: tennuocsx
                    },
    success: function (data) {
                        if (data.code == 200) {
        $('#manuocsx').val('');
    $('#tennuocsx').val('');
    LoadDsNuocSX();
                        } else {

        $('#modalHienThiLoi').modal();
    $('#manuocsx').val('');
                        }

                    }
                });
        });
    $(document).on('click', "button[name='update']", function () {
        $('#btnSubmitAdd').hide();
    $('#btnSubmitEdit').show();
    $('#modaltitle').text("Sửa nước sản xuất");
    let idNuocSX = $(this).closest('tr').attr('id').trim();

    $.ajax({
        url: '/Admin/NuocSanXuats/chiTietNuocSX',
    type: 'get',
    data: {
        id: idNuocSX
                },
    success: function (data) {

                    if (data.code == 200) {



        $('#manuocsx').val(data.da.NuocSX);
    $('#manuocsx').prop('readonly', true);
    $('#tennuocsx').val(data.da.TenNuocSX);
    //$('#tendoan1').prop('readonly', false);
    $('#modalNuocSX').modal();


                    }


                }
            });
        });
    $(document).on('click', "button[name='view']", function () {
        let idNuocSX = $(this).closest('tr').attr('id').trim();
    //alert(idDoAn);
    $.ajax({
        url: '/Admin/NuocSanXuats/chiTietNuocSX',
    type: 'get',
    data: {
        id: idNuocSX
                },
    success: function (data) {
        $('#tblChiTiet').empty();
    if (data.code == 200) {
        $('#modalChiTietNuocSX').modal();
    let tr = '<tr>';
        tr += '<td>' + data.da.NuocSX + '</td>';
        tr += '<td>' + data.da.TenNuocSX + '</td>';


        tr += '</tr>';

    $('#tblChiTiet').append(tr);


                    } else {
        alert("err");
                    }
                }
            });
        });
    $('#btnSubmitEdit').click(function () {
        let manuocsx = $('#manuocsx').val().trim();
    let tennuocsx = $('#tennuocsx').val().trim();
    //alert("edit");
    $.ajax({
        url: '/Admin/NuocSanXuats/editNuocSanXuat',
    type: 'post',
    data: {
        manuocsx: manuocsx,
    tennuocsx: tennuocsx

                },
    success: function (data) {
                    if (data.code == 200) {
        LoadDsNuocSX();
    $('#manuocsx').val('');
    $('#tennuocsx').val('');
    $('#modalNuocSX').modal('hide');


                    }



                }
            });
        });
    $('#btnAdd').click(function () {
        $('#modaltitle').text("Thêm mới nước sản xuất");
    $('#manuocsx').val('');
    $('#tennuocsx').val('');
    $('#btnSubmitEdit').hide();
    $('#btnSubmitAdd').show();
    $('#manuocsx').prop('readonly', false);
    $('#modalNuocSX').modal();

        });

    let ma;
    $(document).on('click', "button[name='delete']", function () {

        $('#modalXoa').modal();
    ma = $(this).closest('tr').attr('id');

        });
    $('#btnDelete').click(function () {

        $.ajax({
            url: '/Admin/NuocSanXuats/xoaNuocSanXuat',
            type: 'post',
            data: {
                id: ma
            },
            success: function (data) {
                if (data.code == 200) {

                    LoadDsNuocSX();
                    $('#modalXoa').modal('hide');
                } else {
                    //alert("KKK");
                    $('#modalHienThiLoiKhiXoa').modal();
                    $('#modalXoa').modal('hide');
                }

            }


        })
    });
    function LoadDsNuocSX() {
        $.ajax({
            url: '/Admin/NuocSanXuats/dsNuocSX',
            type: 'get',
            success: function (data) {
                /*  console.log(data);*/

                $('#tblNuocSX').empty();
                $.each(data.dsNuocSX, function (k, v) {
                    let tr = '<tr id="' + v.NuocSX + '">'
                    tr += '<td>' + v.NuocSX + '</td>';
                    tr += '<td>' + v.TenNuocSX + '</td>';

                    tr += '<td>';

                   // tr += '<img src= ' + "/Public/images/icon/plus.png " + '>';
                    // const img = new Image(100, 200); // width, height
                    // img.src = "~/Public/images/icon/details.png";
                    //// document.querySelector('.container').appendChild(img);
                    // tr += img;;

                    tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                    tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                    tr += '<button class = "btn btn-xs btn-info" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';
                    tr += '</td>';

                    tr += '</tr>';
                    $('#tblNuocSX').append(tr);

                });

            }

        });
        }

