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
            // let result = Promise.resolve(resp.json())
            resp.json()
            .then(result => {
                // console.log(result.status) 
                if (result.status == 0) {  //API不一定要顯示訊息?
                    swalSuccess.fire({
                        text: result.message
                    })
                    afterSuccess(result)
                }
                else {
                    swalError.fire({
                        text: result.message
                    })
                    afterFail(result)
                }
                // return result
            })
        }
    )
}
// function testGet(){
//     fetch('/ApiTest/TestGet')
//         .afterAPI( 
//             ()=>{},
//             ()=> {console.log('失敗後') },
//         )
// }
// function testPost(){
//     fetchPost('/ApiTest/TestPost', {account:'qwe' , password:'qwe'})
//         .afterAPI(
//             ()=> {console.log('成功後') },
//             ()=> {console.log('失敗後') },
//         )
// }



// spinner => button.userAction = call API
//     ... V登入/註冊、
//      V交卷 導向記錄前、
//      V刪除紀錄。    
// 會員中心編輯
//     封裝
//     button.userAction => 
//         按下去 = 掛上 disabled + spinner
//         等回應，除掉 disabled、spinner

let apiDoneEvent = new Event('apiDone');
function processingAPI(btn){
    btn.setAttribute('disabled',true)

    let spinner = 
    '<span aria-hidden="true" style="vertical-align: baseline;" class="spinner-border spinner-border-sm"><!----></span>'
    btn.innerHTML += spinner

    // 等回應，除掉 disabled、spinner
    btn.addEventListener( 'apiDone' , apiDoneEventHandler)
    function apiDoneEventHandler(e){
        let btn = e.target
        btn.removeAttribute('disabled');
        btn.removeChild(btn.lastElementChild);

        btn.removeEventListener('apiDone' , apiDoneEventHandler) //不然事件會越堆越多
    }
}

//【掛監聽的方式：容易和 vue的 掛事件監聽衝突】
// document.querySelectorAll('button.userAction').forEach(btn => {
//     let spinner =
//     '<span aria-hidden="true" style="vertical-align: baseline;" class="spinner-border spinner-border-sm"><!----></span>'
//     // <b-spinner class="align-middle"></b-spinner>

//     // 按下去 = 掛上 disabled + spinner
//     btn.addEventListener( 'click' , (e) =>{
//         console.log('按')
//         // e.target.setAttribute('disabled', true);
//         btn.setAttribute('disabled',true)

//         // e.target.innerHTML += spinner;
//         btn.innerHTML += spinner
//     })

//     // 等回應，除掉 disabled、spinner
//     btn.addEventListener( 'apiDone' , (e)=>{
//         console.log('回應完畢')
//          // e.target.setAttribute('disabled', false);

//         btn.removeAttribute('disabled');
//          // dom.innerHTML -= spinner
//         btn.removeChild(btn.lastElementChild);
//         // btn.removeEventListener('apiDone')
//     })
// })

