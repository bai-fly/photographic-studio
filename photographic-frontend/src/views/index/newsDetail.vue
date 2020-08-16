<template>
	<div class="page" :class="isMobile?'mobile':''">
		<div class="title">{{model.title}}</div>
		<div class="time">
			{{model.createDate|dateTimeFmt}}
		</div>
		<div class="content" v-html='model.content'></div>
	</div>
</template>

<script>
	import {
		getNewsDetail
	} from '@/api/newsService'
	export default {
		data() {
			return {
				model: {}
			}
		},
		created() {
			if (this.$route.params.id) {
				getNewsDetail({
					id: this.$route.params.id
				}).then(rep => {
					this.model = rep.data
				})
			}
		}
	}
</script>

<style scoped lang="scss">
	.page {
		padding: 60px;

		&>.title {
			padding: 40px 0;
			font-size: 30px;
		}
		.time{
			color: #666;
		}
	}
	.page.mobile{
		padding: 10vw;
		&>.title{
			padding: 12vw 0 5vw 0;
		}
	}
</style>
