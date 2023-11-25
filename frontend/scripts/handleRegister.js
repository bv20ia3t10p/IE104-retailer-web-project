register_url = "https://localhost:7136/api/Customer";


const handleRegister = async (e) => {
  e.preventDefault();
  const inputData = new FormData(e.target);
  const newUser = {
    customerCity: inputData.get("customerCity"),
    customerCountry: inputData.get("customerCountry"),
    customerSegment: inputData.get("customerSegment"),
    customerStreet: inputData.get("customerStreet"),
    customerState: inputData.get("customerState"),
    customerZipcode: inputData.get("customerZipcode"),
    customerEmail: inputData.get("customerEmail"),
    customerPassword: inputData.get("customerPassword"),
    customerFname: inputData.get("customerFname"),
    customerLname: inputData.get("customerLname"),
    customerId: 123,
  };
  console.log(newUser);
  const resp = await fetch(register_url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    redirect: "follow",
    referrerPolicy: "no-referrer",
    body: JSON.stringify(newUser),
  });
  const data = await resp.json();
  console.log(data);
};
