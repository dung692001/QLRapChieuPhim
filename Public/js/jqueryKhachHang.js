
$(document).ready(function () {
    LoadDsKhachHang();
});

$('#btnSubmitAdd').click(function () {


    let tenkh = $('#tenkh').val().trim();
    let diachi = $('#diachi').val().trim();
    let ngaysinh = $('#ngaysinh').val().trim();
    let email = $('#email').val().trim();
    let cccd = $('#cccd').val().trim();
    let mk = $('#mk').val().trim();
    let sdt = $('#sdt').val().trim();
    let gioitinh = "";
    if (document.getElementById('Nam').checked) gioitinh = "Nam";
    else gioitinh = "Nữ";

    var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
    let regExp = /^[^ ]+@@[^ ]+\.[a-z]{2, 3}$/;

    //if (email.value.match(regExp)) {
    //    document.getElementById("email_err").innerHTML = "Email bạn nhập chưa đúng định dạng "; check = 0;
    //}

    if (sdt !== '')
        if (vnf_regex.test(sdt) == false) {
            document.getElementById("sdt_err").innerHTML = "Số điện thoại bạn nhập chưa đúng định dạng "; check = 0;
        }

    let check = 1;


    if (tenkh == "") {
        document.getElementById("tenkh_err").innerHTML = "Bạn chưa nhập tênKhách Hàng"; check = 0;
    } else document.getElementById("tenkh_err").innerHTML = "";


    if (sdt == "") {
        document.getElementById("sdt_err").innerHTML = "Bạn chưa nhập số điện thoại "; check = 0;
    } else document.getElementById("sdt_err").innerHTML = "";
    if (email == "") {
        document.getElementById("email_err").innerHTML = "Bạn chưa nhập email "; check = 0;
    } else document.getElementById("email_err").innerHTML = "";
    if (cccd == "") {
        document.getElementById("cccd_err").innerHTML = "Bạn chưa nhập CCCD "; check = 0;
    } else document.getElementById("cccd_err").innerHTML = "";
    if (check == 1)
        $.ajax({
            url: '/Admin/KhachHangs/addKhachHang',
            type: 'post',
            data: {

                tenkh: tenkh,
                diachi: diachi,
                sdt: sdt,
                email: email,
                mk: mk,
                cccd: cccd,
                ngaysinh: ngaysinh,
                gioitinh: gioitinh
            },
            success: function (data) {
                if (data.code == 200) {

                    $('#tenkh').val('');
                    $('#diachi').val('');
                    $('#sdt').val('');
                    $('#ngaysinh').val('');
                    $('#mk').val('');
                    $('#cccd').val('');
                    $('#email').val('');

                    LoadDsKhachHang();


                } else {
                    $('#modalHienThiLoi').modal();
                }

            }
        });
});

