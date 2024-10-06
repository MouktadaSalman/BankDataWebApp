console.log("Script is loaded");

const logoutButt = document.getElementById('logoutBtn');
const editButt = document.getElementById('openAdminProfile');
const aProfMod = document.getElementById('adminProfileModal');

var editProfileButt = document.getElementById('editAdminProfileButton');
var saveProfileButt = document.getElementById('saveAdminProfileButton');
var modalClose = document.getElementById('aClose');

/* Button onclick behaviours */
// Logout button
logoutButt.onclick = function () {
    window.location.href = '/admin/logout';
}
// Edit button (outside of modal)
editButt.onclick = function () {
    aProfMod.style.display = "block";

    // Ensure viewProfile is displayed and editProfileForm is hidden (Added)
    var viewProfileDiv = document.getElementById('viewAdminProfile');
    var editProfileForm = document.getElementById('editAdminProfileForm');
    if (viewProfileDiv) viewProfileDiv.style.display = "block";
    if (editProfileForm) editProfileForm.style.display = "none";
}
// Edit button (inside of modal)
editProfileButt.onclick = function () {
    // Ensure viewProfile is displayed and editProfileForm is hidden (Added)
    var viewProfileDiv = document.getElementById('viewAdminProfile');
    var editProfileForm = document.getElementById('editAdminProfileForm');
    if (viewProfileDiv) viewProfileDiv.style.display = "none";
    if (editProfileForm) editProfileForm.style.display = "block";
}
// Save button (inside of modal)
saveProfileButt.onclick = function () {
    // Ensure viewProfile is displayed and editProfileForm is hidden (Added)
    var viewProfileDiv = document.getElementById('viewAdminProfile');
    var editProfileForm = document.getElementById('editAdminProfileForm');
    if (viewProfileDiv) viewProfileDiv.style.display = "block";
    if (editProfileForm) editProfileForm.style.display = "none";
}

/* Close modal when either 'x' pressed or outside of modal */
modalClose.onclick = function () {
    aProfMod.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == aProfMod) {
        aProfMod.style.display = "none";
    }
}

document.addEventListener('DOMContentLoaded', function () {
    // Get the full path
    var path = decodeURIComponent(window.location.pathname);

    console.log('Decoded path:', path);
    // Use a regular expression to extract the identifier
    const regex = /admin=([^-\s]+)-([^-\s]+)/; // Matches 'admin={identifier}-{lName}'
    const match = regex.exec(path);

    if (match && match.length > 1) {
        const identifier = match[1]; // This will get 'John'
        const lName = match[2]; // This will get 'Sanders'

        // Load profile details with the identifier
        loadProfileDetails(identifier);
    } else {
        console.error('Failed to extract identifier from URL');
    }
});

function loadProfileDetails(user) {
    console.log('Attempt to retrieve admin details');

    var name = document.getElementById('adminName');
    var email = document.getElementById('adminEmail');
    var phone = document.getElementById('adminPhone');

    const apiUrl = `/getadmin/${user}`;

    const requestOption = {
        method: 'GET'
    };

    fetch(apiUrl, requestOption)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log("Successful retrieval of Json response data body");

            if (data.auth) {
                console.log("Data retrieval was successful!");
                // Set the retrieved data to the corresponding HTML elements
                document.getElementById('adminName').innerText = data.name;
                document.getElementById('adminEmail').innerText = `Email: ${data.email}`;
                document.getElementById('adminPhone').innerText = `Phone: ${data.phone}`;

                //Update Modal View Fields
                document.getElementById('viewAdminName').innerText = data.name;
                document.getElementById('viewAdminEmail').innerText = data.email;
                document.getElementById('viewAdminPhone').innerText = data.phone;
                document.getElementById('viewAdminPassword').innerText = data.password;

                //Update Modal Edit Fields
                document.getElementById('editAdminName').innerText = data.name;
                document.getElementById('editAdminEmail').innerText = data.email;
                document.getElementById('editAdminPhone').innerText = data.phone;
                document.getElementById('editAdminPassword').innerText = data.password;
            }
            else {
                console.log("Data retrieval was unsuccessful...");
                throw new Error('Data retrieval failed');
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}
