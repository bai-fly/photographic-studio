<template>
	<div class="app-container">
		<el-form :inline="true" size="mini">
			<el-form-item><el-button @click="showEditModal(null)" type="primary">新增</el-button></el-form-item>
		</el-form>
		<el-table v-loading="loading" :data="list" border size="mini" style="width: 100%">
			<el-table-column prop="id" label="id" width="80"></el-table-column>
			<el-table-column prop="orderNumber" label="订单号" width="220"></el-table-column>
			<el-table-column prop="phone" label="手机号" width="130"></el-table-column>
			<el-table-column label="地址">
				<template slot-scope="scope">
					<div>
						<p>{{scope.row.province}}{{scope.row.city}}{{scope.row.area}}</p>
						<p>{{scope.row.address}}</p>
					</div>
				</template>
			</el-table-column>
			<el-table-column label="定金" width="120">
				<template slot-scope="scope">
					&yen;{{ scope.row.deposit }}
				</template>
			</el-table-column>
			<el-table-column label="尾款" width="120">
				<template slot-scope="scope">
					&yen;{{ scope.row.price }}
				</template>
			</el-table-column>
			<el-table-column width="140" label="预约时间">
				<template slot-scope="scope">
					{{ scope.row.time | dateTimeFmt }}
				</template>
			</el-table-column>
			<el-table-column width="140" label="创建时间">
				<template slot-scope="scope">
					{{ scope.row.createDate | dateTimeFmt }}
				</template>
			</el-table-column>
			<el-table-column width="100" label="状态">
				<template slot-scope="scope">
					{{ scope.row.status|orderStatusFmt }}
				</template>
			</el-table-column>
			<el-table-column label="是否联系" width="80">
				<template slot-scope="scope">
					<span @click="isTelChange(scope.row)">
						<el-switch :validate-event='false' v-model="scope.row.isTel" active-color="#13ce66" inactive-color="#ff4949" :active-value='1' :inactive-value='0' />
					</span>
				</template>
			</el-table-column>
			<el-table-column label="是否发布照片" width="80">
				<template slot-scope="scope">
					<span @click="isPublishChange(scope.row)">
						<el-switch :validate-event='false' v-model="scope.row.isPublish" active-color="#13ce66" inactive-color="#ff4949" :active-value='1' :inactive-value='0' />
					</span>
				</template>
			</el-table-column>
			<el-table-column width="80" label="编辑">
				<template slot-scope="scope">
					<div>
						<el-button type="infor" size="mini" @click="toDetail(scope.row)">详情</el-button>
					</div>
					<!-- <div>
						<el-button type="warning" size="mini" @click="showEditModal(scope.row)">修改</el-button>
					</div> -->
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

	</div>
</template>

<script>
import { getOrderList, getOrderCount, setIsTel, setIsPublish } from '@/api/orderService.js';
import { sex } from '@/utils/const';

export default {
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
		toDetail(item){
			this.toPage('detail/'+item.orderNumber)
		},
		isTelChange(item){
			const errStatus = item.isTel == 0 ? 1 : 0
			this.showLoading();
			setIsTel({
				orderNumber: item.orderNumber,
				isTel: item.isTel
			}).then(() => {
				this.hideLoading();
			}).catch(() => {
				item.isTel = errStatus;
				this.hideLoading();
			})
		},
		isPublishChange(item){
			const errStatus = item.isPublish == 0 ? 1 : 0
			this.showLoading();
			setIsPublish({
				orderNumber: item.orderNumber,
				isPublish: item.isPublish
			}).then(() => {
				this.hideLoading();
			}).catch(() => {
				item.isPublish = errStatus;
				this.hideLoading();
			})
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
			getOrderCount(this.searchModel).then(res => {
				this.sum = res.data;
			});
		},
		async getList() {
			this.loading = true;
			const result = await getOrderList(this.searchModel);
			this.list = result.data;
			this.loading = false;
		},
	}
};
</script>

<style scoped>
.cover {
	width: 100px;
}
</style>
