export const userApi = {
	LOGIN: '/UserInfo/Login',
	REGIST: '/UserInfo/Regist',
	UserInfo: '/UserInfo/GetUserInfo',
	USER_LIST:'/UserInfo/GetList',
	USER_COUNT:'/UserInfo/GetCount',
	USER_EIDT:'/UserInfo/Edit',
	USER_POINT:'/UserInfo/Point',
	USER_CAMERAMAN:'/UserInfo/GetCameraman',
	GET_USER:'/UserInfo/GetById',
	CAMERAMAN_LIST:'/UserInfo/GetCameramanList',
	CAMERAMAN_COUNT:'/UserInfo/GetCameramanCount',

}
export const fileApi = {
	UPLOAD_IMAGE: '/File/Upload',
}
export const wapInfoApi = {
	WAPINFO_GET: '/WapInfo/GetWapInfo',
	WAPINFO_SET: '/WapInfo/Edit',
}
export const newsApi = {
	NEWS_DETAIL: '/News/Get',
	NEWS_LIST: '/News/GetList',
	NEWS_COUNT: '/News/GetCount',
	NEWS_STATUS: '/News/SetStatus',
	NEWS_EDIT: '/News/Edit',
	NEWS_DELETE: '/News/Delete',
	
	NEWS_LIST_CUSTOMER: '/News/List',
}
export const callMeApi = {
	CALLME_ADD:'/CallMe/Add',
	CALLME_LIST:'/CallMe/GetList',
	CALLME_COUNT:'/CallMe/GetCount',
	CALLME_DELETE:'/CallMe/Delete',
}
export const sysconfigApi = {
	SYSCONFIG_GET:'/SysConfig/Get',
}
export const pricingPackageApi = {
	LIST:'/PricingPackage/GetList',
	COUNT:'/PricingPackage/GetCount',
	EDIT:'/PricingPackage/Edit',
	DELETE:''
}
export const orderApi = {
	CREATE_ORDER:'/Order/CreateOrder',
	USER_ORDER:'/Order/MyList',
	USER_COUNT:'/Order/MyCount',
	PAY:'/Order/Pay',
	
	SET_CAMERAMAN:'/Order/setCameraman',
	
	LIST:'/Order/GetList',
	COUNT:'/Order/GetCount',
	SET_ISTEL:'/Order/SetIsTel',
	GET_ORDERNUMBER:'/Order/GetByOrderName',
	
	GET_ORDER_IMAGE:'/Order/GetImageList',
	SET_ORDER_IMAGE:'/Order/SaveImageList',
	SET_ISPUBLISH:'/Order/IsPublish',
	
	ORDER_DETAIL:'/Order/GetOrderDetail',
	COMMENT:'/Order/Comment',
}
export const userImageApi = {
	IMAGE_LIST:'/UserImage/GetImages',
}
export const commentApi = {
	LIST:'/Comment/GetList',
	COUNT:'/Comment/GetCount',
}