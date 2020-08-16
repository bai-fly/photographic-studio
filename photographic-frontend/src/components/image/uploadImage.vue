<template>
  <el-upload class="upload" :style='sizeStyle' :action="UPLOAD_IMAGE" :show-file-list='false' :before-upload="beforeAvatarUpload" :on-success="handleSuccess">
    <img v-if="value" :src="value" class="avatar" :style='sizeStyle'>
    <i v-else class="el-icon-plus avatar-uploader-icon" :style='sizeStyle'></i>
  </el-upload>
</template>

<script>
  import config from '@/utils/config'
  export default {
    props: {
      size: {
        type: Number,
        default: 140
      },
      width: {
        type: Number,
      },
      height: {
        type: Number
      },
      value: {
        type: String
      }
    },
    data() {
      return {
        UPLOAD_IMAGE:config.UPLOAD_IMAGE,
      }
    },
    computed: {
      sizeStyle() {
        const width = this.width ? this.width : this.size;
        const height = this.height ? this.height : this.size;
        return `width:${width}px;height:${height}px;line-height:${height}px`
      }
    },
    methods: {
      beforeAvatarUpload() {
        // if(this.fileType.findIndex(m=>m==file.type)==-1){
        // 	 this.$message.error('文件类型不正确');
        // 	 return false;
        // }
        return true;
      },
      handleSuccess(response) {
        if (response.code === 1) {
          this.$emit('input',response.data);
        }
      },
    },
  }
</script>

<style lang="scss" scoped>
  .upload {
    display: inline-block;
    text-align: center;
    border: 1px dashed #d9d9d9;
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
  }

  .upload:hover {
    border-color: #409EFF;
  }
</style>
