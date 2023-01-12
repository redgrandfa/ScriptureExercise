let vue_recordList = new Vue({
    el: '#vue_recordList',
    data: {
        tableBusy:true,
        fields: [
            { key: 'createTimeId', label: '時間', sortable: true 
                ,formatter:(value, key, item)=>{
                    // console.log(value) // key有對到資料才有值 / null
                    // console.log(key) //同key
                    // console.log(item) // 陣列元素

                    let createTimeId = value//item.createTimeId
                    // 23 01 10 20 31 54
                    let year = createTimeId.substring(0,2)
                    let month = createTimeId.substring(2,4)
                    let date = createTimeId.substring(4,6)
                    let hr = createTimeId.substring(6,8)
                    let min = createTimeId.substring(8,10)

                    return [ `20${year}/${month}/${date}`, `${hr}:${min}`]
                }
                ,tdClass:'td-createTime'
            },
            { key: 'paperCode', label: '卷別', sortable: true 
                ,formatter:(value, key, item)=>{ 
                    let paperCode = value//item.paperCode

                    //拆出三級資訊
                    let scriptureCode = paperCode.substring(0,1)
                    let subjectId = paperCode.substring(1,2)
                    let paperId = paperCode.substring(3,4)
                    
                    //找到對應三級資料
                    let scripture = scriptures_inDB.find(scripture => scripture.code == scriptureCode)
                    let subject =  scripture.subjects.find( subject => subject.id == subjectId)
                    let paper = subject.papers.find( paper => paper.id == paperId)

                    
                    //
                    let subjectChinesePostfix = ''
                    if( scripture.subjects.length > 1 ){
                        subjectChinesePostfix = `(${number_To_Chinese(subjectId)})`
                    }
                    let range_remark = paper.range_remark

                    return [scripture_Code_To_Chinese(scriptureCode), 
                        subjectChinesePostfix, 
                        paperId ,
                        range_remark
                    ]
                }
                ,tdClass:'td-createTime' //沿用
            },
            { key: 'percentScore', label: '得分比', sortable: true 
                ,tdClass:'td-percentScore' 
            },
            //CreateID  => User動作
            { key: 'link', label: '連結', sortable: false
                ,tdClass:'td-action'
            }, 
            { key: 'Delete', label: '刪除', sortable: false 
                ,tdClass:'td-action'
            },
        ],
        records: [
            // {
            //     createTimeId: '202201012359',
            //     paperCode: "F2_1",
            //     percentScore: 11,
            // },
            // {
            //     createTimeId: '202201012300',
            //     paperCode: "A_2",
            //     percentScore: 22,
            // },
        ],
        sortBy:"createTimeId",
        sortDesc:true,
    },
    mounted() { 
        fetch('/ApiExercise/GetRecordList')
        .afterFetch(resp=>{
            resp.json()
            .then(respBody => {
                let recordList = respBody.payload

                this.records = recordList.map( r => {
                    let result = {
                        createTimeId: r.createTime ,
                        percentScore: r.percentScore ,
                    }

                    let scripture = r.exerciseJsonFileName[0]
                    result.paperCode = r.exerciseJsonFileName // F/F2_1
                        .substring(2) // F2_1

                    return result
                })
            })
            this.tableBusy = false
        })
    },
    methods: {
        deleteRecord(createTimeId , e){
            let dom = e.target
            processingAPI(dom)

            fetchPost(`/ApiExercise/DeleteRecord/${createTimeId}`,{})
            .afterAPI(
                (result)=> {
                    dom.dispatchEvent(apiDoneEvent)
                    let idx = this.records.findIndex(r=>r.createTimeId==createTimeId)
                    console.log(idx)
                    this.records.splice(idx, 1)
            },
                (result)=> {
                    dom.dispatchEvent(apiDoneEvent)
                },
            )
        }
    },
    computed: {},
    watch: {},
})