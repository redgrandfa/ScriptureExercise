let vue_member_settings = new Vue({
    el:'#vue_member_settings',
    data: data,
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
        post(url , dataObj ){
            fetch(url,{
                method:'post',
                headers:{
                    'content-type':'application/json;charset=utf-8',
                },
                body:JSON.stringify( dataObj )
            })
            .then(resp=>{
                Promise.resolve(resp.text())
                .then( text => {
                    if(resp.Ok){
                        swal.fire('修改成功' +text)
                    }else{
                        swal.fire(text)
                    }
                })
            })
        },
        postName(){
            this.post('/ApiMember/UpdateName',{
                name: this.fields.name.value,
            })
        },
        postAccount(){
            this.post('/ApiMember/UpdateAccount',{
                account: this.fields.account.value,
            })
        },
        postPassword(){
            this.post('/ApiMember/UpdatePassword',{
                password: this.fields.password.value,
            })
        },
        postScripturesShow(){
            this.post('/ApiMember/UpdateScripture',{
                ScriptureShowList: this.fields.scripturesShow.value
                    .filter(s=>s.show)
                    .map(s=>s.id),
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
        'fields.scripturesShow.value':{
            immediate:true,
            deep:true,
            handler:function(){
                let t = this.fields.scripturesShow.value;
                if( t.some(s=> s.show ) ){
                    this.errMsgs.scripturesShow = "";
                }
                else{
                    this.errMsgs.scripturesShow = "須至少勾選一部經典";
                }
            }
        },
    },
    computed:{},
})
