<template>
	<div class="app-container">
		<div class="bg-white">
			<el-form ref="editForm" :model="model" :rules="rules" size="mini" label-width="120px">
				<el-form-item label="商品名称" prop="name"><el-input v-model="model.name" /></el-form-item>
				<el-form-item label="副标题" prop="subTitle"><el-input v-model="model.subTitle" /></el-form-item>
				<el-form-item label="封面" prop="cover"><uploadImageList v-model="model.cover"></uploadImageList></el-form-item>
				<el-form-item label="编码" prop="sn"><el-input v-model="model.sn" /></el-form-item>
				<el-form-item label="分类" prop="categoryId"><el-cascader v-model="categorySelect" :options="category"></el-cascader></el-form-item>
				<el-form-item label="价格" prop="price">
					<el-input placeholder="请输入商品价格" type="number" v-model="model.price">
						<template slot="prepend">
							&yen;
						</template>
					</el-input>
				</el-form-item>
				<el-form-item label="促销价" prop="promotionPrice">
					<el-input placeholder="请输入商品促销价" type="number" v-model="model.promotionPrice">
						<template slot="prepend">
							&yen;
						</template>
					</el-input>
				</el-form-item>
				<el-form-item label="编码" prop="sn"><el-input placeholder="请输入商品编码" type="text" v-model="model.sn"></el-input></el-form-item>
				<el-form-item label="销量" prop="sale"><el-input-number v-model="model.sale" :min="0" :max="999999" label="请输入销量"></el-input-number></el-form-item>
				<el-form-item label="库存" prop="stock"><el-input-number v-model="model.stock" :min="0" :max="999999" label="请输入库存"></el-input-number></el-form-item>
				<el-form-item label="单位" prop="unit"><el-input placeholder="请输入商品单位" type="text" v-model="model.unit"></el-input></el-form-item>
				<el-form-item label="是否是新品">
					<el-radio-group v-model="model.newStstus">
						<el-radio :label="1">是</el-radio>
						<el-radio :label="0">否</el-radio>
					</el-radio-group>
				</el-form-item>
				<el-form-item label="是否推荐">
					<el-radio-group v-model="model.recommandStatus">
						<el-radio :label="1">是</el-radio>
						<el-radio :label="0">否</el-radio>
					</el-radio-group>
				</el-form-item>
				<el-form-item label="商品属性">
					<div class="goods-attr shadow">
						<div>
							<attrbuteModal @confirm="attrAdd"><el-button type="primary">添加商品属性</el-button></attrbuteModal>
							<specificationModal @confirm="specAdd"><el-button type="success">添加商品规格</el-button></specificationModal>
						</div>
						<div class="attr">
							<div class="title">商品属性</div>
							<div class="attr-list">
								<div class="item" v-for="(item, index) in goodsAttr" :key="index">
									<div class="title">{{ item.name }}</div>
									<div class="value">{{ item.value }}</div>
									<el-button type="danger" @click="goodsAttr.splice(index, 1)">删除</el-button>
								</div>
							</div>
						</div>
						<div class="spec">
							<div class="title">商品规格</div>
							<div class="spec-list">
								<span class="item" v-for="(item, index) in goodsSpec" :key="index">
									<el-tag @close="goodsSpec.splice(index, 1)" closable>{{ item.name }}</el-tag>
								</span>
								<el-row>
									<el-col :span="4">属性</el-col>
									<el-col :span="4">封面</el-col>
									<el-col :span="4">金额</el-col>
									<el-col :span="4">促销价</el-col>
									<el-col :span="4">库存</el-col>
									<el-col :span="4">编码</el-col>
								</el-row>
								<el-row v-for="(item, index) in specification" :key="index+Math.random()">
									<el-col :span="4">{{ item.spData|spDataFmt }}</el-col>
									<el-col :span="4"><uploadImage v-model="item.cover"></uploadImage></el-col>
									<el-col :span="4">
										<el-input placeholder="请输入金额" v-model="item.price" type="number">
											<template slot="prepend">
												&yen;
											</template>
										</el-input>
									</el-col>
									<el-col :span="4">
										<el-input placeholder="请输入促销价" v-model="item.promotionPrice" type="number">
											<template slot="prepend">
												&yen;
											</template>
										</el-input>
									</el-col>
									<el-col :span="4"><el-input v-model="item.stock" placeholder="请输入库存"></el-input></el-col>
									<el-col :span="4"><el-input v-model="item.skuCode" placeholder="请输入编码"></el-input></el-col>
								</el-row>
							</div>
						</div>
					</div>
				</el-form-item>
				<el-form-item></el-form-item>
				<el-form-item>
					<center><el-button v-loading.fullscreen.lock="loading" type="primary" @click="save">保存</el-button></center>
				</el-form-item>
			</el-form>
		</div>
	</div>
</template>

