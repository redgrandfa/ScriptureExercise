let vue_subjects = new Vue({
    el: "#vue_subjects",
    data: {
        searchFor:'',
        categoryChecked: {
            'Bai':false,
            'Ru':false,
            'Si':false,
            'Dao':false,
        },
        subjects:[],
        subjectsShow:[],
    },
    beforeMount(){
        //載入 所有經典資料  攤平成科目
        scriptures_inDB.forEach( scripture => {
            scripture.subjects.forEach(subject=>{
                let subjectToAdd = {
                    scripture: scripture.code, 
                    scriptureTitle: scripture.title,
                    author: scripture.author,
                    belongTo: scripture.belongTo,

                    id:subject.id,
                    title: getSubjectTitle(scripture.code , subject.id),
                    isCollected: false 
                }

                this.subjects.push(subjectToAdd)
            })
        })
        this.subjectsShow = this.subjects

        //有登入 -> 取書單狀況
        if(isAuthenticated){
            fetch('/ApiMember/GetSubjectCollectList')
            .afterFetch( (resp)=>{
                resp.json()
                .then( respBody =>  {
                    let collectList = respBody.payload
    
                    // console.log(collectList)
                    collectList.forEach( subjectCode => {
                        let scripture = subjectCode[0]
                        let id = subjectCode[1]
                        // if( id== undefined) id = 1
    
                        let subject = this.subjects.find(subject => 
                            subject.scripture == scripture  
                            && subject.id == id
                        )
                        subject.isCollected = true
                    })
                })
            })
        }
    },
    methods: {
        filtSubject(){
            let result = this.subjects
            if(this.searchFor != ''){
                result = result.filter( x=> x.title.includes(this.searchFor) )
            }

            let categoryCkeckedlist = Object.keys(this.categoryChecked).filter(k => this.categoryChecked[k])
            if( categoryCkeckedlist.length > 0){
                result = result.filter( x=>  categoryCkeckedlist.includes(x.belongTo))
            }

            this.subjectsShow = result
        },
        toggleCollect(subject){
            let collectStatus = subject.isCollected
            fetchPost('/ApiMember/ToggleSubjectCollect',{
                subjectCode:`${subject.scripture}${subject.id}`,
                collectStatus:collectStatus,
            })
            .afterAPI(
                (result)=>{
                    subject.isCollected = !collectStatus
                    // dom.dispatchEvent(apiDoneEvent)
                },
                (result)=>{
                    // dom.dispatchEvent(apiDoneEvent)
                },
            )
        }
    },
    watch: {
        'searchFor':{
            immediate:false,
            handler(){
                this.filtSubject()
            }
        },
        'categoryChecked':{
            immediate:false,
            deep:true,
            handler(){
                this.filtSubject()
            }
        },
    },
    computed: {},
});
