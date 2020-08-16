export default {
	data() {
		return {
			loadingObj: null,
		}
	},
	computed:{
		isMobile(){
			return this.$store.state.isMobile;
		}
	},
	methods: {
		showLoading() {
			if (this.loadingObj == null) {
				this.loadingObj = this.$loading({
					lock: true,
					text: 'Loading',
					spinner: 'el-icon-loading',
					background: 'rgba(255, 255, 255, 0.7)'
				});
			}
		},
		hideLoading() {
			if (this.loadingObj != null) {
				this.loadingObj.close();
				this.loadingObj = null;
			}
		},
		showSuccessMsg(msg){
			this.$message.success(msg);
		},
		toPage(url) {
			this.$router.push(url)
		},
	}
}
