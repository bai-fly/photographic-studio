<template>
	<el-menu-item  :index="index" v-if='menu.children&&menu.children.length==1' :route="{path:menu.path}" >{{menu.children[0].label}}</el-menu-item>

	<el-submenu :index="index" v-else-if="menu.children&&menu.children.length>1">
		<template slot="title">{{menu.label}}</template>
		<menu-item v-for='(item,i) in menu.children' :index="index+'-'+(i+1)" :menu='item' :key='i' />
	</el-submenu>

	<el-menu-item :index="index" :route="{path:menu.path}" v-else>{{menu.label}}</el-menu-item>
</template>

<script>
	export default {
		name: 'menu-item',
		props: {
			index: {
				type: String
			},
			menu: {
				type: Object
			}
		}
	}
</script>

<style scoped lang="scss">
	
</style>
