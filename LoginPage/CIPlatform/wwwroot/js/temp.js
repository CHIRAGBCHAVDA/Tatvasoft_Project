CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [{ name: 'basicstyles', groups: ['basicstyles'] },
    { name: 'colors', groups: ['colors'] },
    { name: 'tools', groups: ['tools'] },
    { name: 'others', groups: ['others'] },
        '/',
    { name: 'textalign', groups: ['textalign'] },
    { name: 'list', groups: ['list'] },
    { name: 'indent', groups: ['indent'] },
    { name: 'links', groups: ['links'] },
    { name: 'insert', groups: ['insert'] },
        '/',
    { name: 'document', groups: ['document'] },
    { name: 'undo', groups: ['undo'] }
    ];

    config.removeButtons = 'Subscript,Superscript,Save,Templates,ExportPdf,Preview,Print,Cut,Copy,Paste,PasteText,PasteFromWord,Find,Replace,SelectAll,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,CopyFormatting,RemoveFormat,Blockquote,CreateDiv,NewPage,BidiLtr,BidiRtl,Anchor,Language,Table,HorizontalRule,SpecialChar,PageBreak,Iframe,Styles,Format,Font,FontSize,TextColor,BGColor,Maximize,ShowBlocks,About';

    // Add the buttons you want to show in the toolbar
    config.toolbar = [{ name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'TextColor', 'BGColor', 'Highlight'] },
    { name: 'smiley', items: ['Smiley'] },
    { name: 'textalign', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
    { name: 'list', items: ['NumberedList', 'BulletedList'] },
    { name: 'indent', items: ['Outdent', 'Indent'] },
    { name: 'links', items: ['Link', 'Unlink'] },
    { name: 'insert', items: ['Image', 'Table', 'HorizontalRule'] },
    { name: 'document', items: ['TextDirection'] },
    { name: 'undo', items: ['Undo', 'Redo'] }
    ];
};
