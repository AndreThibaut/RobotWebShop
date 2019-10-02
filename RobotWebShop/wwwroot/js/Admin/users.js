var app = new Vue({
    el: '#app',
    data: {
        username: "",
    },
    mounted() {
        // TODO:Get All Users
    },
    methods: {

        createUser() {
            axios.post('/users', { username: this.username })
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err)
                });
        },

    },
})