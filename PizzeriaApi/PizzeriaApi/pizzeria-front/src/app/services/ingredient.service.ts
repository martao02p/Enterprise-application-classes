import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ingredient } from '../models/ingredient';

@Injectable({ providedIn: 'root' })
export class IngredientService {
  private api = 'http://localhost:5269/api/Ingredients';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Ingredient[]> {
    return this.http.get<Ingredient[]>(this.api);
  }

  create(ing: Partial<Ingredient>): Observable<Ingredient> {
    return this.http.post<Ingredient>(this.api, ing);
  }

  update(ing: Ingredient): Observable<void> {
    return this.http.put<void>(`${this.api}/${ing.id}`, ing);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
