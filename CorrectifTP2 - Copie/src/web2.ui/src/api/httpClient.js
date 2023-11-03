import axios from 'axios';

const httpClient = axios.create();

httpClient.interceptors.request.use(request => {
    const account = mainOidc.user;
    const isLoggedIn = mainOidc.isAuthenticated;
    const isApiUrl = request.url.startsWith('/Web2.API');
    
    if (isLoggedIn && isApiUrl) {
        request.headers.common['Authorization'] = `Bearer ${account.access_token}`;
    }
    
    return request;
});

export default httpClient;
