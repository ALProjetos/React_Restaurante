import { IDishModel } from "./DishModel";

export interface IDishHistoricModel{
    date: Date,
    dishes: IDishModel[]
}