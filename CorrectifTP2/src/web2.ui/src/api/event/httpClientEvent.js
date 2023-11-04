import axios from 'axios';

const httpClientEvent = axios.create( {
    baseURL: "https://localhost:7284/",
    timeout: 3000,
    auth: {
        username: 'janedoe',
        password: 'janedoe'
    }

})

httpClientEvent.defaults.headers.post['Content-Type'] = 'application/json'

export default httpClientEvent