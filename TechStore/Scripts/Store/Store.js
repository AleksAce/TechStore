    //Note: Use this with main-content and preloader classes when we wait
var scrollPageToTop = function () {
    $("html,body").animate({ scrollTop: 0 }, "slow");
}
var showLoader = function (interval) {
        $('.main-content').css('display', 'none');
        $('#preloader').css('display', 'initial');
       var interval =setInterval(function () {
        $('.main-content').fadeIn(500);
    $('#preloader').css('display', 'none');
        }, interval)
    };
    $(window).on("load", function () {
        $('.main-content').fadeIn(500);
        $('#preloader').css('display', 'none');
        //Disable this to stop the automatic loader
        //showLoader(10000);
    });
    
    $(document).ready(function () {
       
        //Todo: Fix this!
        $('.scroll-to-top').click(function () {
            scrollPageToTop();
        });
    });