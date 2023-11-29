var cart = [];
var productsFromCart = [];
var fetchedProducts = [];
var customerInfo = {};

window.addEventListener("DOMContentLoaded", function (ev) {
  try {
    cart = JSON.parse(localStorage.getItem("cart"));
    updateBadge(cart.length);
  } catch (e) {
    console.log(e);
  }
  console.log("DOMContentLoaded event");
  getCategories();
  getItemsFromCart();
  getItemRecommendation(cart.map((e) => e.id));
  this.document
    .querySelector("#cartAllItemChk")
    .addEventListener("change", (e) => {
      this.document
        .querySelectorAll(".cartDetails .single .itemCheck")
        .forEach((t) => (t.checked = e.target.checked));
      checkChanged();
    });
  accountInfoLoad();
});

const modifyQuantity = (id, quantity, replace = false) => {
  console.log("Modify params: " + id + " " + quantity);
  cart = cart.map((e) => {
    if (e.id === Number(id)) {
      console.log("Found");
      e.quantity = replace ? Number(quantity) : e.quantity + Number(quantity);
      if (e.quantity <= 0) {
        removeItemFromCart(id);
        return;
      }
    }
    return e;
  });
  if (!replace) updateQuantity(id);
  localStorage.setItem("cart", JSON.stringify(cart));
  updateItemTotal();
};

const updateQuantity = (id) => {
  const containerNode = document.querySelector(
    `.cartDetails .single[productid="${id}"]`
  );
  containerNode.querySelector(".quantityControl .value").value = cart.filter(
    (e) => e.id === id
  )[0].quantity;
};

const removeItemFromCart = (id) => {
  const node = document.querySelector(
    `.cartDetails .single[productid="${id}"]`
  );
  node.parentElement.removeChild(node);
  cart = cart.filter((e) => e.id !== id);
  console.log(cart);
  localStorage.setItem("cart", JSON.stringify(cart));
  updateItemTotal();
};

const updateItemTotal = () => {
  let sum = 0;
  let itemCount = 0;
  try {
    document.querySelectorAll(".cartDetails .single").forEach((e) => {
      let price = e.getAttribute("price");
      let quantity = e.querySelector(".quantityControl .value").value;
      if (e.querySelector(".description .itemCheck").checked) {
        sum += price * quantity;
        itemCount += 1;
      }
      let total = Math.round(price * quantity * 1000) / 1000;
      const itemTotalNode = e.querySelector(".total");
      itemTotalNode.removeChild(itemTotalNode.lastChild);
      itemTotalNode.appendChild(document.createTextNode(total));
    });
    const orderTotalNode = document.querySelector(".orderTotal .final .data");
    orderTotalNode.removeChild(orderTotalNode.childNodes[0]);
    orderTotalNode.appendChild(
      document.createTextNode(Math.round(sum * 1000) / 1000)
    );
    if (itemCount === cart.length) {
      const selectLabel = document.querySelector(
        '.columns label[for="cartAllItemChk"]'
      );
      selectLabel.removeChild(selectLabel.childNodes[0]);
      selectLabel.append(document.createTextNode(`Select all products`));
    }
  } catch (error) {
    console.log(error);
  }
};
const checkChanged = () => {
  const itemCheckBoxes = Array.from(
    document.querySelectorAll(".cartDetails .single .itemCheck")
  );
  const checkedCount = itemCheckBoxes.filter((e) => e.checked).length;
  if (checkedCount === cart.length) {
    document.querySelector("#cartAllItemChk").checked = true;
  }
  console.log(checkedCount);
  const purchaseNode = document.querySelector(".cartMain .purchase");
  purchaseNode.removeChild(purchaseNode.childNodes[0]);
  purchaseNode.appendChild(
    document.createTextNode(`Purchase (${checkedCount})`)
  );
  updateItemTotal();
};

