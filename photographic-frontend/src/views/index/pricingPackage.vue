<template>
	<div class="page" :class="isMobile ? 'mobile' : ''">
		<div class="title"><h1>特惠套餐</h1></div>
		<div class="list grid col-4">
			<div v-for="(item, index) in list" class="item" :key="index" @click="showModal(item)">
				<div class="shadow radius">
					<div><img :src="item.cover" alt="" /></div>
					<div class="price">&yen;{{ item.price }}</div>
					<div class="name">{{ item.name }}</div>
					<div class="desc">{{ item.desc }}</div>
				</div>
			</div>
		</div>
		<el-pagination
			@size-change="pageSizeChange"
			@current-change="pageChange"
			:current-page="searchModel.index"
			:page-sizes="[10, 20, 30, 40]"
			:page-size="searchModel.pageSize"
			layout="total, prev, pager, next, jumper"
			:total="sum"
		/>
		<el-dialog title="填写信息" :visible.sync="modal.show" width="550px">
			<el-form :model="modal" ref="modal" label-width="100px">
				<el-form-item label="手机号" prop="phone" :rules="[{ required: true, message: '手机号不能为空' }, { type: 'number', message: '手机号必须为数字值' }]">
					<el-input type="age" v-model.number="modal.phone" autocomplete="off"></el-input>
				</el-form-item>
				<el-form-item label="预约时间" prop="time" :rules="[{ required: true, message: '请选择预约时间' }]">
					<el-date-picker
						v-model="modal.time"
						value-format="yyyy-MM-dd HH:mm:ss"
						format="yyyy-MM-dd HH:mm:ss"
						type="datetime"
						placeholder="选择日期时间"
					></el-date-picker>
				</el-form-item>
				<el-form-item label="地址">
					<VDistpicker @province='e=>modal.province=e.value' @city='e=>modal.city=e.value' @area='e=>modal.area=e.value'></VDistpicker>
				</el-form-item>
				<el-form-item label="地址详情" prop="address" :rules="[{ required: true, message: '地址详情不能为空' }]">
					<el-input type="age" v-model="modal.address" autocomplete="off"></el-input>
				</el-form-item>
				<el-form-item label="定金">
					<el-input type="text" v-model="deposit" disabled></el-input>
				</el-form-item>
			</el-form>
			<span slot="footer" class="dialog-footer">
				<el-button @click="modal.show = false">取 消</el-button>
				<el-button type="primary" @click="order">确 定</el-button>
			</span>
		</el-dialog>
	</div>
</template>

<script>
import { getPricingPackageCount, getPricingPackages } from '@/api/pricingPackageService.js';
import{getDeposit} from '@/api/sysconfigService.js'
import VDistpicker from 'v-distpicker';
import {createOrder} from '@/api/orderService'
export default {
	components: { VDistpicker },
	data() {
		return {
			searchModel: {
				index: 1,
				pageSize: 20,
				category: null
			},
			loading: false,
			list: null,
			sum: 0,
			category: [],
			deposit:0,//定金
			modal: {
				show: false,
				data: null,
				phone: '',
				address: '',
				time: '',
				province: '',
				city: '',
				area: ''
			}
		};
	},
	async created() {
		getDeposit().then(rep=>{
			this.deposit = rep.data;
		})
		this.search();
	},
	methods: {
		order() {
			this.$refs['modal'].validate(valid => {
				if (valid) {
					if(this.modal.province==''||this.modal.city==''||this.modal.area==''||this.modal.province=='省'||this.modal.city=='市'||this.modal.area=='区'){
						return this.$notify.error('请选择地址！')
					}
					this.showLoading();
					createOrder({
						phone:this.modal.phone,
						address:this.modal.address,
						time:this.modal.time,
						province:this.modal.province,
						city:this.modal.city,
						area:this.modal.area,
						pricingPackageId:this.modal.data.id,
					}).then(()=>{
						this.hideLoading();
						this.$notify.success('预定成功!')
						this.toPage('/order');
					})
				} else {
					console.log('error submit!!');
					return false;
				}
			});
		},
		showModal(item) {
			this.modal.data = item;
			this.modal.phone = '';
			this.modal.address = '';
			this.modal.show = true;
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
			this.showLoading();
			this.loading = true;
			const result = await getPricingPackages(this.searchModel);
			for (let i in result.data) {
				let item = result.data[i];
				if (item.cover) {
					try {
						item.cover = item.cover.split(',');
						continue;
					} catch (e) {
						console.log(e);
					}
				}
				item.cover = [];
			}
			this.list = result.data;
			this.hideLoading();
			this.loading = false;
		}
	}
};
</script>

<style scoped lang="scss">
.page {
	padding: 60px;
	& > .title {
		padding: 60px 0;
	}
	.list {
		.item {
			padding: 2vw;
			transition: 1s;
			.radius {
				padding: 2vw;
			}
			img {
				width: 100%;
			}
			.price {
				padding-top: 1vw;
				color: #ff0000;
			}
			.name {
				font-weight: bold;
			}
			.desc {
				font-size: 14px;
				margin-top: 1vw;
			}
		}
		.item:hover {
			transform: scale(1.2);
		}
	}
}
.page.mobile {
	padding: 10vw;
	& > .title {
		padding: 10vw 0;
	}
	.list {
		.item {
			font-size: 4vw;
		}
	}
}
</style>
