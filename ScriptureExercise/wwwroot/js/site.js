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
            chinese:"生晨",
        },
        {
            code:"B" ,
            chinese:"佛規諭錄",
        },
        {
            code:"C" ,
            chinese:"性理題釋",
        },
        {
            code:"D" ,
            chinese:"皇母訓子十誡",
        },
        {
            code:"E" ,
            chinese:"學庸",
        },
        {
            code:"F",
            chinese:"論語",
        },
        {
            code:"G" ,
            chinese:"孟子",
        },
        {
            code:"H" ,
            chinese:"道清",
        },
        {
            code:"I" ,
            chinese:"金心",
        },
        {
            code:"J" ,
            chinese:"六祖壇經",
        },
    ]
}
function scripture_Code_To_Chinese(code) {
    let t = scriptureTrans().find(scripture => scripture.code == code)
    return t.chinese
}

function scripture_Chinese_To_Code(chinese) {
    let t = scriptureTrans().find(scripture => scripture.chinese == chinese)
    return t.code
}

// subjectId <--> subjectChinesePostFix //用number_To_Chinese 不須另外寫?

// function getSubjectChinesePostFix( scripture , subjectId ){} //仍需要判斷長度、當前id
function getSubjectTitle( scripture , subjectId){
    let subjectChinesePostFix = ''
    if (scripture.subjects.length > 1){
        subjectChinesePostFix = `(${number_To_Chinese(subjectId)})`
    }

    return `${scripture.title}${subjectChinesePostFix}`
}

//paper頁 網址參數 > JsonFileName > DB FilePath
function paperJsonFileName_To_DataSource(jsonFileName) {
    return `/lib/DB/${jsonFileName}.json`
}

function paperJsonFileName_To_paperScheme(jsonFileName) {
    // jsonFileName = 'F/F2_1'
    return {
        scriptureTitle:scripture_Code_To_Chinese(jsonFileName[2]),
        subjectId:jsonFileName[3],
        paperId:jsonFileName[5],
    }
}