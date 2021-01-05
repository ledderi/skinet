import { v4 as uuidv4 } from 'uuid';

export interface IBasket {
    id: string;
    shippingAndHandling: number;
    deliveryMethodId: number;
    items: IBasketItem[];
}

export interface IBasketItem {
    productId: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}

export class Basket implements IBasket {
    id: string;
    deliveryMethodId: number;
    shippingAndHandling: number;
    items: IBasketItem[];

    constructor() {
        this.id = uuidv4();
        this.items = [];
        this.shippingAndHandling = 0;
    }
}
