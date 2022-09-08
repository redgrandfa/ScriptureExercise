let vue_untracked = new Vue({
    el:'#vue_untracked',
    data:data,
    mounted(){},
    methods:{
        changeBinkeyVisible() {
            let t = this.fields.bindKey
            t.visible = !t.visible
        },
        submit(){
            fetch(this.formAction , {
                method:'post',
                headers:{
                    'content-type':'application/json'
                },
                body:JSON.stringify({
                    bindKey:this.fields.bindKey.value,
                }),
            }).then(resp=>{
                Promise.resolve(resp.text())
                .then(text=>{
                    if(resp.ok){
                        location.href="/Member/Settings"
                    }else{
                        swal.fire(text)
                    }
                })
            })
        }
    },
    computed: {
        formAction() {
            let prefix = "/ApiAccount/"
            let postfix = this.isMember ? 'BindMember' : 'CreateMember'
            return prefix + postfix
        }
    },
    watch:{
        'fields.bindKey.value':{
            immediate:true,
            handler:function(){
                let t = this.fields.bindKey;
                if(!/^\w+$/.test(t.value)
                ){
                    this.errMsgs.bindKey = "須為英文、數字的組合";
                }
                else if(t.value.length<3 || t.value.length>12){
                    this.errMsgs.bindKey = "長度須為3~12碼";
                }
                else{
                    this.errMsgs.bindKey = "";
                }
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