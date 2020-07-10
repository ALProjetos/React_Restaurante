import { IDishModel } from "../Model/DishModel";
import client from "../Request/Request";
import { IDishHistoricModel } from "../Model/DishHistoricModel";

export interface IDishRepositoryRequest{
    dishes: number[]
}

export interface IDishRepositoryResponse{
    result?: string
}

class DishesRespository{

    public async GetAllByTimeDayId( timeDayId: number ): Promise<IDishModel[]>{
        var result = null;
        
        try{
            result = await client.Get( `DishesType/${timeDayId}` );
        }
        catch(ex){
            console.error(`Error to request get by timeDayId ${ex}`);
        }

        return result;
    }

    public async PostOrder( timeDayId: number, dishes: number[] ): Promise<IDishRepositoryResponse>{
        var resp: IDishRepositoryResponse = { result: "" };

        var dishRequest: IDishRepositoryRequest = {
            dishes: dishes
        }

        try{
            resp = await client.Post( `DishesType/${timeDayId}`, dishRequest );
        }
        catch(ex){
            console.error(`Error to post new order ${ex}`);
        }

        return resp;
    }

    public async GetHistoricTimeDayId( timeDayId: number ): Promise<IDishHistoricModel[]>{
        var result = null;
        
        try{
            result = await client.Get( `DishesType/${timeDayId}/Historic` );
        }
        catch(ex){
            console.error(`Error to find historic by timeDayId ${ex}`);
        }

        return result;
    }
}

const dishesRespository = new DishesRespository();
export default dishesRespository;