
const loginB = document.getElementById('aLoginButton');

if (loginB) {
    loginB.addEventListener('click', performLogin)
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
    function loadView(status) {
        var apiUrl = '/defaultview';

        if (status === "authenticated")
            apiUrl = '/authenticated';
        if (status === "error")
            apiUrl = '/loginerror';

        console.log("Navigate to:  " + apiUrl);

        window.location.href = apiUrl;
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
                window.location.href = `/authenticated/${uName}`;
            }
            else {
                loadView('error');
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}