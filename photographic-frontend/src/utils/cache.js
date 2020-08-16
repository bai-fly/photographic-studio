const TokenKey = 'FLYTOKEN'
const UserKey = 'FLY'

export function getToken() {
	return localStorage.getItem(TokenKey)
}
export function setToken(token) {
	localStorage.setItem(TokenKey, token)
}
export function removeToken() {
	localStorage.removeItem(TokenKey)
}

export function getUser() {
	let userStr = localStorage.getItem(UserKey)
	return userStr ? JSON.parse(userStr).data : null;
}
export function setUser(user) {
	localStorage.setItem(UserKey, JSON.stringify({
		type: 'object',
		data: user
	}))
}
export function removeUser() {
	localStorage.removeItem(UserKey)
}
export function clear() {
	localStorage.clear()
}
