import React from 'react';
import { useParams, Link } from 'react-router-dom';

function ProductDetails({ products }) {
  // 1. Pobierz parametr id ze ścieżki
  const { id } = useParams();

  // 2. Odfiltruj produkt o podanym id
  const filtered = products.filter((p) => p.id === Number(id));

  // 3. Jeśli nie znaleziono produktu, zwróć null (lub jakiś komunikat)
  if (filtered.length === 0) {
    return <p>Product not found.</p>;
  }

  // 4. Mamy dokładnie 1 produkt – weź go
  const product = filtered[0];

  // 5. Zwróć widok ze szczegółami
  return (
    <div>
      <h1>{product.title}</h1>
      <p>Category: {product.category}</p>
      <p>Brand: {product.brand}</p>
      <p>Description: {product.description}</p>
      <p>Price: {product.price}</p>
      <br/>
      <img src={product.thumbnail} alt={product.title} />
      <br/><br/>
      <Link to="/">Back to product list</Link>
    </div>
  );
}

export default ProductDetails;
