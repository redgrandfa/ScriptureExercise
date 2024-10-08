let vue_login = new Vue({
    el:'#vue_login',
    data:{
        fields:{
            account:{
                text:'帳號',
                value:'',
            },
            password:{
                text:'密碼',
                value:'',
                visible:false,
            },
        },
        errMsgs:{
            account:'',
            password:'',
        },
        disabled:true,
    },
    mounted(){},
    methods:{
        changeVisible(fieldkey) {
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
        register(e){
            let dom = e.target
            processingAPI(dom)

            fetchPost('/ApiAccount/Register' , {
                account:this.fields.account.value,
                password:this.fields.password.value,
            })
            .afterAPI(
                (result)=>{
                    setTimeout(
                        ()=>{
                            dom.dispatchEvent(apiDoneEvent)
                            if(returnUrl == ''
                            ){
                                location.href='/'
                            }
                            else{
                                location.href=returnUrl
                            }
                        }
                    ,3000)
                },
                (result)=>{
                    dom.dispatchEvent(apiDoneEvent)
                }
            )
        },
        login(e){
            let dom = e.target
            processingAPI(dom)

            fetchPost('/ApiAccount/Login' , {
                account:this.fields.account.value,
                password:this.fields.password.value,
            })
            .afterAPI(
                ()=>{
                     dom.dispatchEvent(apiDoneEvent)
                    if(returnUrl == ''
                    ){
                        location.href='/'
                    }
                    else{
                        location.href=returnUrl
                    }
                },
                ()=>{
                    dom.dispatchEvent(apiDoneEvent)
                }
            )
        },
    },
    computed: {
        fieldPasswordType(){
            return this.fields.password.visible?'text':'password'
        }
    },
    watch:{
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
        errMsgs:{
            deep:true,
            immediate:true,
            handler:function(){
                //有一個沒過 就沒過
                this.disabled = Object.values(this.errMsgs)
                    .some( errMsg => errMsg != "" );
            }
        }
    },
})