<script>
import { getGoods, editGoods, getCategorys, getAttributes } from '@/api/goodsService.js';
import uploadImageList from '@/components/image/uploadImageList';
import attrbuteModal from './components/attributeModal.vue';
import specificationModal from './components/specificationModal.vue';
import uploadImage from '@/components/image/uploadImage.vue';
export default {
	components: {
		uploadImageList,
		attrbuteModal,
		specificationModal,
		uploadImage
	},
	data() {
		return {
			category: [],
			categorySelect: null,
			attribute: [], //属性
			guige: [], //规格
			model: {
				brandId: null,
				categoryId: null,
				cover: '',
				desc: '',
				feightTemplateId: null,
				id: null,
				name: '',
				newStstus: 1,
				price: 0,
				promotionPrice: 0,
				publishStatus: 0,
				recommandStatus: 1,
				sale: 0,
				sn: '',
				sort: 0,
				stock: 0,
				subTitle: '',
				unit: ''
			},
			goodsAttr: [],
			goodsSpec: [],
			rules: {
				name: [
					{
						required: true,
						message: '请输入商品名称',
						trigger: 'blur'
					}
				],
				cover: [
					{
						required: true,
						message: '请选择封面',
						trigger: 'blur'
					}
				],
				categoryId: [
					{
						required: true,
						message: '请选择分类',
						trigger: 'blur'
					}
				]
			},
			loading: false
		};
	},
	watch: {
		categorySelect(newVal) {
			this.model.categoryId = newVal[newVal.length - 1];
		}
	},
	computed: {
		specification() {
			var getCombination = array => {
				let resultArr = [];
				array.forEach(spec => {
					let arrItem = spec.valueArr;
					if (resultArr.length === 0) {
						let firstItem = [];
						arrItem.forEach(item => {
							firstItem.push([{ attr: spec.name, value: item }]);
						});
						resultArr = firstItem;
					} else {
						console.log(0, resultArr);
						const emptyArray = [];
						resultArr.forEach(item => {
							arrItem.forEach(value => {
								emptyArray.push([...item, { attr: spec.name, value }]);
							});
						});
						resultArr = emptyArray;
					}
				});
				return resultArr;
			};
			var list = [];
			let specCount = this.goodsSpec.length; //规格数量
			for (let i = 0; i < specCount; i++) {
				let item = this.goodsSpec[i];
				console.log(item);
				let arr = item.value.split(',');
				list.push({
					name: item.name,
					goodsAttributeId: item.goodsAttributeId,
					value: item.value,
					valueArr: arr
				});
			}

			let speclist = getCombination(list);
			let result = [];
			for (let k in speclist) {
				result.push({
					spData: speclist[k],
					skuCode: '',
					price: 0,
					stock: 0,
					cover: '',
					sale: 0,
					promotionPrice: ''
				});
			}
			return result;
		},
		specificationCount() {
			let row = 0;
			for (let i = 0; i < this.goodsSpec.length; i++) {
				let item = this.goodsSpec[i];
				console.log(item);
				let arr = item.value.split(',');
				if (i == 0) {
					row = arr.length;
				} else {
					row = row * arr.length;
				}
			}
			return row;
		}
	},
	async created() {
		// getNewsCategorys().then(rep=>{
		//   this.category = rep.data;
		// })
		getAttributes({ index: 1, pageSize: 9999, type: 0 }).then(rep => {
			this.guige = rep.data;
		});

		let result = await getCategorys({ index: 1, pageSize: 1000 });
		let list = result.data;
		this.category = this.initCategory(null, list);

		if (this.$route.params.id) {
			getGoods({
				id: this.$route.params.id
			}).then(rep => {
				this.model = rep.data;
				this.initCategorySelect(this.model.categoryId, this.category);
			});
		}
	},
	methods: {
		attrAdd(attr) {
			this.goodsAttr.push(attr);
		},
		specAdd(spec) {
			this.goodsSpec.push(spec);
		},
		initCategorySelect(category, categoryTree) {
			let findItem = (c, tree) => {
				for (let k in tree) {
					let item = tree[k];

					if (item.value == c) {
						return [item.value];
					}

					if (item.children && item.children.length > 0) {
						let t = findItem(c, item.children);
						if (t.length > 0) {
							t.splice(0, 0, item.value);
							return t;
						}
					}
				}
				return [];
			};
			console.log(findItem(category, categoryTree));
			this.categorySelect = findItem(category, categoryTree);
		},
		initCategory(parent, arr) {
			let parentId = parent ? parent.value : 0;
			let c = [];
			let temp = [];
			for (let k in arr) {
				let item = arr[k];
				if (arr[k].parentId == parentId) {
					c.push({
						label: item.name,
						value: item.id
					});
				} else {
					temp.push(item);
				}
			}
			for (let k in c) {
				let children = this.initCategory(c[k], temp);
				if (children.length > 0) {
					c[k].children = children;
				}
			}
			return c;
		},
		save() {
			this.$refs['editForm'].validate(valid => {
				if (valid) {
					this.loading = true;
					let model = JSON.parse(JSON.stringify(this.model));
					if (model.type == 1) {
						model.link = '';
					} else {
						model.content = '';
					}
					editGoods(model)
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
	},
	filters:{
		spDataFmt(d){
			let arr=[]
			for(let k in d){
				arr.push(d[k].value)
			}
			return arr.join(',')
		}
	}
};
</script>

<style scoped lang="scss">
.app-container {
	.goods-attr {
		padding: 40px 20px;
		.attr {
			margin-top: 20px;
			padding: 20px;
			background-color: #f1f1f1;
			border-radius: 5px;
			& > .title {
				font-size: 25px;
				text-align: center;
			}
			& > .attr-list {
				& > .item {
					display: flex;
					align-items: center;
					padding: 5px 0;
					& > .title {
						width: 200px;
					}
					& > .value {
						flex: 1;
					}
				}
			}
		}
	}
}
</style>
