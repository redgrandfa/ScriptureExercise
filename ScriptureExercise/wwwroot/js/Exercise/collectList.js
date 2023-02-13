let vue_collectList = new Vue({
    el: "#vue_collectList",
    data: {
        subjects: [],
    },
    beforeMount() {
        //取書單狀況
        fetch("/ApiMember/GetSubjectCollectList").afterFetch((resp) => {
            resp.json().then((respBody) => {
                let collectList = respBody.payload;

                //照收藏順序的寫法
                collectList.forEach( subjectCode => {
                    let scriptureCode = subjectCode[0]
                    let subjectId = parseInt(subjectCode[1])
                    // let subjectId = subjectCode[1]

                    let scripture = scriptures_inDB
                        .find(scripture => scripture.code == scriptureCode)

                    let subject = scripture.subjects
                        .find(subject => subject.id == subjectId)

                    let subjectToAdd = {
                        scripture: scripture.code,
                        scriptureTitle: scripture.title,
                        coverImgFile:scripture.coverImgFile,
                        author: scripture.author,
                        belongTo: scripture.belongTo,
    
                        id: subject.id,
                        title: getSubjectTitle(scripture.code, subject.id),
                        isCollected: true,
                    };
    
                    this.subjects.push(subjectToAdd);
                })

                //載入 所有經典資料  攤平成科目
                // scriptures_inDB.forEach((scripture) => {
                //     scripture.subjects.forEach((subject) => {
                //         let subjectCode = `${scripture.code}${subject.id}`
        
                //         //只需要有收藏的
                //         if(collectList.includes(subjectCode)){
                //             let subjectToAdd = {
                //                 scripture: scripture.code,
                //                 scriptureTitle: scripture.title,
                //                 coverImgFile:scripture.coverImgFile,
                //                 author: scripture.author,
                //                 belongTo: scripture.belongTo,
            
                //                 id: subject.id,
                //                 title: getSubjectTitle(scripture.code, subject.id),
                //                 isCollected: true,
                //             };
            
                //             this.subjects.push(subjectToAdd);
                //         }
                //     })
                // });
            });
        });
    },
    methods: {
        toggleCollect(subject) {
            fetchPost("/ApiMember/ToggleSubjectCollect", {
                subjectCode: `${subject.scripture}${subject.id}`,
                collectStatus: true,
            }).afterAPI(
                (result) => {
                    let idx = this.subjects.findIndex(s=>s==subject)
                    this.subjects.splice(idx, 1)
                },
                (result) => {
                }
            );
        },
        coverStyle(coverImgFile){
            coverImgPath = '../../images/'+coverImgFile
            return `background-image:url('${coverImgPath}')`
        }
    },
    watch: {},
    computed: {},
});
