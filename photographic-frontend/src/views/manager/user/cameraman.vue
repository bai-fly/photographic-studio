<template>
	<div class="app-container">
		<el-form :inline="true" size="mini">
			<el-form-item><el-button @click="showEditModal(null)" type="primary">新增</el-button></el-form-item>
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
		<el-table v-loading="loading" :data="list" border size="mini" style="width: 100%">
			<el-table-column prop="id" label="id" width="80"></el-table-column>

			<el-table-column label="头像">
				<template slot-scope="scope">
					<avatar :avatar="scope.row.avatar"></avatar>
				</template>
			</el-table-column>
			<el-table-column prop="account" label="账号" />
			<el-table-column label="姓名">
				<template slot-scope="scope">
					{{scope.row.name||scope.row.account}}
				</template>
			</el-table-column>
			<el-table-column label="性别" width="60">
				<template slot-scope="scope">
					{{ scope.row.sex | sexFmt }}
				</template>
			</el-table-column>
			<el-table-column prop="desc" label="简介" />

			<el-table-column width="140" label="创建时间">
				<template slot-scope="scope">
					{{ scope.row.createDate | dateTimeFmt }}
				</template>
			</el-table-column>
			<el-table-column width="80" label="编辑">
				<template slot-scope="scope">
					<el-button type="warning" size="mini" @click='showEditModal(scope.row)'>修改</el-button>
					<!-- <el-button type="danger" size="mini" @click="del(scope.row.id)">删除</el-button> -->
				</template>
			</el-table-column>
		</el-table>
		<el-pagination
			@size-change="pageSizeChange"
			@current-change="pageChange"
			:current-page="searchModel.index"
			:page-sizes="[10, 20, 30, 40]"
			:page-size="searchModel.pageSize"
			layout="total, sizes, prev, pager, next, jumper"
			:total="sum"
		></el-pagination>

		<!-- 编辑 -->
		<el-dialog title="新增/修改摄影师" :visible.sync="showEdit" width="550px">
			<el-form :model="edit" status-icon :rules="rules" ref="edit" label-width="70px">
				<el-form-item label="头像" prop="avatar"><uploadImage v-model="edit.avatar"></uploadImage></el-form-item>
				<el-form-item label="账号" prop="account"><el-input v-model="edit.account"></el-input></el-form-item>
				<el-form-item label="姓名"><el-input v-model="edit.name"></el-input></el-form-item>
				<el-form-item v-if='edit.id?false:true' label="密码" prop="password"><el-input type="password" v-model="edit.password" autocomplete="off"></el-input></el-form-item>
				<el-form-item label="性别">
					<el-radio-group v-model="edit.sex">
						<el-radio v-for="item in sex" :label="item.id" :key="item.id">{{ item.name }}</el-radio>
					</el-radio-group>
				</el-form-item>
				<el-form-item label="简介" prop="desc">
					<el-input type="textarea" placeholder="请输入简介" v-model="edit.desc" maxlength="200" :autosize="{ minRows: 3 }" show-word-limit />
				</el-form-item>
			</el-form>
			<span slot="footer" class="dialog-footer">
				<el-button @click="showEdit = false">取 消</el-button>
				<el-button type="primary" @click="save">确 定</el-button>
			</span>
		</el-dialog>
	</div>
</template>

<script>
import { getUsers, getUserCount,editUser } from '@/api/userService.js';
import uploadImage from '@/components/image/uploadImage.vue';
import { sex } from '@/utils/const';

export default {
	components: { uploadImage },
	data() {
		return {
			searchModel: {
				index: 1,
				pageSize: 20,
				role: 3
			},
			sex,
			loading: false,
			list: null,
			sum: 0,
			showEdit: false,
			edit: {
				id: null,
				account: '',
				name:'',
				password: '',
				avatar: '',
				sex: 1,
				role: 3,
				desc: ''
			},
			rules: {
				account: [{ required: true, message: '请输入账号', trigger: 'blur' }, { min: 5, max: 10, message: '长度在 5 到 10 个字符', trigger: 'blur' }],
				password: [{ required: true, message: '请输入密码', trigger: 'blur' }, { min: 5, max: 20, message: '长度在 8 到 20 个字符', trigger: 'blur' }],
				avatar: [{ required: true, message: '请选择头像', trigger: 'blur' }],
				desc: [{ required: true, message: '请输入简介', trigger: 'blur' }]
			}
		};
	},
	async created() {
		this.search();
	},
	methods: {
		showEditModal(model) {
			if (model) {
				this.edit.id = model.id;
				(this.edit.account = model.account),
				this.edit.name = model.name;
					(this.edit.password = ''),
					(this.edit.avatar = model.avatar),
					(this.edit.sex = model.sex),
					(this.edit.role = model.role),
					(this.edit.desc = model.desc);
			} else {
				this.edit.id = null;
				this.edit.name = '';
				(this.edit.account = ''), (this.edit.password = ''), (this.edit.avatar = ''), (this.edit.sex = 1), (this.edit.role = 3), (this.edit.desc = '');
			}
			this.showEdit = true;
		},
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
			getUserCount(this.searchModel).then(res => {
				this.sum = res.data;
			});
		},
		async getList() {
			this.loading = true;
			const result = await getUsers(this.searchModel);
			this.list = result.data;
			this.loading = false;
		},
		save() {
			this.$refs['edit'].validate(valid => {
				if (valid) {
					editUser(this.edit).then(()=>{
						this.$message.success('保存成功')
						this.search();
						this.showEdit = false;
					})
				} else {
					console.log('error submit!!');
					return false;
				}
			});
		},
		publishStatusChange() {
			// const errStatus = item.publishStatus == 0 ? 1 : 0;
			// this.showLoading();
			// setGoodsPublishStatus({
			// 	id: item.id,
			// 	status: item.publishStatus
			// })
			// 	.then(() => {
			// 		this.hideLoading();
			// 	})
			// 	.catch(() => {
			// 		item.publishStatus = errStatus;
			// 		this.hideLoading();
			// 	});
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
	}
};
</script>

<style scoped>
.cover {
	width: 100px;
}
</style>
