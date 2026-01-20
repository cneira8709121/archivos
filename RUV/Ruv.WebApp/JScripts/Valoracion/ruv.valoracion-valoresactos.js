
debugger
var ruv = ruv || {}; ruv.namespace('ruv.valoracion_valoresactos');
ruv.valoracion_valoresactos = (function (APP, $) {
    var windowPanel = $('#pnlValoresAA');
    function beautifyForms() {
        windowPanel.find('.valoresPanel').find('.lbl').addClass('smallbold');
    }
    function hideAllValidationControls() {
        windowPanel.find('span').filter(function () { return $(this).css('visibility') == 'hidden' }).hide();
    }
    function bindRichTextEditorBehavior() {
        windowPanel.find('textarea').each(function () {
            var $textArea = $(this), textArea = $(this)[0];
            new nicEditor().panelInstance(textArea.id);
            var editor = $textArea.closest('div.valoresPanel').find('.nicEdit-main');
            editor.parent().css({ width: '99%', 'max-height': '75px' });
            editor.parent().prev().css({ width: '99%' });
            editor.bind('blur keyup', function () {
                setTimeout(function () {
                    $textArea.val(editor.html());
                });
            });
            editor.bind('paste', function () {
                setTimeout(function () {
                    editor.html(cleanNonFormattingTags(editor.contents()));
                    $textArea.val(editor.html());
                }, 250);
            });
        });
    }
    function cleanNonFormattingTags(htmlContents) {
        if (htmlContents && htmlContents.length) {
            var result = '';
            htmlContents.each(function () {
                var $child = $(this), type = $child.prop('tagName'), isTextNode = this.nodeName == "#text";
                if (isTextNode) {
                    result += this.textContent;
                }
                else if (type == 'B' || type == 'U' || type == 'I' || type == 'BR') { // Allow only these types of tags
                    var innerContent = cleanNonFormattingTags($child.contents());
                    var $newTag = $(document.createElement(type)).html(innerContent);
                    result += $newTag[0].outerHTML;
                }
                else {
                    result += cleanNonFormattingTags($child.contents());
                }
            });
            return result;
        }
        return htmlContents.text();
    }
    function bindExpandCollapseEditorBehavior() {

    }

    return {
        initialize: function () {
            windowPanel = $('#pnlValoresAA');
            if (!windowPanel.find('.nicEdit-main') || windowPanel.find('.nicEdit-main').length == 0) {
                beautifyForms();
                hideAllValidationControls();
                bindRichTextEditorBehavior();
            }
        },
        clean: function (htmlContents) {
            return cleanNonFormattingTags(htmlContents);
        }
    };
} (ruv, jQuery));