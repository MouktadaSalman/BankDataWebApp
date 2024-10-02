// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');
let users = [];

signUpButton.addEventListener('click', () =>
    container.classList.add('right-panel-active'));

signInButton.addEventListener('click', () =>
    container.classList.remove('right-panel-active'));


// Get the modal
var modal = document.getElementById("myModal");

// Get the button that opens the modal
var openModalButton = document.getElementById("openModal");

// Get the <span> element that closes the modal
var closeModalButton = document.getElementsByClassName("close")[0];

var saveProfileButton = document.getElementById("saveButton");




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

    var name = document.getElementById('SName').value;
    var password = document.getElementById('SPass').value;
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
            if(!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            const jsonObject = data;
            if (jsonObject.login) {
                users.push(name);
                loadView('/authenticated');
            }
            else {
                loadView('/error');
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
        })        
        .catch(error => {
            console.error('Fetch error:', error);
            alert('Unable to load user profile. Please try again.');
        });

}



saveProfileButton.onclick = function () {
    modal.style.display = "none";
}


// When the user clicks on the "Update Profile" text in the image, open the modal
openModalButton.onclick = function () {
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
closeModalButton.onclick = function () {
    modal.style.display = "none";
}

//When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

