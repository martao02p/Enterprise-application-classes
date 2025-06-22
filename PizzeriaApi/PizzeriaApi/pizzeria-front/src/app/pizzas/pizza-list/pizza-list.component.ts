import { Component } from '@angular/core';
import { PizzaService } from '../../services/pizza.service';
import { Pizza } from '../../models/pizza';
import { NgFor, NgIf, NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { PizzaEditModalComponent } from '../pizza-edit-modal/pizza-edit-modal.component';
import { PizzaDeleteModalComponent } from '../pizza-delete-modal/pizza-delete-modal.component';
import { PizzaCreateModalComponent } from '../pizza-create-modal/pizza-create-modal.component';

@Component({
  selector: 'app-pizza-list',
  standalone: true,
  imports: [
    NgFor,
    NgIf,
    NgClass,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './pizza-list.component.html',
  styleUrls: ['./pizza-list.component.scss']
})
export class PizzaListComponent {
  pizzas: Pizza[] = [];
  filter = '';

  constructor(
    private pizzaService: PizzaService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadPizzas();
  }

  loadPizzas(): void {
    this.pizzaService.getAll().subscribe(data => this.pizzas = data);
  }

  filteredPizzas(): Pizza[] {
    return this.pizzas.filter(p =>
      p.name.toLowerCase().includes(this.filter.toLowerCase())
    );
  }

  openEditModal(pizza: Pizza): void {
    const dialogRef = this.dialog.open(PizzaEditModalComponent, {
      data: { pizza }
    });

    dialogRef.afterClosed().subscribe((updatedPizza: Pizza | undefined) => {
      if (updatedPizza) {
        this.pizzaService.update(updatedPizza).subscribe(() => {
          this.loadPizzas();
        });
      }
    });
  }

  openDeleteModal(pizza: Pizza): void {
    const dialogRef = this.dialog.open(PizzaDeleteModalComponent, {
      data: { pizza }
    });
    console.log('Deleting pizza with ID:', pizza.id);

    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.pizzaService.delete(pizza.id!).subscribe(() => {
          this.loadPizzas();
        });
      }
    });
  }

  openCreateModal(): void {
    const dialogRef = this.dialog.open(PizzaCreateModalComponent);

    dialogRef.afterClosed().subscribe((newPizza: Pizza) => {
      if (newPizza) {
        this.pizzaService.create(newPizza).subscribe(() => {
          this.loadPizzas();
        });
      }
    });
  }

  expandedPizzaIds = new Set<number>();

  toggleDetails(pizzaId: number): void {
    if (this.expandedPizzaIds.has(pizzaId)) {
      this.expandedPizzaIds.delete(pizzaId);
    } else {
      this.expandedPizzaIds.add(pizzaId);
    }
  }

  isExpanded(pizzaId: number): boolean {
    return this.expandedPizzaIds.has(pizzaId);
  }

  isLast(array: any[], item: any): boolean {
    return array.indexOf(item) === array.length - 1;
  }

}
