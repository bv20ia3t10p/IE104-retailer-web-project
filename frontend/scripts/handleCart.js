var cart = [];
var productsFromCart = [];


window.addEventListener("DOMContentLoaded", function (ev) {
  try {
    cart = JSON.parse(localStorage.getItem("cart"));
    updateBadge(cart.length);
  } catch (e) {
    console.log(e);
  }
  console.log("DOMContentLoaded event");
  getCategories();
});

const getItemsFromCart = async () => {
  
}

const updateBadge = (badgeNumber) => {
  const badgeClass = document.querySelector(".navbar .action.cart .badge");
  try {
    badgeClass.removeChild(badgeClass.lastChild);
  } catch (e) {
    // console.log(e);
  }
  badgeClass.appendChild(document.createTextNode(badgeNumber));
};

const getItemRecommendation = async (ids) => {
  recItemUrl = flask_url + "/mlApi/ProductRec/";
  const resp = await fetch(recItemUrl, {
    method: "POST",
    body: JSON.stringify(ids),
    headers: {
      "Content-Type": "application/json",
    },
    redirect: "follow", // manual, *follow, error
    referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
  });
  const data = await resp.json();
  document.querySelector(".itemDetailMain").insertAdjacentHTML(
    "beforeend",
    `<div class="relevantItems">
    <h1 class="header">You might be interested in</h1>
    ${data
      .map((item, no) => {
        if (no < 20)
          return `
        <div class ="items" >
    <img onClick =openItemDetails(${
      item.productCardId
    }) src = "/Crawled Images/${item.productCardId}_1.png"
    />
    <span class="name">${item.productName}</span>
    <span class="itemSold">${item.productSoldQuantity} sold</span>
    <span class="price">$ ${Math.round(item.productPrice * 1000) / 1000}</span>
    <span class="button"  onclick=addToCart(${
      item.productCardId
    },1)><img src="icons/addCart.png"/></span>
    </div>`;
      })
      .join("")}
    </div>
    `
  );
};
