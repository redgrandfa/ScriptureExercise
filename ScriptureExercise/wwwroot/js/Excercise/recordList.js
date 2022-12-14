let vue_recordList = new Vue({
    el: '#vue_recordList',
    data: {
        fields: [
            { key: 'createTime', label: '時間', sortable: true 
                ,formatter:value=>{ 
                    console.log(value) //%100 /100?
                    let str = `${value}`
                    let min = str.substring(10,12)
                    let hr = str.substring(8,10)
                    let date = str.substring(6,8)
                    let month = str.substring(4,6)
                    return [ `${month}.${date}`, `${hr}:${min}`]
                },
                tdClass:'td-createTime'
            }, //同時也是連結?
            { key: 'paperName', label: '卷名', sortable: true 
                ,tdClass:'td-createTime' //沿用
                
            },
            { key: 'percentScore', label: '得分比', sortable: true 
            ,tdClass:'td-percentScore' 
            },
            { key: 'link', label: '連結', sortable: false
                ,tdClass:'td-action'
            }, //ID  => action
            { key: 'Delete', label: '', sortable: false 
                ,tdClass:'td-action'
            }, //ID  => action
        ],
        records: [
            {
                createTime: 202201012359,
                paperName: "Z1_1",
                percentScore: 11,
                link:'q'
            },
            {
                createTime: 202201012300,
                paperName: "Z1_2",
                percentScore: 22,
                link:'w'
            },
            {
                createTime: 202201012300,
                paperName: "Z1_2",
                percentScore: 22,
                link:'w'
            },
            {
                createTime: 202201012300,
                paperName: "Z1_2",
                percentScore: 22,
                link:'w'
            },
        ],
    },
    mounted() { 
        fetch('/ApiExercise/GetRecordList')
        .then(resp=>{
            if(resp.ok){
                Promise.resolve(resp.json())
                .then(recordList => {
                    this.records = recordList.map( r => {
                        let result = {
                            createTime: r.createTime ,
                            percentScore: r.percentScore ,
                            // link: paperJsonFileName_To_DataSource(r.exerciseJsonFileName),
                            link: `/Exercise/Record/${r.createTime}`,
                        }

                        let scripture = r.exerciseJsonFileName[0]
                        result.paperName = r.exerciseJsonFileName.substring(2) // F/F2_1
                        //result.paperName
                            .replace(scripture, scripture_Code_To_Chinese(scripture) )
                        //result.paperName
                            .replace('_', ' 卷別' )

                        return result
                    })
                })
            }else{
                Promise.resolve(resp.text())
                .then(text => {
                    swal.fire(text)
                })
            }
        })
    },
    methods: {
        deleteRecord(createTimeId){
            fetch(`/ApiExercise/DeleteRecord/${createTimeId}`,{
                method:'post',
                // headers:{
                //     'content-type'://'text/plain'
                //         'application/json',
                // },
                // body://createTimeId
                //     JSON.stringify({
                //         createTimeId:createTimeId
                //     }),
            })
            .then(resp=>{
                Promise.resolve(resp.text() )
                .then(text => {
                    swal.fire(text)
                    if(resp.ok){
                        let idx = this.records.findIndex(r=>r.createTimeId==createTimeId)
                        this.records.splice(idx,1)
                    }
                })
            })
        }
    },
    computed: {},
    watch: {},
})