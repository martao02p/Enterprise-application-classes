import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Pizza } from '../models/pizza';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PizzaService {
  private apiUrl = 'http://localhost:5269/api/Pizzas';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Pizza[]> {
    return this.http.get<Pizza[]>(this.apiUrl);
  }

  get(id: number): Observable<Pizza> {
    return this.http.get<Pizza>(`${this.apiUrl}/${id}`);
  }

  create(pizza: Pizza): Observable<Pizza> {
    return this.http.post<Pizza>(this.apiUrl, pizza);
  }

  update(pizza: Pizza): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${pizza.id}`, pizza);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  
}
