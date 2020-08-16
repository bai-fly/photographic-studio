<template>
	<div class="image-change" :style="cStyle+'--timer:'+timer/1000+'s'">
		<div v-for='(item,index) in images' :class="active==index?'active':''" :key='index' :style="cStyle+'background-image: url('+item+');'"></div>
	</div>
</template>

<script>
	export default {
		props: {
			width: {
				type: Number,
			},
			height: {
				type: Number,
			},
			images: {
				type: Array
			},
			timer: {
				type: Number,
				default: 5000
			}
		},
		data() {
			return {
				active: 0,
				interval: null,
			}
		},
		computed: {
			cStyle() {
				return `width:${this.width}px;height:${this.height}px;`
			}
		},
		destroyed() {
			if (this.timer) {
				clearInterval(this.interval);
			}
		},
		created() {
			if (this.interval == null) {
				this.interval = setInterval(() => {
					if (this.active == this.images.length - 1) {
						this.active = 0;
					} else {
						this.active++;
					}
				}, this.timer)
			}
		}
	}
</script>

<style scoped lang="scss">
	.image-change {
		position: relative;
		display: flex;
		align-items: center;
		justify-content: center;
		overflow: hidden;

		&>div {
			position: absolute;
			opacity: 0;
			background-size: cover;
			background-position: center;
			transition: 1s;
		}

		@keyframes itemAnimation {
			0% {
				transform: scale(1);
			}

			100% {
				transform: scale(1.3);
			}
		}

		&>div.active {
			opacity: 1;
			animation: itemAnimation var(--timer);
		}
	}
</style>
