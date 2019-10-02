var app = new Vue({
    el: '#app',
    data: {
        
            editing: false,
            loading: false,
            objectIndex: 0,
            robots: [],
            robotModel: {
                robotID: 0,
                model: "Model",
                function: "Function",
                movementType: "Movement",
                price: 9999.99,
            },
        },
    mounted() {
        this.getRobots();
    },
    methods: {
        getRobot(id) {
            this.loading = true;
            console.log('GET ID:' + id);
            axios.get('/robots/' + id)
                .then(res => {
                    console.log(res);
                    var robot = res.data;
                    this.robotModel = {
                        robotID: res.data.robotID,
                        model: robot.model,
                        function: robot.function,
                        movementType: robot.movementType,
                        price: robot.price
                    };
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },
        getRobots() {
            this.loading = true;
            axios.get('/robots')
                .then(res => {
                    console.log(res);
                    this.robots = res.data;
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },
        createRobot() {
            this.loading = true;
            axios.post('/robots', this.robotModel)
                .then(res => {
                    console.log(res);
                    this.robots.push(res.data);
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        updateRobot() {
            this.loading = true;
            axios.put('/robots', this.robotModel)
                .then(res => {
                    console.log(res);
                    this.robots.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        deleteRobot(id, index) {
            this.loading = true;
            axios.delete('/robots/' + id)
                .then(res => {
                    console.log(res);
                    this.robots.splice(index, 1);
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },
        editRobot(id, index) {
            this.editing = true;
            this.objectIndex = index;
            this.getRobot(id);
        },
        cancel() {
            this.editing = false;
        },
        newRobot() {
            this.editing = true;
            this.robotModel.robotID = 0;
        },
    }
})