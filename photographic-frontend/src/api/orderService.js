import {post} from '@/utils/http'
import {
	orderApi
} from './apiUrl'


export function createOrder(data) {
	return post(orderApi.CREATE_ORDER, data)
}
export function getUserOrder(data){
	return post(orderApi.USER_ORDER, data)
}
export function getUserOrderCount(data){
	return post(orderApi.USER_COUNT, data)
}
export function payOrder(data){
	return post(orderApi.PAY, data)
}
export function setCameraman(data){
	return post(orderApi.SET_CAMERAMAN, data)
}

export function getOrderList(data){
	return post(orderApi.LIST, data)
}
export function getOrderCount(data){
	return post(orderApi.COUNT, data)
}
export function setIsTel(data){
	return post(orderApi.SET_ISTEL, data)
}
export function setIsPublish(data){
	return post(orderApi.SET_ISPUBLISH, data)
}


export function getByOrderNumber(data){
	return post(orderApi.GET_ORDERNUMBER, data)
}
export function getImages(data){
	return post(orderApi.GET_ORDER_IMAGE, data)
}
export function setImages(data){
	return post(orderApi.SET_ORDER_IMAGE, data)
}
export function getMyOrderDetail(data){
	return post(orderApi.ORDER_DETAIL,data)
}
export function comment(data){
	return post(orderApi.COMMENT,data)
}
