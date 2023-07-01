function hideMenu(){
    menu.classList.remove('active')
    menu_bg.classList.remove('active')
}
function showMenu(){
    menu.classList.add('active')
    menu_bg.classList.add('active')
}
function number_To_Chinese(num) {
    let trans = [
        '零', '一', '二', '三', '四', '五',
        '六', '七', '八', '九', '十',
    ]

    return trans[num]
}



// scriptureCode <--> scriptureTitle  //scriptures_inDB 或另外做字典
function scriptureTrans(){
    return [
        {
            code:"A" ,
            title:"生晨",
        },
        {
            code:"B" ,
            title:"佛規諭錄",
        },
        {
            code:"C" ,
            title:"性理題釋",
        },
        {
            code:"D" ,
            title:"皇母訓子十誡",
        },
        {
            code:"E" ,
            title:"學庸",
        },
        {
            code:"F",
            title:"論語",
            isMulti:true,
        },
        {
            code:"G" ,
            title:"孟子",
            isMulti:true,
        },
        {
            code:"H" ,
            title:"道清",
        },
        {
            code:"I" ,
            title:"金心",
        },
        {
            code:"J" ,
            title:"六祖壇經",
            isMulti:true,
        },
    ]
}
function scripture_Code_To_Title(code) {
    let t = scriptureTrans().find(scripture => scripture.code == code)
    return t.title
}
function scripture_Title_To_Code(title) {
    let t = scriptureTrans().find(scripture => scripture.title == title)
    return t.code
}
// =====subject
// scheme={
//     scriptureCode?
//     scriptureTitle:'六祖壇經',
//     subjectId?
//     subjectChinesePostfix:'(二)', subjectId?
// }

function getSubjectScheme( scriptureCode , subjectId ) {
    // 找到對應 資料
    let scripture = scriptureTrans().find(scripture => scripture.code == scriptureCode)

    let subjectChinesePostfix = ''
    if (scripture.isMulti){ //.length
        subjectChinesePostfix = `(${number_To_Chinese(subjectId)})`
    }

    return {
        scriptureTitle: scripture.title,
        subjectId: subjectId,
        subjectChinesePostfix: subjectChinesePostfix,
    };
}
function getSubjectTitle( scriptureCode , subjectId ){
    let scheme = getSubjectScheme(scriptureCode , subjectId)
    return `${scheme.scriptureTitle}${scheme.subjectChinesePostfix}`
}

// =====paper = Info + Questions
function paperJsonFilePath_To_paperInfo(paperJsonFilePath){
    let paperCode = paperJsonFilePath_To_paperCode(paperJsonFilePath)
    let subjectId = paperCode[1]

    let paperInfo = getSubjectScheme( paperCode[0] , subjectId)
    paperInfo.subjectTitle = 
        paperInfo.scriptureTitle + 
        paperInfo.subjectChinesePostfix

    paperInfo.paperId = paperCode.substring(3)

    return paperInfo // range_remark undefined
}

//==== Json Questions
function paperJsonFilePath_To_paperCode(paperJsonFilePath){
    return paperJsonFilePath.substring(2)
}
function paperCode_To_paperJsonFilePath(paperCode) {
    return paperCode[0] + "/" + paperCode;
}

function paperJsonFilePath_To_dataSource(paperJsonFilePath) {
    return `/lib/DB/${paperJsonFilePath}.json`;
}