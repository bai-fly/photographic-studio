<template>
	<div class="app-container">
		<el-form ref="editForm" :model="model" :rules="rules" size="mini" label-width="120px">
			<el-form-item label="属性名" prop="name"><el-input v-model="model.name" /></el-form-item>
			<el-form-item label="属性分类" prop="goodsAttributeCategoryId">
				<el-select v-model="model.goodsAttributeCategoryId" placeholder="请选择属性分类">
					<el-option v-for="item in category" :key="item.id" :label="item.name" :value="item.id" />
				</el-select>
			</el-form-item>
			<el-form-item label="属性类型">
				<el-radio-group v-model="model.type">
					<el-radio v-for="item in goodsAttributeType" :label="item.id" :key="item.id">{{ item.name }}</el-radio>
				</el-radio-group>
			</el-form-item>
			<el-form-item label="选择类型">
				<el-radio-group v-model="model.selectType">
					<el-radio v-for="item in goodsAttributeSelectType" :label="item.id" :key="item.id">{{ item.name }}</el-radio>
				</el-radio-group>
			</el-form-item>
			<el-form-item label="录入方式">
				<el-radio-group v-model="model.inputType">
					<el-radio v-for="item in goodsAttributeInputType" :label="item.id.toString()" :key="item.id">{{ item.name }}</el-radio>
				</el-radio-group>
			</el-form-item>
			<el-form-item label="属性值">
				<el-input type="textarea" :autosize="{ minRows: 2, maxRows: 4 }" placeholder="请输入属性值,英文逗号分割" v-model="model.inputList"></el-input>
				<div>
					<el-tag v-for='(item,index) in attributeList' :key='index'>{{item}}</el-tag>
				</div>
			</el-form-item>

			<!-- <el-form-item label="是否显示在导航">
				<el-switch :validate-event="false" v-model="model.navStatus" active-color="#13ce66" inactive-color="#ff4949" :active-value="1" :inactive-value="0" />
			</el-form-item>
			<el-form-item label="是否显示">
				<el-switch :validate-event="false" v-model="model.showStatus" active-color="#13ce66" inactive-color="#ff4949" :active-value="1" :inactive-value="0" />
			</el-form-item> -->

			<el-form-item>
				<center><el-button v-loading.fullscreen.lock="loading" type="primary" @click="save">保存</el-button></center>
			</el-form-item>
		</el-form>
	</div>
</template>

<script>
import { getAttributeCategory, getAttribute, editAttribute } from '@/api/goodsService';
import { goodsAttributeType, goodsAttributeInputType, goodsAttributeSelectType } from '@/utils/const';
// import uploadImage from '@/components/image/uploadImage'
export default {
	components: {
		// uploadImage,
	},
	data() {
		return {
			category: [],
			goodsAttributeSelectType,
			goodsAttributeInputType,
			goodsAttributeType,
			model: {
				goodsAttributeCategoryId: null,
				id: null,
				inputList: '',
				inputType: '1',
				name: '',
				selectType: 1,
				sort: 0,
				type: 1
			},
			rules: {
				name: [
					{
						required: true,
						message: '请输入分类名',
						trigger: 'blur'
					}
				],
				goodsAttributeCategoryId: [
					{
						required: true,
						message: '请选择属性分类',
						trigger: 'change'
					}
				]
			},
			loading: false
		};
	},
	computed:{
		attributeList(){
			if(this.model&&this.model.inputList!=null&&!this.model.inputList.isNullOrEmpty()){
				return this.model.inputList.split(',')
			}else{
				return []
			}
		}
	},
	async created() {
		let result = await getAttributeCategory({ index: 1, pageSize: 50 });
		this.category = result.data;
		// getNewsCategorys().then(rep=>{
		//   this.category = rep.data;
		// })
		if (this.$route.params.id) {
			getAttribute({
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
					editAttribute(this.model)
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
