import { IAddress } from "./address";

export interface IOrderToCreate {
    basketId: string;
    deliveryMethodId: number;
    Address: IAddress;
}

export interface IOrder {
    id: number;
    buyerEmail: string;
    orderDate: string;
    Address: IAddress;
    deliveryMethod: string;
    deliveryCost: number;
    items: IOrderItem[];
    subtotal: number;
    status: string;
    total: number;
  }

  export interface IOrderItem {
    productId: number;
    productName: string;
    pictureUrl: string;
    price: number;
    quantity: number;
  }
