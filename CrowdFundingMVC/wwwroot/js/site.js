// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Initialize tooltips

/*document.addEventListener("click", function (e) {
    if (e.target.classList.contains("gallery-item")) {
        const src = e.target.getAttribute("src");
        document.querySelector(".modal-img").src = src;
        const myModal = new bootstrap.Modal(document.getElementById('gallery-modal'));
        myModal.show();
    }
})*/


/* var form = document.getElementById(clicked_id);
var submitButton = document.getElementById(clicked_id);


form.addEventListener('submit', function () {

    // Disable the submit button
    submitButton.setAttribute('disabled', 'disabled');

    // Change the "Submit" text
    submitButton.value = 'Please wait...';

}, false);*/




var tryNumber = 0;
jQuery('input[type=submit]').click(function (event) {
    var self = $(this);

    if (self.closest('form').valid()) {
        if (tryNumber > 0) {
            tryNumber++;
            alert('Your form has been already submited. wait please');
            return false;
        }
        else {
            tryNumber++;
        }
    };
}); 


