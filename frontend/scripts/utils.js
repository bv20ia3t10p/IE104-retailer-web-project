const url = "https://ecommercebackend20231127233624.azurewebsites.net";
const flask_url = "https://itemrecforwobblestore.azurewebsites.net";

const showLoadingPopup = (visibility, main, title = "") => {
  const navbar = document.querySelector("navbar");
  const currentClass = main.getAttribute("class");
  const loaderElement = document.querySelector(".universalLoader");
  switch (visibility) {
    case true:
      main.setAttribute("class", currentClass + " dimmed");
      const titleElement = document.querySelector(".universalLoader .title");
      removeAndReplaceNodeText(titleElement, title);
      loaderElement.setAttribute("class", "universalLoader");
      navbar.setAttribute("class", "navbar dimmed");
      break;
    default:
      main.setAttribute("class", currentClass.replace(" dimmed", ""));
      loaderElement.setAttribute("class", "universalLoader finished");
      navbar.setAttribute("class", "navbar");
  }
};

const addToCart = (id, quantity) => {
  try {
    id = Number(id);
    let cart = JSON.parse(localStorage.getItem("cart"));
    console.log(cart);
    let exist = 0;
    cart.forEach((item) => {
      if (item.id === id) {
        exist = 1;
        item.quantity += 1;
        localStorage.setItem("cart", JSON.stringify(cart));
      }
    });
    if (!exist) {
      let newCart = [...cart, { id, quantity, checked: false }];
      localStorage.setItem("cart", JSON.stringify(newCart));
      updateBadge(newCart.length);
    }
  } catch (e) {
    localStorage.setItem("cart", JSON.stringify([{ id, quantity }]));
    updateBadge(1);
  }
};

const openItemDetails = (id) => {
  window.location.replace(`item.html#${id}`);
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

const removeAndReplaceNodeText = (node, text) => {
  node.removeChild(node.childNodes[0]);
  node.appendChild(document.createTextNode(text));
};
