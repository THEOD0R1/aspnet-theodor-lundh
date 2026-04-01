// Header

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

// Dropdown functionality for the FAQ section


function showDropdown(index, id) {
    if (!Number.isInteger(index)) return;
    if (!id) return;

    const dropdownContent = document.querySelectorAll(`.dropdown-content-${id}`);
    var btn = document.getElementById(`dropdown-btn-${index}-${id}`);
    const dropdown = dropdownContent[index];

    dropdown?.classList.toggle("dropdown-content-open");
    btn?.classList.toggle("active-dropdown-btn");
}
