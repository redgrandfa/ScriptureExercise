let vue_exercise_list = new Vue({
    el: "#vue_exercise_list",
    data: {
        scriptures: scriptures_inDB,
        scriptureChoosed: null, //從cookie 或...取得偏好
        fields: [
            {
                key: "order",
                label: "卷別",
            },
            {
                key: "range_remark",
                label: "範圍",
            },
            {
                key: "link",
                label: "連結",
            },
        ],
    },
    mounted() {
        this.scriptureChoosed = this.scriptures[0];
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
