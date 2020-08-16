<template>
	<div class="page flex align-center justify-center">
		<div class="loign-form">
			<el-card class="card" shadow="always" :body-style="{ backgroundColor: 'transparent' }">
				<el-form ref="loginForm" :model="loginForm" :rules="loginRules" class="login-form" auto-complete="on" label-position="left">
					<div class="text-center">
						<hollowOutText bg="titleBg"><h2 class="title">注册</h2></hollowOutText>
					</div>

					<el-form-item prop="account">
						<div class="flex align-center">
							<input class="input" ref="account" v-model="loginForm.account" placeholder="请输入账号" name="account" type="text" tabindex="1" />
						</div>
					</el-form-item>

					<el-form-item prop="password">
						<div class="flex align-center">
							<input
								class="input"
								:key="passwordType"
								ref="password"
								v-model="loginForm.password"
								:type="passwordType"
								placeholder="请输入密码"
								name="password"
								tabindex="2"
								@keyup.enter="handleLogin"
							/>
							<span class="show-pwd" @click="showPwd"><span :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" /></span>
						</div>
					</el-form-item>
					<div style="width:100%;margin-bottom:30px;">
						<fluxayButton class="login-btn" :loading="loading" color="rgb(81,153,257)" @click.native.prevent="handleRegist">注册</fluxayButton>
					</div>
				</el-form>
			</el-card>
		</div>
	</div>
</template>

<script>
import fluxayButton from '@/components/control/fluxayButton';
import hollowOutText from '@/components/text/hollowOutText';
import { regist } from '@/api/userService';
export default {
	components: {
		fluxayButton,
		hollowOutText
	},
	data() {
		return {
			loginForm: {
				account: '',
				password: ''
			},
			loginRules: {
				account: [
					{
						required: true,
						message: '请输入账号',
						trigger: 'blur'
					}
				],
				password: [
					{
						required: true,
						message: '请输入密码',
						trigger: 'blur'
					}
				]
			},
			loading: false,
			passwordType: 'password'
		};
	},
	methods: {
		showPwd() {
			if (this.passwordType === 'password') {
				this.passwordType = '';
			} else {
				this.passwordType = 'password';
			}
			this.$nextTick(() => {
				this.$refs.password.focus();
			});
		},
		handleRegist() {
			this.$refs.loginForm.validate(valid => {
				if (valid) {
					this.showLoading();
					regist(this.loginForm)
						.then(() => {
							this.hideLoading();
							this.$alert('注册成功', '提示', {
								confirmButtonText: '确定',
								callback: () => {
									this.toPage('/login');
								}
							});
						})
						.catch(() => {
							this.hideLoading();
						});
				} else {
					console.log('error submit!!');
					return false;
				}
			});
		}
	}
};
</script>

<style scoped lang="scss">
.page {
	--bg: linear-gradient(165deg, #e54d42, #39b54a, #0081ff, #9c26b0);
	width: 100vw;
	height: 100vh;
	.titleBg {
		background: linear-gradient(90deg, #e54d42, #39b54a, #0081ff, #9c26b0);
	}
	.loign-form {
		width: 450px;

		.card {
			background-color: transparent;
		}

		.login-btn {
			width: 100%;
			background-color: rgba(120, 184, 239, 1);
		}

		.input {
			display: block;
			width: 100%;
			height: 40px;
			border: 0;
			padding: 0 5px;
			background: transparent;
			outline: none;
			box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
		}
	}
}

.page:after {
	content: '';
	position: fixed;
	left: 0;
	top: 0;
	width: 100vw;
	height: 100vh;
	z-index: -1;
	opacity: 0.5;
	background: var(--bg);
}
</style>
