import axios from "axios";

class Request{
    private m_UrlBase = "http://localhost:5000/api/";

    public async Get( url: string ): Promise<any>{
        let ret: any = null;

        await axios.get( `${this.m_UrlBase}${url}`, {
            method: "GET"
        } )
        .then( result => {
            ret = result.data
        } );

        return ret;
    }

    public async Post( url: string, data: any ): Promise<any>{
        let ret: any = null;

        await axios.post( `${this.m_UrlBase}${url}`, data, {
            method: "POST"
        } )
        .then( result => {
            ret = result.data;
        } );

        return ret;
    }
}

const client = new Request();
export default client;