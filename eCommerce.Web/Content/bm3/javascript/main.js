;(function($) {

   'use strict'
        var isMobile = {
            Android: function() {
                return navigator.userAgent.match(/Android/i);
            },
            BlackBerry: function() {
                return navigator.userAgent.match(/BlackBerry/i);
            },
            iOS: function() {
                return navigator.userAgent.match(/iPhone|iPad|iPod/i);
            },
            Opera: function() {
                return navigator.userAgent.match(/Opera Mini/i);
            },
            Windows: function() {
                return navigator.userAgent.match(/IEMobile/i);
            },
            any: function() {
                return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
            }
        }; // is Mobile

        var responsiveMenu = function() {
            var menuType = 'desktop';

            $(window).on('load resize', function() {
                var currMenuType = 'desktop';

                if ( matchMedia( 'only screen and (max-width: 991px)' ).matches ) {
                    currMenuType = 'mobile';
                }

                if ( currMenuType !== menuType ) {
                    menuType = currMenuType;

                    if ( currMenuType === 'mobile' ) {
                        var $mobileMenu = $('#mainnav').attr('id', 'mainnav-mobi').hide();
                        var hasChildMenu = $('#mainnav-mobi').find('li:has(ul)');
                        var hasChildMenuMega = $('#mainnav-mobi').find('li:has(div.submenu)');

                        $('#header').after($mobileMenu);
                        hasChildMenu.children('ul').hide();
                        hasChildMenu.children('a').after('<span class="btn-submenu"></span>');
                        hasChildMenuMega.children('div.submenu').hide();
                        $('ul.submenu-child').hide();
                        hasChildMenuMega.find('h3').append('<span class="btn-submenu-child"></span>');
                        $('.btn-menu').removeClass('active');

                    } else {
                        var $desktopMenu = $('#mainnav-mobi').attr('id', 'mainnav').removeAttr('style');
                        $desktopMenu.find('.submenu').removeAttr('style');
                        $('#header').find('.nav-wrap').append($desktopMenu);
                        $('.btn-submenu').remove();
                        $('ul.submenu-child').show();
                        $('h3').children('span').removeClass('btn-submenu-child');
                    }
                }
            });

            $('.btn-menu').on('click', function() {         
                $('#mainnav-mobi').slideToggle(300);
                $(this).toggleClass('active');
                return false;
            });

            $(document).on('click', '#mainnav-mobi li.has-mega-menu .row .btn-submenu-child', function(e) {
                $(this).toggleClass('active').parent('h3.cat-title').next('.submenu-child').slideToggle(400);
                e.stopImmediatePropagation();
                return false;
            });

            $(document).on('click', '#mainnav-mobi li .btn-submenu', function(e) {
                $(this).toggleClass('active').next('.submenu').slideToggle(400);
                e.stopImmediatePropagation();
                return false;
            });

        }; // Responsive Menu       

        var responsiveMenuMega_S2 = function() {
            var menuType = 'desktop';

            $(window).on('load resize', function() {
                var currMenuType = 'desktop';

                if ( matchMedia( 'only screen and (max-width: 991px)' ).matches ) {
                    currMenuType = 'mobile';
                }

                if ( currMenuType !== menuType ) {
                    menuType = currMenuType;

                    if ( $('body').hasClass('grid') ) {
                        if (currMenuType === 'mobile') {
                            var $mobileMenuMegaV2 = $('#mega-menu').attr('id', 'mega-mobile').hide();
                            var ChildMenuMegaV2 = $('.header-bottom').find('.grid-right');
                            var ChildDropmenuV2 = $('.drop-menu').children('.one-third');

                            $('.btn-mega').hide();
                            $('#header').after($mobileMenuMegaV2);
                            ChildMenuMegaV2.append('<div class="btn-menu-mega"><span></span></div>');

                            $('.drop-menu').hide();
                            $mobileMenuMegaV2.find('.dropdown').append('<span class="btn-dropdown"></span>');

                            ChildDropmenuV2.children('ul').hide();
                            $('.drop-menu').find('.cat-title').append('<span class="btn-dropdown-child"></span>');

                        } else {
                            var $desktopMenuMegaV2 = $('#mega-mobile').attr('id', 'mega-menu').removeAttr('style');

                            $desktopMenuMegaV2.find('.drop-menu').removeAttr('style');
                            $('.header-bottom.style1').find('.grid-left').append($desktopMenuMegaV2);
                        }
                    };

                };
                
            });

            $(document).on('click', '#mega-mobile ul.menu li a .btn-dropdown', function(e) {
                $(this).toggleClass('active').closest('li').children('.drop-menu').slideToggle(400);
                e.stopImmediatePropagation();
                return false;
            });

            $(document).on('click', '#mega-mobile .btn-dropdown-child', function(e) {
                $(this).toggleClass('active').closest('.one-third').children('ul').slideToggle(400);
                e.stopImmediatePropagation();
                return false;
            });

        }; // Responsive Menu Mega S2

        var responsiveMenuMega = function() {
            var menuType = 'desktop';

            $(window).on('load resize', function() {
                var currMenuType = 'desktop';

                if ( matchMedia( 'only screen and (max-width: 991px)' ).matches ) {
                    currMenuType = 'mobile';
                }

                if ( currMenuType !== menuType ) {
                    menuType = currMenuType;

                    if ( currMenuType === 'mobile' ) {
                        var $mobileMenuMega = $('#mega-menu').attr('id', 'mega-mobile').hide();
                        var ChildMenuMega = $('.header-bottom').find('.col-2');
                        var ChildDropmenu = $('.drop-menu').children('.one-third');

                        $('.btn-mega').hide();
                        $('#header').after($mobileMenuMega);
                        ChildMenuMega.append('<div class="btn-menu-mega"><span></span></div>');

                        $('.drop-menu').hide();
                        $mobileMenuMega.find('.dropdown').append('<span class="btn-dropdown"></span>');

                        ChildDropmenu.children('ul').hide();
                        $('.drop-menu').find('.cat-title').append('<span class="btn-dropdown-child"></span>');

                    } else {
                        var $desktopMenuMega = $('#mega-mobile').attr('id', 'mega-menu').removeAttr('style');

                        $('.btn-mega').show();
                        $desktopMenuMega.find('.drop-menu').removeAttr('style');
                        $('.header-bottom').find('.col-2').append($desktopMenuMega);
                        $('.btn-menu-mega').remove();
                        $('.btn-dropdown-child').remove();
                        $('.drop-menu').children('.one-third').children('ul').show();
                    }
                }
            });

            $(document).on('click', '.btn-menu-mega', function() {       
                $('#mega-mobile').slideToggle(300);
                $(this).toggleClass('active');
                return false;
            });

            $(document).on('click', '#mega-mobile ul.menu li a .btn-dropdown', function(e) {
                $(this).toggleClass('active').closest('li').children('.drop-menu').slideToggle(400);
                e.stopImmediatePropagation();
                return false;
            });

            $(document).on('click', '#mega-mobile .btn-dropdown-child', function(e) {
                $(this).toggleClass('active').closest('.one-third').children('ul').slideToggle(400);
                e.stopImmediatePropagation();
                return false;
            });

            
        }; // Responsive Menu Mega


        var waveButton = function () {
            Waves.attach('.button', ['waves-button', 'waves-float']);
            Waves.init();
        };

        var slider = function() {
            $(".owl-carousel").owlCarousel({
                autoplay:true,
                nav: false,
                responsive: true,
                margin:0,
                loop:true,
                items:1
            });
        };// slider

        var slideBanner = function() {
            $(".owl-carousel-banner").owlCarousel({
                autoplay:true,
                nav: true,
                dots: true,
                responsive: true,
                margin:20,
                loop:true,
                items:1
            });
        };// slide Banner

        var slideProduct = function() {
            $(".owl-carousel-product").owlCarousel({
                autoplay:true,
                nav: true,
                dots: true,
                responsive: true,
                margin:10,
                loop:true,
                items:5,
                responsive:{
                    0:{
                        items: 2,
                        dots: false,
                        margin:10,
                    },
                    479:{
                        items: 2,
                        dots: false
                    },
                    600:{
                        items: 2,
                        dots: false
                    },
                    768:{
                        items: 3,
                         margin:20,
                    },
                    991:{
                        items: 3
                    },
                    1200: {
                        items: 4
                    }
                }
            });
        };// slide Product

        var slideBrand = function() {
            $(".owl-carousel-brand").owlCarousel({
                autoplay:true,
                nav: false,
                dots: false,
                responsive: true,
                margin:20,
                loop:true,
                items:5,
                responsive:{
                    0:{
                        items: 2
                    },

                    479:{
                        items: 4
                    },
                    768:{
                        items: 4
                    },
                    991:{
                        items: 5
                    },
                    1200: {
                        items: 5
                    }
                }
            });
        };// slide Brand

        var slickGallery = function() {
			$('.product-gallery').slick({
				infinite: true,
				arrows: true,
				slidesToShow: 6,
				autoplaySpeed: 7000,
				responsive: [
					{
						breakpoint: 1024,
						settings: {
							slidesToShow: 3,
							slidesToScroll: 3
						}
					},
					{
						breakpoint: 600,
						settings: {
							slidesToShow: 2,
							slidesToScroll: 2
						}
					},
					{
						breakpoint: 480,
						settings: {
							slidesToShow: 1,
							slidesToScroll: 1
						}
					}
				]
			});
		}; // Slick Gallery

        var slickLightbox = function() {
			$('.product-gallery').slickLightbox();
		}; // Slick Lightbox

        var goTop = function(){
            var gotop = $('.btn-scroll');
            gotop.on('click', function() {
                $('html, body').animate({ scrollTop: 0}, 800, 'easeInOutExpo');
                return false;
            });
        }; // Go Top

        var overlay = function(){
            var megaMenu = $('ul.menu li');
            megaMenu.on('mouseover', function() {
                $(this).closest('.boxed').children('.overlay').css({
                    opacity: '1',
                    display: 'block',
                });
            });
            megaMenu.on('mouseleave', function() {
                $(this).closest('.boxed').children('.overlay').css({
                    opacity: '0',
                    display: 'none',
                });
            });
        }; // Overlay
		
        var toggleSearch = function() {
            $( ".menu-search a").on('click', function() {
              $(".box-cart").toggleClass('show');
              $(".top-search").toggleClass('show');
            });
        }; // Toggle WiSearchdget
		
        var resizeWidget = function() {
            $(window).on('load resize', function() {
				var currMenuType = 'desktop';
				
                if ( matchMedia( 'only screen and (max-width: 991px)' ).matches ) {
                    currMenuType = 'mobile';
                }
				
				if (currMenuType == "mobile") {
					$( ".widget .widget-title h3 span" ).removeClass("active").addClass("active")
					$( ".widget .widget-title h3 span" ).closest('.widget').children('.widget-content').slideUp(300);
				}
				
				if (currMenuType == "desktop") {
					$( ".widget .widget-title h3 span" ).removeClass("active")
					$( ".widget .widget-title h3 span" ).closest('.widget').children('.widget-content').slideDown(300);
				}
			});
        }; // Toggle Widget
		
        var toggleWidget = function() {
			$( ".widget .widget-title h3 span" ).on('click', function() {
				$(this).toggleClass('active');
				$(this).closest('.widget').children('.widget-content').slideToggle(300);
			});
        }; // Toggle Widget

        var toggleCatlist = function() {
            $('.cat-list.style1').each(function() {
                $(this).children('li').children('ul.cat-child').hide();
                $( ".cat-list.style1 li span" ).on('click', function() {
                    $(this).parent('li').toggleClass('active');
                    $(this).toggleClass('active');
                    $(this).parent('li').children('ul.cat-child').slideToggle(300);
                });
            })
        }; // Toggle Cat List
		
        var tabProductDetail = function() {
            $('.flat-product-content').each(function() {
                $(this).find('ul.product-detail-bar').children().first().addClass('active');
                $(this).find('.detail-bar').children('.row').first().show().siblings().hide();
                $(this).find('ul.product-detail-bar').children('li').on('click', function(e) {
                    var liActive = $(this).index();
                    $(this).addClass('active').siblings().removeClass('active');
                     $(this).closest('.flat-product-content').find('.detail-bar').children('.row').eq(liActive).fadeIn(1000).show().siblings().hide();
                    e.preventDefault();
                });
            });
        }; // Tab Productdetail

        var filterPrice = function() {
            if( $().slider ) {
                $( function() {
                    $( "#slider-range" ).slider({
                      range: true,
                      min: 2000,
                      max: 20000,
                      values: [ 3000, 10000 ],
                      slide: function( event, ui ) {
                        $( "#amount" ).val( "S/" + ui.values[ 0 ] + " - " + "S/" + ui.values[ 1 ] );
                      }
                    });
                    $( "#amount" ).val( "S/" + $( "#slider-range" ).slider( "values", 0 ) + " - " + "S/" + $( "#slider-range" ).slider( "values", 1 ) );
                });
            }
        }; // Filter Price

        var removePreloader = function() { 
            $(window).on('load', function() {
                setTimeout(function() {
                    $('.preloader').hide(); }, 300           
                ); 
            });  
        }; //remove Preloader

    // Dom Ready
    $(function() {
        responsiveMenu();
        waveButton();
        slider();
        slideBanner();
        slideBrand();
        slideProduct();
		slickGallery();
		slickLightbox();
        goTop();
		toggleSearch();
		resizeWidget();
		toggleWidget();
		toggleCatlist();
		tabProductDetail();
		filterPrice();
        removePreloader();
    });

})(jQuery);