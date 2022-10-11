const btnOpenSidebar = document.getElementById("btnOpenSidebar");
const btnCloseSidebar = document.getElementById("btnCloseSidebar");
const divSidebar = document.getElementById("sidebar");
const divOverlay = document.getElementById("overlay");

btnOpenSidebar.onclick = openSidebar;

btnCloseSidebar.onclick = closeSidebar;

divOverlay.onclick = closeSidebar;

function openSidebar(event) {
    event.preventDefault();
    divSidebar.style.display = "block";
    divOverlay.style.display = "block";
}

function closeSidebar(event) {
    event.preventDefault();
    divSidebar.style.display = "none";
    divOverlay.style.display = "none";
}
