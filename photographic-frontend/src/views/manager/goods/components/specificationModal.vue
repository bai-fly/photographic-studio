<template>
	<div class="attribute-modal">
		<span @click="show=true">
			<slot></slot>
		</span>
		<el-dialog title="属性选择" :visible.sync="show" width="800px">
			<div class="row">
				<span class="title">选择属性：</span>
				<el-select v-model="attrId" placeholder="请选择">
					<el-option v-for="item in attribute" :key="item.id" :label="item.name" :value="item.id"></el-option>
				</el-select>
			</div>
			<div class="row">
				<span class="title">属性值：</span>
				<div v-if="attrSelect ? true : false">
					<!-- 手工录入 -->
					<div v-if="attrSelect.inputType == 0">
						<!-- 唯一,单选 -->
						<div v-if='attrSelect.selectType==0||attrSelect.selectType==1'>
							<el-input placeholder="请输入规格值" type="text" v-model="uniqueValue"></el-input>
						</div>
						<div v-else>
							<el-form :inline="true">
								<el-form-item>
									<el-input placeholder="请输入规格值" type="text" v-model="inputValue"></el-input>
								</el-form-item>
								<el-form-item>
									<el-button
										@click="
											() => {
												this.attrValue.push(this.inputValue);
												this.inputValue = '';
											}
										"
									>
										添加
									</el-button>
								</el-form-item>
							</el-form>
						</div>
						
						
						
					</div>
					<!-- 列表选择 -->
					<div v-else-if="attrSelect.inputType == 1">
						<!-- 唯一,单选 -->
						<div v-if='attrSelect.selectType==0||attrSelect.selectType==1'>
							<el-select v-model="uniqueValue" placeholder="请选择">
								<el-option v-for="item in attrSelect.inputList.split(',')" :key="item" :label="item" :value="item"></el-option>
							</el-select>
						</div>
						<div v-else>
							<el-transfer v-model="attrValue" :data="attrTransferArr"></el-transfer>
						</div>
					</div>
					<div>
						<el-tag
							v-for="(item, index) in attrValue"
							:key="index"
							closable
							@close="attrValue.splice(index, 1)"
						>
							{{ item }}
						</el-tag>
					</div>
				</div>
			</div>
			<span slot="footer" class="dialog-footer">
				<el-button @click="show = false">取 消</el-button>
				<el-button type="primary" @click="confirm">确 定</el-button>
			</span>
		</el-dialog>
	</div>
</template>

<script>
	import { getAttributes } from '@/api/goodsService.js';
	export default {
		data(){
			return {
				show:false,
				attribute:[],
				uniqueValue:'',
				attrValue:[],
				attrId:null,
				inputValue:'',
			}
		},
		computed:{
			attrSelect() {
				if (this.attrId != null) {
					return this.attribute.find(m => m.id == this.attrId);
				}
				return {};
			},
			attrTransferArr(){
				let list = this.attrSelect.inputList.split(',')
				let arr = [];
				for(let k in list){
					arr.push({
						key:list[k],
						label:list[k],
					});
				}
				return arr;
			},
		},
		watch:{
			attrSelect(){
				this.init()
			}
		},
		methods:{
			init(){
				this.uniqueValue = '';
				this.attrValue = [];
				this.inputValue = '';
			},
			confirm(){
				//单选
				if(this.attrSelect.selectType==0||this.attrSelect.selectType==1){
					this.$emit('confirm',{
						goodsAttributeId:this.attrId,
						name:this.attrSelect.name,
						value:this.uniqueValue
					});
				}else{
					this.$emit('confirm',{
						goodsAttributeId:this.attrId,
						name:this.attrSelect.name,
						value:this.attrValue.join(',')
					});
				}
				this.show = false;
			}
		},
		created() {
			getAttributes({ index: 1, pageSize: 9999, type: 0 }).then(rep => {
				this.attribute = rep.data;
			});
		}
	}
</script>

<style scoped lang="scss">
	.attribute-modal{
		display: inline-block;
		.row{
			padding: 10px 0;
			display: flex;
			.title{
				display: inline-block;
				width: 100px;
			}
		}
	}
</style>
