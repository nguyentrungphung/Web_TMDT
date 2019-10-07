var nguoiDungSV = new nguoiDungService();

function dangNhap() {
    var taiKhoan = document.getElementById('txtTaiKhoan').value;
    var matKhau = document.getElementById('txtMatKhau').value;

    nguoiDungSV.DangNhap(taiKhoan, matKhau).then(function(res) {
            console.log(res.data);
            if (res.data.MaloaiNguoiDung === "QuanTri") {
                localStorage.setItem("currentUser", JSON.stringify(res.data));
                window.location.assign('index.html');
            }
        })
        .catch(function(ex) {
            console.log(ex.message);
        })
}

document.getElementById('btnDangNhap').addEventListener('click', dangNhap);