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