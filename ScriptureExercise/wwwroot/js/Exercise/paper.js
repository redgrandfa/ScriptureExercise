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
                    chooesd: null,
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
            .then(resp => resp.json())
            .then(JSobj => {
                this.paper = JSobj
                //this.paper.questions.map(x=> x.choosed = null)
                // reply replies... replies需要先有陣列 才能叫索引
            })
    },
    computed:{},
    methods:{
        postPaper(){
            let type1_Correct = 0,
                type2_Correct = 0,
                type3_Correct = 0,
                replies = []

            this.paper.questions.forEach( (q,i) => {
                if (q.type == 1) {
                    replies.push({
                        id: q.id,
                        chooesd: q.chooesd,
                    });
                    if(q.answer == q.chooesd){
                        type1_Correct++;
                    }
                }
                else if (q.type == 2){ //&& q.reply
                    replies.push({
                        id: q.id,
                        reply: q.reply,
                    });
                    if(q.answer == q.reply){
                        type2_Correct++;
                    }
                }
                else if (q.type == 3) {
                    replies.push({
                        id: q.id,
                        replies: q.replies,
                    });
                    q.answers.forEach( (answer,i) =>{
                        if(answer == q.replies[i]){
                            type3_Correct++;
                        }
                    })
                }
            });

            //批改 打分 
            this.paper.score = type1_Correct+ type3_Correct+ type2_Correct*10
            this.paper.percentScore = Math.round(this.paper.score*100/this.paper.questions.length)


            //呼叫API 儲存 答題記錄 更新會員成就統計
            fetch('/ApiExercise/PostPaper',{
                method:'post',
                headers:{
                    'content-type':'application/json;utf-8',
                },
                body: JSON.stringify({
                    memberUpdate: {
                        choicesQuestion_Correct: type1_Correct,
                        essayQuestion_Correct: type2_Correct,
                        blankFillQuestion_Correct: type3_Correct,
                        choicesQuestion_Done: this.paper.questions.filter(q=>q.type==1).length,
                        essayQuestion_Done: this.paper.questions.filter(q=>q.type==2).length,
                        blankFillQuestion_Done: this.paper.questions.filter(q=>q.type==3).length,
                    },
                    recordCreate: {
                        exerciseJsonFileName: jsonFileName,
                        //paperName: this.paper.title,
                        ReplyJSON: JSON.stringify(replies),
                        score: this.paper.score,
                        percentScore: this.paper.percentScore,
                    },
                }),
            })
                .then(resp => {
                    Promise.resolve(resp.text())
                    .then(text => {
                        if (resp.ok)
                            window.location.href = `/Exercise/Record/${text}`;
                        else {
                            swal.fire(text)
                        }
                    })
                })
        }
    },
    watch:{},
})
