/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.extraPlugins = 'autolink,autoembed,embed,embedbase';
    config.embed_provider = '//ckeditor.iframe.ly/api/oembed?url={url}&callback={callback}';

    config.autoEmbed_widget = 'embed';

    //config.extraPlugins = '';

    config.filebrowserBrowseUrl = '/Scripts/ckfinder/ckfinder.html',
        config.filebrowserUploadUrl = '/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',
        config.filebrowserWindowWidth = '1000',
        config.filebrowserWindowHeight = '700'

};
