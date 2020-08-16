<template>
	<div class="home" ref="home" @scroll="scroll">
		<el-carousel :interval="4000" type="card">
			<el-carousel-item v-for="(item, index) in images" :key="index"><img class="banner-image" :src="item" alt="" /></el-carousel-item>
		</el-carousel>
		<div class="list">
			<div class="item" v-for="(item, index) in list" :key="index" >
				<img :src="item.image" alt="">
				<div class="content" v-html="item.content"></div>
			</div>
		</div>
	</div>
</template>

<script>
import { getWapInfo } from '@/api/wapinfoService';
export default {
	data() {
		return {
			active: 0,
			images: [],
			list: []
		};
	},
	methods: {
		scroll(e) {
			this.active = parseInt(e.target.scrollTop / this.height);
			this.$refs.imageViewImage.style.height = this.height - (e.target.scrollTop % this.height) + 'px';
		}
	},
	created() {
		getWapInfo({ key: 'index_image' }).then(rep => {
			this.images = rep.data.split(',');
		});
		getWapInfo({ key: 'index_data' }).then(rep => {
			this.list = JSON.parse(rep.data);
		});
	},
};
</script>

<style scoped lang="scss">
.home {
	width: 100vw;
	height: 100vh;
	overflow-x: hidden;
	padding-top: 10vw;
	img{
		width: 100%;
	}
	.list{
		.item{
			position: relative;
			.content{
				position: absolute;
				top: 50%;
				left: 50%;
				transform: translate(-50%,-50%);
				color: #fff;
			}
		}
	}
}
</style>
