let vue_member_settings = new Vue({
    el:'#vue_member_settings',
    data: {
        fields: {
            name: {
                text: '改名稱',
                value: initData.Name,
            },
            account: {
                text: '改帳號',
                value: initData.Account,
            },
            password: {
                text: '改密碼',
                value: "",
                visible: false,
            },
        },
        errMsgs: {
            name: '',
            account: '',
            password: '',
        },
    },
    mounted() {},
    methods:{
        changeVisible(fieldkey){
            let t = this.fields[fieldkey]
            t.visible = !t.visible
        },
        check(fieldkey){
            let t = this.fields[fieldkey];
            if(!/^\w+$/.test(t.value)
            ){
                this.errMsgs[fieldkey] = "須為英文、數字的組合";
            }
            else if(t.value.length<3 || t.value.length>12){
                this.errMsgs[fieldkey] = "長度須為3~12碼";
            }
            else{
                this.errMsgs[fieldkey] = "";
            }
        },
        post(e , url , dataObj ){
            let dom = e.target
            processingAPI(dom)

            fetchPost(url, dataObj)
            .afterAPI(
                (result)=> {
                    dom.dispatchEvent(apiDoneEvent)
                },
                (result)=> {
                    dom.dispatchEvent(apiDoneEvent)
                },
            )
        },
        postName(e){
            this.post(e, '/ApiMember/UpdateName',{
                name: this.fields.name.value,
            })
        },
        postAccount(e){
            this.post(e, '/ApiMember/UpdateAccount',{
                account: this.fields.account.value,
            })
        },
        postPassword(e){
            this.post(e, '/ApiMember/UpdatePassword',{
                password: this.fields.password.value,
            })
        },
    },
    watch:{
        'fields.name.value':{
            immediate:true,
            handler:function(){
                let t = this.fields.name;
                if(t.value.length==0){
                    this.errMsgs.name = "不可為空";
                }
                else{
                    this.errMsgs.name = "";
                }
            }
        },
        'fields.account.value':{
            immediate:true,
            handler:function(){
                this.check('account')
            }
        },
        'fields.password.value':{
            immediate:true,
            handler:function(){
                this.check('password')
            }
        },
    },
    computed:{},
})
