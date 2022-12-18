let vue_chapters = new Vue({
    el: "#vue_chapters",
    data: {
        sciptureIntro:'《論語》是以春秋時期思想家孔子言行為主的言論彙編，為儒家重要經典之一，在四庫全書中列為「經」部。<br><br>其內容涉及多方面討論，當中包括儒家治國理念、人倫關係、個人道德規範、先秦時期的社會面貌，乃至孔子及其弟子的經歷等。',
        // scriptureCode
        // scriptureTitle: scriptureCode,
        // subjectId: subjectId,
        // subjectChinesePostFix

        // subjectTitle:'',
        // chapters:[],
        chapterIdChoosed:1,
        chapterChoosed:{}, //id , range_ramark
    },
    beforeCreate(){ //比watch早
        let scripture = scriptures_inDB.find(scripture => scripture.title == scriptureTitle)
        // this.sciptureIntro = scripture.intro

        let subject = scripture.subjects.find(subject => subject.id == subjectId)
        this.subjectTitle = getSubjectTitle( scripture , subjectId)
        this.chapters = subject.papers
    },
    created(){},
    beforeMount(){},
    mounted(){},
    methods: {
        navToPaper(){
            location.href=`/Exercise/${scriptureTitle}_${subjectId}/卷${this.chapterChoosed.id}`
            //網址結尾可能有多餘符號故不行 location.href+=`卷${this.chapterChoosed.id}`
        },
    },
    watch: {
        'chapterIdChoosed':{
            immediate:true,
            handler(){
                this.chapterChoosed = this.chapters[this.chapterIdChoosed-1]
            }
        }
    },
    computed: {},
    components: {},
});
