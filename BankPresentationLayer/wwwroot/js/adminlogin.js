
function setInputError(inputElement, errorMessage) {
    // Set the placeholder to show the error message
    inputElement.placeholder = errorMessage;
    // Add error class to highlight input
    inputElement.classList.add('input-error');
}

function resetInputError(inputElement) {
    // Reset the placeholder to default
    inputElement.placeholder = inputElement.getAttribute('id') === 'aName' ? "Enter Username" : "Enter Password";
    // Remove the error styling
    inputElement.classList.remove('input-error');
}

//Clear errors once user begins typing

function validateForm() {
    var uName = document.getElementById('aName');
    var uPass = document.getElementById('aPass');

    let isValid = true;

    // Reset error state
    resetInputError(uName);
    resetInputError(uPass);

    /* Null check */
    // If username is empty
    if (uName.value.trim() === "") {
        setInputError(uName, "Username is required");
        isValid = false;
    }

    // If password empty
    if (uPass.value.trim() === "") {
        setInputError(uPass, "Password is required");
        isValid = false;
    }

    // If valid, proceed with login
    if (isValid) {
        performLogin(); // Call the login function
    }
}

function performLogin() {
    console.log("A login attempt has been made called from: performLogin()")

    var uName = document.getElementById('aName').value;
    var uPass = document.getElementById('aPass').value;

    var data = {
        Username: uName,
        Password: uPass
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
                console.log('Login successful');
                loadView('adminAuthenticated', 'dashboard', uName);
            }
            else {
                //Show the error message modal
                console.log('Login unsuccessful');
                lModal.style.display = "block";
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function attachAdminLoginEventListeners() {

    document.getElementById('aName').addEventListener('input', function () {
        resetInputError(this);
    });

    document.getElementById('aPass').addEventListener('input', function () {
        resetInputError(this);
    });

    const lModal = document.getElementById('loginModal');

    //Close the error modal when the user clicks on <span> (x)
    const lSpan = document.getElementById('lClose');
    lSpan.onclick = function () {
        lModal.style.display = "none";
    }

    //Close the modal if the user clicks outside of it
    window.onclick = function (event) {
        if (event.target == lModal) {
            lModal.style.display = "none";
        }
    }

    const loginB = document.getElementById('aLoginButton');
    if (loginB) {
        loginB.addEventListener('click', validateForm)
    }

    const backButton = document.getElementById('backHomeButton');
    if (backButton) {
        console.log("back to user login!!");
        backButton.addEventListener('click', function (event) {
            event.preventDefault();
            loadView(' ', 'login', ' ');
        });        
    }
}