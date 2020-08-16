import axios from 'axios'
import {
	MessageBox,
	Message
} from 'element-ui'
import store from '@/store'
import {
	getToken
} from '@/utils/cache'
import config from '@/utils/config'
import Qs from 'qs'

const service = axios.create({
	baseURL: config.DOMAN + config.BASE_API,
	timeout: 50000 // request timeout
})

service.interceptors.request.use(
	config => {
		config.headers['Content-Type'] = 'application/x-www-form-urlencoded'
		config.data = Qs.stringify(config.data)
		if (store.state.token) {
			config.headers['x-token'] = getToken()
		}
		return config
	},
	error => {
		console.log(error)
		return Promise.reject(error)
	}
)

service.interceptors.response.use(
	response => {
		const res = response.data
		switch (res.code) {
			case 1:
				return res
			case -10000:
				MessageBox.confirm('您的账号已失效，请重新登陆', '身份验证失败', {
					confirmButtonText: '重新登陆',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					store.dispatch('resetToken').then(() => {
						location.href = `${location.origin+location.pathname}#/login`
					})
				})
				return Promise.reject(new Error(res.message || 'Error'))
			default:
				Message({
					message: res.data || 'Error',
					type: 'error',
					duration: 5 * 1000
				})
				return Promise.reject(new Error(res.message || 'Error'))
		}
	},
	error => {
		console.log('request异常' + error) // for debug
		Message({
			message: error.date || error.message || 'error',
			type: 'error',
			duration: 5 * 1000
		})
		return Promise.reject(error)
	}
)

export default service
