let vue_ = new Vue({
    el: "#vue_about",
    data: {},
    mounted() { },
    methods: {},
    watch: {},
    computed: {},
    components: {},
});

function copy(value){
    const el = document.createElement('textarea');
    el.value = value;
    document.body.appendChild(el);
    el.select();
    document.execCommand('copy');
    document.body.removeChild(el);

    swalToast.fire({
        icon: 'success',
        title: '已複製'
    })
}