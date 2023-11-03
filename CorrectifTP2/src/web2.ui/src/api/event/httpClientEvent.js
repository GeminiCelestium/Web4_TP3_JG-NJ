import axios from 'axios';

const httpClientEvent = axios.create( {
    baseURL: process.env.VUE_APP_TODO_API_URL,
    timeout: 3000,
    auth: {
        username: 'janedoe',
        password: 'janedoe'
    }

})

httpClientEvent.defaults.headers.post['Content-Type'] = 'application/json'

export default httpClientEvent