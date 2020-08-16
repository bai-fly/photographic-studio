import {post} from '@/utils/http'
import {
	pricingPackageApi
} from './apiUrl'


export function editPricingPackage(data) {
	return post(pricingPackageApi.EDIT, data)
}

export function getPricingPackages(data) {
	return post(pricingPackageApi.LIST, data)

}

export function getPricingPackageCount(data) {
	return post(pricingPackageApi.COUNT, data)

}
export function deletePricingPackage(data) {
	return post(pricingPackageApi.DELETE, data)
}
