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
  document.querySelector(".mainItem").insertAdjacentHTML(
    `beforeend`,
    `
      ${Array.from(Array(6).keys())
        .map(
          (val) =>
            `<img class="${
              val != currentViewing ? "hidden" : ""
            }" src="../data/Crawled Images/${data.pid}_${val}.png"/>`
        )
        .join("")}
    `
  );
  document.querySelector("main .itemDetails").insertAdjacentHTML(
    "beforeend",
    `      <span class="name">${data.pName}</span>
  <span class="depName">${data.dName}</span>
  <span class ="price">${data.price}</span>
  <span class ="id">${data.pid}</span>
  <span class="status">${data.available}</span>`
  );
  document.querySelector("main .purchase").insertAdjacentHTML(
    "beforeend",
    `<span class="total">${data.price*currentQuantity}</span>
    <span class="quantity">${currentQuantity}</span>
    <button class="add" onClick="addItem(${data.price})">
        Add
      </button>
      <button class="subtract" onClick="subtractItem(${data.price})">
        Subtract
      </button>
      <button class="toCart">
        Add to cart
      </button>`
  );
  console.log(data);
};


const addItem = (price) => {
  currentQuantity++;
  document.querySelector("main .purchase .total").textContent = price*currentQuantity;
  document.querySelector("main .purchase .quantity").textContent = currentQuantity;

}

const subtractItem = (price) => {
  if (currentQuantity - 1 < 1 ) return;
  currentQuantity--;
  document.querySelector("main .purchase .total").textContent = price*currentQuantity;
  document.querySelector("main .purchase .quantity").textContent = currentQuantity;
}
