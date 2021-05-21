const sw = document.getElementById("switch-style"), b = document.body;
const dark = window.matchMedia('(prefers-color-scheme: dark)').matches;

if (sw && b) {
  sw.checked = dark;
  var theme = window.localStorage.getItem("theme")
  if (theme !== undefined)
    sw.checked = theme == "dark-theme";

  b.classList.toggle("dark-theme", sw.checked)
  b.classList.toggle("light-theme", !sw.checked)

  sw.addEventListener("change", function () {
    b.classList.toggle("dark-theme", this.checked)
    b.classList.toggle("light-theme", !this.checked)
    if (window.localStorage) {
      this.checked ? localStorage.setItem("theme", "dark-theme") : localStorage.setItem("theme", "light-theme")
    }
  })
}