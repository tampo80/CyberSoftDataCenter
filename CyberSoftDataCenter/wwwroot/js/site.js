
$(document).ready(function () {
    //start of the document ready function
    //delcaring global variable to hold primary key value.
    var Id;
    var Row;
    $('.image-prompt').click(function () {
       
        Id = $(this).attr('id');
       // alert(Id);
        Row = $(this).closest("tr");
        $('.image-pub').attr("src", Id);
        $('#ImageModal').modal('show');
    });

    $('.delete-confirm').click(function () {
        if (Id != '') {
            $.ajax({
                url: url,
                data: { 'Id': Id },
                type: 'get',
                success: function (data) {
                    if (data) {
                        //now re-using the boostrap modal popup to show success message.
                        //dynamically we will change background colour 
                        if ($('.modal-header').hasClass('alert-danger')) {
                            $('.modal-header').removeClass('alert-danger').addClass('alert-success');
                            //hide ok button as it is not necessary
                            $('.delete-confirm').css('display', 'none');
                        }

                        $('.success-message').html('Supprimer avec succès!!!');
                        Row.remove();

                    }
                }, error: function (err) {
                    if (!$('.modal-header').hasClass('alert-danger')) {
                        $('.modal-header').removeClass('alert-success').addClass('alert-danger');
                        $('.delete-confirm').css('display', 'none');
                    }
                    $('.success-message').html(err.statusText);
                }
            });
        }
    });

    //function to reset bootstrap modal popups
    $("#myModal").on("hidden.bs.modal", function () {
        $(".modal-header").removeClass(' ').addClass('alert-danger');
        $('.delete-confirm').css('display', 'inline-block');
        $('.success-message').html('').html('Supprimer ce utilisateur ?');
    });

    //end of the docuement ready function
});