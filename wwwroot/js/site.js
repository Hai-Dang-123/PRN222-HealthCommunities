// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Khi cuộn trang, thay đổi chiều cao của banner và cố định navbar
$(document).ready(function () {
    // Lắng nghe sự kiện cuộn trang
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            // Khi cuộn xuống 100px, thêm class "scrolled"
            $('body').addClass('scrolled');
        } else {
            // Khi cuộn lên đầu trang, loại bỏ class "scrolled"
            $('body').removeClass('scrolled');
        }
    });
});

// Function to trigger motion on scroll
window.addEventListener("scroll", function () {
    var infoBoxes = document.querySelectorAll('.info-box');
    infoBoxes.forEach(function (box) {
        var rect = box.getBoundingClientRect();
        if (rect.top < window.innerHeight) {
            box.classList.add('show');
        }
    });
});

// Function to toggle detailed content
window.addEventListener('scroll', function () {
    const infoBoxes = document.querySelectorAll('.info-box');
    const windowHeight = window.innerHeight;

    infoBoxes.forEach((box) => {
        const boxTop = box.getBoundingClientRect().top;

        if (boxTop < windowHeight * 0.8) {
            box.style.opacity = 1;
            box.style.transform = 'translateY(0)';
        } else {
            box.style.opacity = 0;
            box.style.transform = 'translateY(100px)';
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const aboutSection = document.querySelector("#about-us");
    const infoBoxes = document.querySelectorAll(".info-box");

    function checkScroll() {
        const triggerPoint = window.innerHeight * 0.85;

        // Hiển thị #about-us khi cuộn tới
        if (aboutSection.getBoundingClientRect().top < triggerPoint) {
            aboutSection.style.opacity = "1";
            aboutSection.style.transform = "translateY(0)";
        }

        // Hiển thị các box khi cuộn tới
        infoBoxes.forEach((box, index) => {
            if (box.getBoundingClientRect().top < triggerPoint) {
                box.style.opacity = "1";
                box.style.transform = "translateY(0)";
                box.style.transitionDelay = `${index * 0.2}s`; // Tạo hiệu ứng trễ cho từng box
            }
        });
    }

    window.addEventListener("scroll", checkScroll);
    checkScroll(); // Gọi ngay khi tải trang để kiểm tra nếu đã trong viewport
});
