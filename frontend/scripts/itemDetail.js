function displayHash() {
  var id = window.location.hash.slice(1);
  console.log(id);
  getSingleItem(id);
  getCategories(); //defined in getNavbarCategoires.js
}

window.addEventListener("hashchange", function () {
  console.log("hashchange event");
  displayHash();
});

window.addEventListener("DOMContentLoaded", function (ev) {
  try {
    updateBadge(JSON.parse(localStorage.getItem("cart")).length);
  } catch (e) {
    console.log(e);
  }
  console.log("DOMContentLoaded event");
  displayHash();
});

currentViewing = 0;
currentQuantity = 1;

const addToCart = (id, quantity) => {
  try {
    let cart = JSON.parse(localStorage.getItem("cart"));
    console.log(cart);
    let exist = 0;
    cart.forEach((item) => {
      if (item.id === id) {
        exist = 1;
        item.quantity += quantity;
        localStorage.setItem("cart", JSON.stringify(cart));
      }
    });
    if (!exist) {
      let newCart = [...cart, { id, quantity }];
      localStorage.setItem("cart", JSON.stringify(newCart));
      updateBadge(newCart.length);
    }
  } catch (e) {
    localStorage.setItem("cart", JSON.stringify([{ id, quantity }]));
    updateBadge(1);
  }
};

const updateBadge = (badgeNumber) => {
  const badgeClass = document.querySelector(".navbar .action.cart .badge");
  try {
    badgeClass.removeChild(badgeClass.lastChild);
  } catch (e) {
    console.log(e);
  }
  badgeClass.appendChild(document.createTextNode(badgeNumber));
};

const getSingleItem = async (id) => {
  itemUrl = url + `/Products/${id}`;
  const resp = await fetch(itemUrl, {
    method: "GET",
    mode: "cors", // no-cors, *cors, same-origin
    cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
    credentials: "same-origin", // include, *same-origin, omit
    headers: {
      "Content-Type": "application/json",
    },
    redirect: "follow", // manual, *follow, error
    referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
    // body: JSON.stringify({"id":id}), // body data type must match "Content-Type" header
  });
  const data = await resp.json();
  document.querySelector(".itemDetailMain").insertAdjacentHTML(
    `beforeend`,
    `<div class ="itemImages">
      ${Array.from(Array(6).keys())
        .map(
          (val) =>
            `<img class="singleImages ${
              val != currentViewing ? "hidden" : ""
            }" src="../data/Crawled Images/${data.productCardId}_${val}.png"/>`
        )
        .join("")}
    </div>`
  );
  document.querySelector(".itemDetailMain").insertAdjacentHTML(
    "beforeend",
    `
  <div class="ProductInfo">
  <span class="name">${data.productName}</span>
  <span class="depName"><span class="label">Department in charge: </span><span class="data">${
    data.departmentName
  }</span></span>
  <span class ="sold">${data.productSoldQuantity} products sold</span>
  <span class ="price">$${Math.round(data.productPrice * 1000) / 1000}</span>
  </div>
  <div class="AdvancedProductInfo">
  <h1 class="header">Other product information</h1>
  <span class ="id"><span class="label">Product Id: </span><span class="data">${
    data.productCardId
  }</span></span>
  <span class="status"><span class="label">Availability Status: </span><span class="data">${
    data.productStatus ? "Available" : "Not Available (Order for later)"
  }</span></span>
  </div>`
  );
  document.querySelector(".itemDetailMain").insertAdjacentHTML(
    "beforeend",
    `
    <div class ="description">
    <h1 class="header">Product Description</h1>
    <div class="descriptionDetail">${data.productDescription}</div>
    <button class="expand">&#x21E9</button>
    </div>
    <div class="cart">
    <span class="total">${data.productPrice * currentQuantity}</span>
    <span class="quantity">${currentQuantity}</span>
    <button class="add" onClick="addItem(${data.productPrice})">
        Add
      </button>
      <button class="subtract" onClick="subtractItem(${data.productPrice})">
        Subtract
      </button>
      <button class="toCart">
        Add to cart
      </button></div>`
  );
  document
    .querySelector(".description .expand")
    .addEventListener("click", (e) => {
      let node = document.querySelector(".descriptionDetail");
      let currentClass = node.getAttribute("class");
      let expandNode = e.target;
      try {
        while (expandNode.firstChild)
          expandNode.removeChild(expandNode.lastChild);
      } catch (e) {
        console.log(e);
      }
      expandNode.appendChild(
        document.createTextNode(
          currentClass === "descriptionDetail" ? "\u21E7" : "\u21E9"
        )
      );
      node.setAttribute(
        "class",
        currentClass === "descriptionDetail"
          ? "descriptionDetail open"
          : "descriptionDetail"
      );
    });
  document
    .querySelector(".itemDetailMain .toCart")
    .addEventListener("click", () => {
      addToCart(
        document.querySelector("body > main > div.AdvancedProductInfo > span.id > span.data")
          .textContent,
        currentQuantity
      );
    });
  console.log(data);
};

const addItem = (price) => {
  currentQuantity++;
  document.querySelector(".itemDetailMain  .total").textContent =
    price * currentQuantity;
  document.querySelector(".itemDetailMain  .quantity").textContent =
    currentQuantity;
};

const subtractItem = (price) => {
  if (currentQuantity - 1 < 1) return;
  currentQuantity--;
  document.querySelector(".itemDetailMain  .total").textContent =
    price * currentQuantity;
  document.querySelector(".itemDetailMain  .quantity").textContent =
    currentQuantity;
};
