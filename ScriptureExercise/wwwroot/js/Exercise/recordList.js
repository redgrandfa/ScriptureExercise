let vue_recordList = new Vue({
    el: '#vue_recordList',
    data: {
        tableBusy:true,
        fields: [
            { key: 'createTime', label: '時間/連結', sortable: true 
                ,formatter:value=>{ 
                    // 23 01 10 20 31 54
                    // let value = `${value}`
                    let year = value.substring(0,2)
                    let month = value.substring(2,4)
                    let date = value.substring(4,6)
                    let hr = value.substring(6,8)
                    let min = value.substring(8,10)

                    return [ `20${year}/${month}/${date}`, `${hr}:${min}`]
                }
                ,tdClass:'td-createTime'
            },
            { key: 'paperName', label: '卷名', sortable: true 
                ,formatter:value=>{ 
                    let scriptureCode = value.substring(0,1)
                    let subjectId = value.substring(1,2)
                    let paperId = value.substring(3,4)

                    //範圍註記

                    return [scripture_Code_To_Chinese(scriptureCode), 
                        `(${number_To_Chinese(subjectId)})`, 
                        paperId ]
                }
                ,tdClass:'td-createTime' //沿用
            },
            { key: 'percentScore', label: '得分比', sortable: true 
                ,tdClass:'td-percentScore' 
            },
            // { key: 'link', label: '連結', sortable: false
            //     ,tdClass:'td-action'
            // }, //ID  => action
            { key: 'Delete', label: '刪除', sortable: false 
                ,tdClass:'td-action'
            }, //ID  => action
        ],
        records: [
            // {
            //     createTime: '202201012359',
            //     paperName: "F2_1",
            //     percentScore: 11,
            // },
            // {
            //     createTime: '202201012300',
            //     paperName: "A_2",
            //     percentScore: 22,
            // },
        ],
    },
    mounted() { 
        fetch('/ApiExercise/GetRecordList')
        .afterFetch(resp=>{
            resp.json()
            .then(respBody => {
                let recordList = respBody.payload

                this.records = recordList.map( r => {
                    let result = {
                        createTime: r.createTime ,
                        percentScore: r.percentScore ,
                        // link: `/Exercise/Record/${r.createTime}`,
                    }

                    let scripture = r.exerciseJsonFileName[0]
                    result.paperName = r.exerciseJsonFileName // F/F2_1
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
                    let idx = this.records.findIndex(r=>r.createTimeId==createTimeId)
                    this.records.splice(idx, 1)
                    //dom.dispatchEvent(apiDoneEvent)

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