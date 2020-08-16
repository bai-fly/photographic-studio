<template>
	<div class="nav flex">
		<div class="flex-sub"></div>
		<div class="hover">
			<span>
				<avatar :size='50' :avatar='avatarSrc'></avatar>
			</span>
			<span>{{name}}</span>
			<span>退出</span>
		</div>
	</div>
</template>

<script>
	import avatar from '@/components/image/avatar'
	export default {
		components:{avatar},
		computed:{
			name(){
				let user = this.$store.state.user
				return user?user.account:''
			},
			avatarSrc(){
				let user = this.$store.state.user
				return user?user.avatar:''
			}
		}
	}
</script>

<style scoped lang="scss">
	.nav{
		box-shadow: 0 0 5px 5px #f1f1f1;
		padding: 10px;
		&>div{
			&>span{
				padding-left: 10px;
			}
		}
	}
	
</style>
