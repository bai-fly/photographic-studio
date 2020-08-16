import {post} from '@/utils/http'
import {sysconfigApi} from './apiUrl'
import {deposit} from '@/utils/const'

export function getSysConfig(data){
	return post(sysconfigApi.SYSCONFIG_GET,data)
}
export function getDeposit(){
	return post(sysconfigApi.SYSCONFIG_GET,{key:deposit})
}