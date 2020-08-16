import {post} from '@/utils/http'
import {
	userImageApi
} from './apiUrl'


export function getImages(data) {
	return post(userImageApi.IMAGE_LIST, data)

}