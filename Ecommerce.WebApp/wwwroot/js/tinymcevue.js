﻿var VueEasyTinyMCE = {
    props: {
        id: { type: String, default: "editor" },
        value: { default: "" },
        toolbar1: { default: "" },
        toolbar2: { default: "" },
        plugins: {
            default: function () {
                return []
            },
            type: Array
        },
        other: {
            default: function () {
                return {}
            },
            type: Object
        }
    },
    data: function () {
        return { objTinymce: null }
    },
    template: '<textarea :id="computedId" :value="value"></textarea>',
    computed: {
        computedId: function () {
            return "editor" === this.id || "" === this.id || null === this.id ? "editor-" + this.guidGenerator() : this.id
        }
    }, mounted: function () {
        var t = this, e = {
            target: this.$el,
            toolbar1: this.toolbar1,
            toolbar2: this.toolbar2,
            plugins: this.plugins,
            init_instance_callback: function (e) {
                e.on("Change KeyUp Undo Redo",
                    function (n) {
                    t.updateValue(e.getContent())
                }), 
                    t.objTinymce = e
            }
        },
            n = Object.assign({}, e, this.other); tinymce.init(n)
    },
    methods: {
        guidGenerator: function () {
            function t() {
                return Math.floor(65536 * (1 + Math.random())).toString(16).substring(1)
            }
            return t() + t() + "-" + t() + "-" + t() + "-" + t() + "-" + t() + t() + t()
        },
        updateValue: function (t) { this.$emit("input", t) }
    },
    watch: {
        value: function (t, e) {
            this.value !== this.objTinymce.getContent() && this.objTinymce.setContent(this.value)
        }
    }
}; "undefined" != typeof module && module.exports && (module.exports = VueEasyTinyMCE);