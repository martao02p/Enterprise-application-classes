import { Routes } from '@angular/router';
import { PizzaListComponent } from './pizzas/pizza-list/pizza-list.component';
import { IngredientListComponent } from './ingredients/ingredient-list/ingredient-list.component';

export const routes: Routes = [
  { path: '', redirectTo: 'pizzas', pathMatch: 'full' },
  { path: 'pizzas', component: PizzaListComponent },
  { path: 'ingredients', component: IngredientListComponent },
];
