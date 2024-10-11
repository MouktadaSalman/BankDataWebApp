console.log("Script is loaded");

const logoutButt = document.getElementById('logoutBtn');
const editButt = document.getElementById('openAdminProfile');
const aProfMod = document.getElementById('adminProfileModal');
const accProfMod = document.getElementById('accountProfileModal');
const uAccountList = document.getElementById('userAccountList');
const uAccountButtons = document.getElementById('userAccountsButtonsContainer');
const transactionList = document.getElementById('transactionList');
const adminActivityList = document.getElementById('adminActivityLogs');
const startDateInput = document.getElementById('transactionStartDate');
const endDateInput = document.getElementById('transactionEndDate');

/* Button declarations */
var editProfileButt = document.getElementById('editAdminProfileButton');
var saveProfileButt = document.getElementById('saveAdminProfileButton');
var modalClose = document.getElementById('aClose');
var modalClose2 = document.getElementById('accClose');
var searchUserForm = document.getElementById('searchUserForm');
var uManageEditButt = document.getElementById('editAccountButton');
var uManageCreateButt = document.getElementById('createAccountButton');
var uManageDeleteButt = document.getElementById('deleteAccountButton');
var filterButton = document.getElementById('filterTransactions');
var editAccountButton = document.getElementById('editAccountButtonM');
var saveAccountButton = document.getElementById('saveAccountButton');
var saveCreateButton = document.getElementById('saveCreateButton');

/* Reset inputs and remove error mask when typing */
document.getElementById('editAdminFName').addEventListener('input', function () {
    adminResetInputError(this);
});
document.getElementById('editAdminLName').addEventListener('input', function () {
    adminResetInputError(this);
});
document.getElementById('editAdminEmail').addEventListener('input', function () {
    adminResetInputError(this);
});
document.getElementById('editAdminPassword').addEventListener('input', function () {
    adminResetInputError(this);
});
document.getElementById('editAccountType').addEventListener('input', function () {
    adminResetInputError(this);
});
document.getElementById('editAccountBalance').addEventListener('input', function () {
    adminResetInputError(this);
});
document.getElementById('createAccountType').addEventListener('input', function () {
    adminResetInputError(this);
});
document.getElementById('createAccountBalance').addEventListener('input', function () {
    adminResetInputError(this);
});
document.getElementById('createUserId').addEventListener('input', function () {
    adminResetInputError(this);
});

/* Button onclick behaviours */
// Logout button
logoutButt.onclick = function () {
    window.location.href = '/admin/logout';
}
// Edit button (outside of modal)
editButt.onclick = function () {
    aProfMod.style.display = "flex";

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
    var fName = document.getElementById('editAdminFName');
    var lName = document.getElementById('editAdminLName');
    var email = document.getElementById('editAdminEmail');
    var pass = document.getElementById('editAdminPassword');

    let isValid = true;

    // Reset input error
    adminResetInputError(fName);
    adminResetInputError(lName);
    adminResetInputError(email);
    adminResetInputError(pass);

    if (!fName || !fName.value) {
        adminSetInputError(fName, "Required");
        isValid = false;
    }

    if (!lName || !lName.value) {
        adminSetInputError(lName, "Required");
        isValid = false;
    }

    if (!pass || !pass.value) {
        adminSetInputError(pass, "Required");
        isValid = false;
    }

    if (!email || !email.value) {
        adminSetInputError(email, "Required");
        isValid = false;
    }

    if (isValid) {
        aProfMod.style.display = "none";
        saveAdminChanges();
    }
}

/* Close modal when either 'x' pressed or outside of modal */
modalClose.onclick = function () {
    aProfMod.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == aProfMod) {
        aProfMod.style.display = "none";
    }
    if (event.target == accProfMod) {
        accProfMod.style.display = "none";
    }
}

/* User Account Management Button Click Handlers */
searchUserForm.onsubmit = function (event) {
    event.preventDefault();
    fetchAccountsByIdentifier();
}

uManageDeleteButt.onclick = function () {
    // Ensure that the account number exists before attempting to delete
    var acctNoElement = document.getElementById('viewAccountNo');
    if (!acctNoElement || !acctNoElement.innerText) {
        console.error('Account number not found!');
        alert('Unable to delete: No account selected');
        return; // Exit the function if the account number is not found
    }

    deleteAccount();
}

uManageCreateButt.onclick = function () {
    // Load up user options for account owners
    fetchUsers();

    // Load up modal
    accProfMod.style.display = "flex";
    var viewAccountProfile = document.getElementById('viewAccountProfile');
    var editAccountForm = document.getElementById('editAccountForm');
    var createAccountForm = document.getElementById('createAccountForm');

    if (viewAccountProfile) viewAccountProfile.style.display = "none";
    if (editAccountForm) editAccountForm.style.display = "none";
    if (createAccountForm) createAccountForm.style.display = "block";
}

