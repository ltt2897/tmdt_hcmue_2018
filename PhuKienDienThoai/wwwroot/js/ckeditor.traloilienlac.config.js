CKEDITOR.editorConfig = (config) =>
{
    config.toolbar = [
        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
        { name: 'paragraph', items: ['NumberedList', 'Blockquote', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
        { name: 'styles', items: ['Bold', 'Italic', 'Underline', 'Font', 'FontSize'] },
    ];
}