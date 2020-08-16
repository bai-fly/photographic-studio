<template>
	<div class="page">
		<div class="menu">
			<managerMenu></managerMenu>
		</div>
		
		<div class="main-container">
			<navHeader></navHeader>
			<app-main class="app-main" />
		</div>
	</div>
</template>

<script>
	import AppMain from './AppMain.vue'
	import managerMenu from './managerMenu.vue'
	import navHeader from './navHeader.vue'
	export default {
		components: {
			AppMain,
			managerMenu,
			navHeader
		},
		data() {
			return {

			}
		},

	}
</script>

<style scoped lang="scss">
	@import './style/index.scss';
	.page{
		width: 100vw;
		height: 100vh;
		display: flex;
		&>.menu{
			width: 200px;
			overflow-x: hidden;
			background-color: $menuBg;
		}
		&>.main-container{
			height: 100vh;
			overflow-y: scroll;
			flex: 1;
			.app-main{
				padding: 20px;
			}
		}
	}
</style>
