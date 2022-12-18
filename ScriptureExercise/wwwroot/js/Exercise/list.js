let vue_exercise_list = new Vue({
    el: "#vue_exercise_list",
    data: {
        scriptures: scriptures_inDB.filter((s,i)=> ScriptueShowList.includes(s.id)),//只顯示珍藏

        // /Exercise/生晨3_/卷別3
        scriptureChoosed: null, //可能可從cookie 或...取得偏好
        fields: [
            {
                key: "id",
                label: "卷別",
            },
            {
                key: "range_remark",
                label: "範圍註記",
            },
            {
                key: "link",
                label: "連結",
            },
        ],
    },
    beforeMount() {
        this.scriptureChoosed = this.scriptures[0]; //珍藏的經典中選第一部
    },
    methods: {
        chooseScripture(s) {
            if (this.scriptureChoosed != s) {
                this.scriptureChoosed = s;
            }
        },
    },
    watch: {},
    computed: {},
});
