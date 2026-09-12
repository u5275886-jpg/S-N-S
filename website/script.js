// Client-side download handler & interactive enhancements
document.addEventListener('DOMContentLoaded', () => {
    const downloadBtns = document.querySelectorAll('a[download]');

    downloadBtns.forEach(btn => {
        btn.addEventListener('click', (e) => {
            console.log('Initiating APK Download...');
        });
    });

    // Smooth scroll for internal links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const targetId = this.getAttribute('href');
            if (targetId === '#') return;

            const targetElement = document.querySelector(targetId);
            if (targetElement) {
                targetElement.scrollIntoView({
                    behavior: 'smooth'
                });
            }
        });
    });
});
