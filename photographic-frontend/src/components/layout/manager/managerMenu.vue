<template>
	<el-menu router class="my-menu" @open="handleOpen" @close="handleClose" :background-color="menuBg" text-color="#fff" active-text-color="#ffd04b">
		<menuItem v-for='(item,index) in menus' :index='index+1+""' :menu='item' :key='index' />
	</el-menu>
</template>

<script>
	import menuItem from './menuItem.vue'
	import style from './style/index.scss'
	export default {
		components:{menuItem},
		data() {
			return {
				menus:[],
				menuBg:style.menuBg
			}
		},
		methods: {
			handleOpen(key, keyPath) {
				console.log(key, keyPath);
			},
			handleClose(key, keyPath) {
				console.log(key, keyPath);
			},
			getMenu(arr){
				let list = [];
				for(let k in arr){
					let children = [];
					
					if(arr[k].hidden === false || arr[k].hidden === undefined){
						if(arr[k].children&&arr[k].children.length>0){
							children = this.getMenu(arr[k].children)
						}
						list.push({
							label:arr[k].meta.title,
							path:arr[k].path,
							children: children,
							icon:arr[k].meta.icon || ''
						})
					}
				}
				return list;
			}
		},
		created() {
			let routes = this.$router.options.routes;
			this.menus = this.getMenu(routes)
		}
	}
</script>

<style scoped lang="scss">
	
</style>
