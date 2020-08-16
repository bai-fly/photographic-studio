<template>
	<div class="page flex-center flex-direction">
		<div class="text-center">
			<h3>关于我们</h3>
		</div>
		<div style="width: 100%;">
			<Tinymce v-model='value' :height="600"></Tinymce>
			
		</div>
		<el-button type="primary" @click='save'>保存</el-button>
	</div>
</template>

<script>
	import Tinymce from '@/components/Tinymce'
	import {
		getWapInfo,
		setWapInfo
	} from '@/api/wapinfoService'
	export default {
		components: {
			Tinymce
		},
		data() {
			return {
				key: 'about',
				value: ''
			};
		},
		methods: {
			save(){
				let data = {
					key: this.key,
					value: this.value
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
			getWapInfo({
				key: this.key
			}).then(rep => {
				this.value = rep.data;
			})
		}
	}
</script>

<style scoped lang="scss">
	.page {
		padding: 20px;
		// height: 100vh;
	}
</style>
