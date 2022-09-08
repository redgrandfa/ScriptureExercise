let vue_recordList = new Vue({
    el: '#vue_recordList',
    data: {
        fields: [
            { key: 'createTime', label: '時間', sortable: true }, //同時也是連結?
            { key: 'paperName', label: '卷名', sortable: true },
            { key: 'percentScore', label: '得分比', sortable: true },
            { key: 'link', label: '連結', sortable: false }, //ID  => action
            { key: 'Delete', label: '刪除', sortable: false }, //ID  => action
        ],
        records: [
            // {
            //     createTime: 202201012359,
            //     paperName: "Z1_1",
            //     percentScore: 11,
            // },
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
