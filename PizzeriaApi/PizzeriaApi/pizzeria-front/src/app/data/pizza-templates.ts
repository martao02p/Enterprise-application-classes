import { Pizza } from '../models/pizza';


export const PIZZA_TEMPLATES: { name: string, pizza: Pizza }[] = [
  {
    name: 'Margherita',
    pizza: {
      id: 0,
      name: 'Margherita',
      doughType: 'cienkie',
      smallPrice: 18,
      mediumPrice: 26,
      largePrice: 33,
      ingredients: [
        { id: 0, name: 'cheese', isSystem: true },
        { id: 0, name: 'sauce', isSystem: true }
      ]
    }
  },
  {
    name: 'Funghi',
    pizza: {
      id: 0,
      name: 'Funghi',
      doughType: 'grube',
      smallPrice: 20,
      mediumPrice: 28,
      largePrice: 36,
      ingredients: [
        { id: 0, name: 'cheese', isSystem: true },
        { id: 0, name: 'sauce', isSystem: true },
        { id: 0, name: 'mushrooms', isSystem: false }
      ]
    }
  },
  {
    name: 'Pepperoni',
    pizza: {
      id: 0,
      name: 'Pepperoni',
      doughType: 'cienkie',
      smallPrice: 22,
      mediumPrice: 30,
      largePrice: 38,
      ingredients: [
        { id: 0, name: 'cheese', isSystem: true },
        { id: 0, name: 'sauce', isSystem: true },
        { id: 0, name: 'pepperoni', isSystem: false }
      ]
    }
  }
];
