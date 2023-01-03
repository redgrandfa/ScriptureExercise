//.bindClickHandler() 
const swalSuccess = Swal.mixin({
    // icon: '',
    title: '成功',
})
const swalError = Swal.mixin({
    icon: 'error',
    title: '失敗',
})

function fetchPost( url, bodyObject ){
    return fetch(url , {
        method:'post',
        headers:{
            'content-type':'application/json'
        },
        body:JSON.stringify(bodyObject),
    })
}

Promise.prototype.afterFetch = function(afterSuccess , afterFail){
    this.then( resp => {
        if(resp.ok){
            resp.json()// Promise.resolve(resp.json())
            .then( result => {
                // console.log(result.status) 
                if(result.status > 0){
                    swalError.fire({
                        text:result.message
                    }) 
                    afterFail()
                }
                else{
                    swalSuccess.fire({
                        text:result.message
                    })
                    afterSuccess()
                }
            })
        }else{
            swalError.fire({
                title: '異常',
            }) 
        }
    })
    .catch( err => { // 需搭配 throw new Error('qwe')
        console.log(err)
        swalError.fire({
            title: '前端錯誤',
            text:'如方便，請回報管理者'
        }) 
    })
}


function testGet(){
    fetch('/ApiTest/TestGet')
        .afterFetch( 
            ()=>{},
            ()=> {console.log('失敗後') },
        )
}
function testPost(){
    fetchPost('/ApiTest/TestPost', {account:'qwe' , password:'qwe'})
        .afterFetch(
            ()=> {console.log('成功後') },
            ()=> {console.log('失敗後') },
        )

    // 考慮寫法
    // if (result != null){
    //     // 表示需要再檢查status做 分支流程，不只是顯示訊息
    // }
}
