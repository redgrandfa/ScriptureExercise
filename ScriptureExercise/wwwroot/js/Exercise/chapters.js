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
    },
    watch: {
        'chapterIdChoosed':{
            immediate:true,
            handler(){
                this.chapterChoosed = this.chapters[this.chapterIdChoosed-1]
            }
        },
    },
    computed: {
        link(){
            return `/Exercise/${scriptureTitle}.${subjectId}/卷${this.chapterChoosed.id}`
        }
    },
    components: {},
});
