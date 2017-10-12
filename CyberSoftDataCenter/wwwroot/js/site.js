
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


    $('.reset-prompt').click(function () {
        $("#password").focus();
        Id = $(this).attr('id');
        // alert(Id);
        Row = $(this).closest("tr");
       // $('.image-pub').attr("src", Id);
        $('#resetModal').modal('show');
        $("#password").focus();
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




    $('.reset-confirm').click(function () {
        $('#erreurMessage').removeClass('alert-danger').html('');;
        $('.modal-header').removeClass('alert-danger').addClass('alert-success');
      //  alert($("#password").val());
        if ($("#password").val() == '' || $("#cpassword").val() == '') {
            $("#password").focus();
            $('#erreurMessage').html("Les deux champs sont obligatoires !").addClass('alert-danger');;
        }
        else if ($('#password').val() != $("#cpassword").val()) {
            $('#erreurMessage').html("Les deux mots de passes sont differents !").addClass('alert-danger');
            $("#password").focus();
        }
        else {


            if (Id != '') {
                $.ajax({
                    url: urlR,
                    data: { 'Id': Id, 'password': $("#password").val()  },
                    type: 'post',
                    success: function (data) {
                        if (data) {
                            //now re-using the boostrap modal popup to show success message.
                            //dynamically we will change background colour 
                            if ($('.modal-header').hasClass('alert-danger')) {
                                $('.modal-header').removeClass('alert-danger').addClass('alert-success');
                                //hide ok button as it is not necessary
                                $('.reset-confirm').css('display', 'none');
                            }
                            $('.modal-header').removeClass('alert-danger').addClass('alert-success');
                            $('.success-message').html('Réinitalisé avec succès!!!');
                            //Row.remove();
                            $("#resetModal").modal('hide');

                        }
                    }, error: function (err) {
                        if (!$('.modal-header').hasClass('alert-danger')) {
                            $('.modal-header').removeClass('alert-success').addClass('alert-danger');
                            $('.reset-confirm').css('display', 'none');
                        }
                        $('.success-message').html(err.statusText);
                    }
                });
            }
        }
        });


    //function to reset bootstrap modal popups
    $("#resetModal").on("hidden.bs.modal", function () {
        $(".modal-header").removeClass(' ').addClass('alert-danger');
        $('.reset-confirm').css('display', 'inline-block');
        $('.success-message').html('').html('Supprimer ce utilisateur ?');
    });


    //end of the docuement ready function
});