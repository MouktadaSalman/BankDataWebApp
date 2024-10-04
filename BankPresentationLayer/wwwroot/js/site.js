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
    const headers = { 'Content-Type': 'application/json' };

    fetch(apiUrl, {
        method: 'GET',
        headers: headers
    })
        .then(response => {
            if (!response.ok) throw new Error('Network response was not ok');
            return response.json();
        })
        .then(userProfile => {
            // Update hidden field for user ID
            document.getElementById('Id').value = userProfile.id;

            // Update visible profile fields
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

            // Store all additional fields, I will use them later
            sessionStorage.setItem('username', userProfile.username);
            sessionStorage.setItem('age', userProfile.age);
            sessionStorage.setItem('profilePictureUrl', userProfile.profilePictureUrl);
            sessionStorage.setItem('password', userProfile.password);
        })
        .catch(error => {
            console.error('Error loading profile:', error);
            alert('Unable to load user profile. Please try again.');
        });
}


const logoutButton = document.getElementById("logoutBtn");

if (logoutButton) { 

    document.getElementById("logoutBtn").addEventListener("click", function () {
        document.cookie = "SessionID=; expires=Wed, 02 Oct 2024 00:00:00 UTC; path=/;";

        window.location.href = "/Home/Login";

    });


} 


function createProfile() {
    var UfName = document.getElementById('FName').value;
    var UlName = document.getElementById('LName').value;
    var UEmail = document.getElementById('Email').value;
    var UAge = document.getElementById('Age').value;
    var UAddress = document.getElementById('Address').value;
    var UPhoneNo = document.getElementById('PhoneNo').value;
    var Upassword = document.getElementById('SPass').value;

    
    var profile = {
        FName: UfName,
        LName: UlName,
        Email: UEmail,
        Age: UAge,
        Address: UAddress,
        PhoneNumber: UPhoneNo,
        Password: Upassword
    };

    console.log("Profile to be created: ", profile);

    const apiUrl = '/createProfile';  

    fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(profile)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json(); 
        })
        .then(data => {
            console.log('Profile created successfully:', data);

            // Redirect or provide success message
            if (data.success) {
                alert('Profile created successfully!');


                return RedirectToAction("Login");

            } else {
                alert('Error creating profile: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error creating profile:', error);
            alert('Unable to create profile. Please try again.');
        });
}

// Event listener to trigger profile creation when the sign-up button is clicked
if (document.getElementById('signUpButton')) {
    document.getElementById('signUpButton').onclick = function (event) {
        event.preventDefault(); 
        createProfile(); 
    };
}


function saveProfile() {
    var name = document.getElementById("editName").value;
    var email = document.getElementById("editEmail").value;
    var phone = document.getElementById("editPhone").value;
    var address = document.getElementById("editAddress").value;
    var userId = document.getElementById("Id").value;


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


    if (!userId) {
        console.error('User ID is missing.');
        alert('User ID is missing, unable to update the profile.');
        return;
    }

    // Use additional fields from session storage
    var username = sessionStorage.getItem('username');
    var age = sessionStorage.getItem('age');
    var profilePictureUrl = sessionStorage.getItem('profilePictureUrl');
    var password = sessionStorage.getItem('password');

    // Prepare the profile object
    var profile = {
        Id: parseInt(userId),
        FName: name.split(" ")[0],
        LName: name.split(" ")[1] || "",
        Email: email,
        Username: username,
        Age: parseInt(age),
        Address: address,
        PhoneNumber: phone,
        ProfilePictureUrl: profilePictureUrl,
        Password: password
    };

    console.log("Profile to be updated: ", profile);

    const apiUrl = `/updateprofile`;

    fetch(apiUrl, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(profile)
    })
        .then(response => {
            if (!response.ok) throw new Error('Network response was not ok');
            return response.text();
        })
        .then(data => {
            console.log('Profile updated successfully:', data);

            // Close modal and switch back to view mode
            if (modal) modal.style.display = "none";
            var viewProfileDiv = document.getElementById('viewProfile');
            var editProfileForm = document.getElementById('editProfileForm');
            if (viewProfileDiv) viewProfileDiv.style.display = "block";
            if (editProfileForm) editProfileForm.style.display = "none";
        })
        .catch(error => {
            console.error('Error updating profile:', error);
            alert('Unable to update profile. Please try again.');
        });
}

function createAccount() {
    var UAccountNo = document.getElementById('AccountNumber').value;
    var UBalance = document.getElementById('Balance').value;
    var UAccountType = document.getElementById("AccountType").value; // Account type
    var UserID = document.getElementById("Id").value;

    if (!UAccountNo || !UBalance || !UserID || !UAccountType) {
        alert("All fields must be filled.");
        return;
    }

    var Account = {
        AcctNo: UAccountNo,
        Balance: parseInt(UBalance),
        AccountName: UAccountType,
        UserId: parseInt(UserID),
        History: []
    };

    console.log("Account to be created: ", Account);

    const apiUrl = '/createAccount';

    fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Account)
    })
        .then(response => {
            if (!response.ok) {
                return response.json().then(err => { throw new Error(err.message); });
            }
            return response.json();
        })
        .then(data => {
            console.log('Account created successfully:', data);

            if (data.success) {
                alert('Account created successfully!');

                loadUserAccount();
            } else {
                alert('Error creating Account: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error creating Account:', error.message);
            alert('Unable to create Account. Error: ' + error.message);
        });
}


