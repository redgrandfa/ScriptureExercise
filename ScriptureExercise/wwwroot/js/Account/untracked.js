let vue_untracked = new Vue({
    el:'#vue_untracked',
    data:data,
    mounted(){},
    methods:{
        // changeVisible(){
        //     this.bindKey_visible = !this.bindKey_visible
        // },
        // CheckKeysMatch(){
        //     if(this.fields.bindKey.value!= 
        //         this.fields.bindKeyCheck.value){

        //         this.errMsgs.bindKeyCheck = "兩者不相符 ";
        //     }
        //     else{
        //         this.errMsgs.bindKeyCheck = "";
        //     }
        // },
    },
    watch:{
        'fields.bindKey.value':{
            immediate:true,
            handler:function(){
                if(!/^\w+$/.test(this.fields.bindKey.value)
                ){
                    this.errMsgs.bindKey = "須為英文、數字的組合";
                }
                else if(this.fields.bindKey.value.length<6 ||
                    this.fields.bindKey.value.length>12){
                    this.errMsgs.bindKey = "長度須為6~12碼";
                }
                else{
                    this.errMsgs.bindKey = "";
                }

                //this.CheckKeysMatch()
            }
        },
        // 'fields.bindKeyCheck.value':{
        //     immediate:true,
        //     handler:function(){
        //         this.CheckKeysMatch()
        //     }
        // },
        errMsgs:{
            deep:true,
            immediate:true,
            handler:function(){
                let b = Object.values(this.errMsgs)
                    .some( errMsg => errMsg != "" )
                
                //有一個沒過 就沒過
                this.disabled = b;
            }
        }


    },
    computed:{
        formAction(){
            let prefix = "/Account/"
            let postfix = this.isMember?'BindMember':'CreateMember'
            return prefix+postfix
        }
    },
})