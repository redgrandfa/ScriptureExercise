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

                //載入 所有經典資料  攤平成科目
                scriptures_inDB.forEach((scripture) => {
                    scripture.subjects.forEach((subject) => {
                        let subjectCode = `${scripture.code}${subject.id}`
        
                        //只需要有收藏的
                        if(collectList.includes(subjectCode)){
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
                        }
                    })
                });
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
