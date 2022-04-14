
    $(document).ready(function () {
        LoadDsTheLoai();
        });
    $('#btnSubmitAdd').click(function () {

        let matl = $('#matl').val().trim();
    let tentl = $('#tentl').val().trim();
    let check = 1;
    if (matl == "") {
        document.getElementById("matl_err").innerHTML = "Bạn chưa nhập mã thể loại!"; check = 0;
            }
    else {
        document.getElementById("matl_err").innerHTML = "";
            }
    if (tentl == "") {
        document.getElementById("tentl_err").innerHTML = "Bạn chưa nhập tên thể loại"; check = 0;
            }
    else {
        document.getElementById("tentl_err").innerHTML = "";
            }
    if(check==1)
    $.ajax({
        url: '/Admin/Theloais/addTheLoai',
    type: 'post',
    data: {
        matl: matl,
    tentl: tentl
                },
    success: function (data) {
                    if (data.code == 200) {
        $('#matl').val('');
    $('#tentl').val('');
    LoadDsTheLoai();


                    } else {
        $('#modalHienThiLoi').modal();
                    }

                }
            });
        });
    $(document).on('click', "button[name='update']", function () {
        $('#modaltitle').text("Sửa thể loại");
    $('#btnSubmitAdd').hide();
    $('#btnSubmitEdit').show();
    let idTheLoai = $(this).closest('tr').attr('id').trim();
    //  alert(idTheLoai);

    $.ajax({
        url: '/Admin/Theloais/chiTietTL',
    type: 'get',
    data: {
        id: idTheLoai
                },
    success: function (data) {

                    if (data.code == 200) {



        $('#matl').val(data.da.MaTheLoai);
    $('#matl').prop('readonly', true);
    $('#tentl').val(data.da.TenTheLoai);
    //$('#tendoan1').prop('readonly', false);
    $('#modalTheLoai').modal();


                    }


                }
            });
        });
    $(document).on('click', "button[name='view']", function () {
        let idTheLoai = $(this).closest('tr').attr('id').trim();
    //alert(idDoAn);
    $.ajax({
        url: '/Admin/Theloais/chiTietTL',
    type: 'get',
    data: {
        id: idTheLoai
                },
    success: function (data) {
        $('#tblChiTiet').empty();
    if (data.code == 200) {
        $('#modalChiTietTL').modal();
    let tr = '<tr>';
        tr += '<td>' + data.da.MaTheLoai + '</td>';
        tr += '<td>' + data.da.TenTheLoai + '</td>';


        tr += '</tr>';

    $('#tblChiTiet').append(tr);


                    }
                }
            });
        });
    $('#btnSubmitEdit').click(function () {
        let matl = $('#matl').val().trim();
    let tentl = $('#tentl').val().trim();
    //alert("edit");
    $.ajax({
        url: '/Admin/Theloais/editTheLoai',
    type: 'post',
    data: {
        matl: matl,
    tentl: tentl

                },
    success: function (data) {
                    if (data.code == 200) {
        LoadDsTheLoai();
    $('#matl').val('');
    $('#tentl').val('');
    $('#modalTheLoai').modal('hide');


                    }



                }
            });
        });
    $('#btnAdd').click(function () {
        $('#modaltitle').text("Thêm mới thể loại");
    $('#matl').val('');
    $('#tentl').val('');
    $('#btnSubmitEdit').hide();
    $('#btnSubmitAdd').show();
    $('#matl').prop('readonly', false);
    $('#modalTheLoai').modal();

        });

    let ma;
    $(document).on('click', "button[name='delete']", function () {

        $('#modalXoa').modal();
    ma = $(this).closest('tr').attr('id').trim();


        });
    $('#btnDelete').click(function () {
        // let matl = $('#matl').val().trim();
        $.ajax({
            url: '/Admin/Theloais/xoaTheLoai',
            type: 'post',
            data: {
                id: ma
            },
            success: function (data) {
                if (data.code == 200) {

                    LoadDsTheLoai();
                    $('#modalXoa').modal('hide');
                } else {
                    $('#modalHienThiLoiKhiXoa').modal();
                    $('#modalXoa').modal('hide');
                }

            }


        })
    });
    function LoadDsTheLoai() {
        $.ajax({
            url: '/Admin/Theloais/dsTheLoai',
            type: 'get',
            success: function (data) {
                /*  console.log(data);*/
                //if (data.code == 200) {
                $('#tblTheLoai').empty();
                $.each(data.dsTL, function (k, v) {
                    let tr = '<tr id="' + v.MaTheLoai + '">'
                    tr += '<td>' + v.MaTheLoai + '</td>';
                    tr += '<td>' + v.TenTheLoai + '</td>';

                    tr += '<td>';
                    tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                    tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                    tr += '<button class = "btn btn-xs btn-info" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';
                    tr += '</td>';
                    tr += '</tr>';
                    $('#tblTheLoai').append(tr);

                });
                //}
            }

        });
        }

