<template>
	<div class="about">
		<div v-html="content">
			
		</div>
	</div>
</template>

<script>
	import {
		getWapInfo
	} from '@/api/wapinfoService'
	export default {
		data(){
			return {
				content:''
			}
		},
		created() {
			getWapInfo({key:'about'}).then(rep=>{
				this.content = rep.data;
			})
		}
	}
</script>

<style scoped lang="scss">
	.about{
		width: 100vw;
		height: 100vh;
		display: flex;
		align-items: center;
		justify-content: center;
		padding: 0 5vw;
		color: #FFF;
	}
</style>
