<template>
	<div class="app-container">
		<el-card>
			<el-form class="text-gray" v-if="model != null" size="mini" label-width="120px">
				<el-form-item label="订单号"><div>{{model.orderNumber}}</div></el-form-item>
				<el-form-item label="手机号"><div>{{model.phone}}</div></el-form-item>
				<el-form-item label="预约时间"><div>{{model.time|dateTimeFmt}}</div></el-form-item>
				<el-form-item label="定金"><div>{{model.deposit}}</div></el-form-item>
				<el-form-item label="尾款"><div>{{model.price}}</div></el-form-item>
				<el-form-item label="地址">
					<div>
						<div>{{model.province}}{{model.city}}{{model.area}}</div>
						<div>{{model.address}}</div>
					</div>
				</el-form-item>
			</el-form>
		</el-card>
		<el-card v-if='cameraman?true:false'>
			<div class="text-center">摄影师信息</div>
			<el-form class="text-gray" size="mini" label-width="120px">
				<el-form-item label="头像"><div><avatar :avatar='cameraman.avatar'></avatar></div></el-form-item>
				<el-form-item label="姓名"><div>{{cameraman.name}}</div></el-form-item>
				<el-form-item label="性别"><div>{{cameraman.sex|sexFmt}}</div></el-form-item>
				<el-form-item label="简介"><div>{{cameraman.desc}}</div></el-form-item>
			</el-form>
		</el-card>
		<el-card>
			<div class="text-center">上传照片</div>
			<div>
				<uploadImageList v-model='imageList'></uploadImageList>
			</div>
			<div class="text-center">
				<el-button @click="saveImage" type="primary">保存</el-button>
			</div>
		</el-card>
	</div>
</template>

<script>
import { getByOrderNumber,getImages,setImages} from '@/api/orderService.js';
import { getById } from '@/api/userService.js'
import uploadImageList from '@/components/image/uploadImageList'
export default {
	components:{
		uploadImageList
	},
	data() {
		return {
			model: null,
			cameraman:null,
			loading: false,
			imageList:'',
		};
	},
	methods:{
		getCameraman(){
			if(this.model.cameraman)
			getById({userId:this.model.cameraman}).then(rep=>this.cameraman=rep.data)
		},
		saveImage(){
			setImages({orderNumber:this.model.orderNumber,imageList:this.imageList}).then(()=>{
				this.$notify.success('保存成功！')
			});
		}
	},
	async created() {
		if (this.$route.params.id) {
			let result = await getByOrderNumber({
				orderNumber: this.$route.params.id
			});
			this.model = result.data;
			getImages({orderNumber:this.model.orderNumber}).then(rep=>{
				this.imageList = rep.data.join(',')
			})
		}
	},
	watch: {
		model(newVal){
			if(newVal){
				this.getCameraman()
			}
		}
	}
};
</script>

<style scoped></style>
