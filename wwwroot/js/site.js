document.addEventListener("DOMContentLoaded", function () {
    const fadeIns = document.querySelectorAll('.fade-in');

    function checkScroll() {
        fadeIns.forEach(element => {
            const rect = element.getBoundingClientRect();
            if (rect.top < window.innerHeight - 50) {
                element.classList.add('show');
            }
        });
    }

    window.addEventListener('scroll', checkScroll);
    checkScroll(); // Gọi ngay khi tải trang
});

$(document).ready(function () {
    $("#userDropdown").click(function (event) {
        event.preventDefault(); // Ngăn chặn chuyển hướng nếu có
        $("#dropdownMenu").toggle(); // Hiển thị hoặc ẩn dropdown menu
    });

    // Đóng dropdown khi click ra ngoài
    $(document).click(function (event) {
        if (!$(event.target).closest("#userDropdown, #dropdownMenu").length) {
            $("#dropdownMenu").hide();
        }
    });
});