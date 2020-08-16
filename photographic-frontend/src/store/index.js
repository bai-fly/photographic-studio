import Vue from 'vue'
import Vuex from 'vuex'
import {
	clear,
	setToken,
	setUser,
	getUser,
	getToken
} from '@/utils/cache'
import {login} from '@/api/userService'

Vue.use(Vuex)

export default new Vuex.Store({
	state: {
		width: 0,
		height: 0,
		webTitle: '',
		token: '',
		user: null,
		isMobile:false,
	},
	mutations: {
		setWebTitle: (state, title) => {
			state.webTitle = title;
			document.title = title;
		},
		setSize: (state, size) => {
			state.width = size.width;
			state.height = size.height;
			state.isMobile = size.isMobile;
		},
		setToken: (state, token) => {
			state.token = token;
		},
		setUser: (state, user) => {
			state.user = user;
		},
		loadCache: (state) => {
			state.user = getUser();
			state.token = getToken();
		}
	},
	actions: {
		login({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				login(data).then(response => {
					const {
						token,
						user
					} = response.data;
					commit('setToken', token)
					commit('setUser', user)
					setToken(token)
					setUser(user)
					resolve(response.data)
				}).catch(error => {
					reject(error)
				})
			})
		},
		resetToken({
			commit
		}) {
			return new Promise((resolve) => {
				commit('setToken', '');
				commit('setUser', null);
				clear();
				resolve()
			})
		},
	},
	
	modules: {}
})
