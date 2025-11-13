// ========================================
// CUSTOM OPTIMIZED JAVASCRIPT
// ========================================

(function($) {
    'use strict';

    // === PAGE LOAD ===
    $(window).on('load', function() {
        // Hide loader if exists
        $('.preloader').fadeOut('slow');
        
        // Animate elements on load
        $('.fade-in-up').each(function(i) {
            var $this = $(this);
            setTimeout(function() {
                $this.addClass('animated');
            }, i * 100);
        });
    });

    // === DOCUMENT READY ===
    $(document).ready(function() {
        
        // === SCROLL TO TOP ===
        $(window).scroll(function() {
            if ($(this).scrollTop() > 300) {
                $('#scrollUp').fadeIn('slow');
            } else {
                $('#scrollUp').fadeOut('slow');
            }
        });

        $('#scrollUp').click(function() {
            $('html, body').animate({scrollTop: 0}, 800);
            return false;
        });

        // === STICKY HEADER ===
        var headerHeight = $('#header').outerHeight();
        $(window).scroll(function() {
            if ($(this).scrollTop() > 100) {
                $('#header').addClass('sticky-header');
            } else {
                $('#header').removeClass('sticky-header');
            }
        });

        // === SMOOTH SCROLL FOR ANCHOR LINKS ===
        $('a[href^="#"]').on('click', function(e) {
            var target = $(this.hash);
            if (target.length) {
                e.preventDefault();
                $('html, body').animate({
                    scrollTop: target.offset().top - headerHeight
                }, 800);
            }
        });

        // === DROPDOWN HOVER (Desktop only) ===
        if ($(window).width() > 992) {
            $('.dropdown').hover(
                function() {
                    $(this).find('.dropdown-menu').first().stop(true, true).fadeIn(200);
                },
                function() {
                    $(this).find('.dropdown-menu').first().stop(true, true).fadeOut(200);
                }
            );
        }

        // === SEARCH FORM ENHANCEMENT ===
        $('.search-form input[type="text"]').on('focus', function() {
            $(this).parent().addClass('focused');
        }).on('blur', function() {
            $(this).parent().removeClass('focused');
        });

        // === PRODUCT CARD ANIMATIONS ===
        $('.product-card').hover(
            function() {
                $(this).find('.product-actions').fadeIn(200);
            },
            function() {
                $(this).find('.product-actions').fadeOut(200);
            }
        );

        // === ADD TO CART ANIMATION ===
        $('.add-to-cart-btn').on('click', function(e) {
            var btn = $(this);
            btn.addClass('loading');
            
            setTimeout(function() {
                btn.removeClass('loading').addClass('success');
                btn.html('<i class="fas fa-check"></i> Đã thêm');
                
                setTimeout(function() {
                    btn.removeClass('success');
                    btn.html('<i class="fas fa-shopping-cart"></i> Thêm vào giỏ');
                }, 2000);
            }, 1000);
        });

        // === QUANTITY CONTROLS ===
        $('.quantity-btn').on('click', function() {
            var $button = $(this);
            var $input = $button.siblings('input[type="number"]');
            var oldValue = parseFloat($input.val());
            var newVal;

            if ($button.hasClass('quantity-plus')) {
                newVal = oldValue + 1;
            } else {
                if (oldValue > 1) {
                    newVal = oldValue - 1;
                } else {
                    newVal = 1;
                }
            }

            $input.val(newVal);
            $input.trigger('change');
        });

        // === IMAGE LAZY LOADING ===
        if ('IntersectionObserver' in window) {
            var imageObserver = new IntersectionObserver(function(entries, observer) {
                entries.forEach(function(entry) {
                    if (entry.isIntersecting) {
                        var image = entry.target;
                        image.src = image.dataset.src;
                        image.classList.remove('lazy');
                        imageObserver.unobserve(image);
                    }
                });
            });

            document.querySelectorAll('img.lazy').forEach(function(img) {
                imageObserver.observe(img);
            });
        }

        // === PRICE RANGE SLIDER (if exists) ===
        if ($('.price-range-slider').length) {
            $('.price-range-slider').slider({
                range: true,
                min: 0,
                max: 50000000,
                values: [0, 50000000],
                slide: function(event, ui) {
                    $('#price-min').text(ui.values[0].toLocaleString('vi-VN'));
                    $('#price-max').text(ui.values[1].toLocaleString('vi-VN'));
                }
            });
        }

        // === FORM VALIDATION ===
        $('form').on('submit', function() {
            var isValid = true;
            $(this).find('input[required], select[required], textarea[required]').each(function() {
                if (!$(this).val()) {
                    isValid = false;
                    $(this).addClass('is-invalid');
                } else {
                    $(this).removeClass('is-invalid');
                }
            });
            return isValid;
        });

        // === TOAST NOTIFICATIONS ===
        if ($('.toast').length) {
            $('.toast').toast('show');
        }

        // === ACTIVE MENU HIGHLIGHTING ===
        var currentUrl = window.location.pathname;
        $('.navbar-nav .nav-link').each(function() {
            var href = $(this).attr('href');
            if (currentUrl.indexOf(href) !== -1 && href !== '/') {
                $(this).addClass('active');
            }
        });

        // === MOBILE MENU CLOSE ON CLICK ===
        $('.navbar-nav .nav-link').on('click', function() {
            if ($(window).width() < 992) {
                $('.navbar-collapse').collapse('hide');
            }
        });

        // === WISHLIST TOGGLE ===
        $('.wishlist-btn').on('click', function(e) {
            e.preventDefault();
            $(this).toggleClass('active');
            $(this).find('i').toggleClass('far fas');
        });

        // === COMPARE TOGGLE ===
        $('.compare-btn').on('click', function(e) {
            e.preventDefault();
            $(this).toggleClass('active');
        });

        // === TOOLTIP INITIALIZATION ===
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(function(tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });

        // === POPOVER INITIALIZATION ===
        var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
        popoverTriggerList.map(function(popoverTriggerEl) {
            return new bootstrap.Popover(popoverTriggerEl);
        });

        // === RATING STARS ===
        $('.rating-stars i').on('click', function() {
            var rating = $(this).data('rating');
            $(this).parent().find('i').removeClass('fas').addClass('far');
            $(this).prevAll().addBack().removeClass('far').addClass('fas');
            $('#rating-value').val(rating);
        });

        // === FILTER TOGGLE (Mobile) ===
        $('.filter-toggle').on('click', function() {
            $('.category-sidebar').toggleClass('show');
        });

    }); // Document ready end

    // === WINDOW RESIZE ===
    $(window).on('resize', function() {
        // Adjust for mobile
        if ($(window).width() < 768) {
            $('.category-sidebar').removeClass('show');
        }
    });

})(jQuery);

// ========================================
// VANILLA JS ENHANCEMENTS
// ========================================

// === SERVICE WORKER (PWA Support) ===
if ('serviceWorker' in navigator) {
    window.addEventListener('load', function() {
        // Register service worker if available
        // navigator.serviceWorker.register('/sw.js');
    });
}

// === PERFORMANCE MONITORING ===
window.addEventListener('load', function() {
    var loadTime = window.performance.timing.domContentLoadedEventEnd - window.performance.timing.navigationStart;
    console.log('Page load time: ' + loadTime + 'ms');
});
