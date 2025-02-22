﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function loadView(status, cssName, name) {

    const cssLink = document.getElementById('dynamic-css');
    cssLink.href = `/css/${cssName}.css`;

    var apiUrl = '/defaultview';

    if (status === 'authenticated')
        apiUrl = `/authenticate/${name}`;
    if (status === 'adminLogin')
        apiUrl = '/admin/adminLogin';
    if (status === 'adminAuthenticated')
        apiUrl = `/authenticated/${name}`;
    if (status === 'error')
        apiUrl = '/defaultview';

    console.log("Navigate to:  " + apiUrl);

    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text();
        })
        .then(data => {
            document.getElementById('main').innerHTML = data;

            if (status === 'adminLogin') {
                attachAdminLoginEventListeners();
            }
            else if (status === 'adminAuthenticated'){
                attachDashboardLoginEventListeners();
                loadProfileDetails(name);
                //fetchAllTransactions();
            }
            else {
                attachLoginEventListeners(); // attach the event listeners
            }

            if (status === 'authenticated') {
                loadUserProfile(); // load the user profile here
                loadUserAccount(); // Load accounts if needed
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function performAuth() {
    console.log('performAuth function called'); // Debug log

    var name = document.getElementById('Name').value;
    var password = document.getElementById('Pass').value;

    console.log(`Name: ${name} And password:${password}`);
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
                loadView('authenticated','site' , name);
                //window.location.href = `/authenticate/${name}`;
            }
            else {
                loadView('error', 'login', ' ');
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

            } else {
                alert('Error creating profile: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error creating profile:', error);
            alert('Unable to create profile. Please try again.');
        });
}

function saveProfile() {
    const modal = document.getElementById('myModal');
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
        .then(async response => {
            if (!response.ok) {
                const err = await response.json();
                throw new Error(err.message);
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

function makeTransaction(accountNumber, amount, type) {       
    const apiUrl = '/transaction';

    const headers = {
        'Content-Type': 'application/json',
    };

    const data = {
        accountNumber: accountNumber,
        amount: parseFloat(amount),
        type: type
    };

    const requestOptions = {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(data) // Convert the data object to a JSON string
    };

    fetch(apiUrl, requestOptions)
        .then(async response => {
            console.log('Raw response:', response);
            if (!response.ok) {
                throw new Error(`Error`);
            }
            return response.json()
        })
        .then(result => {
            if (result.success) {
                alert(`${type.charAt(0).toUpperCase() + type.slice(1)} successful!`);
               
                loadUserAccount();
            } else {
                alert(`Error during ${type}: ${result.message}`);
            }
        })
    .catch(error => {
        console.error(`${type} failed`, error);

        // Display a more detailed alert message
        const errorMessage = error.message || 'Unknown error occurred';
        alert(`Error during ${type}: ${errorMessage}. Please check the console for more details.`);
    });
}

function loadAccountHistory(acctNo, startDate, endDate) {
    const apiUrl = `/loadhistory/${acctNo}?startDate=${startDate}&endDate=${endDate}`;

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
            const transactionListElement = document.getElementById('transactionList');
            transactionListElement.innerHTML = ''; // Clear old transactions

            if (accountHistory.length > 0) {
                accountHistory.forEach(historyEntry => {
                    const listItem = document.createElement('li');
                    listItem.className = 'transactionItem';
                    listItem.innerText = historyEntry;
                    transactionListElement.appendChild(listItem);
                });
            } else {
                const listItem = document.createElement('li');
                listItem.className = 'transactionItem';
                listItem.innerText = 'No transactions found for this date range.';
                transactionListElement.appendChild(listItem);
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
            alert('Unable to load transaction history. Please try again.');
        });
}

function filterTransactionsByDateRange(accountNumber) {
    console.log("Filter button clicked for Account:", accountNumber);

    const startDate = document.getElementById('startDate').value;
    const endDate = document.getElementById('endDate').value;

    if (!startDate || !endDate) {
        alert("Please select both a start and end date.");
        return;
    }

    // Construct the API URL with query parameters
    const apiUrl = `/loadhistory/${accountNumber}?startDate=${startDate}&endDate=${endDate}`;
    console.log("API URL:", apiUrl);

    fetch(apiUrl, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => {
            if (!response.ok) throw new Error("Failed to fetch filtered transactions.");
            return response.json();
        })
        .then(filteredHistory => {
            const transactionListElement = document.getElementById('transactionList');
            transactionListElement.innerHTML = ''; // Clear existing items

            if (filteredHistory && filteredHistory.length > 0) {
                filteredHistory.forEach(entry => {
                    const listItem = document.createElement('li');
                    listItem.className = 'transactionItem';
                    listItem.innerText = entry;
                    transactionListElement.appendChild(listItem);
                });
            } else {
                transactionListElement.innerHTML = '<li>No transactions found for this date range.</li>';
            }
        })
        .catch(error => {
            console.error('Error filtering transactions:', error);
            alert('Unable to filter transactions. Please try again.');
        });
}

function attachLoginEventListeners() {

    //When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

    const container = document.getElementById('container');

    const signUpButton = document.getElementById('signUp');
    if (signUpButton) {
        signUpButton.addEventListener('click', () =>
            container.classList.add('right-panel-active'));
    }

    const signInButton = document.getElementById('signIn');
    if (signInButton) {
        signInButton.addEventListener('click', () =>
            container.classList.remove('right-panel-active'));
    }

    const loginButton = document.getElementById('loginButton');
    if (loginButton) {
        loginButton.addEventListener('click', performAuth);
    }

    const modal = document.getElementById("myModal");

    const saveProfileButton = document.getElementById("saveButton");
    if (saveProfileButton) {
        saveProfileButton.onclick = function (event) {
            event.preventDefault(); // Prevent default form submission
            saveProfile();
        }
    }

    // Event listener to trigger profile creation when the sign-up button is clicked
    if (document.getElementById('addAccountButton')) {
        document.getElementById('addAccountButton').onclick = function (event) {
            event.preventDefault();
            createAccount();

        };
    }

    // Event listener to trigger profile creation when the sign-up button is clicked
    if (document.getElementById('signUpButton')) {
        document.getElementById('signUpButton').onclick = function (event) {
            event.preventDefault();
            createProfile();
        };
    }

    // When the user clicks on the "Update Profile" text in the image, open the modal
    const openModalButton = document.getElementById("openModal");    
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
    const editProfileButton = document.getElementById("editProfileButton");
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
    const closeModalButton = document.getElementsByClassName("close")[0];
    if (closeModalButton) {
        closeModalButton.onclick = function () {
            modal.style.display = "none";
        }
    }

    const logoutButton = document.getElementById("logoutBtn");
    if (logoutButton) {
        document.getElementById("logoutBtn").addEventListener("click", function () {
            document.cookie = "SessionID=; expires=Wed, 02 Oct 2024 00:00:00 UTC; path=/;";

            loadView('', 'login', '');
        });
    }

    const showAll = document.getElementById("showAll");
    if (showAll) {
        showAll.addEventListener("click", function (event) {
            event.preventDefault();

            const accountNumber = document.getElementById('accountNumber').innerText;
            loadAccountHistory(accountNumber);
        });
    }

    const filterButton = document.getElementById("filterTransactions");
    if (filterButton) {
        filterButton.addEventListener("click", function (event) {
            event.preventDefault();

            const accountNumber = document.getElementById('accountNumber').innerText;
            filterTransactionsByDateRange(accountNumber);
        });
    }


    const depositButton = document.querySelector(".depositButton");
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

    const withdrawButton = document.querySelector(".withdrawButton");
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

    const transferButton = document.querySelector(".transferButton");
    if (transferButton) {
        transferButton.addEventListener("click", async function (event) {
            event.preventDefault(); // Prevent form submission if it's inside a form

            const amount = document.getElementById("transferAmount").value;
            const fromAccountNumber = document.getElementById("accountNumber").innerText;
            const toAccountNumber = document.getElementById("transferTo").value;

            if (amount && fromAccountNumber && toAccountNumber) {
                try {
                    // Perform 'send' transaction first (withdraw)
                    const negativeAmount = -Math.abs(amount);
                    await makeTransaction(fromAccountNumber, negativeAmount, 'send');

                    // Perform 'receive' transaction only if the first one succeeded
                    await makeTransaction(toAccountNumber, amount, 'receive');
                    alert('Transfer completed successfully');
                } catch (error) {
                    console.error('Transfer failed:', error);
                    alert('Transfer failed. Please try again.');
                }
            } else {
                alert('Please fill in all required fields.');
            }
        });
    }


    const adminLoginButton = document.getElementById('adminLoginButton');
    if (adminLoginButton) {
        adminLoginButton.addEventListener('click', function (event) {
            event.preventDefault();

            loadView('adminLogin', 'adminlogin', ' ');
        });
    }
}