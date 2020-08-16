import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

import homeLayout from '@/components/layout/home.vue'
import managerLayout from '@/components/layout/manager'

export const constantRoutes = [{
		path: '/',
		redirect: '/index',
		component: homeLayout,
		children: [
			{
				path: 'index',
				name: 'index',
				component: () => import('@/views/index/index'),
			},
			{
				path: 'about',
				name: 'about',
				component: () => import('@/views/index/about'),
			},
			{
				path: 'news',
				name: 'news',
				component: () => import('@/views/index/news'),
			},
			{
				path: 'pricingPackage',
				name: 'pricingPackage',
				component: () => import('@/views/index/pricingPackage'),
			},
			
			{
				path: 'newsDetail/:id',
				name: 'newsDetail',
				component: () => import('@/views/index/newsDetail'),
			},
			{
				path: 'callMe',
				name: 'callMe',
				component: () => import('@/views/index/callMe'),
			},
			{
				path: 'order',
				name: 'order',
				component: () => import('@/views/index/order'),
			},
			{
				path: 'images',
				name: 'images',
				component: () => import('@/views/index/images'),
			},
			{
				path: 'cameramans',
				name: 'cameramans',
				component: () => import('@/views/index/cameramans'),
			},
			{
				path: 'comment',
				name: 'comment',
				component: () => import('@/views/index/comment'),
			},
			
			
		],
		hidden: true
	},
	{
		path: '/404',
		component: () => import('@/views/404'),
		hidden: true
	},
	{
		path: '/login',
		component: () => import('@/views/manager/login'),
		hidden: true
	},
	{
		path: '/regist',
		component: () => import('@/views/manager/regist'),
		hidden: true
	},
	{
		path: '/manager',
		redirect: '/manager/index',
		component: managerLayout,
		meta: {
			title: '首页',
			icon: 'example'
		},
		children: [{
			path: 'index',
			name: 'index',
			meta: {
				title: '首页'
			},
			component: () => import('@/views/manager/index/index'),
		}, ],
		hidden: false
	},
	{
		path: '/manager/home',
		redirect: '/manager/home/index',
		component: managerLayout,
		meta: {
			title: '首页配置',
			icon: 'example'
		},
		children: [{
				path: '/manager/home/imagechange',
				name: 'imagechange',
				meta: {
					title: '图片切换列表'
				},
				component: () => import('@/views/manager/index/imageList'),
			},
			{
				path: '/manager/home/data',
				name: 'indexdata',
				meta: {
					title: '首页数据'
				},
				component: () => import('@/views/manager/index/indexData'),
			}
		],
		hidden: false
	},
	{
		path: '/manager/user',
		redirect: '/manager/user/list',
		component: managerLayout,
		meta: {
			title: '用户管理',
			icon: 'example'
		},
		children: [
			{
				path: '/manager/user/list',
				name: 'userlist',
				meta: {
					title: '会员列表'
				},
				component: () => import('@/views/manager/user/list'),
			},
			{
				path: '/manager/user/cameraman',
				name: 'cameraman',
				meta: {
					title: '摄影师列表'
				},
				component: () => import('@/views/manager/user/cameraman'),
			},
			// {
			// 	path: '/manager/goods/add',
			// 	name: 'goodsadd',
			// 	meta: {
			// 		title: '新增商品'
			// 	},
			// 	component: () => import('@/views/manager/goods/edit'),
			// 	hidden: true
			// },
			// {
			// 	path: '/manager/goods/edit/:id',
			// 	name: 'goodsedit',
			// 	meta: {
			// 		title: '修改商品'
			// 	},
			// 	component: () => import('@/views/manager/goods/edit'),
			// 	hidden: true
			// },
			
			// {
			// 	path: '/manager/goods/categoryadd',
			// 	name: 'goodscategoryadd',
			// 	meta: {
			// 		title: '新增分类'
			// 	},
			// 	component: () => import('@/views/manager/goods/categoryEdit'),
			// 	hidden: true
			// },
			// {
			// 	path: '/manager/goods/categoryedit/:id',
			// 	name: 'goodscategoryedit',
			// 	meta: {
			// 		title: '修改分类'
			// 	},
			// 	component: () => import('@/views/manager/goods/categoryEdit'),
			// 	hidden: true
			// },
			// {
			// 	path: '/manager/goods/goodsAttributeCategory',
			// 	name: 'goodsAttributeCategory',
			// 	meta: {
			// 		title: '商品属性分类'
			// 	},
			// 	component: () => import('@/views/manager/goods/goodsAttributeCategory'),
			// },
			// {
			// 	path: '/manager/goods/goodsAttribute',
			// 	name: 'goodsAttributeCategory',
			// 	meta: {
			// 		title: '商品属性'
			// 	},
			// 	component: () => import('@/views/manager/goods/goodsAttribute'),
			// },
			// {
			// 	path: '/manager/goods/goodsAttributeadd',
			// 	name: 'goodsAttributeadd',
			// 	meta: {
			// 		title: '属性新增'
			// 	},
			// 	component: () => import('@/views/manager/goods/goodsAttributeEdit'),
			// 	hidden: true
			// },
			// {
			// 	path: '/manager/goods/goodsAttributeedit/:id',
			// 	name: 'goodsAttributeedit',
			// 	meta: {
			// 		title: '属性修改'
			// 	},
			// 	component: () => import('@/views/manager/goods/goodsAttributeEdit'),
			// 	hidden: true
			// },
		],
		hidden: false
	},
	{
		path: '/manager/pricingPackage',
		redirect: '/manager/pricingPackage/list',
		component: managerLayout,
		meta: {
			title: '套餐管理',
			icon: 'example'
		},
		children: [{
			path: 'list',
			name: 'pricingPackage',
			meta: {
				title: '套餐管理',
			},
			component: () => import('@/views/manager/pricingPackage/list'),
		}, ],
		hidden: false
	},
	{
		path: '/manager/order',
		redirect: '/manager/order/list',
		component: managerLayout,
		meta: {
			title: '订单管理',
			icon: 'example'
		},
		children: [{
			path: 'list',
			name: 'orderlist',
			meta: {
				title: '订单管理',
			},
			component: () => import('@/views/manager/order/list'),
		}, 
		{
			path: '/manager/order/detail/:id',
			name: 'orderdetail',
			meta: {
				title: '订单详情'
			},
			component: () => import('@/views/manager/order/detail'),
			hidden: true
		},],
		hidden: false
	},
	{
		path: '/manager/about',
		redirect: '/manager/about/index',
		component: managerLayout,
		meta: {
			title: '关于我们',
			icon: 'example'
		},
		children: [{
			path: 'index',
			name: 'editabout',
			meta: {
				title: '关于我们',
			},
			component: () => import('@/views/manager/about'),
		}, ],
		hidden: false
	},
	// {
	// 	path: '/manager/callme',
	// 	redirect: '/manager/callme/list',
	// 	component: managerLayout,
	// 	meta: {
	// 		title: '联系我们',
	// 		icon: 'example'
	// 	},
	// 	children: [{
	// 		path: 'list',
	// 		name: 'callme_list',
	// 		meta: {
	// 			title: '联系我们',
	// 		},
	// 		component: () => import('@/views/manager/callme/list'),
	// 	}, ],
	// 	hidden: false
	// },
	{
		path: '/manager/news',
		redirect: '/manager/news/list',
		component: managerLayout,
		meta: {
			title: '新闻管理',
			icon: 'example'
		},
		children: [
			{
				path: '/manager/news/list',
				name: 'newslist',
				meta: {
					title: '新闻列表',
				},
				component: () => import('@/views/manager/news/list'),
			},
			{
				path: '/manager/news/add',
				name: 'newsadd',
				meta: {
					title: '新增新闻',
				},
				component: () => import('@/views/manager/news/edit'),
				hidden:true,
			},
			{
				path: '/manager/news/edit/:id',
				name: 'newsedit',
				meta: {
					title: '编辑新闻',
				},
				component: () => import('@/views/manager/news/edit'),
				hidden:true,
			}
		],
		hidden: false
	},
]


export const asyncRoutes = [{
	path: '*',
	redirect: '/404',
	hidden: true
}]

const createRouter = () => new Router({
	scrollBehavior: () => ({
		y: 0
	}),
	routes: constantRoutes
})

const router = createRouter()

export function resetRouter() {
	const newRouter = createRouter()
	router.matcher = newRouter.matcher // reset router
}

export default router
