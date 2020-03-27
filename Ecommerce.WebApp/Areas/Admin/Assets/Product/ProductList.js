﻿userlist = new Vue({
    el: "#productlist",
    data: {
        loading: true,
        products: [],
        productcategories: [],
        currentUserName: null
    },
    methods: {
        bind: function (result) {
            this.products = result.products;
            this.productcategories = result.productcategories;
        },
        load: function () {
            var self = this;
            fetch(baseUrl + "admin/product/list")
                .then(function (response)
                {
                    return response.json();
                })
                .then(function (result) {
                    self.bind(result);

                    self.loading = false;
                })
                .catch(function (error) { console.log("error:", error); });
        },
        remove: function (product) {
            var self = this;

            var productInfo = "";
            if (product) {
                if (product.name && product.name.length > 0) {
                    productInfo += ' <br/>"' + product.name + '"';
                }
            }
            else {
                return;
            }

            alert.open({
                title: "Delete user",
                body: "delete user confirm" + productInfo,
                confirmCss: "btn-danger",
                confirmIcon: "fa fa-trash",
                confirmText: "delete",
                onConfirm: function () {
                    fetch(baseUrl + "admin/product/delete/" + product.id)
                        .then(function (response) {
                            ok = response.ok;
                            return response.json();
                        })
                        .then(function (result) {
                            notifications.push(result);

                            self.load();
                        })
                        .catch(function (error) {
                            console.log("error:", error);

                            notifications.push({
                                body: error,
                                type: "danger",
                                hide: true
                            });
                        });
                }
            });
        }
      
    }

});