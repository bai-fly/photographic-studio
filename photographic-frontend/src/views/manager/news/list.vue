<template>
	<div class="app-container">

		<el-form :inline="true" size="mini">
			<el-form-item>
				<router-link to="add">
					<el-button type='primary'>新增</el-button>
				</router-link>
			</el-form-item>
			<el-form-item>
				<el-select v-model="searchModel.category" clearable placeholder="请选择">
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
			<el-table-column prop="title" label="新闻标题" />
			<el-table-column label="类型">
				<template slot-scope="scope">
					{{scope.row.type==1?'文章':'链接'}}
				</template>
			</el-table-column>
			<el-table-column v-if='false' label="分类">
				<template slot-scope="scope">
					{{scope.row.category|categoryFmt}}
				</template>
			</el-table-column>
			<el-table-column v-if='false' label="封面">
				<template slot-scope="scope">
					<img v-for='(img,index) in scope.row.cover' :src="img" :key='scope.row.id.toString()+index' class="cover" alt="">
				</template>
			</el-table-column>

			<el-table-column label="是否启用" width="80">
				<template slot-scope="scope">
					<span @click="statusChange(scope.row)">
						<el-switch :validate-event='false' v-model="scope.row.status" active-color="#13ce66" inactive-color="#ff4949" :active-value='1' :inactive-value='0' />
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
					<el-button type="danger" size="mini" @click="del(scope.row.id)">删除</el-button>
				</template>
			</el-table-column>
		</el-table>
		<el-pagination @size-change="pageSizeChange" @current-change="pageChange" :current-page="searchModel.index" :page-sizes="[10, 20, 30, 40]" :page-size="searchModel.pageSize" layout="total, sizes, prev, pager, next, jumper" :total="sum">
		</el-pagination>
	</div>
</template>

<script>
	import {
		getNews,
		getNewsCount,
		updateNewsStatus,
		deleteNews
	} from '@/api/newsService'
	
	export default {
		data() {
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
				getNewsCount(this.searchModel).then(res => {
					this.sum = res.data;
				})
			},
			async getList() {
				this.loading = true
				const result = await getNews(this.searchModel);
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
			statusChange(item) {
				const errStatus = item.status == 0 ? 1 : 0
				this.showLoading();
				updateNewsStatus({
					id: item.id,
					status: item.status
				}).then(() => {
					this.hideLoading();
				}).catch(() => {
					item.status = errStatus;
					this.hideLoading();
				})
			},
			del(id) {
				this.$confirm('此操作将永久删除该数据, 是否继续?', '提示', {
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					let index = this.list.findIndex(m => m.id == id)
					if (index !== -1) {
						this.showLoading();
						deleteNews({
							id: this.list[index].id
						}).then(() => {
							this.$message({
								type: 'success',
								message: '删除成功!'
							});
							this.list.splice(index, 1)
							this.hideLoading();
						}).catch((e)=>{
							console.log(e)
							this.hideLoading();
						})
					}

				}).catch((e) => {
					console.log(e)
					this.$message({
						type: 'info',
						message: '已取消删除'
					});
				});

			}
		},
		filters: {
			categoryFmt(c) {
				return c?'未知':'a'
			}
		}
	}
</script>

<style scoped>
	.cover {
		width: 100px;
	}
</style>
