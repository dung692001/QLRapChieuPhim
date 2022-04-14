
    $(document).ready(function () {
        LoadDsRapPhim();
        });
    $('#btnSubmitAdd').click(function () {

        let marp = $('#marp').val().trim();
    let tenrp = $('#tenrp').val().trim();
    let diachirp = $('#diachirp').val().trim();
    let sdtrp = $('#sdtrp').val().trim();
    var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;

    if (sdtrp !== '')
    if (vnf_regex.test(sdtrp) == false) {
        document.getElementById("sdtrp_err").innerHTML = "Số điện thoại bạn nhập chưa đúng định dạng "; check = 0;
                }

    let check = 1;
    if (marp == "") {
        document.getElementById("marp_err").innerHTML = "Bạn chưa nhập mã Rạp Phim!"; check = 0;
            } else  document.getElementById("marp_err").innerHTML = "";

    if (tenrp == "") {
        document.getElementById("tenrp_err").innerHTML = "Bạn chưa nhập địa chỉ Rạp Phim"; check = 0;
            }  else  document.getElementById("tenrp_err").innerHTML = "";

    if (diachirp == "") {
        document.getElementById("diachirp_err").innerHTML = "Bạn chưa nhập địa chỉ Rạp Phim"; check = 0;
            } else document.getElementById("diachirp_err").innerHTML = "";
    if (sdtrp == "") {
        document.getElementById("sdtrp_err").innerHTML = "Bạn chưa nhập số điện thoại "; check = 0;
            } else document.getElementById("sdtrp_err").innerHTML = "";
    if(check==1)
    $.ajax({
        url: '/Admin/RapPhims/addRapPhim',
    type: 'post',
    data: {
        marp: marp,
    tenrp: tenrp,
    diachi: diachirp,
    sdt: sdtrp
                },
    success: function (data) {
                    if (data.code == 200) {
        $('#marp').val('');
    $('#tenrp').val('');
    $('#diachirp').val('');
    $('#sdtrp').val('');
    LoadDsRapPhim();


                    } else {
        $('#modalHienThiLoi').modal();
                    }

                }
            });
        });
    $(document).on('click', "button[name='update']", function () {
        $('#modaltitleRP').text("Sửa Rạp Phim");
    $('#btnSubmitAdd').hide();
    $('#btnSubmitEdit').show();
    let idRapPhim = $(this).closest('tr').attr('id').trim();
    // $('#modalRapPhim').modal();
    //alert(idRapPhim);

    $.ajax({
        url: '/Admin/RapPhims/chiTietRP',
    type: 'get',
    data: {
        id: idRapPhim
                },
    success: function (data) {
                    
                    if (data.code == 200) {

        $('#marp').val(data.da.MaRap);
    $('#marp').prop('readonly', true);
    $('#tenrp').val(data.da.TenRap);
    $('#diachirp').val(data.da.DiaChi);
    $('#sdtrp').val(data.da.DienThoai);
    $('#tendoan1').prop('readonly', false);
    $('#modalRapPhim').modal();


                    } else {
        alert("hi");
                    }


                }
            });
        });
    $(document).on('click', "button[name='view']", function () {
        let idRapPhim = $(this).closest('tr').attr('id').trim();
    // alert(idRapPhim);
    $.ajax({
        url: '/Admin/RapPhims/chiTietRP',
    type: 'get',
    data: {
        id: idRapPhim
                },
    success: function (data) {
        $('#tblChiTiet').empty();
    if (data.code == 200) {
        $('#modalChiTietRP').modal();
    let tr = '<tr>';
        tr += '<td>' + data.da.MaRap + '</td>';
        tr += '<td>' + data.da.TenRap + '</td>';
        tr += '<td>' + data.da.DiaChi + '</td>';
        tr += '<td>' + data.da.DienThoai + '</td>';

        tr += '</tr>';

    $('#tblChiTiet').append(tr);


                    }
                }
            });
        });
    $('#btnSubmitEdit').click(function () {
        let marp = $('#marp').val().trim();
    let tenrp = $('#tenrp').val().trim();
    let diachirp = $('#diachirp').val().trim();
    let sdtrp = $('#sdtrp').val().trim();
    //alert("edit");
    $.ajax({
        url: '/Admin/RapPhims/editRapPhim',
    type: 'post',
    data: {
        marp: marp,
    tenrp: tenrp,
    diachi: diachirp,
    sdt: sdtrp

                },
    success: function (data) {
                    if (data.code == 200) {
        LoadDsRapPhim();
    $('#marp').val('');
    $('#tenrp').val('');
    $('#diachirp').val('');
    $('#sdtrp').val('');
    $('#modalRapPhim').modal('hide');


                    }



                }
            });
        });
    $('#btnAdd').click(function () {
        $('#modaltitleRP').text("Thêm mới Rạp Phim");
    $('#marp').val('');
    $('#tenrp').val('');
    $('#diachirp').val('');
    $('#sdtrp').val('');
    $('#btnSubmitEdit').hide();
    $('#btnSubmitAdd').show();
    $('#marp').prop('readonly', false);
    $('#modalRapPhim').modal();

            

        });

    let ma;
    $(document).on('click', "button[name='delete']", function () {

        $('#modalXoa').modal();
    ma = $(this).closest('tr').attr('id');


        });
    $('#btnDelete').click(function () {
        // let marp = $('#marp').val().trim();
        $.ajax({
            url: '/Admin/RapPhims/xoaRapPhim',
            type: 'post',
            data: {
                id: ma
            },
            success: function (data) {
                if (data.code == 200) {

                    LoadDsRapPhim();
                    $('#modalXoa').modal('hide');
                } else {
                    $('#modalHienThiLoiKhiXoa').modal();
                    $('#modalXoa').modal('hide');
                }

            }


        })
    });
    function LoadDsRapPhim() {
        $.ajax({
            url: '/Admin/RapPhims/dsRapPhim',
            type: 'get',
            success: function (data) {
                /*  console.log(data);*/

                $('#tblRapPhim').empty();
                $.each(data.dsRP, function (k, v) {
                    let tr = '<tr id="' + v.MaRap + '">'
                    tr += '<td>' + v.MaRap + '</td>';
                    tr += '<td>' + v.TenRap + '</td>';
                    tr += '<td style="width:200px;height:100px;">' + v.DiaChi + '</td>';
                    tr += '<td>' + v.DienThoai + '</td>';
                    tr += '<td>';
                    tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                    tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                    tr += '<button class = "btn btn-xs btn-info" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';
                    tr += '</td>';
                    tr += '</tr>';
                    $('#tblRapPhim').append(tr);

                });

            }

        });
        }

