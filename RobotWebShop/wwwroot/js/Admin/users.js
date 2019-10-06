var app = new Vue({
    el: '#app',
    data: {
        username: "",
        password: ""
    },
    mounted() {
        // TODO:Get All Users
    },
    methods: {

        createUser() {
            axios.post('/users', { username: this.username, password: this.password })
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.username = "";
                    this.password = "";
                });
        },

    },
})