<template>
	<div class="order" :class="isMobile ? 'mobile' : ''">
		<div class="title"><h1>订单</h1></div>
		<div class="list">
			<div v-for="(item, index) in list" class="flex align-center item" :key="index">
				<div class="cover"><img :src="item.cover" alt="" /></div>
				<div class="info flex-sub">
					<div class="flex justify-between order-num">
						<span>订单号：{{ item.orderNumber }}</span>
						<span class="time">创建日期{{ item.createTime | dateTimeFmt }}</span>
					</div>
					<div>套餐：{{ item.name }}</div>
					<div>定金：{{ item.deposit }}</div>
					<div>预约：{{ item.time | dateTimeFmt }}</div>
					<div>尾款：&yen;{{ item.price }}</div>
					<div>状态：{{ item.status | orderStatusFmt }}</div>
					<div>地址：{{ item.province }}{{ item.city }}{{ item.area }}{{ item.address }}</div>
				</div>
				<div class="status-info flex align-center shadow hover">
					<!-- <div class="status">{{ item.status | orderStatusFmt }}</div> -->
					<div class="status" v-if="item.status == 1" @click="doPay(item)">支付尾款</div>
					<div class="status" v-if="!item.cameraman && item.status == 2" @click="showCameraman(item)">选择摄影师</div>
					<div class="status" v-if="item.status >= 2" @click="showOrderDetail(item)">查看订单</div>
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

		<el-dialog title="选择摄影师" :visible.sync="cameramanModal.show" width="550px">
			<el-form ref="modal" :model="cameramanModal">
				<el-form-item prop="cameramanId" :rules="[{ required: true, message: '摄影师不能为空', trigger: 'change' }]">
					<el-select v-model="cameramanModal.cameramanId" placeholder="请选择">
						<el-option v-for="item in cameramanModal.list" :key="item.id" :label="item.name || item.account" :value="item.id"></el-option>
					</el-select>
				</el-form-item>
			</el-form>
			<el-card v-if="cameraman ? true : false">
				<div class="flex">
					<avatar :avatar="cameraman.avatar"></avatar>
					<div class="flex-sub">
						<el-row>
							<el-col :offset="1" :span="22">
								<div>{{ cameraman.name }}</div>
								<div class="text-gray">{{ cameraman.sex | sexFmt }}</div>
								<div class="text-grey">{{ cameraman.desc }}</div>
							</el-col>
						</el-row>
					</div>
				</div>
			</el-card>
			<span slot="footer" class="dialog-footer">
				<el-button @click="cameramanModal.show = false">取 消</el-button>
				<el-button type="primary" @click="selectCameraman">确 定</el-button>
			</span>
		</el-dialog>
		<el-dialog title="订单详情" :visible.sync="detailModal.show" fullscreen>
			<el-card>
				<el-form class="text-gray" v-if="detailModal.data.order ? true : false" size="mini" label-width="120px">
					<el-form-item label="订单号">
						<div>{{ detailModal.data.order.orderNumber }}</div>
					</el-form-item>
					<el-form-item label="手机号">
						<div>{{ detailModal.data.order.phone }}</div>
					</el-form-item>
					<el-form-item label="预约时间">
						<div>{{ detailModal.data.order.time | dateTimeFmt }}</div>
					</el-form-item>
					<el-form-item label="定金">
						<div>{{ detailModal.data.order.deposit }}</div>
					</el-form-item>
					<el-form-item label="尾款">
						<div>{{ detailModal.data.order.price }}</div>
					</el-form-item>
					<el-form-item label="地址">
						<div>
							<div>{{ detailModal.data.order.province }}{{ detailModal.data.order.city }}{{ detailModal.data.order.area }}</div>
							<div>{{ detailModal.data.order.address }}</div>
						</div>
					</el-form-item>
				</el-form>
			</el-card>
			<el-card v-if="detailModal.data.cameraman ? true : false">
				<div class="text-center">摄影师信息</div>
				<el-form class="text-gray" size="mini" label-width="120px">
					<el-form-item label="头像">
						<div><avatar :avatar="detailModal.data.cameraman.avatar"></avatar></div>
					</el-form-item>
					<el-form-item label="姓名">
						<div>{{ detailModal.data.cameraman.name }}</div>
					</el-form-item>
					<el-form-item label="性别">
						<div>{{ detailModal.data.cameraman.sex | sexFmt }}</div>
					</el-form-item>
					<el-form-item label="简介">
						<div>{{ detailModal.data.cameraman.desc }}</div>
					</el-form-item>
				</el-form>
			</el-card>
			<el-card v-if="detailModal.data.images ? true : false">
				<div class="text-center">照片</div>
				<div class="grid col-4"><img v-for="(item, index) in detailModal.data.images" :key="index" :src="item.path" alt="" /></div>
			</el-card>
			<el-card v-if="detailModal.data.comment ? true : false">
				<div>评价</div>
				<div class="text-gray">{{detailModal.data.comment.content}}</div>
			</el-card>
			<el-card v-else>
				<el-input type="textarea" :autosize="{ minRows: 4, maxRows: 8 }" placeholder="请输入评价" v-model="detailModal.comment" />
				<el-button @click="addComment" type="primary">评价</el-button>
			</el-card>
			<span slot="footer" class="dialog-footer"><el-button type="primary" @click="detailModal.show = false">确 定</el-button></span>
		</el-dialog>
	</div>
