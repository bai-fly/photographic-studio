<template>
	<div class="page flex-center flex-direction">
		<div class="text-center">
			<h3>请选择图片上传</h3>
		</div>
		<div style="width: 100%;">
			<uploadImageList v-model='images'></uploadImageList>
			
		</div>
		<el-button type="primary" @click='save'>保存</el-button>
	</div>
</template>

<script>
	import uploadImageList from '@/components/image/uploadImageList.vue'
	import {
		getWapInfo,
		setWapInfo
	} from '@/api/wapinfoService'
	export default {
		components: {
			uploadImageList
		},
		data() {
			return {
				key: 'index_image',
				images: ''
			};
		},
		methods: {
			save(){
				let data = {
					key: this.key,
					value: this.images
				};
				this.showLoading();
				setWapInfo(data).then(()=>{
					this.showSuccessMsg('保存成功')
					this.hideLoading();
				}).catch(()=>{
					this.hideLoading();
				})
			}
		},
		mounted() {
			this.showLoading()
			getWapInfo({
				key: this.key
			}).then(rep => {
				this.images = rep.data;
				this.hideLoading()
			}).catch(()=>this.hideLoading)
		}
	}
</script>

<style scoped lang="scss">
	.page {
		padding: 20px;
		// height: 100vh;
	}
</style>
