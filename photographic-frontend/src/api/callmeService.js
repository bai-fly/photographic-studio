import {post} from '@/utils/http'
import {callMeApi} from './apiUrl'

export function add(data){
	return post(callMeApi.CALLME_ADD,data)
}
export function getCallMes(data){
	return post(callMeApi.CALLME_LIST,data)
}
export function getCallMeCount(data){
	return post(callMeApi.CALLME_COUNT,data)
}
export function deleteCallMe(data){
	return post(callMeApi.CALLME_DELETE,data)
}