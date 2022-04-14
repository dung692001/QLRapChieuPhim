
/////// <reference path="./Scripts/jquery-3.4.1.intellisense.js" />
    const container = document.querySelector(".tcontainer");
    const seats = document.querySelectorAll(".row .seat:not(.sold)");
    const count = document.getElementById("count");
    const total = document.getElementById("total");
    const movieSelect = document.getElementById("movie");
    const roomSelect = document.getElementById("room");
    const screeningSelect = document.getElementById("screening");
    const orderbtn = document.getElementById("order")

    populateUI();
    let ticketPrice = screeningSelect.value;
    let MovieId = movieSelect.value;
    let totalseats = roomSelect.value;
    let roomid = roomSelect.id;
    let fullrow = Math.floor(totalseats / 10);
    let remainder = totalseats % 10;
    let screeningID = screeningSelect.options[screeningSelect.selectedIndex].id

    
    // Save selected movie index and price
    function setMovieData(movieIndex, movieID, movieSeats) {
        localStorage.setItem("selectedMovieIndex", movieIndex);
        localStorage.setItem("selectedmovieID", movieID);
    }

    function setRoomData(roomSeats, roomId) {
        localStorage.setItem("selectedroomSeats", roomSeats);
        localStorage.setItem("selectedroomId", roomId);
    }

    // Update total and count
    function updateSelectedCount() {
        const selectedSeats = document.querySelectorAll(".row .seat.selected");

        const seatsIndex = [...selectedSeats].map((seat) => [...seats].indexOf(seat));

        localStorage.setItem("selectedSeats", JSON.stringify(seatsIndex));

        const selectedSeatsCount = selectedSeats.length;

        count.innerText = selectedSeatsCount;
        total.innerText = selectedSeatsCount * ticketPrice;

        setMovieData(movieSelect.selectedIndex, movieSelect.value);
        setRoomData(roomSelect.value);
    }

    // Get data from localstorage and populate UI
    function populateUI() {
        const selectedSeats = JSON.parse(localStorage.getItem("selectedSeats"));

        if (selectedSeats !== null && selectedSeats.length > 0) {
            seats.forEach((seat, index) => {
                if (selectedSeats.indexOf(index) > -1) {
                    console.log(seat.classList.add("selected"));
                }
            });
        }

        const selectedMovieIndex = localStorage.getItem("selectedMovieIndex");
        const selectedRoomSeats = localStorage.getItem("selectedRoomSeats");
        const selectedroomId = localStorage.getItem("selectedroomId");

        //if (selectedMovieIndex !== null) {
        //    movieSelect.selectedIndex = selectedMovieIndex;
        //    console.log(selectedMovieIndex);
        //}
        //if (selectedRoomSeats !== null) {
        //    roomSelect.value = selectedRoomSeats;
        //    console.log(selectedRoomSeats);
        //}
        //if (selectedroomId !== null) {
        //    roomSelect.value = selectedroomId;
        //    console.log(selectedroomId);
        //}
    }
    console.log();
    // Movie select event
    movieSelect.addEventListener("change", (e) => {
        MovieId = e.target.options[movieSelect.selectedIndex].value;
        setMovieData(e.target.selectedIndex, e.target.value);
        GetAllScreenings();
        orderbtn.disabled = true;
        screeningID = "";
        $(".tcontainer").empty();
        $("#room option[value=0]").prop('selected', 'selected');
        updateSelectedCount();
    });
    console.log();
    //screening select event
    screeningSelect.addEventListener("change", (e) => {
        screeningID = e.target.options[screeningSelect.selectedIndex].id;
        ticketPrice = e.target.options[screeningSelect.selectedIndex].value;
        updateSelectedCount();
        GetAllTickets();
    });
    console.log();

    //Initial seats generation
    $(".tcontainer").append(
        "<div class='screen'>[<span class='purple'>NHOM9</span>|<span class='blue'>STUDIO</span>] PRESENTS<br><br><span class='red'>WEB PROJECT</span></div>"
    );
    for (let i = 0; i < fullrow; i++) {
        $(".tcontainer").append("<div class='row'></div>");
        $("div.row:last").attr("value", i);
        for (let k = 0; k < 10; k++) {
            $("div.row:last").append("<div class='seat'></div>");
            $("div.seat:last").attr("id", k + i * 10);
            $("div.seat:last").html(String.fromCharCode(65 + i) + k);
        }
    }
    if (!remainder == 0) {
        $(".tcontainer").append("<div class='row'></div>");
        $("div.row:last").attr("id", fullrow);
        for (let k = 0; k < remainder; k++) {
            $("div.row:last").append("<div class='seat'></div>");
            $("div.seat:last").attr("id", k + fullrow * 10);
            $("div.seat:last").html(String.fromCharCode(65 + fullrow) + k);
        }
    }
    // Room select event
    roomSelect.addEventListener("change", (e) => {
        totalseats = e.target.value;
        roomid = e.target.options[roomSelect.selectedIndex].id;
        fullrow = Math.floor(totalseats / 10);
        remainder = totalseats % 10;
        setRoomData(e.target.value);
        updateSelectedCount();
        $(".tcontainer").empty();
        $(".tcontainer").append("<div class='screen'></div>");
        GetAllScreenings();
        for (let i = 0; i < fullrow; i++) {
            $(".tcontainer").append("<div class='row'></div>");
            $("div.row:last").attr("value", i);
            for (let k = 0; k < 10; k++) {
                $("div.row:last").append("<div class='seat'></div>");
                $("div.seat:last").attr("id", k + i * 10);
                $("div.seat:last").html(String.fromCharCode(65 + i) + k);
            }
        }
        if (!remainder == 0) {
            $(".tcontainer").append("<div class='row'></div>");
            $("div.row:last").attr("id", fullrow);
            for (let k = 0; k < remainder; k++) {
                $("div.row:last").append("<div class='seat'></div>");
                $("div.seat:last").attr("id", k + fullrow * 10);
                $("div.seat:last").html(String.fromCharCode(65 + fullrow) + k);
            }
        }

        orderbtn.disabled = true;
        screeningID = "";
    });

    // Seat click event
    container.addEventListener("click", (e) => {
        if (
            e.target.classList.contains("seat") &&
            !e.target.classList.contains("sold")
        ) {
            e.target.classList.toggle("selected");

            updateSelectedCount();
        }
        if (($(".tcontainer>div.row>div.selected").length) > 0 && screeningID.length > 0) {
            orderbtn.disabled = false;
        }
    });

    //order function
    function onClickEventOrder() {
        for (i = 0; i < $(".tcontainer>div.row>div.selected").length; i++) {
            let row = Math.floor($(".tcontainer>div.row>div.selected:eq(" + i.toString() + ")").attr('id') / 10)
            let seat = $(".tcontainer>div.row>div.selected:eq(" + i.toString() + ")").attr('id') % 10
            let idTicket = screeningID.toString() + "VP" + row.toString() + seat.toString()
            insertTickets(idTicket.toString(), String.fromCharCode(65 + row), seat)
        }
        GetAllTickets();
    }


    //hàm l?y tên phim theo id
    $(document).ready(function () {
        GetAllMovies();
    });
    //Hàm l?y ra toàn b? danh sách Khách Hàng. Dùng $.ajax() th?c hi?n method HTTP GET
    function GetAllMovies() {
        $.ajax({
            url: '/api/movies/',
            method: 'GET',
            contentType: 'json',
            dataType: 'json',
            error: function (response) {
            },
            success: function (reponse) {
                const len = reponse.length;
                console.log(reponse);
                let table = '';
                for (var i = 0; i < len; i++) {
                    $("#movie").append("<option value=" + reponse[i].MaPhim + ">" + reponse[i].TenPhim + "</option>");
                    table = table + '<tr>';
                    table = table + '<td>' + reponse[i].MaPhim + '</td>';
                    table = table + '<td>' + reponse[i].TenPhim + '</td>';
                    table = table + '</tr>';
                    if (localStorage.SelectedMovie) {
                        //$(document).ready(function () {
                            $("#movie option[value=" + localStorage.SelectedMovie.trim() + "]").prop('selected', 'selected');
                            console.log(localStorage.SelectedMovie);
                        //})
                    }
                }
            },
            fail: function (response) {
            }
        });
    }



    //hàm l?y s? gh? theo idphòng
    $(document).ready(function () {
        GetAllRooms();
    });
    //Hàm l?y ra toàn b? danh sách Khách Hàng. Dùng $.ajax() th?c hi?n method HTTP GET
    function GetAllRooms() {
        $.ajax({
            url: '/api/rooms/',
            method: 'GET',
            contentType: 'json',
            dataType: 'json',
            error: function (response) {
            },
            success: function (reponse) {
                const len = reponse.length;
                console.log(reponse);
                let table = '';
                for (var i = 0; i < len; i++) {
                    table = table + '<tr>';
                    $("#room").append("<option id=" + reponse[i].MaPhong + " value=" + reponse[i].TongGhe + ">" + reponse[i].TenPhong + "</option>");
                    table = table + '<td>' + reponse[i].MaPhong + '</td>';
                    table = table + '<td>' + reponse[i].TenPhong + '</td>';
                    table = table + '<td>' + reponse[i].TongGhe + '</td>';
                    table = table + '</tr>';
                }
            },
            fail: function (response) {
            }
        });
    }


    //hàm l?y toàn b? bc
    let bcID = 'BC0';
    $(document).ready(function () {
        GetAllScreenings();
    });
    //Hàm l?y ra toàn b? danh sách Khách Hàng. Dùng $.ajax() th?c hi?n method HTTP GET
    function GetAllScreenings() {
        if (MovieId.length > 0 && roomid.length > 0) {
            $.ajax({
                url: '/api/screenings?movieID=' + MovieId + '&roomID=' + roomid,
                method: 'GET',
                contentType: 'json',
                dataType: 'json',
                error: function (response) {
                },
                success: function (reponse) {
                    $("#screening").empty();
                    $("#screening").append("<option value = " + '0' + "> --Please Select a Screening-- </option>");
                    const len = reponse.length;
                    console.log(reponse);
                    let table = '';
                    for (var i = 0; i < len; i++) {
                        table = table + '<tr>';
                        $("#screening").append("<option id=" + reponse[i].MaBuoiChieu + " value=" + reponse[i].GiaVe + ">" + reponse[i].NgayChieu + "||" + reponse[i].GioChieu + "</option>");
                        table = table + '<td>' + reponse[i].GiaVe + '</td>';
                        table = table + '<td>' + reponse[i].GioChieu + '</td>';
                        table = table + '<td>' + reponse[i].MaBuoiChieu + '</td>';
                        table = table + '<td>' + reponse[i].MaPhim + '</td>';
                        table = table + '<td>' + reponse[i].MaPhong + '</td>';
                        table = table + '<td>' + reponse[i].NgayChieu + '</td>';
                        table = table + '</tr>';
                    }
                },
                fail: function (response) {
                }
            });
        }
    }

    //Hàm l?y ra toàn b? danh sách Khách Hàng. Dùng $.ajax() th?c hi?n method HTTP GET
    function GetAllTickets() {
        if (screeningID.length > 0) {
            $(".tcontainer>div.row>div.sold").removeClass("sold");
            $(".tcontainer>div.row>div.selected").removeClass("selected");
            updateSelectedCount();
            $.ajax({
                url: '/api/tickets?screeningID=' + screeningID,
                method: 'GET',
                contentType: 'json',
                dataType: 'json',
                error: function (response) {
                },
                success: function (reponse) {
                    const len = reponse.length;
                    console.log(reponse);
                    let table = '';
                    for (var i = 0; i < len; i++) {
                        $("#" + parseInt(((reponse[i].HangGhe.trim().charCodeAt(0) - 65) * 10 + reponse[i].SoGhe))).addClass('sold');
                        table = table + '<tr>';
                        table = table + '<td>' + reponse[i].MaBuoiChieu + '</td>';
                        table = table + '<td>' + reponse[i].HangGhe + '</td>';
                        table = table + '<td>' + reponse[i].SoGhe + '</td>';
                        table = table + '</tr>';
                    }
                    //document.getElementById('allKH').innerHTML = table;
                },
                fail: function (response) {
                }
            });
            orderbtn.disabled = true;

        }
    }

    //Hàm l?y Thêm m?i Khách hàng. Dùng $.ajax() th?c hi?n method HTTP POST
    function insertTickets(idVe, hang, soghe) {
        var url = '/api/tickets?id=' + idVe + '&bcId=' + screeningID + '&row=' + hang + '&seat=' + soghe + '&type=thuong&cost=' + ticketPrice;
        $.ajax({
            url: url,
            method: 'POST',
            contentType: 'json',

            dataType: 'json',
            error: function (response) {
                console.log("Order fail:((");
            },
            success: function (reponse) {
                console.log("Order success <3");
            }
        });

}


    // Initial count and total set
    updateSelectedCount();
