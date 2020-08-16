import {post} from '@/utils/http'
import {
	commentApi
} from './apiUrl'


export function getComments(data) {
	return post(commentApi.LIST, data)

}

export function getCommentCount(data) {
	return post(commentApi.COUNT, data)

}
