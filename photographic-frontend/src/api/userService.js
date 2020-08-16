import {post} from '@/utils/http'
import {userApi} from './apiUrl'

export function login(data){
	return post(userApi.LOGIN,data)
}
export function regist(data){
	return post(userApi.REGIST,data)
}
export function getUsers(data) {
	return post(userApi.USER_LIST, data)

}

export function getUserCount(data) {
	return post(userApi.USER_COUNT, data)

}
export function editUser(data) {
	return post(userApi.USER_EIDT, data)
}
export function editPoint(data) {
	return post(userApi.USER_POINT, data)
}
export function getCameraman(data){
	return post(userApi.USER_CAMERAMAN, data)
}
export function getById(data){
	return post(userApi.GET_USER, data)
}
export function getCameramanList(data){
	return post(userApi.CAMERAMAN_LIST, data)
}
export function getCameramanCount(data){
	return post(userApi.CAMERAMAN_COUNT, data)
}

