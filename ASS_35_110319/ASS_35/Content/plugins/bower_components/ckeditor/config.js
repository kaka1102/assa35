/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    config.plugins = 'dialogui,html5validation,dialog,a11yhelp,about,video,button,toolbar,floatpanel,notification,basicstyles,bidi,blockquote,clipboard,codemirror,lineutils,widget,codesnippet,panelbutton,panel,colorbutton,colordialog,menu,contextmenu,dialogadvtab,div,elementspath,enterkey,entities,popup,filebrowser,find,fakeobjects,flash,floating-tools,floatingspace,listblock,richcombo,font,format,forms,googledocs,horizontalrule,htmlwriter,iframe,image,imagebrowser,imagepaste,indent,indentblock,indentlist,justify,menubutton,language,link,list,liststyle,magicline,newpage,pagebreak,pastefromexcel,pastefromword,pastetext,pbckcode,preview,print,removeformat,resize,save,scayt,selectall,showblocks,showborders,smiley,sourcearea,specialchar,stylescombo,tab,table,tabletools,tabletoolstoolbar,templates,undo,uploadcare,filetools,notificationaggregator,uploadwidget,uploadimage,wenzgmap,wysiwygarea,youtube';
    config.skin = 'office2013';
    // %REMOVE_END%
    config.language = 'vi';
    config.uiColor = '#778eb2';
    config.height = 400;
    config.fullPage = true;
    config.extraPlugins = 'video';
    config.extraPlugins = 'codemirror,preview,selectall,googledocs,eqneditor,html5validation,wordcount,htmlwriter,undo,uploadimage';

    config.baseFloatZIndex = 9000;
    config.allowedContent = true;

    config.codemirror = {
        theme: 'xq-light',
        lineNumbers: true,
        lineWrapping: true,
        matchBrackets: true,
        autoCloseTags: true,
        autoCloseBrackets: true,
        enableSearchTools: true,
        enableCodeFolding: true,
        enableCodeFormatting: true,
        autoFormatOnStart: true,
        autoFormatOnModeChange: true,
        autoFormatOnUncomment: true,
        mode: 'htmlmixed',
        showSearchButton: true,

        // Whether or not to show Trailing Spaces
        showTrailingSpace: true,

        // Whether or not to highlight all matches of current word/selection
        highlightMatches: true,

        // Whether or not to show the format button on the toolbar
        showFormatButton: true,

        // Whether or not to show the comment button on the toolbar
        showCommentButton: true,

        // Whether or not to show the uncomment button on the toolbar
        showUncommentButton: true,


        // Whether or not to show the showAutoCompleteButton button on the toolbar
        showAutoCompleteButton: true,
        // Whether or not to highlight the currently active line
        styleActiveLine: true
    };
};

