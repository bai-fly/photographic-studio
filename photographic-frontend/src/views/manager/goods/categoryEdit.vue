<template>
	<div class="app-container">
		<el-form ref="editForm" :model="model" :rules="rules" size="mini" label-width="120px">
			<el-form-item label="名称" prop="name"><el-input v-model="model.name" /></el-form-item>
			<el-form-item label="图标" prop="icon"><uploadImage v-model="model.icon"></uploadImage></el-form-item>
			<el-form-item label="上级">
				<el-select v-model="model.parentId" placeholder="请选择上级菜单"><el-option v-for="item in category" :key="item.id" :label="item.name" :value="item.id" /></el-select>
			</el-form-item>
			<el-form-item label="是否显示在导航">
				<el-switch :validate-event='false' v-model="model.navStatus" active-color="#13ce66" inactive-color="#ff4949" :active-value='1' :inactive-value='0' />
			</el-form-item>
			<el-form-item label="是否显示">
				<el-switch :validate-event='false' v-model="model.showStatus" active-color="#13ce66" inactive-color="#ff4949" :active-value='1' :inactive-value='0' />
			</el-form-item>
			
			<el-form-item>
				<center><el-button v-loading.fullscreen.lock="loading" type="primary" @click="save">保存</el-button></center>
			</el-form-item>
		</el-form>
	</div>
</template>

<script>
import { getCategoryDetail, getCategorys, editCategory } from '@/api/goodsService.js';

import uploadImage from '@/components/image/uploadImage'
export default {
	components: {
		uploadImage,
	},
	data() {
		return {
			category: [],
			model: {
				icon: '',
				id: null,
				name: "",
				navStatus: 0,
				order: 0,
				parentId: 0,
				showStatus: 0,
			},
			rules: {
				name: [
					{
						required: true,
						message: '请输入分类名',
						trigger: 'blur'
					}
				],
			},
			loading: false
		};
	},
	async created() {
		let result = await getCategorys({index:1,pageSize:50,parentId:0})
		result.data.splice(0,0,{id:0,name:'无'});
		this.category = result.data;
		// getNewsCategorys().then(rep=>{
		//   this.category = rep.data;
		// })
		if (this.$route.params.id) {
			getCategoryDetail({
				id: this.$route.params.id
			}).then(rep => {
				this.model = rep.data;
			});
		}
	},
	methods: {
		save() {
			this.$refs['editForm'].validate(valid => {
				if (valid) {
					this.loading = true;
					editCategory(this.model)
						.then(() => {
							this.loading = false;
							this.$message({
								showClose: true,
								message: '保存成功',
								type: 'success'
							});
						})
						.catch(e => {
							console.log(e);
							this.loading = false;
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

<style scoped></style>
