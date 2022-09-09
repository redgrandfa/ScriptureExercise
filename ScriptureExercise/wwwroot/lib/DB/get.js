let json = {
    title: document.querySelector('[data-automation-id="formMainTitle"] span').innerHTML,
};
json.range_remark = document.querySelector('[data-automation-id="formSubTitle"]>span')
if( json.range_remark != null ){
    json.range_remark =  json.range_remark.innerHTML
}
else{
    json.range_remark = ""
}


let questionDoms = document.querySelectorAll('[data-automation-id="questionContent"]');
let questionDomArray = Array.from(questionDoms)
questionDomArray.length -=4 //倒數四個要去掉


json.questions = questionDomArray.map( (qDom , idx)=>{
    let result = {
        id:idx+1,
        order: idx+1,
    }

    let choiceDoms = qDom.querySelectorAll('[data-automation-id="questionChoiceOptionContainer"]')
    let 默寫正解Dom =  qDom.querySelector('div:nth-of-type(2)>div:nth-of-type(2)>div:nth-of-type(2)>span')
    if( choiceDoms.length > 0){
        result.type = 1
        result.stem = qDom.querySelector('[data-automation-id="questionTitle"]>span>span.text-format-content').innerHTML.trim()
        
        result.choices = Array.from( choiceDoms )
            .map( (choiceDom, cIdx) => {
                //順便確認此選項是否為答案
                let answer = choiceDom.querySelector('span:nth-of-type(3)')
                if( answer != null ){
                    result.answer = cIdx
                }
    
                return choiceDom.querySelector('span.text-format-content').innerHTML.trim()
            })

    }
    else if( 默寫正解Dom != null){
        result.type = 2

        //和選擇題一樣的題幹取法
        result.stem = qDom.querySelector('[data-automation-id="questionTitle"]>span>span.text-format-content').innerHTML.trim(),

        // let qDom = document.querySelectorAll('[data-automation-id="questionContent"]')[14]
        result.answer = 默寫正解Dom.innerHTML.trim()
    }
    else{
        result.type = 3
    }


    return result
})


let anchor = document.createElement('a')
let blob = new Blob([ JSON.stringify(json) ],{'type':'application/json'})
anchor.href=window.URL.createObjectURL(blob)
anchor.download = `${json.title}.json`
anchor.click()

// div.-xx-166
//    主標題
// .-yL-190"    data-automation-id="formMainTitle"
//     <span class="text-format-content">壇經二12</span>
//     <span class="-Dv-194">(100 點)</span>
//    副標題(可能沒有)
// .-zW-191"  data-automation-id="formSubTitle"
//     <span class="text-format-content">(p116~p191)</span>


//  第三個div
//     選擇題
//                         button data-automation-id="questionWrapper"
//                             div
//                             div data-automation-id="questionContent"
//                                 div id="QuestionId_亂碼" 【題號 題目區】
//                                     div data-automation-id="questionTitle"
//                                             span
//                                                 span 題號
//                                                 span.text-format-content 題幹 
//                                                 //span:nth-of-type(2)  
//                                                 //span.text-format-content
//                                             span
//                                     div 【選項/ 輸入回答區】
//                                         div role="radiogroup"
//                                             div*4 【選項分裂起點】
//                                                 div data-automation-id="questionChoiceOptionContainer"
//                                                     div
//                                                         span
//                                                         span
//                                                             span.text-format-content 【選項文字】
//                                                         span 有存在第三個的是正解
//     默寫題
//         div
//             div aria-label="文字 問題設計工具"
//                 div
//                     div role="heading"
//                         button data-automation-id="questionWrapper"
//                             div
//                             div data-automation-id="questionContent"
//                                 div id="QuestionId_亂碼" 【題目區】
//                                 div 【選項/ 輸入回答區】
//                                     div輸入區
//                                     div正解區
//                                         div 正確答案:
//                                         div 
//                                             span 正解值
//         div

    // 共通屬性
    //         "id":1,
    //         "order": 1,

    //     {
    //         type:1, //選擇
    //         stem:'佛規諭錄是「」搖鸞垂訓逐條詳加述說成書',
    //         choices:[
    //             {order:1 , text:'活佛師尊' },
    //             {order:2 , text:'彌勒組師' },
    //             {order:3 , text:'南海古佛' },
    //         ],
    //         answer:1
    //     },
    //     {
    //         type:2,
    //         stem:'子曰：恭而無禮則勞,慎而無禮則葸,勇而無禮則亂,直而無禮…，………偷。',
    //         answer:'qwe'
    //     },
    //     {
    //         type:3,
    //         //題目一定比空格先出現
    //         stem:['子曰：恭而無禮則勞,慎而','則葸,勇而','則亂,直而無禮。'],
    //         answers:['無禮','無禮']
    //     }
