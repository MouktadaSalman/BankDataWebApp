// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

if (signUpButton) {
    signUpButton.addEventListener('click', () =>
        container.classList.add('right-panel-active'));
}

if (signInButton) {
    signInButton.addEventListener('click', () =>
        container.classList.remove('right-panel-active'));
}

document.addEventListener("DOMContentLoaded", function () {
    const loginButton = document.getElementById('loginButton');
    if (loginButton) {
        loginButton.addEventListener('click', performAuth);
    }
});

// Get the modal
var modal = document.getElementById("myModal");

// Get the button that opens the modal
var openModalButton = document.getElementById("openModal");

// Get the <span> element that closes the modal
var closeModalButton = document.getElementsByClassName("close")[0];

var saveProfileButton = document.getElementById("saveButton");


var editProfileButton = document.getElementById("editProfileButton");



function loadView(status) {
    var apiUrl = '/defaultview';

    if (status === "authenticated")
        apiUrl = '/authenticate';
    if (status === "error")
        apiUrl = '/loginerror';

    console.log("Navigate to:  " + apiUrl);

    window.location.href = apiUrl;
}

function performAuth() {
    console.log('performAuth function called'); // Debug log

    var name = document.getElementById('Name').value;
    var password = document.getElementById('Pass').value;
    var data = {
        Username: name,
        Password: password
    };
    const apiUrl = '/auth';

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
                console.log('Login successful');
                // loadView('authenticated');
                window.location.href = `/authenticate/${name}`;
            }
            else {
                loadView('error');
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function loadUserProfile() {

    const apiUrl = '/loadprofile';

    const header = {
        'Content-Type': 'application/json'
    };

    const requestOption = {
        method: 'GET',
        headers: header
    }

    fetch(apiUrl, requestOption)
        .then(response => {
            if(!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(userProfile => {

            document.getElementById('userName').textContent = userProfile.fName + " " + userProfile.lName;
            document.getElementById('userEmail').textContent = userProfile.email;
            document.getElementById('userPhone').textContent = userProfile.phoneNumber;

            document.getElementById('viewName').innerText = userProfile.fName + " " + userProfile.lName;
            document.getElementById('viewEmail').innerText = userProfile.email;
            document.getElementById('viewPhone').innerText = userProfile.phoneNumber;
            document.getElementById('viewAddress').innerText = userProfile.address;

            document.getElementById('editName').value = userProfile.fName + " " + userProfile.lName;
            document.getElementById('editEmail').value = userProfile.email;
            document.getElementById('editPhone').value = userProfile.phoneNumber;
            document.getElementById('editAddress').value = userProfile.address;


        })        
        .catch(error => {
            console.error('Fetch error:', error);
            alert('Unable to load user profile. Please try again.');
        });

}

function saveProfile() {

    var name = document.getElementById("editName").value; // Modified
    var email = document.getElementById("editEmail").value; // Modified
    var phone = document.getElementById("editPhone").value; // Modified
    var address = document.getElementById("editAddress").value; // Modified

    document.getElementById('userName').innerText = name;
    document.getElementById('userEmail').innerText = email;
    document.getElementById('userPhone').innerText = phone;

    document.getElementById('viewName').innerText = name;
    document.getElementById('viewEmail').innerText = email;
    document.getElementById('viewPhone').innerText = phone;
    document.getElementById('viewAddress').innerText = address;

    document.getElementById('editName').value = name;
    document.getElementById('editEmail').value = email;
    document.getElementById('editPhone').value = phone;
    document.getElementById('editAddress').value = address;

    var profile = {
        name: name,
        email: email,
        phone: phone,
        address: address,
    };

    console.log(profile);

    // Close modal and switch back to view mode (Added)
    if (modal) modal.style.display = "none";
    var viewProfileDiv = document.getElementById('viewProfile');
    var editProfileForm = document.getElementById('editProfileForm');
    if (viewProfileDiv) viewProfileDiv.style.display = "block";
    if (editProfileForm) editProfileForm.style.display = "none";
}


function loadUserAccount() {
    const apiUrl = '/loadbankaccount';

    const header = {
        'Content-Type': 'application/json'
    };

    const requestOption = {
        method: 'GET',
        headers: header
    }

    fetch(apiUrl, requestOption)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(userAccounts => {
        const firstAccount = userAccounts[0];
        console.log("account number: " + firstAccount.acctNo + " Account balance: " + firstAccount.balance);

        // Set account details
        document.getElementById('accountNumber').innerText = firstAccount.acctNo;
        document.getElementById('accountBalance').innerText = firstAccount.balance;

        const accountSummaryList = document.getElementById("accountSummaryList");
        accountSummaryList.innerHTML = '';// Clear any existing elements

        userAccounts.forEach(account => {
            const listItem = document.createElement('li');
            listItem.className = 'accountItem';

            const accountNumberElement = document.createElement('p');
            accountNumberElement.innerText = `Account Number: ${account.acctNo}`;

            const accountBalanceElement = document.createElement('p');
            const formattedBalance = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(account.balance);
            accountBalanceElement.innerText = `Balance: ${formattedBalance}`;

            const accountTypeElement = document.createElement('p');
            accountTypeElement.innerText = `Type: ${account.accountName || "Unknown"}`; // Assuming `accountType` is available

            listItem.appendChild(accountNumberElement);
            listItem.appendChild(accountBalanceElement);
            listItem.appendChild(accountTypeElement);

            accountSummaryList.appendChild(listItem);
        });

        loadAccountHistory(firstAccount.acctNo);
        
    })
    .catch(error => {
        console.error('Fetch error:', error);
        alert('Unable to load user profile. Please try again.');
    });

}

function loadAccountHistory(acctNo){
    const apiUrl = `/loadhistory/${acctNo}`;

    const header = {
        'Content-Type': 'application/json'
    };

    const requestOption = {
        method: 'GET',
        headers: header
    }

    fetch(apiUrl, requestOption)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
        .then(accountHistory => {

        // Populate transaction history
        const transactionListElement = document.getElementById('transactionList');
        transactionListElement.innerHTML = ''; // Clear any existing items
        
        accountHistory.forEach(historyEntry => {
            const listItem = document.createElement('li');
            listItem.className = 'transactionItem';
            listItem.innerText = historyEntry; // Set the text to the history string directly
            transactionListElement.appendChild(listItem);
        });
        
    })
    .catch(error => {
        console.error('Fetch error:', error);
        alert('Unable to load user profile. Please try again.');
    });
}





if (saveProfileButton) {
    saveProfileButton.onclick = function (event) {
        event.preventDefault(); // Prevent default form submission
        saveProfile();
    }
}

// When the user clicks on the "Update Profile" text in the image, open the modal
if (openModalButton) {
    openModalButton.onclick = function () {
        modal.style.display = "block";

        // Ensure viewProfile is displayed and editProfileForm is hidden (Added)
        var viewProfileDiv = document.getElementById('viewProfile');
        var editProfileForm = document.getElementById('editProfileForm');
        if (viewProfileDiv) viewProfileDiv.style.display = "block";
        if (editProfileForm) editProfileForm.style.display = "none";
    }
}

// Add event listener to "Edit" button (Added)
if (editProfileButton) {
    editProfileButton.onclick = function () {
        // Hide the viewProfile section and show the editProfileForm
        var viewProfileDiv = document.getElementById('viewProfile');
        var editProfileForm = document.getElementById('editProfileForm');

        if (viewProfileDiv) viewProfileDiv.style.display = "none";
        if (editProfileForm) editProfileForm.style.display = "block";

        // Populate the edit form fields with current user data        
    }
}

// When the user clicks on <span> (x), close the modal
if (closeModalButton) {
    closeModalButton.onclick = function () {
        modal.style.display = "none";
    }
}

//When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
