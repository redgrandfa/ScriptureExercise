function number_To_Chinese(num) {
    let trans = [
        '零', '一', '二', '三', '四', '五',
        '六', '七', '八', '九', '十',
    ]

    return trans[num]
}

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

function paperJsonFileName_To_DataSource(jsonFileName) {
    return `/lib/DB/${jsonFileName}.json`
}