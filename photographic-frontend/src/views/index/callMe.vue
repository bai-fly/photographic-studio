<template>
	<div class="call-me" :class="isMobile?'mobile':''">
		<div class="title">联系我们</div>
		<div class="sub-title">如果您对我们公司有任何问题或疑问，请使用下面的表格。</div>
		<div class="flex-center">
			<div class="form">
				<el-form ref="editForm" :model="model" :rules="rules">
					<el-form-item label="姓名" prop='name'>
						<el-input v-model="model.name" placeholder="请输入姓名"></el-input>
					</el-form-item>
					<el-form-item label="邮箱" prop='email'>
						<el-input v-model="model.email" placeholder="请输入邮箱"></el-input>
					</el-form-item>
					<el-form-item label="地址" prop='address'>
						<el-input placeholder="请输入地址" v-model="model.address"></el-input>
					</el-form-item>
					<el-form-item label="留言内容" prop='content'>
						<el-input type="textarea" :autosize="{ minRows: 4, maxRows: 8}" placeholder="请输入内容" v-model="model.content">
						</el-input>
					</el-form-item>
					<el-form-item>
						<center>
							<el-button type="danger" @click='save'>提交</el-button>
						</center>
					</el-form-item>
				</el-form>
			</div>
		</div>
	</div>
</template>

<script>
	import {add} from '@/api/callmeService.js'
	export default {
		data() {
			return {
				model: {
					name: '',
					email: '',
					address: '',
					content: ''
				},
				rules: {
					name: [{
						required: true,
						message: '请输入姓名',
						trigger: 'blur'
					}],
					email: [{
						required: true,
						message: '请输入邮箱',
						trigger: 'blur'
					}],
					address: [{
						required: true,
						message: '请输入地址',
						trigger: 'blur'
					}],
					content: [{
						required: true,
						message: '请输入内容',
						trigger: 'blur'
					}]
				},
			}
		},
		methods: {
			save() {
				this.$refs['editForm'].validate((valid) => {
					if (valid) {
						this.showLoading()
						add(this.model).then(() => {
							this.hideLoading();
							this.showSuccessMsg('提交成功');
							this.model.name = '';
							this.model.email = '';
							this.model.address = '';
							this.model.content = '';
						}).catch(e => {
							console.log(e)
							this.hideLoading()
						})
					} else {
						console.log('error submit!!')
						return false
					}
				})
			}
		}
	}
</script>

<style scoped lang="scss">
	.call-me {
		padding: 60px;

		&>.title {
			padding: 50px 0 0 0;
			font-size: 30px;
			color: #e60012;
		}

		&>.sub-title {
			padding: 30px 0;
		}

		.form {
			margin-top: 20px;
			width: 600px;
			padding: 20px;
			border-radius: 50px;
			box-shadow: 0px 0px 20px 2px #a5fdfd;
		}
	}
	.call-me.mobile{
		padding: 15vw 0;
		&>.title{
			padding: 10vw 5vw 0 5vw;
		}
		&>.sub-title{
			padding: 5vw;
		}
		.form{
			padding: 5vw;
			margin-top: 0vw;
			width: 100%;
			box-shadow: none;
		}
	}
</style>
