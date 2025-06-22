/* src/app/ingredients/ingredient-list/ingredient-list.component.ts */
import { Component, OnInit } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';

import { IngredientService } from '../../services/ingredient.service';
import { Ingredient } from '../../models/ingredient';
import { IngredientModalComponent } from '../ingredient-modal/ingredient-modal.component';

@Component({
  selector: 'app-ingredient-list',
  standalone: true,
  imports: [CommonModule, NgFor, MatButtonModule],
  templateUrl: './ingredient-list.component.html',
  styleUrls: ['./ingredient-list.component.scss']
})
export class IngredientListComponent implements OnInit {

  systemIngredients:   Ingredient[] = [];
  editableIngredients: Ingredient[] = [];

  loading  = false;
  errorMsg = '';

  constructor(
    private ingService: IngredientService,
    private dialog: MatDialog
  ) {}

  ngOnInit() { this.load(); }


  add(): void {
    const ref = this.dialog.open(IngredientModalComponent, { data: { initialName: '' } });
    ref.afterClosed().subscribe(name => {
      if (name) {
        this.ingService.create({ name, isSystem: false })
          .subscribe(() => this.load());
      }
    });
  }

  rename(ing: Ingredient): void {
    const ref = this.dialog.open(IngredientModalComponent, { data: { initialName: ing.name } });
    ref.afterClosed().subscribe(name => {
      if (name && name !== ing.name) {
        this.ingService.update({ ...ing, name })
          .subscribe(() => this.load());
      }
    });
  }

  delete(ing: Ingredient): void {
    if (!confirm(`Definitely remove the ingredient „${ing.name}”?`)) { return; }

    this.ingService.delete(ing.id!).subscribe({
      next: () => this.load(),
      error: err => {
        this.errorMsg = err.error ??
          '.';
        setTimeout(() => this.errorMsg = 'Cannot remove ingredient (probably used in pizzas)', 4000);
      }
    });
  }

  private load(): void {
    this.loading = true;
    this.ingService.getAll().subscribe({
      next: list => {
        this.systemIngredients   = list.filter(i => i.isSystem);
        this.editableIngredients = list.filter(i => !i.isSystem);
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }
}
