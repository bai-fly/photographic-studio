<template>
	<div class="images-page">
		<div class="list shadow radius" v-infinite-scroll="getList">
			<div class="item" v-for='(item,index) in list' :key='index'>
				<img :src="item" alt="">
			</div>
		</div>
	</div>
</template>

<script>
	import {getImages} from '@/api/userImageService.js'
	export default {
		data(){
			return {
				searchModel: {
					index: 1,
					pageSize: 12,
					category: null,
				},
				loading: false,
				finish: false,
				list: [],
				sum: 0,
			}
		},
		methods:{
			async getList() {
				if(this.loading||this.finish)return;
				this.loading = true
				const result = await getImages(this.searchModel);
				this.searchModel.index++;
				
				for(let i in result.data){
					this.list.push(result.data[i])
				}
				if(result.data.length==0)this.finish = true;
				this.loading = false
			},
		}
	}
</script>

<style scoped lang="scss">
	.images-page{
		width: 100vw;
		height: 100vh;
		overflow-y: auto;
		display: flex;
		align-items: center;
		justify-content: center;
		.list{
			padding: 5vw;
			width: 90vw;
			height: 80vh;
			overflow: auto;
			display: flex;
			flex-wrap: wrap;
			.item{
				width: 25%;
				img{
					width: 100%;
				}
			}
		}
	}
</style>
