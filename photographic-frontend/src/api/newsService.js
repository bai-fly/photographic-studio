import {post} from '@/utils/http'
import {
	newsApi
} from './apiUrl'


export function editNews(data) {
	return post(newsApi.NEWS_EDIT, data)
}
export function updateNewsStatus(data) {
	return post(newsApi.NEWS_STATUS, data)

}
export function getNewsDetail(data) {
	return post(newsApi.NEWS_DETAIL, data)

}
export function getNews(data) {
	return post(newsApi.NEWS_LIST, data)

}
export function getCustomerNews(data) {
	return post(newsApi.NEWS_LIST_CUSTOMER, data)

}

export function getNewsCount(data) {
	return post(newsApi.NEWS_COUNT, data)

}
export function deleteNews(data) {
	return post(newsApi.NEWS_DELETE, data)
}
