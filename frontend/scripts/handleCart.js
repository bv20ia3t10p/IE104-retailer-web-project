window.addEventListener("DOMContentLoaded", function (ev) {
  try {
    updateBadge(JSON.parse(localStorage.getItem("cart")).length);
  } catch (e) {
    console.log(e);
  }
  console.log("DOMContentLoaded event");
  getCategories();
});

const updateBadge = (badgeNumber) => {
  const badgeClass = document.querySelector(".navbar .action.cart .badge");
  try {
    badgeClass.removeChild(badgeClass.lastChild);
  } catch (e) {
    // console.log(e);
  }
  badgeClass.appendChild(document.createTextNode(badgeNumber));
};
