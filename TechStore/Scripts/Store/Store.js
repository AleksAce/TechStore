    //Note: Use this with main-content and preloader classes when we wait
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
        //showLoader(5000);
    });

    $(document).ready(function () {
        $('#scroll-to-top').click(function () {
            $("html").animate({scrollTop:0}, "slow");
        });
    });