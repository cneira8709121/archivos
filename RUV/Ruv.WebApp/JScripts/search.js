$(function () {
    var linkExpandCollapse = $('#expandCollapseFilters');
    var panelExpandCollapse = $('.actionFilterBox .content');
    if (linkExpandCollapse && linkExpandCollapse.length) {
        linkExpandCollapse.bind('click', function () {
            var state = linkExpandCollapse.data('state');
            if (state == 'open') {
                linkExpandCollapse.text('Mostrar Filtros').data('state', 'closed');
                panelExpandCollapse.slideUp();
            }
            else {
                linkExpandCollapse.text('Ocultar Filtros').data('state', 'open');
                panelExpandCollapse.slideDown();
            }
        });
    }
    var filterButton = panelExpandCollapse.find('input[data-filter="true"]');
    if (filterButton.length) {
        panelExpandCollapse.find('input[type="text"], select').bind('keyup', function (event) {
            if (event.which == 13) {
                event.stopImmediatePropagation();
                filterButton.click();
            }
        });
    }
});