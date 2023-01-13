let vue_chapters = new Vue({
    el: "#vue_chapters",
    data: {
        // sciptureIntro:'',

        // subjectTitle:'',

        // chapters:[],
        chapterIdChoosed:1,
        chapterChoosed:{}, //id , range_ramark
    },
    beforeCreate(){ 
        // let scripture = scriptures_inDB.find(scripture => scripture.title == scriptureTitle)
        let scripture = scriptures_inDB.find(scripture => scripture.code == scriptureCode)
        this.sciptureIntro = scripture.introHtml


        this.subjectTitle = getSubjectTitle_fromDB(scripture , subjectId)
        
        let subject = scripture.subjects.find(subject => subject.id == subjectId)
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
