<template>
	<div class="app-container">
		<el-form :inline="true" size="mini">
			<el-form-item><el-button @click="showEditModal(null)" type="primary">新增</el-button></el-form-item>
		</el-form>
		<el-table v-loading="loading" :data="list" border size="mini" style="width: 100%">
			<el-table-column prop="id" label="id" width="80"></el-table-column>

			<el-table-column label="封面">
				<template slot-scope="scope">
					<avatar :avatar="scope.row.cover"></avatar>
				</template>
			</el-table-column>
			<el-table-column prop="name" label="套餐名" />
			<el-table-column label="排序" width="60" prop="order"/>
			<el-table-column label="金额" width="120">
				<template slot-scope="scope">
					&yen;{{ scope.row.price }}
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
					<el-button type="warning" size="mini" @click="showEditModal(scope.row)">修改</el-button>
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
				<el-form-item label="封面" prop="cover"><uploadImage v-model="edit.cover"></uploadImage></el-form-item>
				<el-form-item label="套餐名" prop="name"><el-input v-model="edit.name"></el-input></el-form-item>
				<el-form-item label="套餐价" prop="price">
					<el-input placeholder="请输入金额" type="number" v-model="edit.price">
						<template slot="prepend">
							&yen;
						</template>
					</el-input>
				</el-form-item>
				<el-form-item label="排序"><el-input-number v-model="edit.order" :min="0" :max="99999" label="排序"></el-input-number></el-form-item>
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
import { getPricingPackages, getPricingPackageCount, editPricingPackage } from '@/api/pricingPackageService.js';
import uploadImage from '@/components/image/uploadImage.vue';
import { sex } from '@/utils/const';

export default {
	components: { uploadImage },
	data() {
		return {
			searchModel: {
				index: 1,
				pageSize: 20
			},
			sex,
			loading: false,
			list: null,
			sum: 0,
			showEdit: false,
			edit: {
				id: null,
				name: '',
				cover: '',
				desc: '',
				price: 0,
				deposit: 0,
				order: 0
			},
			rules: {
				name: [{ required: true, message: '请输入套餐名称', trigger: 'blur' }],
				cover: [{ required: true, message: '请选择封面', trigger: 'blur' }],
				desc: [{ required: true, message: '请输入简介', trigger: 'blur' }],
				price: [{ required: true, message: '请输入套餐价格', trigger: 'blur' }]
			}
		};
	},
	created() {
		
		this.search();
	},
	methods: {
		showEditModal(model) {
			if (model) {
				this.edit.id = model.id;
				this.edit.name = model.name;
				this.edit.cover = model.cover;
				this.edit.desc = model.desc;
				this.edit.price = model.price;
				this.edit.deposit = model.deposit;
				this.edit.order = model.order;
			} else {
				this.edit.id = null;
				this.edit.name = '';
				this.edit.cover = '';
				this.edit.desc = '';
				this.edit.price = 0;
				this.edit.deposit = 0;
				this.edit.order = 0;
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
			getPricingPackageCount(this.searchModel).then(res => {
				this.sum = res.data;
			});
		},
		async getList() {
			this.loading = true;
			const result = await getPricingPackages(this.searchModel);
			this.list = result.data;
			this.loading = false;
		},
		save() {
			this.$refs['edit'].validate(valid => {
				if (valid) {
					editPricingPackage(this.edit).then(() => {
						this.$message.success('保存成功');
						this.search();
						this.showEdit = false;
					});
				} else {
					console.log('error submit!!');
					return false;
				}
			});
		}
	}
};
</script>

<style scoped>
.cover {
	width: 100px;
}
</style>
