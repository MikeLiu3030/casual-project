import axios from 'axios';


const instance = axios.create({
    baseURL:'https://localhost:7103',
    timeout:5000,
    headers:{"Content-Type": "application/json"}
});

export const apiGet = async <T>(url:string):Promise<T> => {
    const response = await instance.get<T>(url);
    return response.data;
};