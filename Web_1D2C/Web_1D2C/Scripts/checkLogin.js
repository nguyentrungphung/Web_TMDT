// Kiểm tra đăng nhập
var currentUser = localStorage.getItem('currentUser');
if (!currentUser) {
    window.location.assign('loginAdmin.html');
}