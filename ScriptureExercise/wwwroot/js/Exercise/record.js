let vue_record = new Vue({
    el: '#vue_record',
    data: {
        //從DB寫死直接傳來
        percentScore:0,
        score:0,
        //從DB寫死直接傳來

        filters:{
            onlyWrong_Checked:false,
            onlyDone_Checked:false,
        },
        showQuestions: [],
        paper:{
            info:{
                // scriptureTitle:'論語',
                // subjectId:0,
                subjectTitle:'論語(一)',
                paperId:0,
                "range_remark": "(1-3)",
            },
            "questions": [
                {
                    "id":1,
                    "order": 1,
                    "type": 1,
                    "stem": "未選",
                    "choices": ['q','w'],
                    "answer": 1,
                },
                {
                    "id":2,
                    "order": 2,
                    "type": 1,
                    "stem": "選錯",
                    "choices": ['q','w'],
                    "answer": 0,
                    choosed: 1,
                },
                {
                    "id":3,
                    "order": 3,
                    "type": 1,
                    "stem": "答對",
                    "choices": ['q','w'],
                    "answer": 1,
                    choosed: 1,
                },
                {
                    "id": 4,
                    "order": 4,
                    "type": 2,
                    "stem": "未答",
                    "answer": "qwe",
                },
                {
                    "id": 5,
                    "order": 5,
                    "type": 2,
                    "stem": "答錯",
                    "answer": "qwe",
                    reply: "q",
                },
                {
                    "id": 6,
                    "order": 6,
                    "type": 2,
                    "stem": "答對",
                    "answer": "qwe",
                    reply: 'qwe',
                },
                // {
                //     "id": 3,
                //     "order": 1,
                //     "type": 3,
                //     "stems":[''],//['子曰：恭而無禮則勞,慎而','則葸,勇而','則亂,直而無禮。'],
                //     //題目一定比空格先出現
                //     "answers":[''],
                //     replys:[],
                // },
            ],
        },
        link_exerciseAgain:'',
        link_chapters:'',
    },
    mounted() { 
        fetch(`/ApiExercise/GetRecord/${createTimeId}`)
            .afterFetch( resp => {
                resp.json()
                .then(result => {
                    if(result.status > 0){ //API不一定要顯示訊息?
                        swalError.fire({
                            text:result.message
                        }) 
                        return 
                    }
                    else{
                        let record = result.payload
                        this.score = record.score
                        this.percentScore = record.percentScore


                        let info = paperJsonFilePath_To_paperInfo(record.fK_JsonFileName)
                        this.paper.info = info
                        
                        this.link_chapters = `/Exercise/${info.scriptureTitle}.${info.subjectId}`
                        this.link_exerciseAgain =  this.link_chapters + `/卷${info.paperId}`
    
                        //題庫
                        this.getPaperDetailByRecord(record)
                    }
                })
            })
    },
    methods: {
        getPaperDetailByRecord(record){
            fetch(paperJsonFilePath_To_dataSource(record.fK_JsonFileName) )
            .afterFetch( resp => {
                resp.json()
                .then(paper => {
                    this.paper.info.range_remark = paper.range_remark
                    this.paper.questions = []
                    let replies = JSON.parse(record.replyJSON)
                    //[ {id:1 , choosed:1}]//長這樣 

                    replies.forEach( (r,i) => {
                        //找到此回答對應的題目 //如果未來有刪題目機制 使用軟刪除
                        let q = paper.questions.find(q => q.id == r.id) 
                        if(q.type == 1){
                            // 新舊版適配
                            if(r.choosed==undefined){ 
                                q.choosed = r.chooesd //舊>>錯字esd
                            }else{
                                q.choosed = r.choosed
                            }
                        }
                        else if(q.type == 2){
                            q.reply = r.reply 
                        }
                        else if(q.type == 3){
                            q.replies = r.replies 
                        }
                        this.paper.questions.push(q)
                    })
                    this.show()
                })
            })
        },
        show(){
            let result = this.paper.questions
            if(this.filters.onlyWrong_Checked){
                result = result.filter(q => 
                    (q.type==1 && q.answer!= q.choosed)
                    ||(q.type==2 && q.answer!= q.reply)
                )
            }

            if(this.filters.onlyDone_Checked){
                result = result.filter(q => 
                    (q.type==1 && typeof q.choosed == 'number' )
                    ||(q.type==2 && typeof q.reply == 'string' && q.reply.length>0)
                    // ||(q.type==3 )
                )
            }

            this.showQuestions = result
        },
    },
    watch: {
        "filters":{
            immediate:true,
            deep:true,
            handler:function(){
                this.show()
            },
        }
    },
    computed: {},
    components: {},
})
