let vue_paper = new Vue({
    el:'#vue_paper',
    data: {
        paper: {
            "title": "",
            "range_remark": "",
            "questions": [
                {
                    "id":1,
                    "order": 1,
                    "type": 1,
                    "stem": "",
                    "choices": [],
                    "answer": 0,
                    choosed: null,
                },
                {
                    "id": 2,
                    "order": 1,
                    "type": 2,
                    "stem": "",
                    "answer": "",
                    reply: null,
                },
                {
                    "id": 3,
                    "order": 1,
                    "type": 3,
                    "stems":[''],//['子曰：恭而無禮則勞,慎而','則葸,勇而','則亂,直而無禮。'],
                    //題目一定比空格先出現
                    "answers":[''],
                    replies:[],
                },
            ],
        },
    },
    mounted() {
        fetch( paperJsonFileName_To_DataSource(jsonFileName) )
        .afterFetch(resp => {
            resp.json()
            .then(JSobj => {
                this.paper = JSobj
                //this.paper.questions.map(x=> x.choosed = null)
                // reply replies... replies需要先有陣列 才能叫索引
            })
        })
    },
    computed:{},
    methods:{
        postPaper(e){
            let dom = e.target
            processingAPI(dom)

            let questionTypes_Counter = {
                type1:{ correct:0 , done:0 ,weight:1},
                type2:{ correct:0 , done:0 ,weight:10},
                type3:{ correct:0 , done:0 ,weight:1},
            }
            let replies = []

            this.paper.questions.forEach( (q,i) => {
                //選擇
                if (q.type == 1) {
                    replies.push({
                        id: q.id,
                        choosed: q.choosed,
                    });
                    questionTypes_Counter.type1.done++;
                    if(q.answer == q.choosed){
                        questionTypes_Counter.type1.correct++;
                    }
                }
                //簡答
                else if (q.type == 2){ //&& q.reply
                    replies.push({
                        id: q.id,
                        reply: q.reply,
                    });
                    questionTypes_Counter.type2.done++;
                    if(q.answer == q.reply){
                        questionTypes_Counter.type2.correct++;
                    }
                }
                //填充
                else if (q.type == 3) {
                    replies.push({
                        id: q.id,
                        replies: q.replies,
                    });
                    q.answers.forEach( (answer,i) =>{
                        questionTypes_Counter.type3.done++;
                        if(answer == q.replies[i]){
                            questionTypes_Counter.type3.correct++;
                        }
                    })
                }
            });

            //批改 打分 
            let denominator = 0
            let score = 0
            let percentScore = 0
            for(let key in questionTypes_Counter){
                let type = questionTypes_Counter[key]
                denominator += type.done* type.weight
                // this.paper.score += type.correct* type.weight
                score += type.correct* type.weight
            }
    
            percentScore = Math.round(score*100/denominator)



            //呼叫API 儲存 答題記錄 更新會員成就統計
            fetchPost('/ApiExercise/PostPaper',{
                recordCreate: {
                    exerciseJsonFileName: jsonFileName,
                    //paperName: this.paper.title,
                    ReplyJSON: JSON.stringify(replies),
                    score: score,
                    percentScore: percentScore,
                },
                memberUpdate: {
                    choicesQuestion_Correct: questionTypes_Counter.type1.correct,
                    essayQuestion_Correct: questionTypes_Counter.type2.correct,
                    blankFillQuestion_Correct: questionTypes_Counter.type3.correct,
                    choicesQuestion_Done:  questionTypes_Counter.type1.done,
                    essayQuestion_Done:  questionTypes_Counter.type2.done,
                    blankFillQuestion_Done:  questionTypes_Counter.type3.done,
                },
            }).afterAPI(
                (result)=> {
                    window.location.href = `/Exercise/Record/${result.payload}`;
                    // dom.dispatchEvent(apiDoneEvent)
                },
                (result)=> {
                    dom.dispatchEvent(apiDoneEvent)
                },
            )
        }
    },
    watch:{},
})