const getItemsFromCart = async () => {
  if (!cart.length) return;
  const requestBody = cart.map((e) => e.id);
  console.log(requestBody);
  const getItemsFromCartUrl = url + "/api/Products/Multiple";
  const resp = await fetch(getItemsFromCartUrl, {
    method: "POST",
    body: JSON.stringify(requestBody),
    headers: {
      "Content-Type": "application/json",
    },
    redirect: "follow", // manual, *follow, error
    referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
  });
  fetchedProducts = await resp.json();
  console.log(fetchedProducts);
  document.querySelector(".cartMain .cartDetails").insertAdjacentHTML(
    "beforeend",
    fetchedProducts
      .map(
        (e, index) => `<div class ="single" productid=${
          e.productCardId
        } price=${e.productPrice}>
      <div class="description">
        <input type="checkbox" class="itemCheck"/>
        <img src = "Crawled Images/${e.productCardId}_0.png" class="itemImg"/>
        <p class ="productName">${e.productName}</p>
      </div>
      <div class="price">
        ${e.productPrice}
      </div>
      <div class="quantityControl">
        <button class="subtract" onclick=modifyQuantity(${
          e.productCardId
        },-1)>-</button>
        <input type="number" class="value" value="${cart[index].quantity}"/>
        <button class="add" onclick=modifyQuantity(${
          e.productCardId
        },1)>+</button>
      </div>
      <div class="total">
        ${Math.round(e.productPrice * cart[index].quantity * 1000) / 1000}
      </div>
      <button class="delete" onclick=removeItemFromCart(${
        e.productCardId
      })></button>
    </div>`
      )
      .join("")
  );
  const selectLabel = document.querySelector(
    '.columns label[for="cartAllItemChk"]'
  );
  selectLabel.removeChild(selectLabel.childNodes[0]);
  selectLabel.append(
    document.createTextNode(`Select all products (${cart.length} products)`)
  );
  document
    .querySelectorAll(".cartDetails .single .quantityControl .value")
    .forEach((e) =>
      e.addEventListener("change", (t) => {
        const containerNode = t.target.parentNode.parentNode;
        let id = containerNode.getAttribute("productid");
        modifyQuantity(id, t.target.value, true);
      })
    );
  document
    .querySelectorAll(".cartDetails .single .itemCheck")
    .forEach((e) => e.addEventListener("change", checkChanged));
  updateItemTotal();
};

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
  recItemUrl = flask_url + "/mlApi/ProductRec";
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
  document.querySelector(".recommendations").insertAdjacentHTML(
    "beforeend",
    `
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
      .join("")}    `
  );
};

const accountInfoLoad = async () => {
  let accountToken = "";
  try {
    accountToken = localStorage.getItem("accountToken");
    if (accountToken.length < 1) throw new Error("Not logged in");
  } catch (e) {
    return;
  }
  const accoutnInfoUrl = url + "/api/Customer/Email";
  const resp = await fetch(accoutnInfoUrl, {
    method: "GET",
    headers: {
      Authorization: "Bearer " + accountToken,
    },
    redirect: "follow", // manual, *follow, error
    referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
  });
  const accountData = await resp.json();
  console.log(accountData);
  customerInfo = accountData;
  removeAndReplaceNodeText(
    document.querySelector(".delivery .advanced .country .data"),
    customerInfo.customerCountry
  );
  removeAndReplaceNodeText(
    document.querySelector(".delivery .advanced .city .data"),
    customerInfo.customerCity
  );
  removeAndReplaceNodeText(
    document.querySelector(".delivery .advanced .state .data"),
    customerInfo.customerState
  );
  removeAndReplaceNodeText(
    document.querySelector(".delivery .advanced .zip .data"),
    customerInfo.customerZipcode
  );
  removeAndReplaceNodeText(
    document.querySelector(".delivery .advanced .street .data"),
    customerInfo.customerStreet
  );
  removeAndReplaceNodeText(
    document.querySelector(".delivery .basic .name"),
    customerInfo.customerFname + " " + customerInfo.customerLname
  );
  removeAndReplaceNodeText(
    document.querySelector(".delivery .basic .email"),
    customerInfo.customerEmail
  );
};

const createOrder = () => {
  var date = new Date();
  var day = date.getDate(); // yields date
  var month = date.getMonth() + 1; // yields month (add one as '.getMonth()' is zero indexed)
  var year = date.getFullYear(); // yields year
  var hour = date.getHours(); // yields hours
  var minute = date.getMinutes(); // yields minutes
  var second = date.getSeconds(); // yields seconds
  var time = day + "/" + month + "/" + year + " " + hour + ':' + minute + ':' + second; 
};

const removeAndReplaceNodeText = (node, text) => {
  node.removeChild(node.childNodes[0]);
  node.appendChild(document.createTextNode(text));
};
