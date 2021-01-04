import { IAddress } from './address';

export interface IOrderToApi {
    basketId: string;
    deliveryMethodId: number;
    DeliveryAddress: IAddress;
}

export interface IOrderFromApi {
    id: number;
    orderDate: Date;
    status: string;
    deliveryMethod: string;
    deliveryMethodId: number;
    shippingPrice: number;
    buyerEmail: string;
    subTotal: number;
    total: number;
    deliveryAddress: IAddress;
    orderItems: IOrderItem[];
}

export interface IOrderItem {
    productId: number;
    productName: string;
    pictureUrl: string;
    price: number;
    quantity: number;
}
