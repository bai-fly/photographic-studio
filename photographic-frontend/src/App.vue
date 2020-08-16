<template>
	<div id="app">
		<router-view />
	</div>
</template>

<script>
	import {getSysConfig} from '@/api/sysconfigService'
	export default {
		name: 'App',
		created() {
			getSysConfig({key:'wap_title'}).then(rep=>{
				this.$store.commit('setWebTitle', rep.data)
			})
			
		}
	}
</script>

<style lang="scss">
	@import '@/style/index.scss';
	@import '@/style/element-ui-customer';
	
</style>
