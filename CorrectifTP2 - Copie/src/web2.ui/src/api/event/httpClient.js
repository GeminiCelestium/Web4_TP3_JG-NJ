import axios from 'axios';

const httpClient = axios.create( {
    baseURL: process.env.VUE_APP_TODO_API_URL,
    timeout: 3000,
    auth: {
        username: 'janedoe',
        password: 'janedoe'
    }

})

httpClient.defaults.headers.post['Content-Type'] = 'application/json'

export default httpClient