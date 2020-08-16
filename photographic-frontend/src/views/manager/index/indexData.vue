<template>
	<div class="page flex-center flex-direction">
		<div class="list grid col-3">
			<div class="item" v-for='(item,index) in list' :key='index'>
				<div class="card">
					<div class="delete el-icon-delete" @click="list.splice(index,1)"></div>
					<div>
						图片：
					</div>
					<uploadImage v-model='item.image'></uploadImage>
					<div>
						文本：
					</div>
					<div>
						<el-input type="textarea" :autosize="{ minRows: 4, maxRows: 8}" placeholder="请输入内容" v-model="item.content" />
					</div>
				</div>
			</div>
		</div>
		<div class="flex">
			<el-button type="success" @click='addItem'>添加数据</el-button>
			<el-button type="primary" @click='save'>保存</el-button>
		</div>
	</div>
</template>

<script>
	import {
		getWapInfo,
		setWapInfo
	} from '@/api/wapinfoService'
	import uploadImage from '@/components/image/uploadImage'
	export default {
		components: {
			uploadImage
		},
		data() {
			return {
				key: 'index_data',
				list: []
			}
		},
		methods: {
			addItem() {
				this.list.push({
					image: '',
					content: '',
				})
			},
			save() {
				let data = {
					key: this.key,
					value: JSON.stringify(this.list)
				};
				this.showLoading();
				setWapInfo(data).then(() => {
					this.showSuccessMsg('保存成功')
					this.hideLoading();
				}).catch(() => {
					this.hideLoading();
				})
			}
		},
		mounted() {
			this.showLoading()
			getWapInfo({
				key: this.key
			}).then(rep => {

				this.list = JSON.parse(rep.data);
				this.hideLoading()
			}).catch(() => {
				this.hideLoading()
			})
		}
	}
</script>

<style scoped lang="scss">
	.list{
		width: 900px;
		padding: 30px 0;
		.item{
			padding: 20px;
			.card {
				padding: 20px;
				border-radius: 10px;
				box-shadow: 20px 20px 60px #d9d9d9,
					-20px -20px 60px #ffffff;
				position: relative;
				.delete{
					position: absolute;
					right: 10px;
					top: 10px;
					cursor: pointer;
				}
			}
		}
	}
	
</style>
