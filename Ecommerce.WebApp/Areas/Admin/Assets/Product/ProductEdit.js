/*global
    piranha userlist
*/
//Vue.use(CKEditor);
productedit = new Vue({
    el: "#productedit",
    
    data: {
        loading: true,
        isNew: false,
        productModel: null,
        currentProductName: null,
        imageData: [],
        formData : new FormData(),
        /* Your data and models here */
        myModel: '<p><span style="color: black;"></span></p>',

        /* Config can be declare here */
        myPlugins: [
            'advlist autolink lists link image charmap print preview anchor textcolor',
            'searchreplace visualblocks code fullscreen',
            'insertdatetime media table contextmenu paste code directionality template colorpicker textpattern'
        ],
        myToolbar1: 'undo redo | bold italic strikethrough | forecolor backcolor | template link | bullist numlist | ltr rtl | removeformat | image',
        myToolbar2: '',
        myOtherOptions: {
            height: 300,
            templates: [
                { title: 'Test template 1', content: 'Test 1' },
                { title: 'Test template 2', content: 'Test 2' }
            ],
            content_css: 'css/tinymce-content.css',
            //,width:600,
            //directionality: 'rtl',
            //theme: 'modern',
            //menubar: false
            //, etc...
            file_browser_callback: function RoxyFileBrowser(field_name, url, type, win) {
                var cmsURL = '/lib/fileman/index.html?integration=tinymce4';  // script URL - use an absolute path!
                if (cmsURL.indexOf("?") < 0) {
                    cmsURL = cmsURL + "?type=" + type;
                }
                else {
                    cmsURL = cmsURL + "&type=" + type;
                }
                cmsURL += '&input=' + field_name + '&value=' + win.document.getElementById(field_name).value;
                tinyMCE.activeEditor.windowManager.open({
                    file: cmsURL,
                    title: 'Roxy File Browser',
                    width: 850, // Your dimensions may differ - toy around with them!
                    height: 650,
                    resizable: "yes",
                    plugins: "media",
                    inline: "yes", // This parameter only has an effect if you use the inlinepopups plugin!
                    close_previous: "no"
                }, {
                    window: win,
                    input: field_name
                });
                return false;
            },
            file_browser_callback_types: 'file image media'
        },
        
        
    },
   
    components: {
        'tinymce': VueEasyTinyMCE
    },
    methods: {
        bind: function (result) {
            this.productModel = result;
            this.isNew = result.product.id === "00000000-0000-0000-0000-000000000000";
        },
        load: function (id, isNew) {
            var self = this;

            var url = isNew ? baseUrl + "admin/product/add" : baseUrl + "admin/product/edit/" + id;

            fetch(url)
                .then(function (response) {
                    return response.json();
                })
                .then(function (result) {
                    self.bind(result);
                    self.loading = false;
                })
                .catch(function (error) { console.log("error:", error); });
        },
        getProductRows: function () {
            var productRows = Array();
            $.each(this.productModel.productCategorys, function (key, value) {
                productRows.push(value);
            });
            return productRows;
        },
        save: function () {
            // Validate form
            var form = document.getElementById("usereditForm");
            if (form.checkValidity() === false) {
                form.classList.add("was-validated");
                return;
            }
            var files = document.getElementById("files");
             
            for (var i = 0; i < files.files.length; ++i) {
                    this.formData.append("files", files.files[i]);
                    this.imageData.push(files.files[i]);
                    this.productModel.productImages.push(files.files[i]);
                }
            
            
            var ok = false;
            var self = this;
           
            console.log(JSON.stringify(self.productModel));
            fetch(baseUrl + "admin/product/save", {
                method: "post",
                headers: {

                    "Content-Type": "application/json"
                },
                body: JSON.stringify(self.productModel, self.formData)
            })
                .then(function (response) {
                    ok = response.ok;
                    return response.json();
                })
                .then(function (result) {
                    if (ok) {
                        self.bind(result);

                        notifications.push({
                            body: "luu thanh cong",
                            type: "success",
                            hide: true
                        });
                    }
                    else if (result.status) {
                        notifications.push(result.status);
                    }
                    else {
                        notifications.push({
                            body: "<strong>" + "co loi xay ra" + "</strong>",
                            type: "danger",
                            hide: true
                        });
                    }

                })
                .catch(function (error) {
                    notifications.push({
                        body: error,
                        type: "danger",
                        hide: true
                    });

                    console.log("error:", error);
                });
        },
        remove: function (productId) {
            var self = this;

            alert.open({
                title: "Delete product",
                body: "delete user confirm" + productId,
                confirmCss: "btn-danger",
                confirmIcon: "fa fa-trash",
                confirmText: "delete",
                onConfirm: function () {
                    fetch(baseUrl + "admin/product/delete/" + productId)
                        .then(function (response) {
                            ok = response.ok;
                            return response.json();
                        })
                        .then(function (result) {
                            notifications.push(result);
                            if (ok) {
                                window.location = baseUrl + "admin/products/?d=1";
                            }
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
