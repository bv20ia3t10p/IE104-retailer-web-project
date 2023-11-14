register_url = "https://localhost:7136/api/Customer";

document.addEventListener("DOMContentLoaded", function () {
  const returnUrl = new URL(window.location.href);
  let googleToken = "";
  try {
    googleToken = returnUrl.searchParams.get("access_token");
    console.log("Token", googleToken);
  } catch (e) {
    alert(e);
  }
  console.log("token2",googleToken);
  document
    .querySelector("#register-form")
    .addEventListener("submit", handleRegister);
  document
    .querySelector("#registerWithGoogle")
    .addEventListener("click", ()=>trySampleRequest());
});

const handleRegister = async (e) => {
  e.preventDefault();
  const inputData = new FormData(e.target);
  const newUser = {
    customerCity: inputData.get("customerCity"),
    customerCountry: inputData.get("customerCountry"),
    customerSegment: inputData.get("customerSegment"),
    customerStreet: inputData.get("customerStreet"),
    customerState: inputData.get("customerState"),
    customerZipcode: inputData.get("customerZipcode"),
    customerEmail: inputData.get("customerEmail"),
    customerPassword: inputData.get("customerPassword"),
    customerFname: inputData.get("customerFname"),
    customerLname: inputData.get("customerLname"),
    customerId: 123,
  };
  console.log(newUser);
  const resp = await fetch(register_url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    redirect: "follow",
    referrerPolicy: "no-referrer",
    body: JSON.stringify(newUser),
  });
  const data = await resp.json();
  console.log(data);
};

var YOUR_CLIENT_ID =
  "433141860892-7qmra9ujnn35sslqurun4upjapcl2q2p.apps.googleusercontent.com";
var YOUR_REDIRECT_URI = "http://127.0.0.1:5500/frontend/register.html";
var fragmentString = location.hash.substring(1);

// Parse query string to see if page request is coming from OAuth 2.0 server.
var params = {};
var regex = /([^&=]+)=([^&]*)/g,
  m;
while ((m = regex.exec(fragmentString))) {
  params[decodeURIComponent(m[1])] = decodeURIComponent(m[2]);
}
if (Object.keys(params).length > 0) {
  localStorage.setItem("oauth2-test-params", JSON.stringify(params));
  if (params["state"] && params["state"] == "try_sample_request") {
    trySampleRequest();
  }
}

// If there's an access token, try an API request.
// Otherwise, start OAuth 2.0 flow.
function trySampleRequest() {
  var params = JSON.parse(localStorage.getItem("oauth2-test-params"));
  console.log(params['access_token']);
  if (params && params["access_token"]) {
    var xhr = new XMLHttpRequest();
    xhr.open(
      "GET",
      "https://www.googleapis.com/oauth2/v3/userinfo?" +
        "access_token=" +
        params["access_token"]
    );
    xhr.onreadystatechange = function (e) {
      if (xhr.readyState === 4 && xhr.status === 200) {
        console.log(xhr.response);
        serverResponse = JSON.parse(xhr.response);
        console.log(serverResponse);
        if (typeof serverResponse.given_name) document.querySelector("#register-fname").value = serverResponse.given_name;
        if (typeof serverResponse.family_name) document.querySelector("#register-lname").value = serverResponse.family_name;
        if (typeof serverResponse.email) document.querySelector("#register-email").value = serverResponse.email;
      } else if (xhr.readyState === 4 && xhr.status === 401) {
        // Token invalid, so prompt for user permission.
        oauth2SignIn();
      }
    };
    xhr.send(null);
  } else {
    oauth2SignIn();
  }
}

/*
 * Create form to request access token from Google's OAuth 2.0 server.
 */
function oauth2SignIn() {
  // Google's OAuth 2.0 endpoint for requesting an access token
  var oauth2Endpoint = "https://accounts.google.com/o/oauth2/v2/auth";

  // Create element to open OAuth 2.0 endpoint in new window.
  var form = document.createElement("form");
  form.setAttribute("method", "GET"); // Send as a GET request.
  form.setAttribute("action", oauth2Endpoint);

  // Parameters to pass to OAuth 2.0 endpoint.
  var params = {
    client_id: YOUR_CLIENT_ID,
    redirect_uri: YOUR_REDIRECT_URI,
    scope: `profile email`,
    state: "try_sample_request",
    include_granted_scopes: "true",
    response_type: "token",
  };

  // Add form parameters as hidden input values.
  for (var p in params) {
    var input = document.createElement("input");
    input.setAttribute("type", "hidden");
    input.setAttribute("name", p);
    input.setAttribute("value", params[p]);
    form.appendChild(input);
  }

  // Add form to page and submit it to open the OAuth 2.0 endpoint.
  document.body.appendChild(form);
  form.submit();
  const returnUrl = new URL(window.location.href);
  const googleToken = returnUrl.searchParams.get("access_token");
  console.log("Result",googleToken);
  // To get user profile / check for expiration
  // https://www.googleapis.com/oauth2/v3/userinfo?access_token={access_token retrieved from above}
}
