var sidebarBox = document.getElementById("sidebar");
var btn = document.getElementById("sidebarCollapse");
var content = document.getElementById("content");

btn.addEventListener('click', function (event) {
    if (this.classList.contains('active')) {
        this.classList.remove('active');
        sidebarBox.classList.remove('active');

    }
    else {
        this.classList.add('active');
        sidebarBox.classList.add('active');
    }
});

content.addEventListener('click', function (even) {
    if (sidebarBox.classList.contains('active')) {
        btn.classList.remove('active');
        sidebarBox.classList.remove('active');
    }
});

window.addEventListener('keydown', function (event) {
    if (sidebarBox.classList.contains('active') && event.keyCode == 27) {
        btn.classList.remove('active');
        sidebarBox.classList.remove('active');
    }
});