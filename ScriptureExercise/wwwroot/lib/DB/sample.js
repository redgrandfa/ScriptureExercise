// .-h9-166
// class="-yL-190"
//     <span class="text-format-content">壇經二12</span>
//     <span class="-Dv-194">(100 點)</span>
// .-zW-191"  //data-automation-id="formSubTitle"
//     <span class="text-format-content">(p116~p191)</span> -->

    // .-iH-172.-T--182
    //倒數四個要去掉
    // .-om-213


let mainDom = document.querySelector('.-h9-166');

let json = {
    title: mainDom.querySelector('[data-automation-id="formMainTitle"] span').innerHTML,
    range_remark: mainDom.querySelector('[data-automation-id="formSubTitle"]>span').innerHTML, //.-zW-191
    // questions:[    ]
}

let questionDoms = mainDom.querySelectorAll('[data-automation-id="questionContent"]');
    
let qArray = Array.from(questionDoms)
qArray.length -=4

json.questions = qArray.map( (q,i)=>({
    id:i, //權宜之計 以後可能擴充安插 故需要id對應
    order:i+1,
    type:1, //選擇
    stem: q.querySelector('[data-automation-id="questionTitle"]>span>span.text-format-content').innerHTML.trim(),
        // 題目
        // -nq-219>span>span:nth-type(2)
        // -nq-219>span.text-format-content

    choices: Array.from( q.querySelectorAll('[data-automation-id="questionChoiceOptionContainer"]') ), //-zl-247
    answer:null,
}))

json.questions.forEach( q => {
    //調整題型
    // if(q.tyoe = 1){}
    // 1 各選項
    //     -zl-247 
    //         -Cc-251 > span .innerHTML (去選項括號?)
    //         +-h9-143 -mj-249 有存在的是正解
    q.choices = q.choices.map( (c, idx) => {
        let answer = c.querySelector('span:nth-child(3)')//.-h9-143.-mj-249
        if( answer != undefined ){
            q.answer = idx
        }

        return c.querySelector(' span.text-format-content').innerHTML.trim()//.-Cc-251 >
    })
})

let anchor = document.createElement('a')
let blob = new Blob([ JSON.stringify(json) ],{'type':'application/json'})
anchor.href=window.URL.createObjectURL(blob)
anchor.download = `${json.title}.json`
anchor.click()
