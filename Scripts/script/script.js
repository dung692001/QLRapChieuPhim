
//$(document).ready(function () {

    
//    //Click Dang nhap o navbar
//    $(document).on('click', '#navbar-list-dang-nhap', function (event) {
//        event.preventDefault();
//        var modal = document.getElementById("openFormDangNhap");
//        modal.style.position = "fixed";
//        modal.style.display = "flex";
//    })

//    //Click Dang xuato navbar
//    $(document).on('click', '#navbar-list-dang-xuat', function (event) {
//        event.preventDefault();
//        $(".navbar-dangxuat").addClass("hidden");
//        $(".navbar-dangnhap").removeClass("hidden");
//        $(".loginInfo").css({ "display": "none" });
//    })
//    //Click btn trở lại
//    $(document).on('click', '.auth-form__controls-back', function (event) {
//        var dangnhap = document.getElementById("openFormDangNhap");
//        var dangki = document.getElementById("openFormDangKi");
//        dangnhap.style.display = "none";
//        dangki.style.display = "none";
//    })
//    //click dang ki
//    $(document).on('click', '.btn-dang-ki-open', function (event) {
//        event.preventDefault();
//        var dangnhap = document.getElementById("openFormDangNhap");
//        var dangki = document.getElementById("openFormDangKi");
//        dangnhap.style.display = "none";
//        dangki.style.position = "fixed";
//        dangki.style.display = "flex";
//    })
//    //click dang nhap
//    $(document).on('click', '.btn-dang-nhap-open', function (event) {
//        event.preventDefault();
//        var dangnhap = document.getElementById("openFormDangNhap");
//        var dangki = document.getElementById("openFormDangKi");
//        dangki.style.display = "none";
//        dangnhap.style.position = "fixed";
//        dangnhap.style.display = "flex";
//    })

//    $(".app__content-nav-item a").click(function(e){
//        //xóa active thằng hiện có
//        var tagActive = $(".app__content-nav-active");
//        tagActive.removeClass("app__content-nav-active");
//        //them active vào thằng đc click
//        $(this).addClass("app__content-nav-active");
        
//    })

//    $(".btn-dangnhap").click(function(e){

//        var account_dangnhap = $(".account-dangnhap").val();
//        var password_dangnhap = $(".password-dangnhap").val();
//        if (account_dangnhap == "abc" && password_dangnhap == "1") {
//            alert("Dang nhap thanh cong");
//            $("#openFormDangNhap").css({ "display": "none" });
//            e.preventDefault();
//            $(".navbar-dangnhap").addClass("hidden");
//            $(".navbar-dangxuat").removeClass("hidden");
//            $(".loginInfo").html("Xin chào " + account_dangnhap + "!");
//            $(".loginInfo").css({ "display": "block" });
           
//        }
//    })
    
//    $(".btn-datve").click(function (e) {
//        console.log("id:" + e.target.id.toString());
//        movieselected = e.target.id;
//        setMovieSelected(movieselected);
//    })
//})
