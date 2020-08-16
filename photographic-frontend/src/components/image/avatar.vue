<template>
	<div :style="avatarStyle" style="display: inline-block;"></div>
</template>

<script>
	export default{
		props:{
			avatar:{
				type:String
			},
			size:{
				type:Number,
				default:100
			}
		},
		computed:{
			avatarStyle(){
				let style = 'background-size: cover;background-image:url('+this.avatar+');'
				style+=`width:${this.size}px;height:${this.size}px;`
				style+='border-radius: '+(this.size/2)+'px;';
				return style;
			}
		}
	}
</script>

