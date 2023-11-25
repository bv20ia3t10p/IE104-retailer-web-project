oath_url = "https://localhost:7136/api/Auth";

var YOUR_CLIENT_ID =
  "433141860892-7qmra9ujnn35sslqurun4upjapcl2q2p.apps.googleusercontent.com";
var YOUR_REDIRECT_URI = "http://127.0.0.1:5500/frontend/loginOrRegister.html";

document.addEventListener("DOMContentLoaded", function () {
  const currentWindow = new URL(
    window.location.href.replace("#state=", "?state=")
  );
  try {
    localStorage.setItem(
      "googleToken",
      currentWindow.searchParams.get("access_token")
    );
    alert(currentWindow.searchParams);
  } catch {
    alert("No access token");
  }
  document
    .querySelector("#register-form")
    .addEventListener("submit", handleRegister);
  document
    .querySelector("#registerWithGoogle")
    .addEventListener("click", trySampleRequest);
  document.querySelector("#login-form").addEventListener("submit", handleLogin);
  document
    .querySelector("#signinWithGoogle")
    .addEventListener("click", signInGoogle);
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
  trySampleRequest();
  // await document.querySelector("#login-form").submit();
};

const handleLogin = async (e) => {
  e.preventDefault();
  inputData = new FormData(e.target);
  const customer = {
    customerEmail: inputData.get("customerEmail"),
    customerPassword: inputData.get("customerPassword"),
  };
  console.log(customer);
  let ggToken = "0";
  try {
    ggToken = JSON.parse(localStorage.getItem("googleToken"));
  } catch {
    console.log("No google auth");
  }
  const resp = await fetch(oath_url + `?GoogleToken=${ggToken}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    redirect: "follow",
    referrerPolicy: "no-referrer",
    body: JSON.stringify(customer),
  });
  const data = await resp.json();
  console.log(data.token);
};

const trySampleRequest = async () => {
  var ggToken = "";
  try {
    ggToken =
      localStorage.getItem("googleToken") === "null"
        ? "0"
        : localStorage.getItem("googleToken");
    alert(ggToken);
    if (ggToken === "0" || ggToken == "null") throw new Error("No token");
    const requestUrl =
      "https://www.googleapis.com/oauth2/v3/userinfo?" +
      "access_token=" +
      ggToken;
    console.log(requestUrl);
    const resp = await fetch(requestUrl);
    const serverResponse = await resp.json();
    console.log(serverResponse);
    if (typeof serverResponse.email)
      document.querySelector("#login-email").value = serverResponse.email;
    document.querySelector("#login-password").value = serverResponse.email;
    if (typeof serverResponse.given_name)
      document.querySelector("#register-fname").value =
        serverResponse.given_name;
    if (typeof serverResponse.family_name)
      document.querySelector("#register-lname").value =
        serverResponse.family_name;
    if (typeof serverResponse.email)
      document.querySelector("#register-email").value = serverResponse.email;
  } catch (e) {
    oauth2SignIn();
  }
};

/*
 * Create form to request access token from Google's OAuth 2.0 server.
 */
function oauth2SignIn() {
  alert("sending oauth2");
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
    state: "pass-through value",
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
}
