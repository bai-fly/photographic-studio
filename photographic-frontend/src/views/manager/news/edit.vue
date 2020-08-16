<template>
  <div class="app-container">
    <el-form ref="editForm" :model="model" :rules="rules" size="mini" label-width="120px">
      <el-form-item label="新闻标题" prop="title">
        <el-input v-model="model.title" />
      </el-form-item>
      <el-form-item v-if='false' label="封面" prop="cover">
        <uploadImageList v-model='model.cover'></uploadImageList>
      </el-form-item>
      <el-form-item v-if='false' label="分类">
        <el-select v-model="model.category" placeholder="请选择分类">
          <el-option v-for="item in category" :key="item.id" :label="item.name" :value="item.id" />
        </el-select>
      </el-form-item>
      <el-form-item label="类型">
        <el-radio v-model="model.type" :label="1" border>文章</el-radio>
        <el-radio v-model="model.type" :label="2" border>链接</el-radio>
      </el-form-item>
      <el-form-item v-show='model.type==1' label="内容">
        <tinymce v-model="model.content" :height="600" />
      </el-form-item>
      <el-form-item v-show='model.type==2' label="链接">
        <el-input v-model="model.link" placeholder="例:https://www.baidu.com" />
      </el-form-item>
      <el-form-item>
        <center>
          <el-button v-loading.fullscreen.lock="loading" type="primary" @click="save">保存</el-button>
        </center>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
  import {
    getNewsDetail,
    editNews
  } from '@/api/newsService.js'
  // import uploadImageList from '@/components/uploadImageList'
import Tinymce from '@/components/Tinymce'
  export default {
    components: {
      // uploadImageList,
      Tinymce
    },
    data() {
      return {
        category:[],
        model: {
          id: null,
          category:0,
          title: '',
          cover:'',
          content: '',
          isHot:0,
          order: 0,
          type: 1,
          link:'',
        },
        rules: {
          title: [{
            required: true,
            message: '请输入新闻标题',
            trigger: 'blur'
          }],
          cover:[{
            required: true,
            message: '请选择封面',
            trigger: 'blur'
          }]
        },
        loading: false,

      }
    },
    created() {
      // getNewsCategorys().then(rep=>{
      //   this.category = rep.data;
      // })
      if (this.$route.params.id) {
        getNewsDetail({
          id: this.$route.params.id
        }).then(rep => {
          this.model=rep.data
        })
      }
    },
    methods: {
      save() {
        this.$refs['editForm'].validate((valid) => {
          if (valid) {
            this.loading = true
            let model = JSON.parse(JSON.stringify(this.model))
            if(model.type==1){
              model.link = ''
            }else{
              model.content = ''
            }
            editNews(model).then(()=>{
                this.loading = false
                this.$message({
                  showClose: true,
                  message: '保存成功',
                  type: 'success'
                })
            }).catch(e=>{
				console.log(e)
              this.loading = false
            })
          } else {
            console.log('error submit!!')
            return false
          }
        })
      }
    }
  }
</script>

<style scoped>

</style>
