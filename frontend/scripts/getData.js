$(document).ready(() => {
  console.log("Ready event");
  $.ajax({
    type:"GET",
    dataType: "json",
    crossDomain:true,
    url: "https://localhost:7136/items",
    headers: {
      "Access-Control-Allow-Origin":"*",
      "Access-Control-Allow-Headers":"*"
    },
    // data: {
    //   //   zipcode: 97201
    // },
    success: function (result) {
      //   $( "#weather-temp" ).html( "<strong>" + result + "</strong> degrees" );
      console.log(result);
    },
      error: function (xhr, status) {
      alert("error");
  }
  });
});

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
