import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

import { Pizza } from '../../models/pizza';
import { Ingredient } from '../../models/ingredient';
import { IngredientService } from '../../services/ingredient.service';

@Component({
  selector: 'app-pizza-edit-modal',
  standalone: true,
  templateUrl: './pizza-edit-modal.component.html',
  styleUrls: ['./pizza-edit-modal.component.scss'],
  imports: [CommonModule, FormsModule, MatButtonModule]
})
export class PizzaEditModalComponent implements OnInit {
  pizza: Pizza;

  allIngredients: Ingredient[] = [];
  systemIngredients: Ingredient[] = [];
  editableIngredients: Ingredient[] = [];

  constructor(
    public dialogRef: MatDialogRef<PizzaEditModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { pizza: Pizza },
    private ingredientService: IngredientService
  ) {
    // kopiujemy, żeby nie modyfikować obiektu z listy
    this.pizza = { ...data.pizza, ingredients: [...data.pizza.ingredients] };
  }

  /* ───────────────────────────── lifecycle ─────────────────────────── */
  ngOnInit(): void {
    this.ingredientService.getAll().subscribe(ings => {
      this.allIngredients    = ings;
      this.systemIngredients = ings.filter(i => i.isSystem);
      this.editableIngredients = ings.filter(i => !i.isSystem);

      // upewnij się, że ser i sos są w edytowanej pizzy
      this.ensureSystemIngredients();
    });
  }

  toggleIngredient(ing: Ingredient): void {
    const pos = this.pizza.ingredients.findIndex(i => i.id === ing.id);
    pos > -1 ? this.pizza.ingredients.splice(pos, 1)
             : this.pizza.ingredients.push(ing);
  }

  isChecked(ing: Ingredient): boolean {
    return this.pizza.ingredients.some(i => i.id === ing.id);
  }

  save(): void {
    this.ensureSystemIngredients();

  const payload: Pizza = {
    ...this.pizza,
    ingredients: this.pizza.ingredients.map(i => ({
      id:   i.id!,
      name: i.name,
      isSystem: i.isSystem
    }))
  };


    this.dialogRef.close(payload);
  }

  cancel(): void {
    this.dialogRef.close();
  }

  private ensureSystemIngredients(): void {
    for (const sys of this.systemIngredients) {
      if (!this.pizza.ingredients.some(i => i.id === sys.id)) {
        this.pizza.ingredients.push(sys);
      }
    }
  }
}
