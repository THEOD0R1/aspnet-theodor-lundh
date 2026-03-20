const btn = document.getElementById('menuBtn');
const menu = document.getElementById('mainNav');
const links = document.querySelectorAll(".menu-item");

btn.addEventListener('click', () => {
    const isOpen = menu.classList.toggle('header-toggle-menu');

    btn.classList.toggle('menu-btn-is-open');
    btn.setAttribute('aria-expanded', isOpen);

    const newTabIndex = isOpen ? 0 : -1;

    links.forEach(link => {
        link.setAttribute('tabindex', newTabIndex);
        link.setAttribute('aria-hidden', !isOpen);
    });
});

