
$(document).ready(function () {
    LoadDsNhanVien();
   
});



$(document).on('click', "button[name='LuuNV']", function () {
    let manv = $('#manv').val().trim();
    $.ajax({
        url: '/Admin/NhanViens/Loi',
        type: 'post',
        data: {
            id: manv
        },
        success: function (data) {

            if (data.code == 500) 

                $('#modalHienThiLoi').modal();
            

        }


    })
   

});
let gioi = "";
$(document).on('click', "button[name='view']", function () {
    let idNhanVien = $(this).closest('tr').attr('id').trim();

    // alert(idNhanVien);
    $.ajax({
        url: '/Admin/NhanViens/chiTietNV',
        type: 'get',
        data: {
            id: idNhanVien
        },
        dataType: "json",
        success: function (data) {
            data.da.NgaySinh = new Date(parseInt(data.da.NgaySinh.replace("/Date(", "").replace(")/", ""), 10));
            let x = new Date(data.da.NgaySinh);
            let ngaysinh = x.getDate() + '/' + (x.getMonth() + 1) + '/' + x.getFullYear();
            data.da.NgayVaoLam = new Date(parseInt(data.da.NgayVaoLam.replace("/Date(", "").replace(")/", ""), 10));
            let y = new Date(data.da.NgayVaoLam);
            let ngayvaolam = y.getDate() + '/' + (y.getMonth() + 1) + '/' + y.getFullYear();

           
            $('#tblChiTiet').empty();
            $('#img').empty();
            gioi = data.da.GioiTinh;
            if (data.code == 200) {
                $('#modalChiTietNV').modal();
                let anh = '<img class="d-block mx-auto" style="height:300px;width:250px;" src=' + "/Public/images/NhanVien/" + data.da.Anh + '>';
                let tr = '<tr>';

                tr += '<td>' + data.da.MaNhanVien + '</td>';
                tr += '<td>' + data.da.TenNhanVien + '</td>';
                tr += '<td>' + data.da.DiaChi + '</td>';
                tr += '<td>' + data.da.DienThoai + '</td>';
                tr += '<td>' + data.da.Email + '</td>';
                tr += '<td>' + ngaysinh + '</td>';
                tr += '<td>' + data.da.SoCMND + '</td>';
                tr += '<td>' + data.da.GioiTinh + '</td>';
                tr += '<td>' + data.da.ChucVu + '</td>';
                tr += '<td>' + ngayvaolam + '</td>';
                tr += '<td>' + data.da.HSL + '</td>';
                tr += '</tr>';

                $('#tblChiTiet').append(tr);

                $('#img').append(anh);
            }
        }
    });
});
//$('#btnSubmitEdit').click(function () {
//    let manv = $('#manv').val().trim();
//let tennv = $('#tennv').val().trim();
//let diachi = $('#diachi').val().trim();
//let sdt = $('#sdt').val().trim();
//let ngaysinh = $('#ngaysinh').val().trim();
//let email = $('#email').val().trim();
//let gioitinh = "";
//let ngayvaolam = $('#ngayvaolam').val().trim();
//let chucvu = $('#chucvu').val().trim();
//let marap = $('#marap').val();
//let anh = $('#anh').val().trim();
//let hsl = $('#hsl').val().trim();
//if (document.getElementById('Nam').checked) gioitinh = "Nam";
//else gioitinh = "Nữ";

//let cccd = $('#cccd').val().trim();
//let mk = $('#mk').val().trim();
////alert("edit");
//$.ajax({
//    url: '/Admin/NhanViens/editNhanVien',
//type: 'post',
//data: {
//    manv: manv,
//tennv: tennv,
//diachi: diachi,
//sdt: sdt,
//email: email,
//mk: mk,
//cccd: cccd,
//ngaysinh: ngaysinh,
//gioitinh: gioitinh,
//ngayvaolam: ngayvaolam,
//chucvu: chucvu,
//marap: marap,
//anh: anh,
//hsl: hsl


//        },
//success: function (data) {
//            if (data.code == 200) {
//    LoadDsNhanVien();
//$('#manv').val('');
//$('#tennv').val('');
//$('#diachi').val('');
//$('#sdt').val('');
//$('#ngaysinh').val('');
//$('#mk').val('');
//$('#cccd').val('');
//$('#ngayvaolam').val('');
//$('#anh').val('');
//$('#hsl').val('');
//$('#marap').val('');
//$('#chucvu').val('');
//$('#modalNhanVien').modal('hide');


//            }



