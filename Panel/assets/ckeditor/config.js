/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function(config) {
    // Define changes to default configuration here.
    // For complete reference see:
    // http://docs.ckeditor.com/#!/api/CKEDITOR.config

    // The toolbar groups arrangement, optimized for a single toolbar row.
    /*
     config.toolbarGroups = [
     { name: 'document',	   groups: [ 'mode', 'document', 'doctools' ] },
     { name: 'clipboard',   groups: [ 'clipboard', 'undo' ] },
     { name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ] },
     { name: 'forms' },
     { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
     { name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align', 'bidi' ] },
     { name: 'links' },
     { name: 'insert' },
     { name: 'styles' },
     { name: 'colors' },
     { name: 'tools' },
     { name: 'others' },
     { name: 'about' }
     ];
     */
    config.toolbar = 'Full';
    config.extraPlugins = 'blockquote,youtube';
    config.toolbar_Full = [
        {name: 'document', items: ['Source']}
        , {name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', '-', 'RemoveFormat']}
        , {name: 'clipboard', items: ['Undo', 'Redo']}
        , {name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker']}
        , {name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote']}
        , {name: 'links', items: ['Link', 'Unlink']}
//        , {name: 'styles', items: ['Styles', 'Format', 'FontSize']}
        , {name: 'styles', items: ['Styles', 'Format']}
        , {name: 'colors', items: ['TextColor']}
        , {name: 'tools', items: ['Maximize']}
        , {name: 'others', items: ['Youtube', 'Image', 'Image2']}
    ];

    // The default plugins included in the basic setup define some buttons that
    // are not needed in a basic editor. They are removed here.
    // config.removeButtons = 'Cut,Copy,Paste,Undo,Redo,Anchor,Underline,Strike,Subscript,Superscript';
    config.disableNativeSpellChecker = false;
//	config.extraPlugins = 'youtube';
    // config.allowedContent = true;
	config.extraPlugins = 'image';
	config.extraPlugins = 'image2';

    config.pasteFromWordPromptCleanup = true;
    config.pasteFromWordRemoveFontStyles = true;
    config.forcePasteAsPlainText = true;
    config.ignoreEmptyParagraph = true;
    config.removeFormatAttributes = true;

    config.height = '400px';
    config.disableObjectResizing = false;
    config.resize_enabled = true;
    config.resize_dir = 'vertical';
    config.resize_maxHeight = 1000;

    // Dialog windows are also simplified.
    config.removeDialogTabs = 'link:advanced';
};
