<template>
	<div class="page" :class="isMobile ? 'mobile' : ''">
		<div class="title"><h1>摄影师</h1></div>
		<div class="list grid col-4">
			<div v-for="(item, index) in list" class="item" :key="index" @click="showModal(item)">
				<div class="shadow radius">
					<div><img :src="item.avatar" alt="" /></div>
					<div class="name">{{ item.name||item.account }}&nbsp;<span class="text-red">{{ item.sex|sexFmt }}</span></div>
					<div class="desc">{{ item.desc }}</div>
				</div>
			</div>
		</div>
		<el-pagination
			@size-change="pageSizeChange"
			@current-change="pageChange"
			:current-page="searchModel.index"
			:page-sizes="[10, 20, 30, 40]"
			:page-size="searchModel.pageSize"
			layout="total, prev, pager, next, jumper"
			:total="sum"
		/>
	</div>
</template>

<script>
import { getCameramanList, getCameramanCount } from '@/api/userService.js';
export default {
	data() {
		return {
			searchModel: {
				index: 1,
				pageSize: 20,
				category: null
			},
			loading: false,
			list: null,
			sum: 0,
		};
	},
	async created() {
		this.search();
	},
	methods: {
		pageSizeChange(pageSize) {
			this.searchModel.pageSize = pageSize;
			this.getList();
		},
		pageChange(index) {
			this.searchModel.index = index;
			this.getList();
		},
		search(index) {
			if (index) {
				this.searchModel.index = index;
			}
			this.getList();
			getCameramanCount(this.searchModel).then(res => {
				this.sum = res.data;
			});
		},
		async getList() {
			this.showLoading();
			this.loading = true;
			const result = await getCameramanList(this.searchModel);
			this.list = result.data;
			this.hideLoading();
			this.loading = false;
		}
	}
};
</script>

<style scoped lang="scss">
.page {
	padding: 60px;
	& > .title {
		padding: 60px 0;
	}
	.list {
		.item {
			padding: 2vw;
			transition: 1s;
			.radius {
				padding: 2vw;
			}
			img {
				width: 100%;
			}
			.price {
				padding-top: 1vw;
				color: #ff0000;
			}
			.name {
				font-weight: bold;
			}
			.desc {
				font-size: 14px;
				margin-top: 1vw;
			}
		}
		.item:hover {
			transform: scale(1.2);
		}
	}
}
.page.mobile {
	padding: 10vw;
	& > .title {
		padding: 10vw 0;
	}
	.list {
		.item {
			font-size: 4vw;
		}
	}
}
</style>
