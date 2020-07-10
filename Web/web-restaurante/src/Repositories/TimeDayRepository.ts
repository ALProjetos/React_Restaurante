import { ITimeDayModel } from "../Model/TimeDayModel";
import client from "../Request/Request";

class TimeDayRepository{

    public async GetAll(): Promise<ITimeDayModel[]>{
        var result = null;
        
        try{
            result = client.Get( "TimeDays" );
        }
        catch(ex){
            console.error(`Error to request get all timeDays ${ex}`);
        }

        return result;
    }
}

const timeDayRepository = new TimeDayRepository();
export default timeDayRepository;