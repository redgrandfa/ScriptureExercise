let vue_member_settings = new Vue({
    el:'#vue_member_settings',
    data:data,
    mounted(){
        // 後端傳過來 總答題數、答對題數

    },
    methods:{
        changeVisible(){
            this.bindKey_visible = !this.bindKey_visible
        },
        submit(){
            fetch('/api/Member/Edit',{
                method:'post',
                headers:{
                    'content-type':'application/json;charset=utf-8',
                },
                body:JSON.stringify({
                    name:this.name,
                    memberId:this.memberId,
                })
            }).then(resp=>resp.text())
            .then(msg => alert(msg) )
        }
    },
    watch:{},
    computed:{},
})
