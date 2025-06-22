import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormsModule, NgModel } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-ingredient-modal',
  standalone: true,
  imports: [CommonModule, FormsModule, MatButtonModule],
  template: `
    <div class="ingredient-modal">
      <h2>{{ title }}</h2>

      <form #form="ngForm">
        <label class="input-label">
          Name of ingredient:
          <input
            [(ngModel)]="name"
            name="ingredientName"
            class="styled-input"
            required
            #nameModel="ngModel"
          />
        </label>
        <div class="error" *ngIf="nameModel.invalid && nameModel.touched">
          Name is required.
        </div>

        <div class="actions">
          <button
            mat-raised-button
            class="pizza pizza-orange"
            (click)="save()"
            [disabled]="form.invalid">
            Save
          </button>
          <button
            mat-button
            class="pizza pizza-black"
            (click)="cancel()">
            Cancel
          </button>
        </div>
      </form>
    </div>
  `,
  styles: [`
    .ingredient-modal {
      background-color: #f5f5dc;
      padding: 2rem;
      border-radius: 12px;
      color: #333;
      font-family: 'Roboto Mono', monospace;
    }

    h2 {
      color: #b22222;
      margin-bottom: 1.5rem;
    }

    .input-label {
      display: flex;
      flex-direction: column;
      font-weight: bold;
      margin-bottom: 0.5rem;
    }

    .styled-input {
      padding: 0.5rem;
      font-size: 16px;
      border: 1px solid #ccc;
      border-radius: 6px;
      margin-top: 0.3rem;
    }

    .styled-input:focus {
      outline: none;
      border-color: #b22222;
      box-shadow: 0 0 3px #b22222;
    }

    .error {
      color: #b22222;
      font-size: 13px;
      margin-bottom: 1rem;
    }

    .actions {
      margin-top: 1.5rem;
      display: flex;
      gap: 1rem;
    }

    .pizza-orange {
      background-color: #ff8c00;
      color: white;
      font-weight: bold;
      border-radius: 6px;
    }

    .pizza-orange:hover {
      background-color: #e67e00;
    }

    .pizza-black {
      background-color: #333;
      color: white;
      font-weight: bold;
      border-radius: 6px;
    }

    .pizza-black:hover {
      background-color: #222;
    }
  `]
})
export class IngredientModalComponent {
  name = '';

  get title() {
    return this.data.initialName ? 'Change the name of ingredient' : 'Add ingredient';
  }

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { initialName?: string },
    private ref: MatDialogRef<IngredientModalComponent>
  ) {
    this.name = this.data.initialName ?? '';
  }

  save()  { this.ref.close(this.name.trim()); }
  cancel(){ this.ref.close(); }
}
