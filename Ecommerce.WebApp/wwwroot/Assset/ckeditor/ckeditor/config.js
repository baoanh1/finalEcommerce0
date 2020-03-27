/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';

    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Assset/ckeditor/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assset/ckeditor/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Assset/ckeditor/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Assset/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/data/';
    config.filebrowserFlashUploadUrl = '/Assset/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/Assset/ckeditor/ckfinder/');

    config.removePlugins = 'easyimage, cloudservices';
};