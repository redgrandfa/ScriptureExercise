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
        //會員對其收藏狀況(controller來?也已有)
        let collectList = []
        //先載入 所有經典資料(已有) 要把科目攤平?深拷貝?
        scriptures_inDB.forEach( scripture => {
            let getSubjectChinesePostFix = ()=>''
            if (scripture.subjects.length > 1){
                getSubjectChinesePostFix = (num)=>`(${number_To_Chinese(num)})`
            }
            scripture.subjects.forEach(subject=>{
                let subjectToAdd = {
                    scripture: scripture.code, 
                    //網址要中文嗎
                    scriptureTitle: scripture.title,
                    id:subject.id,
                    //網址要中文嗎
                    // subjectChinesePostFix: getSubjectChinesePostFix(subject.id),
                    
                    title:`${scripture.title}${getSubjectChinesePostFix(subject.id)}`,
                    author: scripture.author,
                    belongTo: scripture.belongTo,
                    isCollected: false //collectList.contains
                }
                // subjectToAdd.title=subjectToAdd.scripture + subjectToAdd.subjectChinesePostFix

                this.subjects.push(subjectToAdd)
            })
        })
        this.subjectsShow = this.subjects
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
        navToSubject(subject){
            location.href=`/Exercise/${subject.scriptureTitle}_${subject.id}`
        }
        ,toggleCollect(subject){ //從show來的  、API互動
            subject.isCollected = !subject.isCollected
            // APi

            // fetch('',{data:subject.isCollected})
            // .then(resp => {
            //     if(resp.ok){

            //     }else{
            //         // 異常
            //     }
            // })
        },
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
