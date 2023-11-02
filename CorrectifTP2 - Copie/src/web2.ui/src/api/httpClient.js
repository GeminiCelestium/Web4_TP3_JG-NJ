httpCLient.interceptors.request.use(request => {
    const account = mainOidc.user;
    const isLoggedIn = mainOidc.isAuthenticated;
    const isApiUrl = request.url.startsWith('/api')//prefix de votre api
    if (isLoggedIn && isApiUrl) {
    Request.headers.common.Authorization = `Bearer ${account.access_token}`;
    }
    return request;
});

export default httpCLient;