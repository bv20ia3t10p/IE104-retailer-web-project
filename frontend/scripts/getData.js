const url = "https://localhost:44390/api";

const getSingleItem = async (id) => {
  itemUrl = url + `/products/${id}`;
  const resp = await fetch(url, {
    method: "GET",
    mode: "cors", // no-cors, *cors, same-origin
    cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
    credentials: "same-origin", // include, *same-origin, omit
    headers: {
      "Content-Type": "application/json",
    },
    redirect: "follow", // manual, *follow, error
    referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
    body: JSON.stringify(id), // body data type must match "Content-Type" header
  });
  const data = await resp.json();
  console.log(data);
};

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
      `<div class ="items">
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

var itemCounter = 0;
var windowStart = 0;
var windowEnd = 12;
var maxItem = 12;

const getCategories = async () => {
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
  // Insert into Navbar
  data.map((cat, key) => {
    document
      .querySelector(".navbar .categories")
      .insertAdjacentHTML(
        "beforeend",
        `<span class="item ${key < maxItem ? "" : "hidden"}">${
          cat.cName
        }</span>`
      );
  });
  //Insert into aside
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

document.addEventListener(
  "DOMContentLoaded",
  async function () {
    await getData();
    await getCategories();
  },
  false
);

//JQUERY APPROACH

// $(document).ready(() => {
//   console.log("Ready event");
//   $.ajax({
//     type:"GET",
//     dataType: "json",
//     crossDomain:true,
//     url: "https://localhost:7136/items",
//     headers: {
//       "Access-Control-Allow-Origin":"*",
//       "Access-Control-Allow-Headers":"*"
//     },
//     // data: {
//     //   //   zipcode: 97201
//     // },
//     success: function (result) {
//       //   $( "#weather-temp" ).html( "<strong>" + result + "</strong> degrees" );
//       console.log(result);
//     },
//       error: function (xhr, status) {
//       alert("error");
//   }
//   });
// });

// $.ajax({
//   url: "http://localhost:8079/students/add/",
//   type: "POST",
//   crossDomain: true,
//   data: JSON.stringify(somejson),
//   dataType: "json",
//   success: function (response) {
//       var resp = JSON.parse(response)
//       alert(resp.status);
//   },
//   error: function (xhr, status) {
//       alert("error");
//   }
// });
