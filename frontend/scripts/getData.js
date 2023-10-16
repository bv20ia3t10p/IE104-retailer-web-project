const url = "https://localhost:7136";

const getSingleItem = async (id) => {
  itemUrl = url + `/items/${id}`;
  const resp = await fetch(url, {
    method: "GET",
    mode: "cors", // no-cors, *cors, same-origin
    cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
    credentials: "same-origin", // include, *same-origin, omit
    headers: {
      "Content-Type": "application/json",
      // 'Content-Type': 'application/x-www-form-urlencoded',
    },
    redirect: "follow", // manual, *follow, error
    referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
    body: JSON.stringify(id), // body data type must match "Content-Type" header
  });
  const data = await resp.json();
  console.log(data);
};

const getData = async () => {
  itemUrl = url + `/items`;
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
    document.querySelector(".itemsList").insertAdjacentHTML('beforeend',`<div class ="items"><a href="">${item.pName}</a>
    <img src = "../data/Crawled Images/${item.pid}_1.png"/>
    <h3>$ ${item.price}</h3>
    </div>`);
  });
  console.log(data);
};

document.addEventListener(
  "DOMContentLoaded",
  async function () {
    await getData();
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
