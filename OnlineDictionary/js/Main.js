jQuery(document).ready(function () {

    //Change txtSearch with
    jQuery('.jsTxtSearchMasterPage').click(function () {
        var current = jQuery(this);
        if (!current.hasClass('txtSearchLong')) {
            current.addClass('txtSearchLong');
        }
        else {
            current.removeClass('txtSearchLong');
        }
    });

    //Change pnlLogin style
    jQuery('.jsLbtnExpandLoginForm').click(function () {
        var current = jQuery(this);
        if (!current.hasClass('btnLoginCancel')) {
            current.addClass('btnLoginCancel');
        }
        else {
            current.removeClass('btnLoginCancel');
        }
    });

    //Clear txtEmail
    jQuery('.jsTxtEmail').click(function () {
        var current = jQuery(this);
        if (current.val() == 'E-mail') {
            current.val('');
        }
    });

    jQuery('.jsPassword').click(function () {
        var current = jQuery(this);
        if (current.val() == 'Password') {
            current.val('');
        }
    });

    //Hide pnlChangePassword
    jQuery('.jsPanelChangePassword').hide();
    jQuery('.jsPanelChangePasswordControl').show();

    var hasFile = jQuery('.jsUploadedFile').val();
    if (hasFile == "") {
        jQuery('.jsPanelPreviewPicture').hide(); // For Upload Picture
    }

    
});

//Show pnlChangePassword
function ShowPanelChangePasswor() {
    jQuery('.jsPanelChangePassword').show();
    jQuery('.jsPanelChangePasswordControl').hide();
};

// Select Rating for review
function SelectStar() {
    jQuery(document).ready(function () {
        // Inputs
        jQuery('.form input:text').focus(function () {
            var current = jQuery(this);
            if (current.attr('alt') == current.val()) {
                current.val('');
            }
        });

        jQuery('.form input:text').blur(function () {
            var current = jQuery(this);
            if (!jQuery.trim(current.val()).length) {
                if (current.attr('alt')) {
                    current.val(current.attr('alt'));
                }
            }
        });

        jQuery('.userRating li').hover(function () {
            var rating = jQuery(this).find(">:first-child").val();
            jQuery(this).parent().removeClass();
            jQuery(this).parent().addClass("userRating rating" + (parseFloat(rating) * 10));
        });

        jQuery('.userRating').mouseout(function () {
            vizualizeRating();
        });

        jQuery('.submit-btn').click(function () {
            hideReviewSubmit(this);
        });
    });
    vizualizeRating();
    function vizualizeRating() {
        var element = jQuery('.userRating input:checked');
        if (element.length == 0) {
            element = jQuery('.userRating input[value="5"]');
            element.attr('checked', 'checked');
        }
        var current = element.val();
        var container = jQuery('.userRating');
        container.removeClass();
        container.addClass('userRating rating' + (parseFloat(current) * 10));
    }


    function hideReviewSubmit(element) {
        if (typeof (Page_ClientValidate) == 'function') {
            var isPageValid = Page_ClientValidate("revform");

            if (isPageValid) {
                jQuery(element).hide();
            }

        }
    }
};
//end

//Upload Picture
function HasFileIntoFileUpload() {
    jQuery('.jsUploadedFile').val(jQuery('input[type=file]').attr('value'));

    jQuery('.jsPanelPreviewPicture').show();
    //jQuery('.jsPanelPreviewPicture').html('<img src="' + jQuery('.jsFuSource').val() + '" width=100 height=100 alt="Loaded image." />');
};