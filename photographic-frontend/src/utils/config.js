import {fileApi} from '@/api/apiUrl.js'
const env = process.env.NODE_ENV;
const DOMAN = env==='development'?'https://localhost:44319':location.origin;
const BASE_API = '/api';
const UPLOAD_IMAGE = DOMAN+BASE_API+fileApi.UPLOAD_IMAGE;
export default {
    DOMAN,
    BASE_API,
	UPLOAD_IMAGE,
}
