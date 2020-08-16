<template>
	<div class="app-container">

		<el-form :inline="true" size="mini">
			<el-form-item>
				<router-link to="goodsAttributeadd">
					<el-button type='primary'>新增</el-button>
				</router-link>
			</el-form-item>
			<el-form-item>
				<el-select v-model="searchModel.categoryId" clearable placeholder="请选择">
					<el-option v-for="item in category" :key="item.id" :label="item.name" :value="item.id">
					</el-option>
				</el-select>
			</el-form-item>
			<el-form-item>
				<el-button type='primary' @click='search(1)'>搜索</el-button>
			</el-form-item>
		</el-form>
		<el-table v-loading="loading" :data="list" border size='mini' style="width: 100%">
			<el-table-column prop="id" label="id" width="80">
			</el-table-column>
			<el-table-column prop="categoryName" label="属性分类" />
			<el-table-column prop="name" label="属性名称" />
			<el-table-column prop="inputList" width="200" label="属性值" />

			<el-table-column label="类型" width="80">
				<template slot-scope="scope">
					{{scope.row.type|typeFmt}}
				</template>
			</el-table-column>
			<el-table-column label="录入方式" width="80">
				<template slot-scope="scope">
					{{scope.row.inputType|inputTypeFmt}}
				</template>
			</el-table-column>
			<el-table-column label="选择方式" width="80">
				<template slot-scope="scope">
					{{scope.row.selectType|selectTypeFmt}}
				</template>
			</el-table-column>

			<el-table-column width="140" label="创建时间">
				<template slot-scope="scope">
					{{scope.row.createDate|dateTimeFmt}}
				</template>
			</el-table-column>
			<el-table-column width="100" label="编辑">
				<template slot-scope="scope">
					<router-link :to="'goodsAttributeedit/'+scope.row.id">
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
		getAttributes,
		getAttributeCount,
		getAttributeCategory
	} from '@/api/goodsService'
	import {goodsAttributeType,goodsAttributeInputType,goodsAttributeSelectType} from '@/utils/const'
	
	export default {
		data() {
			return {
				searchModel: {
					index: 1,
					pageSize: 20,
					categoryId: ''
				},
				loading: false,
				list: null,
				sum: 0,
				category: [],
			}
		},
		async created() {
			let result = await getAttributeCategory();
			result.data.splice(0,0,{id:'',name:'全部'});
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
				getAttributeCount(this.searchModel).then(res => {
					this.sum = res.data;
				})
			},
			async getList() {
				this.loading = true
				const result = await getAttributes(this.searchModel);
				for (let i in result.data) {
					let item = result.data[i];
					let p = this.category.find(m=>m.id===item.goodsAttributeCategoryId)
					item.categoryName = p?p.name:''
				}
				this.list = result.data;
				this.loading = false
			},
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
		filters:{
			typeFmt(t){
				let item = goodsAttributeType.find(m=>m.id==t)
				return item?item.name:'未知'
			},
			inputTypeFmt(val){
				let item = goodsAttributeInputType.find(m=>m.id==val)
				return item?item.name:'未知'
			},
			selectTypeFmt(val){
				let item = goodsAttributeSelectType.find(m=>m.id==val)
				return item?item.name:'未知'
			}
		}
	}
</script>

<style scoped>
	.cover {
		width: 100px;
	}
</style>
