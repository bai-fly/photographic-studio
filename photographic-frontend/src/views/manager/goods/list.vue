<template>
	<div class="app-container">

		<el-form :inline="true" size="mini">
			<el-form-item>
				<router-link to="add">
					<el-button type='primary'>新增</el-button>
				</router-link>
			</el-form-item>
			<!-- <el-form-item>
				<el-select v-model="searchModel.category" clearable placeholder="请选择">
					<el-option v-for="item in category" :key="item.id" :label="item.name" :value="item.id">
					</el-option>
				</el-select>
			</el-form-item> -->
			<!-- <el-form-item>
				<el-button type='primary' @click='search(1)'>搜索</el-button>
			</el-form-item> -->
		</el-form>
		<el-table v-loading="loading" :data="list" border size='mini' style="width: 100%">
			<el-table-column prop="id" label="id" width="80">
			</el-table-column>
			
			<el-table-column label="图标">
				<template slot-scope="scope">
					<img :src="scope.row.icon" alt="">
				</template>
			</el-table-column>
			<el-table-column prop="categoryName" label="分类" />
			<el-table-column prop="name" label="商品名称" />

			<el-table-column label="是否上架" width="80">
				<template slot-scope="scope">
					<span @click="publishStatusChange(scope.row)">
						<el-switch :validate-event='false' v-model="scope.row.publishStatus" active-color="#13ce66" inactive-color="#ff4949" :active-value='1' :inactive-value='0' />
					</span>
				</template>
			</el-table-column>
			<el-table-column label="是否推荐" width="100">
				<template slot-scope="scope">
					<span @click="recommandStatusChange(scope.row)">
						<el-switch :validate-event='false' v-model="scope.row.recommandStatus" active-color="#13ce66" inactive-color="#ff4949" :active-value='1' :inactive-value='0' />
					</span>
				</template>
			</el-table-column>

			<el-table-column width="140" label="创建时间">
				<template slot-scope="scope">
					{{scope.row.createDate|dateTimeFmt}}
				</template>
			</el-table-column>
			<el-table-column width="100" label="编辑">
				<template slot-scope="scope">
					<router-link :to="'edit/'+scope.row.id">
						<el-button type="warning" size="mini">修改</el-button>
					</router-link>
					<!-- <el-button type="danger" size="mini" @click="del(scope.row.id)">删除</el-button> -->
				</template>
			</el-table-column>
		</el-table>
		<el-pagination @size-change="pageSizeChange" @current-change="pageChange" :current-page="searchModel.index" :page-sizes="[10, 20, 30, 40]" :page-size="searchModel.pageSize" layout="total, sizes, prev, pager, next, jumper" :total="sum">
		</el-pagination>
	</div>
</template>

<script>
	import {
		getGoodsList,
		getGoodsCount,
		getCategorys,
		setGoodsPublishStatus,
		setGoodsRecommandStatus
	} from '@/api/goodsService'
	
	export default {
		data() {
			return {
				searchModel: {
					index: 1,
					pageSize: 20,
				},
				loading: false,
				list: null,
				sum: 0,
				category: [],
			}
		},
		async created() {
			let result = await getCategorys({index:1,pageSize:100})
			this.category = result.data;
			this.search()
		},
		methods: {
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
				getGoodsCount(this.searchModel).then(res => {
					this.sum = res.data;
				})
			},
			async getList() {
				this.loading = true
				const result = await getGoodsList(this.searchModel);
				for (let i in result.data) {
					let item = result.data[i];
					if(item.categoryId){
						let p = this.category.find(m=>m.id==item.categoryId)
						item.categoryName = p?p.name:''
					}else{
						item.categoryName = ''
					}
				}
				this.list = result.data;
				this.loading = false
			},
			publishStatusChange(item) {
				const errStatus = item.publishStatus == 0 ? 1 : 0
				this.showLoading();
				setGoodsPublishStatus({
					id: item.id,
					status: item.publishStatus
				}).then(() => {
					this.hideLoading();
				}).catch(() => {
					item.publishStatus = errStatus;
					this.hideLoading();
				})
			},
			recommandStatusChange(item){
				const errStatus = item.recommandStatus == 0 ? 1 : 0
				this.showLoading();
				setGoodsRecommandStatus({
					id: item.id,
					status: item.recommandStatus
				}).then(() => {
					this.hideLoading();
				}).catch(() => {
					item.recommandStatus = errStatus;
					this.hideLoading();
				})
			}
			// del(id) {
			// 	this.$confirm('此操作将永久删除该数据, 是否继续?', '提示', {
			// 		confirmButtonText: '确定',
			// 		cancelButtonText: '取消',
			// 		type: 'warning'
			// 	}).then(() => {
			// 		let index = this.list.findIndex(m => m.id == id)
			// 		if (index !== -1) {
			// 			this.showLoading();
			// 			deleteNews({
			// 				id: this.list[index].id
			// 			}).then(() => {
			// 				this.$message({
			// 					type: 'success',
			// 					message: '删除成功!'
			// 				});
			// 				this.list.splice(index, 1)
			// 				this.hideLoading();
			// 			}).catch((e)=>{
			// 				console.log(e)
			// 				this.hideLoading();
			// 			})
			// 		}

			// 	}).catch((e) => {
			// 		console.log(e)
			// 		this.$message({
			// 			type: 'info',
			// 			message: '已取消删除'
			// 		});
			// 	});

			// }
		},
	}
</script>

<style scoped>
	.cover {
		width: 100px;
	}
</style>
