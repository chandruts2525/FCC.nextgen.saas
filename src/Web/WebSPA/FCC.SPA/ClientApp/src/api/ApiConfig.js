import axios from 'axios';
import handleError from './handleError';

class ApiConfig {
  constructor() {
    // const hostname = window && window.location && window.location.origin;
    // const scheme = hostname.match(/^(.*\.)?localhost$/) ? 'http' : 'https';
    // let backendURL = process.env.REACT_APP_API_URL;

    // if (hostname === `${scheme}://fcc-fms-ui-dev.azurewebsites.net`) {
    //   backendURL = `${scheme}://fcc-fms-identityandaccesscontrol-dev.azurewebsites.net/api/`;
    // } else if (hostname === `${scheme}://fcc-fms-ui-qa.azurewebsites.net`) {
    //   backendURL = `${scheme}://fcc-fms-identityandaccesscontrol-qa.azurewebsites.net/api/`;
    // } else if (hostname === `${scheme}://fcc-fms-ui-uat.azurewebsites.net`) {
    //   backendURL = `${scheme}://fcc-fms-identityandaccesscontrol-uat.azurewebsites.net/api/`;
    // }

    // hostname === 'fcc-fms-ui-dev.azurewebsites.net' ? `${scheme}://fcc-fms-identityandaccesscontrol-dev.azurewebsites.net/api/`
    //   : hostname === 'fcc-fms-ui-qa.azurewebsites.net' ? `${scheme}://fcc-fms-identityandaccesscontrol-qa.azurewebsites.net/api/`
    //   : hostname === 'fcc-fms-ui-uat.azurewebsites.net' ? `${scheme}://fcc-fms-identityandaccesscontrol-uat.azurewebsites.net/api/`
    //     : process.env.REACT_APP_API_URL;
    // let backendURL = '';
    // if (hostname.includes('localhost')) {
    //   backendURL = 'http://localhost:3600/api/';
    // } else {
    //   backendURL = hostname + '/api/';
    // }

    this.instance = axios.create({
      baseURL: window.location.origin,
    });
    this.instance.interceptors.request.use((req) => {
      if (req?.url !== 'login') {
        const _token = '';
        if (_token) {
          req.headers.Authorization = `Bearer ${_token}`;
        }
      }
      return req;
    });
  }

  get(endpoint, params = {}, config = {}) {
    return this.instance
      .get(endpoint, {
        ...config,
        params,
      })
      .catch((error) => handleError(error, config));
  }

  // post(endpoint, data, config, params) {
  //   return this.instance
  //     .post("/api/WebApp", data, { ...config, params })
  //     .catch((error) => handleError(error, config));
  // }

  post(payload, isUpload = false) {
    if (isUpload) {
      return this.instance
        .post('/api/WebApp/upload/attachments', payload)
        .catch((error) => handleError(error));
    } else {
      return this.instance
        .post('/api/WebApp', payload)
        .catch((error) => handleError(error));
    }
  }

  put(endpoint, data, params, config) {
    return this.instance
      .put(endpoint, data, { ...config, params })
      .catch((error) => handleError(error, config));
  }

  patch(endpoint, data, params, config) {
    return this.instance
      .patch(endpoint, data, { ...config, params })
      .catch((error) => handleError(error, config));
  }

  delete(endpoint, params, config) {
    return this.instance
      .delete(endpoint, { ...config, params })
      .catch((error) => handleError(error, config));
  }

  request(config) {
    return this.instance.request(config).catch((error) => handleError(error, config));
  }

  xWWWFormURLEncoded(endpoint, method, data) {
    return this.instance({
      method,
      url: endpoint,
      data: JSON.stringify(data || {}),
      headers: {
        'Content-type': 'application/json',
        // 'Content-type':'application/x-www-form-urlencoded',
        // 'Access-Control-Allow-Origin': '*'
      },
    }).catch((error) => handleError(error));
  }

  formData(endpoint, method, data) {
    return this.instance({
      method,
      url: endpoint,
      data,
      headers: {
        'Content-type': 'multipart/form-data',
      },
    }).catch((error) => handleError(error));
  }
}

export default new ApiConfig();
