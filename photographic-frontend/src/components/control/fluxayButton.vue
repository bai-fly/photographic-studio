<template>
  <button class="fluxa-button" :style="'--lineColor:'+color">
    <slot></slot>
    <span class="line"></span>
    <span class="line"></span>
    <span class="line"></span>
    <span class="line"></span>
  </button>
</template>

<script>
  export default {
    props:{
      color:{
        type:String,
        default:'rgba(235, 251, 255, 1.0)'
      }
    }
  }
</script>

<style scoped lang="scss">
  .fluxa-button{
    display: inline-block;
    padding: 10px;
    border-radius: 5px;
    position: relative;
    overflow: hidden;
    border: none;
    $lineBg:var(--lineColor);
    background-color: transparent;
    outline: none;
    .line{
      position: absolute;
      z-index: 10;
    }
    @keyframes line1Animation{
      50%,100%{
        left:100%;
      }
    }
    .line:nth-child(1){
      background: linear-gradient(90deg,transparent,$lineBg);
      width: 100%;
      height: 3px;
      left: -100%;
      top: 0;
      animation: line1Animation 1s 0s linear infinite;
    }
    @keyframes line2Animation{
      50%,100%{
        top:100%;
      }
    }
    .line:nth-child(2){
      background: linear-gradient(0deg,$lineBg,transparent);
      width: 3px;
      height: 100%;
      left: 100%;
      top: -100%;
      // margin-left: 100%;
      transform: translate(-100%,0);
      animation: line2Animation 1s .25s linear infinite;
    }
    @keyframes line3Animation{
      50%,100%{
        left:-100%;
      }
    }
    .line:nth-child(3){
      background: linear-gradient(90deg,$lineBg,transparent);
      width: 100%;
      height: 3px;
      left: 100%;
      top: 100%;
      transform: translate(0,-100%);
      animation: line3Animation 1s 0.5s linear infinite;
    }
    @keyframes line4Animation{
      50%,100%{
        top:-100%;
      }
    }
    .line:nth-child(4){
      background: linear-gradient(0deg,transparent,$lineBg);
      width: 3px;
      height: 100%;
      left: 0%;
      top: 100%;
      // margin-left: 100%;
      transform: translate(0%,0);
      animation: line4Animation 1s 0.75s linear infinite;
    }
  }
  .fluxa-button:hover{
    cursor: pointer;
    box-shadow: 0px 0px 5px 2px #FFFFFF, 5px 5px 5px 5px var(--lineColor);
  }
  .fluxa-button:active{
    border: none;
  }
</style>
