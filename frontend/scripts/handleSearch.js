var currentWindow = new URL(window.location.href);
var searchParams = currentWindow.searchParams;
const categorySearch = searchParams.get("category");
var main = document.querySelector(".searchMain");

document.addEventListener("DOMContentLoaded", async() => {
  getCategories();
  if (categorySearch) await getItemsByCategory(categorySearch);
});

const getItemsByCategory = async (id) => {
  const queryUrl = url + `/odata/Products?filter=CategoryId Eq ${id}`;
  fetch(queryUrl)
    .then((e) => (e.ok ? e.json() : e))
    .then((data) => {
      data.value.map((item) => {
        document.querySelector(".searchResult").insertAdjacentHTML(
          "beforeend",
          `<div class ="item" >
            <img onClick =openItemDetails(${
              item.ProductCardId
            }) src = "/Crawled Images/${item.ProductCardId}_1.png"
            />
            <span class="name">${item.ProductName}</span>
            <span class="itemSold">${item.ProductSoldQuantity} sold</span>
            <span class="price">$${
              Math.round(item.ProductPrice * 1000) / 1000
            }</span>
            <span class="button"  onclick=addToCart(${
              item.ProductCardId
            },1)><img src="icons/addCart.png"/></span>
            </div>`
        );
      });
    });
};
