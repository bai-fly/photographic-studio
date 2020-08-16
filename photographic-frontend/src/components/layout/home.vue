<template>
	<div class="page">
		<div class="nav text-bold flex align-center justify-center shadow">
			<!-- <div class="item flex align-center justify-center">
				<img class="logo" src="../../assets/logo.png" alt="">
				{{webTitle}}
			</div> -->
			<div class="item" v-for="(item, index) in menu" :key="index" @click="toPage(item.path)">{{ item.label }}</div>
			<div class="user">
				<div v-if="user ? true : false">
					<span>
						<el-dropdown>
							<span class="el-dropdown-link">
								{{ user.account }}
								<i class="el-icon-arrow-down el-icon--right"></i>
							</span>
							<el-dropdown-menu slot="dropdown">
								<el-dropdown-item>
									<span @click="toPage('/order')">订单列表</span>
								</el-dropdown-item>
								<el-dropdown-item>
									<span @click="loginOut">退出</span>
								</el-dropdown-item>
							</el-dropdown-menu>
						</el-dropdown>
					</span>
				</div>
				<div v-else>
					<span @click="toPage('/login')">登录</span>
					<span @click="toPage('/regist')">注册</span>
				</div>
			</div>
		</div>
		<div class="page-body"><router-view :key="key" /></div>
	</div>
</template>

<script>
export default {
	data() {
		return {
			menu: [
				{
					label: '首页',
					path: '/index'
				},
				{
					label: '公司简介',
					path: '/about'
				},
				{
					label: '特惠套餐',
					path: '/pricingPackage'
				},
				// {
				// 	label: '旅游城市',
				// 	path: '/about'
				// },
				{
					label: '客照欣赏',
					path: '/images'
				},
				{
					label: '客户评价',
					path: '/comment'
				},
				{
					label: '摄影师简介',
					path: '/cameramans'
				},
				{
					label: '新闻中心',
					path: '/news'
				}
			],
			mobile: {
				showMenu: false
			}
		};
	},
	computed: {
		webTitle() {
			return this.$store.state.webTitle;
		},
		key() {
			return this.$route.path;
		},
		user() {
			return this.$store.state.user;
		}
	},
	methods:{
		loginOut(){
			this.$store.dispatch('resetToken').then(() => {
				location.href = `${location.origin+location.pathname}#/login`
			})
		}
	}
};
</script>

<style scoped lang="scss">
$linearColor: linear-gradient(90deg, #e54d42, #fbbd08, #39b54a, #0081ff, #9c26b0);
.page {
	width: 100vw;
	height: 100vh;
	.nav {
		position: fixed;
		width: 100%;
		left: 0;
		top: 0;
		z-index: 100;
		color: #fff;
		.user {
			span {
				margin-left: 0.5vw;
				font-weight: 0;
				font-size: 0.9vw;
				color: #FFF;
			}
			span:hover {
				cursor: pointer;
			}
		}

		.item:nth-child(1) {
			transform: translate(0, 20px);
			font-size: 26px;
			font-family: cursive;
			.logo {
				width: 40px;
			}
		}
		.item {
			padding: 20px;
			cursor: pointer;
			filter: drop-shadow(1px 1px 10px #e7e7e7);
		}
		.item:hover {
			transform: scale(1.2);
			transition: 0.5s;
		}
	}

	.page-body {
		width: 100vw;
		height: 100vh;
		position: absolute;
		left: 0;
		top: 0;
		box-sizing: border-box;
		// overflow-y: scroll;
		// overflow-x: hidden;
	}
}
.page.mobile {
	.nav {
		padding: 10vw;
		display: flex;
		align-items: center;
		.logo {
			width: 10vw;
		}
		.web-title {
			padding-left: 5vw;
		}
		.menu {
			font-size: 8vw;
		}
		.menu-list {
			text-align: center;
			position: fixed;
			top: -100vh;
			left: 100vw;
			width: 100vw;
			height: 100vh;
			z-index: 10;
			background-color: #fff;
			transition: 0.5s;
			display: flex;
			flex-direction: column;
			align-items: center;
			justify-content: center;
			.close {
				font-size: 8vw;
				position: absolute;
				top: 5vw;
				right: 10vw;
			}
		}
		.menu-list.show {
			left: 0;
			top: 0;
		}
	}
}
.page::after {
	width: 100vw;
	height: 100vh;
	content: '';
	position: fixed;
	left: 0;
	top: 0;
	z-index: -1;
	// background: $linearColor;
	background-color: var(--defaultColor);
	// opacity: .2;
}
</style>
