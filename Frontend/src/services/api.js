import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5168', 
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: false,
});

export default api;
