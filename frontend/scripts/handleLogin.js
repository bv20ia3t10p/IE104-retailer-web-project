oath_url = "https://localhost:7136/api/Auth";

var YOUR_CLIENT_ID =
  "433141860892-7qmra9ujnn35sslqurun4upjapcl2q2p.apps.googleusercontent.com";
var YOUR_REDIRECT_URI = "http://127.0.0.1:5500/frontend/login.html";

document.addEventListener("DOMContentLoaded", function () {
  document
    .querySelector("#login-form")
    .addEventListener("submit", () => handleLogin);
  document
    .querySelector("#signinWithGoogle")
    .addEventListener("click", () => signInGoogle);
  try {
    var params = JSON.parse(localStorage.getItem("oauth2-test-params"));
    console.log(params["access_token"]);
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
          if (typeof serverResponse.email)
            document.querySelector("#login-email").value = serverResponse.email;
          document.querySelector("#login-password").value =
            serverResponse.email;
        } else if (xhr.readyState === 4 && xhr.status === 401) {
          // Token invalid, so prompt for user permission.
          oauth2SignIn();
        }
      };
      xhr.send(null);
    }
  } catch {
    console.log("No access token");
  }
  document.querySelectorAll(".activatePage").forEach((e) =>
    e.addEventListener("click", () => {
      const loginPage = document.querySelector(".loginOrRegister .login");
      const registerPage = document.querySelector(".loginOrRegister .register");
      loginPage.setAttribute(
        "class",
        loginPage.getAttribute("class") === "login activePage"
          ? "login inactivePage"
          : "login activePage"
      );
      registerPage.setAttribute(
        "class",
        registerPage.getAttribute("class") === "register activePage"
          ? "register inactivePage"
          : "register activePage"
      );
    })
  );
});

const signInGoogle = async () => {
  console.log("clicked");
  trySampleRequest();
  await document.querySelector("#login-form").submit();
};

const handleLogin = async (e) => {
  e.preventDefault();
  inputData = new FormData(e.target);
  const customer = {
    customerEmail: inputData.get("customerEmail"),
    customerPassword: inputData.get("customerPassword"),
  };
  console.log(customer);
  const resp = await fetch(
    oath_url +
      `?GoogleToken=${
        JSON.parse(localStorage.getItem("oauth2-test-params"))["access_token"]
      }`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      redirect: "follow",
      referrerPolicy: "no-referrer",
      body: JSON.stringify(customer),
    }
  );
  const data = await resp.json();
  console.log(data.token);
};

// If there's an access token, try an API request.
// Otherwise, start OAuth 2.0 flow.
function trySampleRequest() {
  var params = JSON.parse(localStorage.getItem("oauth2-test-params"));
  console.log(params["access_token"]);
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
        if (typeof serverResponse.email)
          document.querySelector("#login-email").value = serverResponse.email;
        document.querySelector("#login-password").value = serverResponse.email;
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
  console.log("Result", googleToken);
  // To get user profile / check for expiration
  // https://www.googleapis.com/oauth2/v3/userinfo?access_token={access_token retrieved from above}
}
