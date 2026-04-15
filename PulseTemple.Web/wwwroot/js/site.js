// Header

const btn = document.getElementById('menuBtn');
const menu = document.getElementById('mainNav');
const links = document.querySelectorAll(".menu-item");

btn?.addEventListener('click', () => {
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

document.getElementById("about-form-file-input")?.addEventListener("change", function (e) {
    const fileNameDisplay = document.getElementById("file-name-display");

    if (this.files && this.files.length > 0) {
        const fileName = this.files[0].name;
        fileNameDisplay.textContent = fileName;

        fileNameDisplay.style.color = "var(--dark-color)";
    } else {
        fileNameDisplay.textContent = "Upload Profile Image";
        fileNameDisplay.style.color = "#757575";
    }
});


/**
 * Toggles specified classes on a list of DOM elements based on their IDs.
 * @param {Array<{elementId: string, classToAdd: string}>} toggleElementClassList - An array of objects containing element IDs and the classes to toggle.
 * @param {number} index - The index to validate (must be an integer).
 * @param {string|number} id - The unique identifier to validate.
 */
function toggleClassOnClick(toggleElementClassList, index, id) {
    if (!Number.isInteger(index) || !toggleElementClassList || !id) return;

    for (let i = 0; 0 < toggleElementClassList.length; i++) {
        const item = toggleElementClassList[i];
        const element = document.getElementById(item?.elementId);

        if (!element) return;
            element.classList.toggle(item.classToAdd);
    }

}