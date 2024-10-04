
const loginB = document.getElementById('aLoginButton');
const lModal = document.getElementById('loginModal');
const lSpan = document.getElementById('lClose');

if (loginB) {
    loginB.addEventListener('click', validateForm)
}

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
document.getElementById('aName').addEventListener('input', function () {
    resetInputError(this);
});
document.getElementById('aPass').addEventListener('input', function () {
    resetInputError(this);
});

function validateForm() {
    var uName = document.getElementById('aName');
    var uPass = document.getElementById('aPass');

    let isValid = true;

    // Reset error state
    resetInputError(uName);
    resetInputError(uPass);

    // Validate Username
    if (uName.value.trim() === "") {
        setInputError(uName, "Username is required");
        isValid = false;
    }

    // Validate Password
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
                //Successfule login
                console.log('Login successful');
                window.location.href = `/authenticated/${uName}`;
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

//Close the error modal when the user clicks on <span> (x)
lSpan.onclick = function () {
    lModal.style.display = "none";
}

//Close the modal if the user clicks outside of it
window.onclick = function (event) {
    if (event.target == lModal) {
        lModal.style.display = "none";
    }
}
