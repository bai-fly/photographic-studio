import {sex,orderStatus} from '@/utils/const'
export function dateTimeFmt(date) {
  var moment = require('moment');
	return moment(date).format('YYYY-MM-DD HH:mm:ss')
}
export function sexFmt(s){
	let item = sex.find(m=>m.id==s)
	return item?item.name:'未知'
}
export function orderStatusFmt(s){
	let item = orderStatus.find(m=>m.id==s)
	return item?item.name:'未知'
}
