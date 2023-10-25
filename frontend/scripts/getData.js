
document.addEventListener(
  "DOMContentLoaded",
  async function () {
    await getData();
    await getCategories();
    await getSidebarCategories();
  },
  false
);



const openItemDetails = (id) =>{
  window.location.replace(
    `item.html#${id}`,
  );
}

const getData = async () => {
  itemUrl = url + `/products`;
  const resp = await fetch(itemUrl, {
    method: "GET",
    // mode: "cors", // no-cors, *cors, same-origin
    // cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
    // credentials: "same-origin", // include, *same-origin, omit
    headers: {
      "Content-Type": "application/json",
      // 'Content-Type': 'application/x-www-form-urlencoded',
    },
    redirect: "follow", // manual, *follow, error
    referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
    // body: JSON.stringify(data), // body data type must match "Content-Type" header
  });
  const data = await resp.json();
  data.map((item) => {
    document.querySelector(".productList ").insertAdjacentHTML(
      "beforeend",
      `<div class ="items" onClick =openItemDetails(${item.pid})>
    <img src = "../data/Crawled Images/${item.pid}_1.png"
    />
    <span class="name">${item.pName}</span>
    <span class="price">$ ${Math.round(item.price*1000)/1000}</span>
    <span class="button"><img src="icons/addCart.png"/></span>
    </div>`
    );
  });
  console.log(data);
};
;
const getSidebarCategories = async () => {
  categoriesUrl = url + "/category";
  const resp = await fetch(categoriesUrl, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
    redirect: "follow",
    referrerPolicy: "no-referrer",
  });
  const data = await resp.json();
  data.map((cat, key) => {
    document
      .querySelector(".sideBar")
      .insertAdjacentHTML(
        "beforeend",
        `<span class="item"><img class="icon" src ="icons/category_icon/${cat.cid}.png" /><span class="title">${cat.cName}</span></span>`
      );
  });
  console.log(data);
};