uManageEditButt.onclick = function () {
    // Ensure that the account number exists before attempting to delete
    var acctNoElement = document.getElementById('viewAccountNo');
    if (!acctNoElement || !acctNoElement.innerText) {
        console.error('Account number not found!');
        alert('Unable to edit: No account selected');
        return; // Exit the function if the account number is not found
    }

    accProfMod.style.display = "flex";
    var viewAccountProfile = document.getElementById('viewAccountProfile');
    var editAccountForm = document.getElementById('editAccountForm');
    var createAccountForm = document.getElementById('createAccountForm');

    if (viewAccountProfile) viewAccountProfile.style.display = "block";
    if (editAccountForm) editAccountForm.style.display = "none";
    if (createAccountForm) createAccountForm.style.display = "none";
}
editAccountButton.onclick = function () {
    var viewAccountProfile = document.getElementById('viewAccountProfile');
    var editAccountForm = document.getElementById('editAccountForm');
    var createAccountForm = document.getElementById('createAccountForm');

    if (viewAccountProfile) viewAccountProfile.style.display = "none";
    if (editAccountForm) editAccountForm.style.display = "block";
    if (createAccountForm) createAccountForm.style.display = "none";
}
saveAccountButton.onclick = function () {

    // Ensure that the account number exists before attempting to delete
    var acctNoElement = document.getElementById('viewAccountNo');
    var type = document.getElementById('editAccountType');
    var balance = document.getElementById('editAccountBalance');

    let isValid = true;

    // Reset input error
    adminResetInputError(type);
    adminResetInputError(balance);

    if (!acctNoElement || !acctNoElement.innerText) {
        console.error('Account number not found!');
        alert('Unable to update: Account number not found.');
        return;
    }
    if (!type || !type.value) {
        adminSetInputError(type, "Account type required");
        isValid = false;
    }
    if (!balance || !balance.value) {
        adminSetInputError(balance, "Balance required");
        isValid = false;
    }

    if (isValid) {
        saveAccountChanges();
        accProfMod.style.display = "none";
    }
}

saveCreateButton.onclick = function () {
    var type = document.getElementById('createAccountType');
    var balance = document.getElementById('createAccountBalance');
    var userId = document.getElementById('createUserId');

    let isValid = true;

    // Reset input error
    adminResetInputError(type);
    adminResetInputError(balance);
    adminResetInputError(userId);

    if (!type || !type.value) {
        adminSetInputError(type, "Account type required");
        isValid = false;
    }
    if (!balance || !balance.value) {
        adminSetInputError(balance, "Balance required");
        isValid = false;
    }
    if (!userId || !userId.value) {
        adminSetInputError(userId, "Account owner required");
        isValid = false;
    }

    if (isValid) {
        createAccount();
        accProfMod.style.display = "none";
    }
}

/* Close modal when either 'x' pressed or outside of modal */
modalClose2.onclick = function () {
    accProfMod.style.display = "none";
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

function adminSetInputError(inputElement, errorMessage) {
    // Set the placeholder to show the error message
    inputElement.placeholder = errorMessage;
    // Add error class to highlight input
    inputElement.classList.add('input-error');
}

function adminResetInputError(inputElement) {
    // Reset the placeholder to default
    inputElement.placeholder = "";
    // Remove the error styling
    inputElement.classList.remove('input-error');
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
    checkLogUpdates();
});

/* Function In Relation To Lists */
function extractAccountInfo(item) {
    // Query all span elements directly from the item
    const spans = item.querySelectorAll('span');

    // Check if spans exist and are sufficient
    if (spans.length >= 4) {
        const accountNo = spans[0].textContent.split(': ')[1];
        const accountType = spans[1].textContent.split(': ')[1];
        const accountOwnerId = spans[2].textContent.split(': ')[1];
        const accountOwner = spans[3].textContent.split(': ')[1];
        const accountBalance = spans[4].textContent.split('$')[1];

        // Log the extracted information
        console.log('Account No:', accountNo);
        console.log('Type:', accountType);
        console.log('OwnerId:', accountOwnerId);
        console.log('Owner:', accountOwner);
        console.log('Balance:', accountBalance);

        //Update Modal View Fields
        document.getElementById('viewAccountNo').innerText = accountNo;
        document.getElementById('viewAccountType').innerText = accountType;
        document.getElementById('viewAccountOwner').innerText = accountOwner;
        document.getElementById('viewAccountBalance').innerText = accountBalance;

        //Update Modal Edit Fields
        document.getElementById('editAccountType').value = accountType;
        document.getElementById('editAccountBalance').value = accountBalance;

    } else {
        console.error("Expected spans are not found in the item:", item);
    }
}
function adjustAccountListInteractions() {
    // Disable hover effect if there's <= 1 item
    if (uAccountList.children.length <= 1) {

        if (uAccountList.children[0].textContent === "No accounts found") {
            uAccountList.classList.add('disable-hover');
            uAccountButtons.style.display = "none";
        }
        else {
            uAccountList.classList.remove('disable-hover');
            uAccountButtons.style.display = "flex";
        }
    } else {
        uAccountList.classList.remove('disable-hover');
        uAccountButtons.style.display = "flex";
    }

    // Clickable list items when clicked
    uAccountList.addEventListener('click', function (event) {
        const item = event.target.closest('li'); // Get the clicked li element

        if (uAccountList.children.length >= 1 &&
          !(uAccountList.children[0].textContent === "No accounts found")) {
            if (item) {
                // Remove 'active' class from all other list items
                const allItems = uAccountList.querySelectorAll('li');
                allItems.forEach(function (li) {
                    li.classList.remove('active'); // Remove 'active' class
                });

                item.classList.toggle('active');

                // Extract account information from the clicked item
                extractAccountInfo(item);
            }
        }
    });
}

