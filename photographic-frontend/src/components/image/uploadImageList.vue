<template>
	<div>
		<el-upload class="upload" list-type="picture-card" :limit="8" :file-list="fileList" :action="UPLOAD_IMAGE" :on-remove="handleRemove" :on-preview="handlePictureCardPreview" :before-upload="beforeAvatarUpload" :on-success="handleSuccess">
			<i class="el-icon-plus avatar-uploader-icon"></i>
		</el-upload>
		<el-dialog :visible.sync="dialogVisible">
			<img width="100%" :src="dialogImageUrl" alt="">
		</el-dialog>
	</div>
</template>

<script>
	import config from '@/utils/config'
	export default {
		props: {
			size: {
				type: Number,
				default: 140
			},
			width: {
				type: Number,
			},
			height: {
				type: Number
			},
			value: {
				type: String
			}
		},
		data() {
			return {
				UPLOAD_IMAGE: config.UPLOAD_IMAGE,
				fileList: [],
				dialogImageUrl: '',
				dialogVisible: false
			}
		},
		computed: {
			sizeStyle() {
				const width = this.width ? this.width : this.size;
				const height = this.height ? this.height : this.size;
				return `width:${width}px;height:${height}px;line-height:${height}px`
			}
		},
		methods: {
			handleRemove(file) {
				let temp = []
				for (let i in this.fileList) {
					if (this.fileList[i].uid !== file.uid) {
						temp.push(this.fileList[i].url)
					}
				}
				this.$emit('input', temp.join(','))
			},
			handlePictureCardPreview(file) {
				this.dialogImageUrl = file.url;
				this.dialogVisible = true;
			},
			beforeAvatarUpload() {
				// if(this.fileType.findIndex(m=>m==file.type)==-1){
				// 	 this.$message.error('文件类型不正确');
				// 	 return false;
				// }
				return true;
			},
			handleSuccess(response, file, fileList) {
				console.log(response, file, fileList)
				if (response.code === 1) {
					let temp = this.value ? this.value.split(',') : []
					temp.push(response.data)
					this.$emit('input', temp.join(','));
				}
			},
		},
		watch: {
			value(newVal) {
				if (newVal) {
					let temp = newVal.split(',')
					let fileList = []
					for (let i in temp) {
						fileList.push({
							uid: new Date().getTime() + i,
							url: temp[i]
						})
					}
					this.fileList = fileList
				}
			}
		}
	}
</script>

<style lang="scss" scoped>
	// .upload {
	//   display: inline-block;
	//   text-align: center;
	//   border: 1px dashed #d9d9d9;
	//   border-radius: 6px;
	//   cursor: pointer;
	//   position: relative;
	//   overflow: hidden;
	// }

	// .upload:hover {
	//   border-color: #409EFF;
	// }
</style>
