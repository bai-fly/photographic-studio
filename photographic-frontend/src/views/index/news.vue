<template>
	<div class="page" :class="isMobile?'mobile':''">
		<div class="title"><h1>新闻中心</h1></div>
		<div class="list">
			<div v-for='(item,index) in list' class="flex item" :key='index'>
				<div class="time">{{item.createDate|dateTimeFmt}}</div>
				<div class="title" @click="toDetail(item)">{{item.title}}</div>
			</div>
		</div>
		<el-pagination @size-change="pageSizeChange" @current-change="pageChange" :current-page="searchModel.index" :page-sizes="[10, 20, 30, 40]" :page-size="searchModel.pageSize" layout="total, prev, pager, next, jumper" :total="sum" />
	</div>
</template>

<script>
	import {
		getCustomerNews,
		getNewsCount
	} from '@/api/newsService'
	export default {
		data(){
			return {
				searchModel: {
					index: 1,
					pageSize: 20,
					category: null,
				},
				loading: false,
				list: null,
				sum: 0,
				category: [],
			}
		},
		async created() {
			this.search()
		},
		methods:{
			toDetail(item){
				if(item.type==1){
					this.toPage('newsDetail/'+item.id)
				}else{
					location.href = item.link;
				}
				
			},
			pageSizeChange(pageSize) {
				this.searchModel.pageSize = pageSize;
				this.getList()
			},
			pageChange(index) {
				this.searchModel.index = index
				this.getList()
			},
			search(index) {
				if (index) {
					this.searchModel.index = index
				}
				this.getList()
				getNewsCount(this.searchModel).then(res => {
					this.sum = res.data;
				})
			},
			async getList() {
				this.loading = true
				const result = await getCustomerNews(this.searchModel);
				for (let i in result.data) {
					let item = result.data[i];
					if (item.cover) {
						try {
							item.cover = item.cover.split(',')
							continue;
						} catch (e) {
							console.log(e)
						}
					}
					item.cover = []
				}
				this.list = result.data;
				this.loading = false
			},
		}
	}
</script>

<style scoped lang="scss">
	.page{
		padding: 60px;
		&>.title{
			padding: 60px 0;
		}
		.list{
			.item:nth-child(n+2){
				border-top: 1px solid #666666;
			}
			.item{
				font-size: 20px;
				padding: 20px 0;
				transition: .5s;
				.time{
					color: #999;
				}
				.title{
					color: #333;
					padding-left: 30px;
					cursor: pointer;
					
				}
			}
			.item:hover{
				font-size: 22px;
			}
		}
	}
	.page.mobile{
		padding: 10vw;
		&>.title{
			padding: 10vw 0;
		}
		.list{
			.item{
				font-size: 4vw;
			}
		}
	}
</style>
