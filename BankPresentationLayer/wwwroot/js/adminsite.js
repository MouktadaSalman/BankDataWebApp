console.log("Script is loaded");

const logoutButt = document.getElementById('logoutBtn');
const editButt = document.getElementById('openAdminProfile');
const aProfMod = document.getElementById('adminProfileModal');
const uAccountList = document.getElementById('userAccountList');
const transactionList = document.getElementById('transactionList');
const adminActivityList = document.getElementById('adminActivityLogs');
const startDateInput = document.getElementById('transactionStartDate');
const endDateInput = document.getElementById('transactionEndDate');

/* Button declarations */
var editProfileButt = document.getElementById('editAdminProfileButton');
var saveProfileButt = document.getElementById('saveAdminProfileButton');
var modalClose = document.getElementById('aClose');
var searchUserForm = document.getElementById('searchUserForm');
var uManageEditButt = document.getElementById('editAccountButton');
var uManageCreateButt = document.getElementById('createAccountButton');
var uManageDeleteButt = document.getElementById('deleteAccountButton');
var filterButton = document.getElementById('filterTransactions');

/* Search Results */
var accounts;

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
searchUserForm.onsubmit = function (event) {
    event.preventDefault();
    fetchAccountsByIdentifier();
}

/* Transaction Button Clicks */
filterButton.onclick = function () {
    var startDate = startDateInput.value ? new Date(startDateInput.value).toISOString() : null;
    var endDate = endDateInput.value ? new Date(endDateInput.value).toISOString() : null;

    if (startDate == null && endDate == null) {
        fetchAllTransactions();
    }
    else {
        fetchTransactionsByFilter(startDate, endDate);
    }
}

document.addEventListener('DOMContentLoaded', function () {
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
    fetchAllTransactions();

    //Adjust Initial List Interactions
    adjustAccountListInteractions();
});

/* Function to adjust list behavior after DOM or data changes */
function adjustAccountListInteractions() {
    // Disable hover effect if there's <= 1 item
    if (uAccountList.children.length <= 1) {
        uAccountList.classList.add('disable-hover');

    } else {
        uAccountList.classList.remove('disable-hover');
    }

    // Clickable list items when clicked
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
}

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

function fetchAccountsByIdentifier() {
    var identifier = document.getElementById('searchUser').value;
    console.log(`Fetch accounts via: ${identifier}`);

    const apiUrl = `/getaccounts/${identifier}`;

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
            console.log(`Send to load`);
            loadAccounts(data);
            adjustAccountListInteractions();
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function loadAccounts(accounts) {
    console.log(`Reset list`);
    // Clear the current list
    uAccountList.innerHTML = '';

    // Check if there is data (accounts) returned
    if (accounts.length > 0) {
        console.log(`Accounts has entries`);
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
        console.log(`Account has no entires`);
        const listItem = document.createElement('li');
        listItem.textContent = "No accounts found";
        uAccountList.appendChild(listItem);
    }
}

function fetchAllTransactions() {
    const apiUrl = `/gettransactions`;

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
            loadTransactions(data);
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function fetchTransactionsByFilter(start, end) {
    const apiUrl = `/gettransactions/${start}/${end}`;
    console.log(`Get Transactions with: ${start} - ${end}`);

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
            loadTransactions(data);
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function loadTransactions(transactions) {
    console.log(`Reset Transactions list`);
    // Clear the current list
    transactionList.innerHTML = '';

    // Check if there is data (accounts) returned
    if (transactions.length > 0) {
        console.log(`Transactions has entries`);
        // Iterate through the account list and populate the user list UI
        transactions.forEach(transaction => {
            const listItem = document.createElement('li');
            listItem.innerHTML = `
                <div style="flex: 1; display: flex;  flex-direction: column; justify-content: space-between;">
                    <span style="flex: 1;">Date: ${transaction.date}</span>
                    <span style="flex: 0.5;">Type: ${transaction.type}</span>
                    <span style="flex: 2;">Owner: ${transaction.acctNo}</span>
                </div>
                <div style="flex: 1; text-align: left;">
                    <span>Balance: $${transaction.amt}</span>
                </div>
            `;
            transactionList.appendChild(listItem);
        });
    } else {
        console.log(`Transactions has no entires`);
        const listItem = document.createElement('li');
        listItem.textContent = "No accounts found";
        transactionList.appendChild(listItem);
    }
}