import Vue from 'vue'
import App from './App.vue'
import router from '@/router'
import store from './store'
import base from '@/mixins/base'
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import * as filters from './utils/filters'
import '@/utils/extension'
Object.keys(filters).forEach(key => {
  Vue.filter(key, filters[key])
})

Vue.use(ElementUI);


Vue.mixin(base)
Vue.config.productionTip = false


store.commit('loadCache');
new Vue({
	router,
	store,
	render: h => h(App),
	created: () => {
	}
}).$mount('#app')
