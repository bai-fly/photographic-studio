import Vue from 'vue'
import avatar from '@/components/image/avatar.vue'
Vue.component('avatar',avatar)
String.prototype.isNullOrEmpty=function(){
  return this == undefined || this == null || this == ''
}