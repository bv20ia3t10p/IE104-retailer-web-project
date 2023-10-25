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
      ${[0, 1, 2, 3, 4, 5, 6].map((val) => (
        `<img src="../data/Crawled Images/${data.pid}_${val}.png"/>`
      ))}
      <span class="name">${data.pName}</span>
      <span class="depName">${data.dName}</span>
      <span class ="price">${data.price}</span>
      <span class ="id">${data.pid}</span>
      <span class="status">${data.available}</span>
    `
  );
  console.log(data);
};