// Event listener to trigger profile creation when the sign-up button is clicked
if (document.getElementById('addAccountButton')) {
    document.getElementById('addAccountButton').onclick = function (event) {
        event.preventDefault();
        createAccount();
        
    };
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
            const accountSummaryList = document.getElementById("accountSummaryList");
            accountSummaryList.innerHTML = '';

            userAccounts.forEach(account => {
                const listItem = document.createElement('li');
                listItem.className = 'accountItem';

                const accountNumberElement = document.createElement('p');
                accountNumberElement.innerText = `Account Number: ${account.acctNo}`;

                const accountBalanceElement = document.createElement('p');
                const formattedBalance = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(account.balance);
                accountBalanceElement.innerText = `Balance: ${formattedBalance}`;

                const accountTypeElement = document.createElement('p');
                accountTypeElement.innerText = `Type: ${account.accountName || "Unknown"}`;

                listItem.appendChild(accountNumberElement);
                listItem.appendChild(accountBalanceElement);
                listItem.appendChild(accountTypeElement);

                // Add click event listener to load account details when clicked
                listItem.addEventListener('click', function () {
                    loadAccountDetails(account.acctNo, account.balance);
                    loadAccountHistory(account.acctNo); // Load the selected account's transaction history
                });

                accountSummaryList.appendChild(listItem);
            });

            // Automatically load the first account details by default
            if (userAccounts.length > 0) {
                loadAccountDetails(userAccounts[0].acctNo, userAccounts[0].balance);
                loadAccountHistory(userAccounts[0].acctNo);
            }

        })
        .catch(error => {
            console.error('Fetch error:', error);
            alert('User has no Accounts.');
        });
}

function loadAccountDetails(accountNumber, accountBalance) {
    // Update the account number and balance fields
    document.getElementById('accountNumber').innerText = accountNumber;
    document.getElementById('accountBalance').innerText = accountBalance;
}


document.addEventListener("DOMContentLoaded", function () {
    const depositButton = document.querySelector(".depositButton");
    const withdrawButton = document.querySelector(".withdrawButton");
    const transferButton = document.querySelector(".transferButton");

    if (depositButton) {
        depositButton.addEventListener("click", function (event) {
            event.preventDefault();
            const amount = document.getElementById("depositAmount").value;
            const accountNumber = document.getElementById("accountNumber").innerText;

            if (amount && accountNumber) {
                makeTransaction(accountNumber, amount, "deposit");
            } else {
                alert("Please enter a valid deposit amount.");
            }
        });
    }

    if (withdrawButton) {
        withdrawButton.addEventListener("click", function (event) {
            event.preventDefault();
            const amount = document.getElementById("withdrawAmount").value;
            const accountNumber = document.getElementById("accountNumber").innerText;

            if (amount && accountNumber) {
                // Make the withdraw amount negative
                const negativeAmount = -Math.abs(amount);
                makeTransaction(accountNumber, negativeAmount, "withdraw");
            } else {
                alert("Please enter a valid withdraw amount.");
            }
        });

    }

    if (transferButton) {
        transferButton.addEventListener("click", function (event) {
            const amount = document.getElementById("transferAmount").value;
            const fromAccountNumber = document.getElementById("accountNumber").innerText;
            const toAccountNumber = document.getElementById("transferTo").value;

            if (amount && accountNumber) {

                makeTransaction(toAccountNumber, amount, "receive");

                const negativeAmount = -Math.abs(amount);
                makeTransaction(fromAccountNumber, negativeAmount, "send");
            }
        });
    }

    function makeTransaction(accountNumber, amount, type) {       
        const apiUrl = `/transaction`;

        const data = {
            accountNumber: accountNumber,
            amount: parseFloat(amount),
            type: type
        };

        fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
        .then(response => response.json())
        .then(result => {
            if (result.success) {
                alert(`${type.charAt(0).toUpperCase() + type.slice(1)} successful!`);
                // Optionally, reload the user account to reflect updated balance
                loadUserAccount();
            } else {
                alert(`Error during ${type}: ${result.message}`);
            }
        })
        .catch(error => {
            console.error(`${type} failed`, error);
            alert(`Error during ${type}. Please try again.`);
        });
    }
});

function loadAccountHistory(acctNo) {
    const apiUrl = `/loadhistory/${acctNo}`;
    const header = {
        'Content-Type': 'application/json'
    };

    const requestOption = {
        method: 'GET',
        headers: header
    };

    fetch(apiUrl, requestOption)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(accountHistory => {
            const transactionListElement = document.getElementById('transactionList');
            transactionListElement.innerHTML = ''; // Always clear the previous transaction history

            if (Array.isArray(accountHistory) && accountHistory.length === 0) {
                // If no transactions found, show a message
                const listItem = document.createElement('li');
                listItem.className = 'transactionItem';
                listItem.innerText = 'No transactions found for this account.';
                transactionListElement.appendChild(listItem);
            } else {
                // Populate transaction history if available
                accountHistory.forEach(historyEntry => {
                    const listItem = document.createElement('li');
                    listItem.className = 'transactionItem';
                    listItem.innerText = historyEntry;
                    transactionListElement.appendChild(listItem);
                });
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
            const transactionListElement = document.getElementById('transactionList');
            transactionListElement.innerHTML = '';

            alert('Unable to load transaction history.');

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
