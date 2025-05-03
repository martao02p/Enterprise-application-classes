import React from "react";
import { useState } from "react";
import { useEffect } from "react";

import ProductItem from "./ProductItem";
import axios from "axios";

function ProductList() {
  const [products, setProducts] = useState([]);
  const [filter, setFilter] = useState("");

  //   useEffect(() => {
  //     const initialProducts = [
  //       { id: 1, title: "iPhone 14", brand: "Apple" },
  //       { id: 2, title: "iPad Air", brand: "Apple" },
  //       { id: 3, title: "Galaxy A51", brand: "Samsung" },
  //     ];
  //     setProducts(initialProducts);
  //   }, []);

  useEffect(() => {
    axios
      .get("https://dummyjson.com/products")
      .then((res) => {
        setProducts(res.data.products);
      })
      .catch((err) => {
        console.error("Błąd pobierania produktów", err);
      });
  }, []);

  console.log("ProductList rendered with:", products);
  const filteredProducts = products.filter((product) =>
    product.title.toLowerCase().includes(filter.toLowerCase())
  );
  return (
    <div>
      <h1>List of products</h1>

      <label>
        Filter by product title:{" "}
        <input
          type="text"
          value={filter}
          onChange={(e) => setFilter(e.target.value)}
        />
      </label>

      <ul>
        {/* {products.map((product) => ( */}
        {filteredProducts.map((product) => (
          <ProductItem
            key={product.id}
            id={product.id}
            title={product.title}
            brand={product.brand}
          />
        ))}
      </ul>
    </div>
  );
}

export default ProductList;
