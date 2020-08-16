<template>
	<div class="app-container">
		<el-form :inline="true" size="mini">
			<el-form-item><el-button type="primary" @click="showEdit(null)">新增</el-button></el-form-item>
		</el-form>
		<el-table v-loading="loading" :data="list" border size="mini" style="width: 100%">
			<el-table-column prop="id" label="id" width="80"></el-table-column>

			<!-- <el-table-column label="图标">
				<template slot-scope="scope">
					<img :src="scope.row.icon" alt="">
				</template>
			</el-table-column> -->
			<el-table-column prop="name" label="属性分类名称" />

			<!-- <el-table-column label="显示在导航" width="100">
				<template slot-scope="scope">
					<span @click="showNavStatusChange(scope.row)">
						<el-switch :validate-event='false' v-model="scope.row.navStatus" active-color="#13ce66" inactive-color="#ff4949" :active-value='1' :inactive-value='0' />
					</span>
				</template>
			</el-table-column> -->

			<el-table-column width="140" label="创建时间">
				<template slot-scope="scope">
					{{ scope.row.createDate | dateTimeFmt }}
				</template>
			</el-table-column>
			<el-table-column width="100" label="编辑">
				<template slot-scope="scope">
					<!-- <router-link :to="'categoryedit/'+scope.row.id">
						<el-button type="warning" size="mini">修改</el-button>
					</router-link> -->
					<el-button type="warning" size="mini" @click="showEdit(scope.row)">修改</el-button>
				</template>
			</el-table-column>
		</el-table>
	</div>
</template>

<script>
import { getAttributeCategory, editAttributeCategory } from '@/api/goodsService';

export default {
	data() {
		return {
			searchModel: {
				index: 1,
				pageSize: 20
			},
			loading: false,
			list: null,
			sum: 0
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
			// getCategoryCount(this.searchModel).then(res => {
			// 	this.sum = res.data;
			// })
		},
		async getList() {
			this.loading = true;
			const result = await getAttributeCategory();
			this.list = result.data;
			this.loading = false;
		},
		showEdit(item) {
			console.log(item);
			this.$prompt('请输入属性分类名称', '提示', {
				confirmButtonText: '确定',
				cancelButtonText: '取消',
				inputPattern: /.*[^\s]/,
				inputErrorMessage: '格式不正确',
			})
				.then(({ value }) => {
					editAttributeCategory({
						id: item ? item.id : '',
						name: value.trim()
					})
						.then(() => {
							this.loading = false;
							this.$message({
								showClose: true,
								message: '保存成功',
								type: 'success'
							});
							this.search()
						})
						.catch(e => {
							console.log(e);
							this.loading = false;
						});
					
				})
				.catch(() => {
					this.$message({
						type: 'info',
						message: '取消输入'
					});
				});
		}
		// showStatusChange(item) {
		// 	const errStatus = item.showStatus == 0 ? 1 : 0
		// 	this.showLoading();
		// 	setShowCategory({
		// 		id: item.id,
		// 		status: item.showStatus
		// 	}).then(() => {
		// 		this.hideLoading();
		// 	}).catch(() => {
		// 		item.showStatus = errStatus;
		// 		this.hideLoading();
		// 	})
		// },
	}
};
</script>

<style scoped>
.cover {
	width: 100px;
}
</style>