function loadProfileDetails(user) {
    console.log('Attempt to retrieve admin details');

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
                document.getElementById('adminName').innerText = `${data.fName} ${data.lName}`;
                document.getElementById('adminEmail').innerText = `Email: ${data.email}`;
                document.getElementById('adminPhone').innerText = `Phone: ${data.phone}`;

                //Update Modal View Fields
                document.getElementById('viewAdminName').innerText = `${data.fName} ${data.lName}`;
                document.getElementById('viewAdminEmail').innerText = data.email;
                document.getElementById('viewAdminPhone').innerText = data.phone;
                document.getElementById('viewAdminAddress').innerText = data.address;
                document.getElementById('viewAdminPassword').innerText = data.password;

                //Update Modal Edit Fields
                document.getElementById('editAdminFName').value = data.fName;
                document.getElementById('editAdminLName').value = data.lName;
                document.getElementById('editAdminEmail').value = data.email;
                document.getElementById('editAdminPhone').value = data.phone;
                document.getElementById('editAdminAddress').value = data.address;
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

function saveAdminChanges() {
    console.log("Extract admin id via path");
    // Get the full path
    var path = decodeURIComponent(window.location.pathname);

    // Use a regular expression to extract the identifier (matches id directly after 'admindashboard/')
    const regex = /admindashboard\/(\d+)/; // Matches 'admindashboard/{id}/'
    const match = regex.exec(path);
    var identifier = null;

    if (match && match.length === 2) { // Check if we have a match and the ID group
        identifier = match[1]; // This will get the 'id'
        console.log("Identifier extracted:", identifier);
    } else {
        console.error('Failed to extract identifier from URL');
        return; // Exit the function if identifier extraction fails
    }

    console.log("An update attempt has been made")

    var fName = document.getElementById('editAdminFName').value;
    var lName = document.getElementById('editAdminLName').value;
    var email = document.getElementById('editAdminEmail').value;
    var phone = document.getElementById('editAdminPhone').value;
    var address = document.getElementById('editAdminAddress').value;
    var pass = document.getElementById('editAdminPassword').value;

    var data = {
        Id: parseInt(identifier, 10),
        FName: fName,
        LName: lName,
        Email: email,
        Username: "",
        Age: 1,
        Address: address,
        PhoneNumber: phone,
        ProfilePictureUrl: "",
        Password: pass
    };
    const apiUrl = `/updateadmin/${identifier}`;

    const headers = {
        'Content-Type': 'application/json'
    };

    const requestOption = {
        method: 'PUT',
        headers: headers,
        body: JSON.stringify(data)
    }

    fetch(apiUrl, requestOption)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.auth) {
                //Successful updated
                console.log('Update successful');
                reloadWithUpdated(data.name, data.password);
            }
            else {
                //Show the error
                console.log('Update unsuccessful');
                throw new Error('Update was unsuccessful');
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function saveAccountChanges() {
    var acctNo = document.getElementById('viewAccountNo').innerText;
    var type = document.getElementById('editAccountType').value;
    var bal = document.getElementById('editAccountBalance').value;

    var data = {
         AcctNo: acctNo,
        AccountName: type,
        Balance: parseInt(bal),
        UserId: 0,
        History: null
    }

    const apiUrl = `/updateaccount/${acctNo}`;

    const headers = {
        'Content-Type': 'application/json'
    };

    const requestOption = {
        method: 'PUT',
        headers: headers,
        body: JSON.stringify(data)
    }

    fetch(apiUrl, requestOption)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.check) {
                //Successful updated
                console.log('Update successful');
                fetchAccountsByIdentifier();
            }
            else {
                //Show the error
                console.log('Update unsuccessful');
                throw new Error('Update was unsuccessful');
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function createAccount() {
    const apiUrl = `/admin/createaccount`;

    var type = document.getElementById('createAccountType').value;
    var balance = document.getElementById('createAccountBalance').value;
    var userId = document.getElementById('createUserId').value;

    var data = {
        AcctNo: 0, //For now '0' as temp, will auto-generate in controller
        AccountName: type,
        Balance: parseInt(balance),
        UserId: userId,
        History: null
    }

    const headers = {
        'Content-Type': 'application/json'
    };

    const requestOption = {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(data)
    }

    fetch(apiUrl, requestOption)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.success) {
                //Successful updated
                console.log('Creation successful');
                fetchAccountsByIdentifier();
            }
            else {
                //Show the error
                console.log('Creation unsuccessful');
                throw new Error('Creation was unsuccessful');
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function deleteAccount() {
    var acctNo = document.getElementById('viewAccountNo').innerText;

    const apiUrl = `/deleteaccount/${acctNo}`;

    const requestOption = {
        method: 'DELETE'
    }

    fetch(apiUrl, requestOption)
        .then(response => {
            if (response.ok) {
                fetchAccountsByIdentifier();
            }
            else {
                throw new Error('Network response was not ok');
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function reloadWithUpdated(identifier, password) {
    console.log("A an attempt to reload updated dashboard");

    var data = {
        Username: identifier,
        Password: password
    };
    const apiUrl = '/authenticate';

    const headers = {
        'Content-Type': 'application/json'
    };

    const requestOption = {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(data)
    }

    fetch(apiUrl, requestOption)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.login) {
                //Successful login
                console.log('Successful updated retrieval');
                window.location.href = `/authenticated/${identifier}`;
            }
            else {
                //Show the error
                throw new Error('Cant reload with updated')
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function fetchUsers() {
    const apiUrl = `/getusers`;

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
        .then(users => {
            const userSelect = document.getElementById('createUserId');
            userSelect.innerHTML = ''; // Clear existing options

            users.forEach(user => {
                const option = document.createElement('option');
                option.value = user.id;
                option.textContent = `${user.id}: ${user.fName} ${user.lName}`;
                userSelect.appendChild(option);
            });
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
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
            adjustAccountListInteractions();
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
                <div style="flex: 1.5; display: flex;  flex-direction: column; justify-content: space-between;">
                    <span style="flex: 1.5;">Account No: ${account.acctNo}</span>
                    <span style="flex: 1.5;">Type: ${account.acctType}</span>
                    <span style="flex: 1;">OwnerId: ${account.acctOwnerId}</span>
                    <span style="flex: 1;">Owner: ${account.acctOwner}</span>
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
                <div style="flex: 1; display: flex;  flex-direction: row; justify-content: space-between;">
                    <span style="flex: 1.5;">Date: ${transaction.date}</span>
                    <span style="flex: 1;">Type: ${transaction.type}</span>
                    <span style="flex: 1;">Account: ${transaction.acctNo}</span>
                </div>
                <div style="flex: 1; text-align: left;">
                    <span>${transaction.hString}</span>
                </div>
            `;
            transactionList.appendChild(listItem);
        });
    } else {
        console.log(`Transactions has no entires`);
        const listItem = document.createElement('li');
        listItem.textContent = "No transactions found";
        transactionList.appendChild(listItem);
    }
}

let previousAdminLogs = [];

function checkLogUpdates() {
    const apiUrl = `/adminlogs`;
    console.log(`Get admin logs`);

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
            console.log('Fetched logs:', data);
            // Compare the new list with the previous list
            if (JSON.stringify(data) !== JSON.stringify(previousAdminLogs)) {
                console.log('List has been updated!');
                // Update the previous list
                previousAdminLogs = [...data];
                // Handle the updated list (e.g., update UI)
                loadAdminLogs(data);
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function loadAdminLogs(logs) {
    console.log(`Reset admin logs`);
    // Clear the current list
    adminActivityList.innerHTML = '';

    // Check if there is data (accounts) returned
    if (logs.length > 0) {
        console.log(`Transactions has entries`);
        // Iterate through the account list and populate the user list UI
        logs.forEach(log => {
            const listItem = document.createElement('li');
            listItem.textContent = log;
            adminActivityList.appendChild(listItem);
        });
    } else {
        console.log(`Transactions has no entires`);
        const listItem = document.createElement('li');
        listItem.textContent = "No logs found";
        adminActivityList.appendChild(listItem);
    }
}

setInterval(checkLogUpdates, 5000); // Poll the server every 5 seconds