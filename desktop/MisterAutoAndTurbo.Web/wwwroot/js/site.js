// Mister Auto & Turbo - site scripts
document.addEventListener('DOMContentLoaded', function () {
  // Password visibility toggles
  document.querySelectorAll('.password-toggle').forEach(function (btn) {
    btn.addEventListener('click', function () {
      var id = btn.getAttribute('data-target');
      var input = document.getElementById(id);
      if (!input) return;
      var show = input.type === 'password';
      input.type = show ? 'text' : 'password';
      var open = btn.querySelector('.eye-open');
      var closed = btn.querySelector('.eye-closed');
      if (open && closed) {
        open.style.display = show ? 'none' : '';
        closed.style.display = show ? '' : 'none';
      }
    });
  });
});
