//.bindClickHandler() 
const swalSuccess = Swal.mixin({
    // icon: '',
    title: '成功',
})
const swalError = Swal.mixin({
    icon: 'error',
    title: '失敗',
})

// function fetchBase( url, bodyObject ){
//     let promise 
//     if( bodyObject == null){
//         promise = fetch(url)
//     }else{
//         promise = fetchPost(url , bodyObject)
//     }

//     return promise
// }

function fetchPost( url, bodyObject ){
    return fetch(url , {
        method:'post',
        headers:{
            'content-type':'application/json'
        },
        body:JSON.stringify(bodyObject),
    })
}



Promise.prototype.afterFetch = function( okToDO ){
    this.then( resp => {
        if(resp.ok){
            okToDO(resp)
        }else{
            swalError.fire({
                title: '異常',
                text:'如方便，請回報管理者'
            }) 
        }
    })
    .catch( err => { // 需搭配 throw new Error('qwe')
        console.log(err)
        swalError.fire({
            title: '錯誤',
            text:'如方便，請回報管理者'
        }) 
    })
}

Promise.prototype.afterAPI = function (afterSuccess, afterFail) {
    this.afterFetch(
        (resp) => {
            resp.json()// let result = Promise.resolve(resp.json()) 或?? Promise.resolve(resp.json()).then()
                .then(result => {
                    // console.log(result.status) 
                    if (result.status > 0) { //API不一定要顯示訊息?
                        swalError.fire({
                            text: result.message
                        })
                        afterFail(result)
                    }
                    else {
                        swalSuccess.fire({
                            text: result.message
                        })
                        afterSuccess(result)
                    }
                    // return result
                })
        }
    )
}
function testGet(){
    fetch('/ApiTest/TestGet')
        .afterAPI( 
            ()=>{},
            ()=> {console.log('失敗後') },
        )
}
function testPost(){
    fetchPost('/ApiTest/TestPost', {account:'qwe' , password:'qwe'})
        .afterAPI(
            ()=> {console.log('成功後') },
            ()=> {console.log('失敗後') },
        )

}
