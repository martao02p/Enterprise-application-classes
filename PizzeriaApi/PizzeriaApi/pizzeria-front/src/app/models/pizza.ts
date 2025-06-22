import { Ingredient } from './ingredient';

export interface Pizza {
  id?: number;
  name: string;
  doughType: string;
  smallPrice: number;
  mediumPrice: number;
  largePrice: number;
  ingredients: Ingredient[];
}
