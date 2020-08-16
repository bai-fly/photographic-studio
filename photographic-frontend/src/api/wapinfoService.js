import {post} from '@/utils/http'
import {wapInfoApi} from './apiUrl'

export function getWapInfo(data){
	return post(wapInfoApi.WAPINFO_GET,data)
}
export function setWapInfo(data){
	return post(wapInfoApi.WAPINFO_SET,data)
}