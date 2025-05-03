import "./App.css";
import ProductList from "./ProductList";
import React, { useState, useEffect, useMemo } from "react";
import axios from "axios";

// import { BrowserRouter, Routes, Route } from "react-router-dom";
import ProductDetails from "./ProductDetails";
import { createBrowserRouter, RouterProvider } from "react-router-dom";

function App() {
  // const [products, setProducts] = useState([]);

  // useEffect(() => {
  //   axios
  //     .get("https://dummyjson.com/products")
  //     .then((res) => {
  //       setProducts(res.data.products);
  //     })
  //     .catch((err) => console.error("Błąd pobierania produktów", err));
  // }, []);

  // useEffect(() => {
  //   axios
  //     .get("https://dummyjson.com/products")
  //     .then((res) => {
  //       console.log("before set ", res);
  //       setProducts(res.data.products);
  //       console.log(products);
  //     })
  //     .catch((err) => {
  //       console.error("Błąd pobierania produktów", err);
  //     });
  // }, []);

  const router = createBrowserRouter([
    {
      path: "/",
      element: <ProductList />
    },
    {
      path: "/details/:id",
      element: <ProductDetails />
    }
  ]);

  return (
    // <div className="App">
    //   <ProductList products={products} />
    // </div>

    // ----------------------
    // <BrowserRouter>
    //   <Routes>
    //     <Route path="/" element={<ProductList products={products} />} />
    //     <Route
    //       path="details/:id"
    //       element={<ProductDetails products={products} />}
    //     />
    //   </Routes>
    // </BrowserRouter>
    // ----------------------
    <RouterProvider router={router} />
  );
}

export default App;
