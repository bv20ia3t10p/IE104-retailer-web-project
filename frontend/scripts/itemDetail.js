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
  console.log("DOMContentLoaded event");
  displayHash();
});

currentViewing = 0;
currentQuantity = 1;

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
  <span class="depName">${data.departmentName}</span>
  <span class ="price">${data.productPrice}</span>
  <span class ="id">${data.productCardId}</span>
  <span class="status">${data.productStatus}</span>
  </div>`
  );
  document.querySelector(".itemDetailMain").insertAdjacentHTML(
    "beforeend",
    `
    <div class ="description">${data.productDescription}</div>
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
