import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

import { Pizza } from '../../models/pizza';
import { Ingredient } from '../../models/ingredient';
import { IngredientService } from '../../services/ingredient.service';
import { PIZZA_TEMPLATES } from '../../data/pizza-templates';

@Component({
  selector: 'app-pizza-create-modal',
  standalone: true,
  templateUrl: './pizza-create-modal.component.html',
  styleUrls: ['./pizza-create-modal.component.scss'],
  imports: [CommonModule, FormsModule, MatButtonModule]
})
export class PizzaCreateModalComponent implements OnInit {
  pizza: Pizza = this.getEmptyPizza();
  templates = PIZZA_TEMPLATES;
  selectedTemplateName: string | null = 'Custom';

  allIngredients: Ingredient[] = [];
  systemIngredients: Ingredient[] = [];
  editableIngredients: Ingredient[] = [];

  constructor(
    public dialogRef: MatDialogRef<PizzaCreateModalComponent>,
    private ingredientService: IngredientService
  ) {}


  ngOnInit(): void {
    this.loadIngredients();
  }


  private loadIngredients(): void {
    this.ingredientService.getAll().subscribe(ings => {
      this.allIngredients    = ings;
      this.systemIngredients = ings.filter(i => i.isSystem);
      this.editableIngredients = ings.filter(i => !i.isSystem);

      if (this.selectedTemplateName === 'Custom') {
        this.ensureSystemIngredients();
      }
    });
  }


  selectTemplate(name: string, tmplPizza: Pizza): void {
    this.selectedTemplateName = name;
    this.pizza = { ...tmplPizza, ingredients: [...tmplPizza.ingredients] };
    this.synchroniseTemplateIngredients();
  }

  clearTemplate(): void {
    this.selectedTemplateName = 'Custom';
    this.pizza = this.getEmptyPizza();
    this.ensureSystemIngredients();
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
    this.dialogRef.close({
      ...this.pizza,
      ingredients: this.pizza.ingredients.map(i => ({ id: i.id!, name: i.name }))
    });
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

  private synchroniseTemplateIngredients(): void {
    const ops = this.pizza.ingredients.map(tmplIng => {
      const match = this.allIngredients.find(i => i.name.toLowerCase() === tmplIng.name.toLowerCase());
      if (match) {
        return Promise.resolve(match);
      }
      return this.ingredientService.create({
        name: tmplIng.name,
        isSystem: tmplIng.isSystem
      }).toPromise();
    });

    Promise.all(ops).then(createdOrMatched => {
      this.loadIngredients();
      this.pizza.ingredients = createdOrMatched as Ingredient[];
    });
  }

  private getEmptyPizza(): Pizza {
    return {
      name: '',
      doughType: '',
      smallPrice: 0,
      mediumPrice: 0,
      largePrice: 0,
      ingredients: []
    };
  }
}