$(document).on('click', "button[name='update']", function () {
    $('#modaltitleKH').text("Sửa Khách Hàng");
    $('#btnSubmitAdd').hide();
    $('#btnSubmitEdit').show();
    let idKhachHang = $(this).closest('tr').attr('id').trim();

    $.ajax({
        url: '/Admin/KhachHangs/chiTietKH',
        type: 'get',
        data: {
            id: idKhachHang
        },
        success: function (data) {
            data.da.NgaySinh = new Date(parseInt(data.da.NgaySinh.replace("/Date(", "").replace(")/", ""), 10));
            let x = new Date(data.da.NgaySinh);
            let day = "";
            let month = "";
            if (x.getDate() < 10) day = "0" + x.getDate();
            else day = x.getDate();
            if ((x.getMonth() + 1) < 10) month = "0" + (x.getMonth() + 1);
            else month = (x.getMonth() + 1);
            let ngay = x.getFullYear() + "-" + month + "-" + day;
            if (data.da.GioiTinh == "Nam") {
                let radiobtn = document.getElementById("Nam");
                radiobtn.checked = true;
            }
            else {
                let radiobtn = document.getElementById("Nu");
                radiobtn.checked = true;
            }


            if (data.code == 200) {

                $('#makh').val(data.da.MaKhachHang);
                $('#makh').prop('readonly', true);
                $('#tenkh').val(data.da.TenKhachHang);
                $('#diachi').val(data.da.DiaChi);
                $('#sdt').val(data.da.DienThoai);
                $('#email').val(data.da.Email);
                document.getElementById("ngaysinh").value = ngay;
                //$('#ngaysinh').val(ngay);
                $('#cccd').val(data.da.SoCMND);
                $('#mk').val(data.da.Password);
                $('#modalKhachHang').modal();


            } else {
                alert("hi");
            }


        }
    });
});
let gioi = "";
$(document).on('click', "button[name='view']", function () {
    let idKhachHang = $(this).closest('tr').attr('id').trim();

    // alert(idKhachHang);
    $.ajax({
        url: '/Admin/KhachHangs/chiTietKH',
        type: 'get',
        data: {
            id: idKhachHang
        },
        dataType: "json",
        success: function (data) {
            data.da.NgaySinh = new Date(parseInt(data.da.NgaySinh.replace("/Date(", "").replace(")/", ""), 10));
            let x = new Date(data.da.NgaySinh);
            let ngay = x.getDate() + '/' + (x.getMonth() + 1) + '/' + x.getFullYear();


            console.log(data.da.NgaySinh)
            $('#tblChiTiet').empty();
            gioi = data.da.GioiTinh;
            if (data.code == 200) {
                $('#modalChiTietKH').modal();
                let tr = '<tr>';
                tr += '<td>' + data.da.MaKhachHang + '</td>';
                tr += '<td>' + data.da.TenKhachHang + '</td>';
                tr += '<td>' + data.da.DiaChi + '</td>';
                tr += '<td>' + data.da.DienThoai + '</td>';
                tr += '<td>' + data.da.Email + '</td>';
                tr += '<td>' + ngay + '</td>';
                tr += '<td>' + data.da.SoCMND + '</td>';
                tr += '<td>' + data.da.GioiTinh + '</td>';
                tr += '</tr>';

                $('#tblChiTiet').append(tr);


            }
        }
    });
});
$('#btnSubmitEdit').click(function () {
    let makh = $('#makh').val().trim();
    let tenkh = $('#tenkh').val().trim();
    let diachi = $('#diachi').val().trim();
    let sdt = $('#sdt').val().trim();
    let ngaysinh = $('#ngaysinh').val().trim();
    let email = $('#email').val().trim();
    let gioitinh = "";
    if (document.getElementById('Nam').checked) gioitinh = "Nam";
    else gioitinh = "Nữ";

    let cccd = $('#cccd').val().trim();
    let mk = $('#mk').val().trim();
    //alert("edit");
    $.ajax({
        url: '/Admin/KhachHangs/editKhachHang',
        type: 'post',
        data: {
            makh: makh,
            tenkh: tenkh,
            diachi: diachi,
            sdt: sdt,
            email: email,
            mk: mk,
            cccd: cccd,
            ngaysinh: ngaysinh,
            gioitinh: gioitinh

        },
        success: function (data) {
            if (data.code == 200) {
                LoadDsKhachHang();
                $('#makh').val('');
                $('#tenkh').val('');
                $('#diachi').val('');
                $('#sdt').val('');
                $('#ngaysinh').val('');
                $('#mk').val('');
                $('#cccd').val('');
                $('#modalKhachHang').modal('hide');


            }



        }
    });
});
$('#btnAdd').click(function () {
    $('#modaltitleKH').text("Thêm mới Khách Hàng");
    $('#makh').val('');
    $('#makh').hide();
    $('#lbmakh').hide();
    $('#tenkh').val('');
    $('#diachi').val('');
    $('#sdt').val('');
    $('#ngaysinh').val('');
    $('#mk').val('');
    $('#cccd').val('');
    $('#btnSubmitEdit').hide();
    $('#btnSubmitAdd').show();
    $('#makh').prop('readonly', false);
    $('#modalKhachHang').modal();



});

let ma;
$(document).on('click', "button[name='delete']", function () {

    $('#modalXoa').modal();
    ma = $(this).closest('tr').attr('id');


});
$('#btnDelete').click(function () {
    // let makh = $('#makh').val().trim();
    $.ajax({
        url: '/Admin/KhachHangs/xoaKhachHang',
        type: 'post',
        data: {
            id: ma
        },
        success: function (data) {

            if (data.code == 200) {

                LoadDsKhachHang();
                $('#modalXoa').modal('hide');
            } else {
                $('#modalHienThiLoiKhiXoa').modal();
                $('#modalXoa').modal('hide');
            }

        }


    })
});
function LoadDsKhachHang() {
    $.ajax({
        url: '/Admin/KhachHangs/dsKhachHang',
        type: 'get',
        success: function (data) {
            /*  console.log(data);*/

            $('#tblKhachHang').empty();
            $.each(data.dsKH, function (k, v) {
                let tr = '<tr id="' + v.MaKhachHang + '">'
                tr += '<td>' + v.MaKhachHang + '</td>';
                tr += '<td>' + v.TenKhachHang + '</td>';
                tr += '<td style="width:100px;height:100px;">' + v.DiaChi + '</td>';
                tr += '<td>' + v.DienThoai + '</td>';
                tr += '<td>' + v.CCCD + '</td>';
                tr += '<td>' + v.Email + '</td>';

                tr += '<td>';
                tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                tr += '<button class = "btn btn-xs btn-info" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';
                tr += '</td>';
                tr += '</tr>';
                $('#tblKhachHang').append(tr);

            });

        }

    });
}
