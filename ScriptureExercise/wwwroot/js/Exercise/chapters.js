let vue_chapters = new Vue({
    el: "#vue_chapters",
    data: {
        // scriptureCode
        // scriptureTitle: scriptureCode,
        // sciptureIntro:'',
        // subjectId: subjectId,
        // subjectChinesePostFix

        // subjectTitle:'',
        // chapters:[],
        chapterIdChoosed:1,
        chapterChoosed:{}, //id , range_ramark
    },
    beforeCreate(){ 
        let scripture = scriptures_inDB.find(scripture => scripture.title == scriptureTitle)
        this.sciptureIntro = scripture.introHtml

        let subject = scripture.subjects.find(subject => subject.id == subjectId)
        this.subjectTitle = getSubjectTitle( scripture , subjectId)
        this.chapters = subject.papers
    },
    created(){ 
    },
    beforeMount(){
    },
    mounted(){
    },
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
        },
    },
    computed: {},
    components: {},
});