</template>

<script>
import { getUserOrder, getUserOrderCount, payOrder, setCameraman, getMyOrderDetail, comment } from '@/api/orderService';
import { getCameraman } from '@/api/userService';
export default {
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
			cameramanModal: {
				data: null,
				show: false,
				list: [],
				cameramanId: null
			},
			detailModal: {
				show: false,
				data: {},

				comment: ''
			}
		};
	},
	computed: {
		cameraman() {
			if (this.cameramanModal.cameramanId) {
				let item = this.cameramanModal.list.find(m => m.id == this.cameramanModal.cameramanId);
				return item || null;
			}
			return null;
		}
	},
	async created() {
		this.search();
	},
	methods: {
		addComment() {
			if (!this.detailModal.comment) {
				this.$notify.error('请输入内容');
				return;
			}
			this.showLoading();
			comment({ orderNumber: this.detailModal.data.order.orderNumber,content:this.detailModal.comment }).then(() => {
				this.$notify.success('提交成功');
				this.detailModal.data.comment = {content:this.detailModal.comment}
				this.getList()
				this.hideLoading();
			});
		},
		showOrderDetail(item) {
			this.showLoading();
			getMyOrderDetail({ orderNumber: item.orderNumber }).then(rep => {
				this.hideLoading();
				this.detailModal.comment = '';
				this.detailModal.data = rep.data;
				this.detailModal.show = true;
			});
		},
		selectCameraman() {
			this.$refs['modal'].validate(valid => {
				if (valid) {
					this.showLoading();
					setCameraman({
						orderNumber: this.cameramanModal.data.orderNumber,
						cameramanId: this.cameramanModal.cameramanId
					})
						.then(() => {
							this.hideLoading();
							this.$notify.success('预定成功!');
							this.cameramanModal.data.cameraman = this.cameramanModal.cameramanId;
							this.cameramanModal.show = false;
						})
						.catch(() => {
							this.hideLoading();
						});
				} else {
					console.log('error submit!!');
					return false;
				}
			});
		},
		async showCameraman(item) {
			this.cameramanModal.data = item;
			if (this.cameramanModal.list.length == 0) {
				let result = await getCameraman();
				this.cameramanModal.list = result.data;
			}
			this.cameramanModal.cameramanId = '';
			this.cameramanModal.show = true;
		},
		doPay(item) {
			this.$confirm('是否支付尾款?', '提示', {
				confirmButtonText: '确定',
				cancelButtonText: '取消',
				type: 'warning'
			})
				.then(() => {
					this.showLoading();
					payOrder({ orderNumber: item.orderNumber }).then(() => {
						this.hideLoading();
						this.$notify.success('支付成功！');
						item.status = 2;
					});
				})
				.catch(() => {});
		},
		toDetail(item) {
			if (item.type == 1) {
				this.toPage('newsDetail/' + item.id);
			} else {
				location.href = item.link;
			}
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
			getUserOrderCount(this.searchModel).then(res => {
				this.sum = res.data;
			});
		},
		async getList() {
			this.showLoading();
			this.loading = true;
			const result = await getUserOrder(this.searchModel);
			this.list = result.data;
			this.hideLoading();
			this.loading = false;
		}
	}
};
</script>

<style scoped lang="scss">
.order {
	padding: 60px;
	padding-bottom: 10vw;
	& > .title {
		padding: 60px 0;
	}
	.list {
		.item:nth-child(n + 2) {
			border-top: 1px solid #666666;
		}
		.item {
			font-size: 20px;
			padding: 20px 0;
			transition: 0.5s;
			position: relative;
			.cover {
				width: 10vw;
				img {
					width: 100%;
				}
			}
			.info {
				padding: 0 2vw;
				.order-num {
					.time {
						color: #f1f1f1;
					}
				}
				& > div:nth-child(n + 2) {
					font-size: 1.2vw;
					color: #666;
				}
			}
			.status-info {
				position: absolute;
				bottom: 2vw;
				right: 2vw;
				border-radius: 50%;
			}
			.status {
				margin-left: 1vw;
				padding: 0.5vw 1.5vw;
				border-radius: 50px;
				background: #62d5ff;
				box-shadow: 20px 20px 60px #53b5d9, -20px -20px 60px #71f5ff;
				color: #fff;
			}
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
