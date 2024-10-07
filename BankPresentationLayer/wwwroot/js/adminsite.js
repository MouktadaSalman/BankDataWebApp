console.log("Script is loaded");

const logoutButt = document.getElementById('logoutBtn');
const editButt = document.getElementById('openAdminProfile');
const aProfMod = document.getElementById('adminProfileModal');
const uAccountList = document.getElementById('userAccountList');
const listItems = document.querySelectorAll('#userAccountList li');

/* Button declarations */
var editProfileButt = document.getElementById('editAdminProfileButton');
var saveProfileButt = document.getElementById('saveAdminProfileButton');
var modalClose = document.getElementById('aClose');
var uManageEditButt = document.getElementById('editAccountButton');
var uManageCreateButt = document.getElementById('createAccountButton');
var uManageDeleteButt = document.getElementById('deleteAccountButton');

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

/* User Account Management Button Click Handlers */

document.addEventListener('DOMContentLoaded', function () {
    /* Clickable list items when clicked */
    uAccountList.addEventListener('click', function (event) {
        const item = event.target.closest('li'); // Get the clicked li element

        if (uAccountList.children.length > 1) {
            if (item) {
                // Remove 'active' class from all other list items
                const allItems = uAccountList.querySelectorAll('li');
                allItems.forEach(function (li) {
                    li.classList.remove('active'); // Remove 'active' class
                });

                item.classList.toggle('active');
            }
        }
    });

    // Get the full path
    var path = decodeURIComponent(window.location.pathname);

    console.log('Decoded path:', path);
    // Use a regular expression to extract the identifier
    const regex = /admin=([^-\s]+)-([^-\s]+)/; // Matches 'admin={identifier}-{lName}'
    const match = regex.exec(path);

    if (match && match.length === 3) {
        const identifier = match[1]; // This will get 'John'
        const lName = match[2]; // This will get 'Sanders'

        // Load profile details with the identifier
        loadProfileDetails(identifier);
    } else {
        console.error('Failed to extract identifier from URL');
    }

    fetchAllAccounts();
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
                document.getElementById('editAdminName').value = data.name;
                document.getElementById('editAdminEmail').value = data.email;
                document.getElementById('editAdminPhone').value = data.phone;
                document.getElementById('editAdminPassword').value = data.password;
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

function saveChanges(aIdentifier, aName, aEmail, aPhone, aPassword) {

}

function fetchAllAccounts() {
    const apiUrl = `/getaccounts`;

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
            loadAccounts(data);
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function loadAccounts(accounts) {
    // Clear the current list
    uAccountList.innerHTML = '';

    // Check if there is data (accounts) returned
    if (accounts.length > 0) {
        // Iterate through the account list and populate the user list UI
        accounts.forEach(account => {
            const listItem = document.createElement('li');
            listItem.innerHTML = `
                <div style="flex: 1; display: flex;  flex-direction: column; justify-content: space-between;">
                    <span style="flex: 1;">Account No: ${account.acctNo}</span>
                    <span style="flex: 0.5;">Type: ${account.acctType}</span>
                    <span style="flex: 2;">Owner: ${account.acctOwner}</span>
                </div>
                <div style="flex: 1; text-align: left;">
                    <span>Balance: $${account.acctBal}</span>
                </div>
            `;
            uAccountList.appendChild(listItem);
        });
    } else {
        const listItem = document.createElement('li');
        listItem.textContent = "No accounts found";
        uAccountList.appendChild(listItem);
    }
}