//        }
//    });
//});
//$('#btnAdd').click(function () {
//    $('#modaltitleNV').text("Thêm mới Nhân Viên");
//$('#manv').val('');
//$('#tennv').val('');
//$('#diachi').val('');
//$('#sdt').val('');
//$('#ngaysinh').val('');
//$('#mk').val('');
//$('#cccd').val('');
//$('#ngayvaolam').val('');
//$('#anh').val('');
//$('#hsl').val('');
//$('#marap').val('');
//$('#chucvu').val('');
//$('#btnSubmitEdit').hide();
//$('#btnSubmitAdd').show();
//$('#manv').prop('readonly', false);
//$('#modalNhanVien').modal();



//});

let ma;
$(document).on('click', "button[name='delete']", function () {

    $('#modalXoa').modal();
    ma = $(this).closest('tr').attr('id');


});
$('#btnDelete').click(function () {
    // let manv = $('#manv').val().trim();
    $.ajax({
        url: '/Admin/NhanViens/xoaNhanVien',
        type: 'post',
        data: {
            id: ma
        },
        success: function (data) {

            if (data.code == 200) {

                LoadDsNhanVien();
                $('#modalXoa').modal('hide');
            } else {

                $('#modalXoa').modal('hide');
            }

        }


    })
});
function LoadDsNhanVien() {
    $.ajax({
        url: '/Admin/NhanViens/dsNhanVien',
        type: 'get',
        success: function (data) {
             // console.log(data);

            $('#tblNhanVien').empty();
            $.each(data.dsNV, function (k, v) {
                let tr = '<tr id="' + v.MaNhanVien + '">';
                tr += '<td>';
                tr += '<img style="height:100px;width:100px;" src= ' + "/Public/images/NhanVien/" + v.Anh + '>';
                tr += '</td>';
                tr += '<td>' + v.MaNhanVien + '</td>';
                tr += '<td>' + v.TenNhanVien + '</td>';
                tr += '<td>' + v.DienThoai + '</td>';
                tr += '<td>' + v.CCCD + '</td>';
                tr += '<td>' + v.ChucVu + '</td>';

                tr += '<td>';
                tr += '<button class = "btn btn-xs btn-info" " name = "view"><i class="fa-solid fa-info"></i></button>&nbsp';
                tr += '<a  class="btn btn-xs btn-success" href= ' + "/Admin/NhanViens/Edit/" + v.MaNhanVien + '>  <i class="fa-solid fa-pen-to-square" ></i> </a >&nbsp';
                // tr += '<button class = "btn btn-xs btn-info" id="btnEdit" " name = "update"><i class="fa-solid fa-pen-to-square"></i></button>&nbsp';
                tr += '<button class = "btn btn-xs btn-danger" " name = "delete"><i class="fa-solid fa-trash-can"></i></button>';


                tr += '</td>';
                tr += '</tr>';
                $('#tblNhanVien').append(tr);

            });

        }

    });
}

   //$('#btnSubmitAdd').click(function () {

        //    let manv = $('#manv').val().trim();
        //    let tennv = $('#tennv').val().trim();
        //    let diachi = $('#diachi').val().trim();
        //    let ngaysinh = $('#ngaysinh').val().trim();
        //    let email = $('#email').val().trim();
        //    let cccd = $('#cccd').val().trim();
        //    let mk = $('#mk').val().trim();
        //    let sdt = $('#sdt').val().trim();
        //    let ngayvaolam = $('#ngayvaolam').val().trim();
        //    let chucvu = $('#chucvu').val().trim();
        //    let marap = $('#marap').val();
        //    let anh = $('#uploadhinh').val();
        //    let hsl = $('#hsl').val().trim();
        //    let gioitinh = "";
        //    if (document.getElementById('Nam').checked) gioitinh = "Nam";
        //    else gioitinh = "Nữ";

        //    var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
        //    let regExp = /^[^ ]+@@[^ ]+\.[a-z]{2,3}$/;

        //    //if (email.value.match(regExp)) {
        //    //    document.getElementById("email_err").innerHTML = "Email bạn nhập chưa đúng định dạng "; check = 0;
        //    //}

        //    if (sdt !== '')
        //        if (vnf_regex.test(sdt) == false) {
        //            document.getElementById("sdt_err").innerHTML = "Số điện thoại bạn nhập chưa đúng định dạng "; check = 0;
        //        }

        //    let check = 1;
        //    if (manv == "") {
        //        document.getElementById("manv_err").innerHTML = "Bạn chưa nhập mã Nhân Viên!"; check = 0;
        //    } else document.getElementById("manv_err").innerHTML = "";

        //    if (tennv == "") {
        //        document.getElementById("tennv_err").innerHTML = "Bạn chưa nhập địa chỉ Nhân Viên"; check = 0;
        //    } else document.getElementById("tennv_err").innerHTML = "";

        //    if (diachi == "") {
        //        document.getElementById("diachi_err").innerHTML = "Bạn chưa nhập địa chỉ Nhân Viên"; check = 0;
        //    } else document.getElementById("diachi_err").innerHTML = "";
        //    if (sdt == "") {
        //        document.getElementById("sdt_err").innerHTML = "Bạn chưa nhập số điện thoại "; check = 0;
        //    } else document.getElementById("sdt_err").innerHTML = "";
        //    if (email == "") {
        //        document.getElementById("email_err").innerHTML = "Bạn chưa nhập email "; check = 0;
        //    } else document.getElementById("email_err").innerHTML = "";
        //    if (cccd == "") {
        //        document.getElementById("cccd_err").innerHTML = "Bạn chưa nhập CCCD "; check = 0;
        //    } else document.getElementById("cccd_err").innerHTML = "";
        //    if (check == 1)
        //        $.ajax({
        //            url: '/Admin/NhanViens/addNhanVien',
        //            type: 'post',
        //            data: {
        //                manv: manv,
        //                tennv: tennv,
        //                diachi: diachi,
        //                sdt: sdt,
        //                email: email,
        //                mk: mk,
        //                cccd: cccd,
        //                ngaysinh: ngaysinh,
        //                gioitinh: gioitinh,
        //                ngayvaolam: ngayvaolam,
        //                chucvu: chucvu,
        //                marap: marap,
        //                anh: anh,
        //                hsl: hsl
        //            },
        //            success: function (data) {
        //                console.log(data);
        //                if (data.code == 200) {

        //                    $('#manv').val('');
        //                    $('#tennv').val('');
        //                    $('#diachi').val('');
        //                    $('#sdt').val('');
        //                    $('#ngaysinh').val('');
        //                    $('#mk').val('');
        //                    $('#cccd').val('');
        //                    $('#email').val('');
        //                    $('#ngayvaolam').val('');
        //                    $('#anh').val('');
        //                    $('#hsl').val('');
        //                    $('#marap').val('');
        //                    $('#chucvu').val('');
        //                    LoadDsNhanVien();


        //                } else {
        //                    alert("500");
        //                    $('#modalHienThiLoi').modal();
        //                }

        //            }
        //        });
        //});

        //$(document).on('click', "button[name='update']", function () {
        //    $('#modaltitleNV').text("Sửa Nhân Viên");
        //    $('#btnSubmitAdd').hide();
        //    $('#btnSubmitEdit').show();
        //    let idNhanVien = $(this).closest('tr').attr('id').trim();

        //    $.ajax({
        //        url: '/Admin/NhanViens/chiTietNV',
        //        type: 'get',
        //        data: {
        //            id: idNhanVien
        //        },
        //        success: function (data) {
        //            data.da.NgaySinh = new Date(parseInt(data.da.NgaySinh.replace("/Date(", "").replace(")/", ""), 10));
        //            data.da.NgayVaoLam = new Date(parseInt(data.da.NgayVaoLam.replace("/Date(", "").replace(")/", ""), 10));
        //            let x = new Date(data.da.NgaySinh);
        //            let y = new Date(data.da.NgayVaoLam);
        //            let dayNS = "";
        //            let monthNS = "";
        //            if (x.getDate() < 10) dayNS = "0" + x.getDate();
        //            else dayNS = x.getDate();
        //            if ((x.getMonth() + 1) < 10) monthNS = "0" + (x.getMonth() + 1);
        //            else monthNS = (x.getMonth() + 1);
        //            let ns = x.getFullYear() + "-" + monthNS + "-" + dayNS;

        //            let dayVL = "";
        //            let monthVL = "";
        //            if (y.getDate() < 10) dayNS = "0" + y.getDate();
        //            else dayVL = y.getDate();
        //            if ((y.getMonth() + 1) < 10) monthVL = "0" + (y.getMonth() + 1);
        //            else monthVL = (x.getMonth() + 1);
        //            let nvl = y.getFullYear() + "-" + monthVL + "-" + dayVL;


        //            if (data.da.GioiTinh == "Nam") {
        //                let radiobtn = document.getElementById("Nam");
        //                radiobtn.checked = true;
        //            }
        //            else {
        //                let radiobtn = document.getElementById("Nu");
        //                radiobtn.checked = true;
        //            }


        //            if (data.code == 200) {

        //                $('#manv').val(data.da.MaNhanVien);
        //                $('#manv').prop('readonly', true);
        //                $('#tennv').val(data.da.TenNhanVien);
        //                $('#diachi').val(data.da.DiaChi);
        //                $('#sdt').val(data.da.DienThoai);
        //                $('#email').val(data.da.Email);
        //                document.getElementById("ngaysinh").value = ns;
        //                document.getElementById("ngayvaolam").value = nvl;
        //                // $('#ngaysinh').val(ns);
        //                $('#ngayvaolam').val(nvl);
        //                $('#cccd').val(data.da.SoCMND);
        //                $('#mk').val(data.da.Password);
        //                $('#modalNhanVien').modal();


        //            } else {
        //                alert("hi");
        //            }


        //        }
        //    });
        //});