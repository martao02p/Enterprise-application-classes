import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PizzaService } from '../../services/pizza.service';
import { Pizza } from '../../models/pizza';

@Component({
  selector: 'app-pizza-delete-modal',
  standalone: true,
  templateUrl: './pizza-delete-modal.component.html',
  styleUrls: ['./pizza-delete-modal.component.scss'],
})
export class PizzaDeleteModalComponent {
  constructor(
    private dialogRef: MatDialogRef<PizzaDeleteModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { pizza: Pizza },
    private pizzaService: PizzaService
  ) {}

  confirm(): void {
    this.dialogRef.close(true);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
