var app = new Vue({
    el: '#app',
    data: {
        stockExists: false,
        loading: false,
        robots: [],
        selectedRobot: null,
        selectedStock: null,
        newStock: {
            robotID: 0,
            model: "Model",
            quantity: 1
        },
    },
    mounted() {
        this.getStock();
    },
    methods: {

        getStock() {
            this.loading = true;
            axios.get('/stocks')
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

        addStock() {
            this.loading = true;
            for (i = 0; i < this.selectedStock.length; ++i) {
                if (this.selectedStock[i].model == this.newStock.model) {
                    console.log('!!Exists!!')
                    this.stockExists = true;
                    break;
                }
                else {
                    console.log('OK')
                }
            }
            if (this.stockExists == false) {
                axios.post('/stocks', this.newStock)
                    .then(res => {
                        console.log(res);
                        this.selectedRobot.stock.push(res.data);
                    })
                    .catch(err => {
                        console.log(err)
                    })
                    .then(() => {
                        this.loading = false;
                        //this.selectedRobot = null;
                    });
                this.stockExists = false;
            }
            else {
                window.confirm("This Model aready exists!");
                this.stockExists = false;
            }

            
        },

        deleteStock(id, index) {
            axios.delete('/stocks/' + id)
                .then(res => {
                    console.log(res);
                    this.selectedRobot.stock.splice(index, 1);
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },

        updateStock() {
            var vm = this.selectedRobot.stock.map(x => {
                return {
                    stockID: x.stockID,
                    model: x.model,
                    quantity: x.quantity,
                    robotID: this.selectedRobot.robotID
                };
            });
            console.log(vm);
            axios.put('/stocks', {stock: vm})
                .then(res => {
                    console.log(res);
                    //this.selectedRobot.stock.splice(index, 1);
                })
                .catch(err => {
                    console.log(err)
                })
                .then(() => {
                    this.loading = false;
                });
        },

        selectRobot(robot) {
            this.selectedRobot = robot;
            this.selectedStock = robot.stock;
            this.newStock.robotID = robot.robotID;
        },
    },
})