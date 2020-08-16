<!-- 镂空字体 -->
<template>
	<div class="text-view" :class="bg?bg:'bg-primary'" :style="cStyle">
		<slot></slot>
	</div>
</template>

<script>
	export default {
		props:{
			bg:{
				type:String,
				default:null,
			}
		},
		computed:{
			cStyle(){
				return `display: inline-block;-webkit-background-clip:text;-webkit-text-fill-color: transparent;`
			}
		}
	}
</script>
<style scoped lang="scss">
	@keyframes textAmimation{
		0% {
			background-position-x: 0;
		}
		50%{
			background-position-x: 100%;
		}
		100% {
			background-position-x: 0;
		}
	}
	.text-view{
    margin: 0;
    padding: 0;
		display: inline-block;
		animation: textAmimation 10s linear;
		animation-iteration-count:infinite;
		box-shadow:none !important;
		background-position-x: 0;
		background-size: 300% !important;
	}
</style>