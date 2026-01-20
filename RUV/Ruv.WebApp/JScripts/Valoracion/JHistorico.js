$(document).ready(function () {
   //$('.expandible').expander();
    $('.expandible dd:eq(0)').expander({
        //slicePoint: 80,  // default is 100
        //expandPrefix: ' ', // default is '... '
        expandText: '[Leer mas]', // default is 'read more'
        //collapseTimer: 5000, // re-collapses after 5 seconds; default is 0, so no re-collapsing
        userCollapseText: '[^]'  // default is 'read less'
    });
})