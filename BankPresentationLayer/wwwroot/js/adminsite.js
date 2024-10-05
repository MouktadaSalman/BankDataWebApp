console.log("Script is loaded");

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
