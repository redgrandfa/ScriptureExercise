// bai 4 ...Ru 1+3+7 ... Si5 ... Dao 1 ... 21科
//4家 10經 21科 22x卷
let scriptures_inDB = [
    {
        code: "A",
        title: "生晨",
        coverImgFile: "cover_A.jpg",
        author: "活佛師尊",
        introHtml: "一貫道白陽道場的道學字典，包括常用道學索引、聖訓選讀、道學辭彙，是現今修士人才必備的一本工具書。",
        belongTo: "Bai",
        subjects: [
            {
                id: 1,
                papers: [
                    { id: 1, range_remark: "(P.5-P.68)" },
                    { id: 2, range_remark: "(P.69-P.132)" },
                    { id: 3, range_remark: "(P.133-P.195)" },
                    { id: 4, range_remark: "(P.196-P.258)" },
                    { id: 5, range_remark: "(P.322-P.384)" },
                    { id: 6, range_remark: "(P.322-P.384)" },
                    { id: 7, range_remark: "(P.5-P.132)" },
                    { id: 8, range_remark: "(P.133-P.258)" },
                    { id: 9, range_remark: "(P.259-P.384)" },
                    { id: 10, range_remark: "(P.5-P.195)" },
                    { id: 11, range_remark: "(P.196-P.384)" },
                    { id: 12, range_remark: "(P.5-P.384)" },
                ],
            },
        ],
    },
    {
        code: "B",
        title: "佛規諭錄",
        coverImgFile: "cover_B.png",
        author: "活佛師尊",
        introHtml: "為使修道人有修行的依據準則、規矩方圓，故定下此佛規十五條，由師尊老大人籌策成書，只要能依循實踐，自然不用擔心三期浩劫。",
        belongTo: "Bai",
        subjects: [
            {
                id: 1,
                papers: [
                    { id: 1, range_remark: "(1-3)" },
                    { id: 2, range_remark: "(4-6)" },
                    { id: 3, range_remark: "(7-9)" },
                    { id: 4, range_remark: "(10-12)" },
                    { id: 5, range_remark: "(13-15)" },
                    { id: 6, range_remark: "(1-5)" },
                    { id: 7, range_remark: "(6-10)" },
                    { id: 8, range_remark: "(11-15)" },
                    { id: 9, range_remark: "(1-7)" },
                    { id: 10, range_remark: "(8-15)" },
                    { id: 11, range_remark: "(全)" },
                    { id: 12, range_remark: "(全)" },
                    { id: 13, range_remark: "(全)" },
                    { id: 14, range_remark: "(全)" },
                ],
            },
        ],
    },
    {
        code: "C",
        title: "性理題釋",
        coverImgFile: "cover_C.jpg",
        author: "活佛師尊",
        introHtml: "《性理題釋》一書，原係由濟公活佛恩師慈悲解答道親修辦道的疑問，故有「疑問解答」之作。以問答方式，於每題解答時，釋義內容則力求簡明，言詞則力求淺近。共有數十題之多，然內容含蓋廣泛，若能細究參研，自能疑釋理明。故《性理題釋》一書，實可作為渡人成全人之門徑；若能照言實踐力行，更是修道入門之指南。當人手一冊，以達成濟公活佛恩師之期望，正己成人、徹悟真理、脫出苦海、共登覺路。",
        belongTo: "Bai",
        subjects: [
            {
                id: 1,
                papers: [
                    { id: 1, range_remark: "(1-15)" },
                    { id: 2, range_remark: "(16-30)" },
                    { id: 3, range_remark: "(31-45)" },
                    { id: 4, range_remark: "(46-60)" },
                    { id: 5, range_remark: "(61-75)" },
                    { id: 6, range_remark: "(76-90)" },
                    { id: 7, range_remark: "(1-30)" },
                    { id: 8, range_remark: "(31-60)" },
                    { id: 9, range_remark: "(61-90)" },
                    { id: 10, range_remark: "(1-45)" },
                    { id: 11, range_remark: "(46-90)" },
                    { id: 12, range_remark: "(1-90)" },
                ],
            },
        ],
    },
    {
        code: "D",
        title: "皇母訓子十誡",
        coverImgFile: "cover_D.jpg",
        author: "明明上帝",
        introHtml: "《皇母訓子十誡》一書,成書於民國三十年,是一貫道道義之最基礎理念,從原靈下凡之因緣,到大道普傳、三曹普渡之末後收圓大事,以及上帝老母對原胎佛子之期許與告誡, 全濃縮在這十段慈訓中。<br><br>皇母憂心修道子龍蛇混雜,認不清大道宗旨,故特別臨壇慈示天地成住壞空及降道降劫之因由,以及如何修道以達歸根認母的要訣。<br><br>皇母更交代道中壇主、領袖等之責任使命,應以身作則、發強剛毅,聞過喜、聞善改,遵前提後與三省四勿,做好領導者的風範。尤應悟透三教一理之妙、道貫三極之真,方能修出水火不溺的真法身,不被外相考倒,以符皇母賜名「一貫」之本意。",
        belongTo: "Bai",
        subjects: [
            {
                id: 1,
                papers: [
                    { id: 1, range_remark: "(p.1-59)" },
                    { id: 2, range_remark: "(p.60-102)" },
                    { id: 3, range_remark: "(p.103-126)" },
                    { id: 4, range_remark: "(p.127-147)" },
                    { id: 5, range_remark: "(p.148-175)" },
                    { id: 6, range_remark: "(p.148-196)" },
                    { id: 7, range_remark: "(p.1-102)" },
                    { id: 8, range_remark: "(p.103-147)" },
                    { id: 9, range_remark: "(p.148-196)" },
                    { id: 10, range_remark: "(p.1-126)" },
                    { id: 11, range_remark: "(p.127-196)" },
                    { id: 12, range_remark: "全(p.2-196)" },
                    { id: 13, range_remark: "全(p.2-196)" },
                ],
            },
        ],
    },
    {
        code: "E",
        title: "學庸",
        coverImgFile: "cover_E.jpg",
        author: "大學曾子夫子、中庸子思夫子",//  曾子/子思/朱熹
        introHtml: "《大學》、《中庸》出自《禮記》中的兩個篇章。<br><br>《大學》以人的修身為核心，強調人的修身養性不只是內省的過程，更是同外物相接觸，窮究物理而獲得知識，培養道德品性、完善人格的過程。格物、致知、誠意、正心是修身的方法，為「內修」。齊家、治國、平天下則是修身的目的，為「外治」。<br><br>《中庸》的中心思想是儒學的中庸之道，它的主要內容並非現代人所普遍理解的中立、平庸之意，其主旨在於修養人性。其中包括學習的方式：博學之、審問之、慎思之、明辨之、篤行之，也包括儒家做人的規範如君臣、父子、夫婦、兄弟以及朋友之間的相處交往規則和智、仁、勇三種重要的德行等。",
        belongTo: "Ru",
        subjects: [
            {
                id: 1,
                papers: [
                    { id: 1, range_remark: "(p1-p34)" },
                    { id: 2, range_remark: "(p35-p58)" },
                    { id: 3, range_remark: "(p59-p78)" },
                    { id: 4, range_remark: "(p79-p95)" },
                    { id: 5, range_remark: "(p96-p120)" },
                    { id: 6, range_remark: "(p121-p162)" },
                    { id: 7, range_remark: "(p163-p201)" },
                    { id: 8, range_remark: "(p1-p58)" },
                    { id: 9, range_remark: "(p59-p95)" },
                    { id: 10, range_remark: "(p59-p95)" },
                    { id: 11, range_remark: "(p1-p95)" },
                    { id: 12, range_remark: "(p96-p201)" },
                    { id: 13, range_remark: "(全)" },
                    { id: 14, range_remark: "(全)" },
                ],
            },
        ],
    },
    {
        code: "F",
        title: "論語",
        coverImgFile: "cover_FG.jpg",
        author: "孔子聖人及其弟子",
        introHtml:
            "《論語》是以春秋時期思想家孔子言行為主的言論彙編，為儒家重要經典之一，在四庫全書中列為「經」部。<br><br>其內容涉及多方面討論，當中包括儒家治國理念、人倫關係、個人道德規範、先秦時期的社會面貌，乃至孔子及其弟子的經歷等。",
        belongTo: "Ru",
        subjects: [
            {
                id: 1,
                papers: [
                    { id: 1, range_remark: "(學而、為政)" },
                    { id: 2, range_remark: "(八佾)" },
                    { id: 3, range_remark: "(里仁)" },
                    { id: 4, range_remark: "(公冶長)" },
                    { id: 5, range_remark: "(雍也)" },
                    { id: 6, range_remark: "(述而)" },
                    { id: 7, range_remark: "(學而、為政、八佾)" },
                    { id: 8, range_remark: "(里仁、公冶長)" },
                    { id: 9, range_remark: "(雍也、述而)" },
                    { id: 10, range_remark: "(學而~里仁)" },
                    { id: 11, range_remark: "(公冶長~述而)" },
                    { id: 12, range_remark: "全(學而~述而)" },
                ],
            },
            {
                id: 2,
                papers: [
                    { id: 1, range_remark: "(泰伯第八)" },
                    { id: 2, range_remark: "(子罕第九)" },
                    { id: 3, range_remark: "(鄉黨第十)" },
                    { id: 4, range_remark: "(先進第十一)" },
                    { id: 5, range_remark: "(顏淵第十二)" },
                    { id: 6, range_remark: "(子路第十三)" },
                    { id: 7, range_remark: "(泰伯~子罕)" },
                    { id: 8, range_remark: "(鄉黨~先進)" },
                    { id: 9, range_remark: "(顏淵~子路)" },
                    { id: 10, range_remark: "(泰伯~鄉黨)" },
                    { id: 11, range_remark: "(先進~子路)" },
                    { id: 12, range_remark: "全(泰伯~子路)" },
                ],
            },
            {
                id: 3,
                papers: [
                    { id: 1, range_remark: "(憲問第十四)" },
                    { id: 2, range_remark: "(衛靈公第十五)" },
                    { id: 3, range_remark: "(季氏第十六)" },
                    { id: 4, range_remark: "(陽貨第十七)" },
                    { id: 5, range_remark: "(微子第十八)" },
                    { id: 6, range_remark: "(子張、堯曰)" },
                    { id: 7, range_remark: "(憲問、衛靈公)" },
                    { id: 8, range_remark: "(微子18~堯曰20)" },
                    { id: 9, range_remark: "(憲問14~季氏16)" },
                    { id: 10, range_remark: "(陽貨17~堯曰20)" },
                    { id: 11, range_remark: "(陽貨17~堯曰20)" },
                    { id: 12, range_remark: "全(憲問14~堯曰20)" },
                ],
            },
        ],
    },
    {
        code: "G",
        title: "孟子",
        coverImgFile: "cover_FG.jpg",
        author: "孟子弟子與再傳弟子",
        introHtml:
            "《孟子》是一本記述孟子思想與言行的儒家經書，完成於戰國時代中後期，屬於《十三經》之一。該書詳實地記載了孟子的思想、言論和事跡。注本主要有東漢趙岐《孟子章句》、南宋朱熹《孟子集注》、清焦循《孟子正義》等。在四庫全書中為經部",
        belongTo: "Ru",
        subjects: [
            {
                id: 1,
                papers: [
                    { id: 1, range_remark: "(P.31-P.55)" },
                    { id: 2, range_remark: "(P.56-P.81)" },
                    { id: 3, range_remark: "(P.82-P.106)" },
                    { id: 4, range_remark: "(P.107-P.132)" },
                    { id: 5, range_remark: "(P.133-P.157)" },
                    { id: 6, range_remark: "(P.158-P.177)" },
                    { id: 7, range_remark: "(P.31-P.81)" },
                    { id: 8, range_remark: "(P.82-P.132)" },
                    { id: 9, range_remark: "(P.133-P.177)" },
                    { id: 10, range_remark: "(P.31-P.177)" },
                ],
            },
            {
                id: 2,
                papers: [
                    { id: 1, range_remark: "(P.179-202)" },
                    { id: 2, range_remark: "(P.203-228)" },
                    { id: 3, range_remark: "(P.229-252)" },
                    { id: 4, range_remark: "(P.253-276)" },
                    { id: 5, range_remark: "(P.277-300)" },
                    { id: 6, range_remark: "(P.301-321)" },
                    { id: 7, range_remark: "(P.179-228)" },
                    { id: 8, range_remark: "(P.229-276)" },
                    { id: 9, range_remark: "(P.277-321)" },
                    { id: 10, range_remark: "(全)" },
                    { id: 11, range_remark: "(全)" },
                ],
            },
            {
                id: 3,
                papers: [
                    { id: 1, range_remark: "滕文公篇P323-P348" },
                    { id: 2, range_remark: "滕文公篇P349-P382" },
                    { id: 3, range_remark: "滕文公篇P323-P382" },
                    { id: 4, range_remark: "滕文公篇P383-P412" },
                    { id: 5, range_remark: "滕文公篇P413-P448" },
                    { id: 6, range_remark: "滕文公篇P383-P448" },
                    { id: 7, range_remark: "滕文公篇P323-P448" },
                    { id: 8, range_remark: "滕文公篇P383-P448" },
                ],
            },
            {
                id: 4,
                papers: [
                    { id: 1, range_remark: "(P.449-468)" },
                    { id: 2, range_remark: "(P.469-486)" },
                    { id: 3, range_remark: "(P.487-519)" },
                    { id: 4, range_remark: "(P.520-544)" },
                    { id: 5, range_remark: "(P.545-578)" },
                    { id: 6, range_remark: "(P.579-608)" },
                    { id: 7, range_remark: "(P.609-638)" },
                    { id: 8, range_remark: "(P.449-486)" },
                    { id: 9, range_remark: "(P.487-578)" },
                    { id: 10, range_remark: "(P.579-638)" },
                ],
            },
            {
                id: 5,
                papers: [
                    { id: 1, range_remark: "(P.639-664)" },
                    { id: 2, range_remark: "(P.665-693)" },
                    { id: 3, range_remark: "(P.694-723)" },
                    { id: 4, range_remark: "(P.724-746)" },
                    { id: 5, range_remark: "(P.639-693)" },
                    { id: 6, range_remark: "(P.694-746)" },
                    { id: 7, range_remark: "全(P.639-746)" },
                ],
            },
            {
                id: 6,
                papers: [
                    { id: 1, range_remark: "(P.747-794)" },
                    { id: 2, range_remark: "(P.795-843)" },
                    { id: 3, range_remark: "(P.844-880)" },
                    { id: 4, range_remark: "(P.881-908)" },
                    { id: 5, range_remark: "(P.747-843)" },
                    { id: 6, range_remark: "(P.844-908)" },
                    { id: 7, range_remark: "全(P.747-908)" },
                ],
            },
            {
                id: 7,
                papers: [
                    { id: 1, range_remark: "(P.909-945)" },
                    { id: 2, range_remark: "(P.946-982)" },
                    { id: 3, range_remark: "(P.983-1022)" },
                    { id: 4, range_remark: "(P.1023-1057)" },
                    { id: 5, range_remark: "(P.1058-1093)" },
                    { id: 6, range_remark: "(P.1094-1146)" },
                    { id: 7, range_remark: "(P.909-982)" },
                    { id: 8, range_remark: "(P.983-1057)" },
                    { id: 9, range_remark: "(P.1058-1146)" },
                    { id: 10, range_remark: "全A (P.909-1022)" },
                    { id: 11, range_remark: "全B (P.1023-1146)" },
                ],
            },
        ],
    },
    {
        code: "I",
        title: "金心",
        coverImgFile: "cover_I.jpg",
        author: "釋迦牟尼佛、須菩提尊者（譯：鳩摩羅什）",//釋迦牟尼
        introHtml: "《金剛經》以禪宗之大力弘揚，加上士大夫大多尊崇此經，認為讀通之後可以放下執著並成就佛果，民間則認為本經典有不可思議感應，單純誦經時也能感召金剛不動佛、金剛般若菩薩、金剛藏王菩薩、四金剛菩薩（金剛眷、金剛索、金剛愛、金剛語）、八大金剛來護法。明朝末年的祕密宗教大力推崇《金剛經》，將「空」等同於「道」，視之為能源出萬物的「真空家鄉」。<br><br>心經是《大品般若經》的別生經，取自〈習應品〉和〈勸持品〉，乃《大般若經》的精要，無序分與流通分。與《金剛經》並為通行最廣的般若經，地位重要，曾被稱作《經王》。",
        belongTo: "Si",
        subjects: [
            {
                id: 1,
                papers: [
                    { id: 1, range_remark: "(1～5)" },
                    { id: 2, range_remark: "(6～10)" },
                    { id: 3, range_remark: "(11～16)" },
                    { id: 4, range_remark: "(17～21)" },
                    { id: 5, range_remark: "(22～26)" },
                    { id: 6, range_remark: "(27～32)" },
                    { id: 7, range_remark: "(1～10)" },
                    { id: 8, range_remark: "(11～21)" },
                    { id: 9, range_remark: "(22～32)" },
                    { id: 10, range_remark: "(1～16)" },
                    { id: 11, range_remark: "(17～32)" },
                    { id: 12, range_remark: "(1～32)" },
                    { id: 13, range_remark: "全(1～32)" },
                ],
            },
        ],
    },
    {
        code: "J",
        title: "六祖壇經",
        coverImgFile: "cover_J.jpg",
        author: "六祖惠能（口述者）",//六祖惠能說，弟子法海集錄
        introHtml: "《六祖壇經》記載惠能一生得法傳法的事跡及啟導門徒的言教，內容豐富，文字通俗，是研究禪宗思想淵源的重要依據。<br>最早六祖惠能大師應邀至大梵寺開示摩訶般若波羅蜜法，法海將此事記錄題為《摩訶般若波羅蜜經六祖惠能大師於韶州大梵寺施法一卷》。<br>《六祖壇經》可分三部份，第一部份即是在大梵寺開示「摩訶般若波羅蜜法」。<br>第二部分，回曹溪山後，傳授「無相戒」，故法海於書名補上「兼授無相戒」。這時《壇經》開始外傳。<br>第三部分，是六祖與弟子之間的問答。",
        belongTo: "Si",
        subjects: [
            {
                id: 1,
                papers: [
                    { id: 1, range_remark: "(p1~20)" },
                    { id: 2, range_remark: "(P21~P40)" },
                    { id: 3, range_remark: "(P41~P62)" },
                    { id: 4, range_remark: "(P63~P115)" },
                    { id: 5, range_remark: "(P85~P103)" },
                    { id: 6, range_remark: "(P104~P115)" },
                    { id: 7, range_remark: "(p1~p40)" },
                    { id: 8, range_remark: "(P41~P84)" },
                    { id: 9, range_remark: "(P85~P115)" },
                    { id: 10, range_remark: "(P1~P62)" },
                    { id: 11, range_remark: "(P1~P115)" },
                    { id: 12, range_remark: "(P1~P115)" },
                ],
            },
            {
                id: 2,
                papers: [
                    { id: 1, range_remark: "(p116~p126)", },
                    { id: 2, range_remark: "(P127~P137)", },
                    { id: 3, range_remark: "(P138~P149)", },
                    { id: 4, range_remark: "(P150~P164)", },
                    { id: 5, range_remark: "(P165~P177)", },
                    { id: 6, range_remark: "(P178~P191)", },
                    { id: 7, range_remark: "(p116~p191)", },
                    { id: 8, range_remark: "(p116~p191)", },
                    { id: 9, range_remark: "(p116~p191)", },
                    { id: 10, range_remark: "(p116~p191)", },
                    { id: 11, range_remark: "(p116~p191)", },
                    { id: 12, range_remark: "(p116~p191)", },
                ],
            },
            {
                id: 3,
                papers: [
                    { id: 1, range_remark: "(p192~206)", },
                    { id: 2, range_remark: "(p207-224)", },
                    { id: 3, range_remark: "(p225-236)", },
                    { id: 4, range_remark: "(p237-252)", },
                ],
            },
            {
                id: 4,
                papers: [
                    { id: 1, range_remark: "(p252-273)", },
                    { id: 2, range_remark: "(p274-282)", },
                    { id: 3, range_remark: "(p283-298)", },
                    { id: 4, range_remark: "(p299-320)", },
                    { id: 5, range_remark: "(p321-342)", },
                ],
            },
        ],
    },
    {
        code: "H",
        title: "道清",
        coverImgFile: "cover_H.jpg",
        author: "老子",
        introHtml:
            "《道德經》以哲學意義之「道」與「德」為綱，論述修身、治國、用兵、養生之理，而多以政治為旨歸，對傳統思想、科學、政治、文學、藝術等領域產生了深刻影響。<br><br>《清靜經》分上下兩章，上章言好清靜則得道，下章說勿貪求而沉苦海。",
        belongTo: "Dao",
        subjects: [
            {
                id: 1,
                papers: [
                    { id: 1, range_remark: "(1-15)" },
                    { id: 2, range_remark: "(16-30)" },
                    { id: 3, range_remark: "(31-45)" },
                    { id: 4, range_remark: "(46-60)" },
                    { id: 5, range_remark: "(61-75)" },
                    { id: 6, range_remark: "(76-81 +清靜經)" },
                    { id: 7, range_remark: "(1-30)" },
                    { id: 8, range_remark: "加強 (31-60)" },
                    { id: 9, range_remark: "(61-81)" },
                    { id: 10, range_remark: "加強 (1-45)" },
                    { id: 11, range_remark: "加強 (46-81 +清靜經)" },
                    { id: 12, range_remark: "加強 (1-81 +清靜經)" },
                    { id: 13, range_remark: "(1-81)+清靜經" },
                ],
            },
        ],
    },
];

function getSubjectTitle_fromDB( scripture , subjectId ) {
    let subjectChinesePostfix = ''
    if (scripture.subjects.length>1){ //.length
        subjectChinesePostfix = `(${number_To_Chinese(subjectId)})`
    }

    return scripture.title + subjectChinesePostfix
}

// function getPaperScheme_withDB(scriptureCode , subjectId , paperId) {
//     let scheme = getSubjectScheme(scriptureCode , subjectId); //多找了一次
//     let scheme = getSubjectScheme(scriptureCode , subjectId); //多找了一次

//     let scripture = scriptures_inDB.find( scripture => scripture.code == scriptureCode);
//     let subject = scripture.find( subject => subject.id == subjectId);
//     let paper = subject.papers.find((paper) => paper.id == paperId);

//     scheme.paperId = paperId;
//     scheme.range_remark = paper.range_remark;

//     return scheme;
// }