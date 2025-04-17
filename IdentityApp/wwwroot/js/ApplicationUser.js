$(function () {

    // Handle Add button click
    $('#btnAdd').click(function () {
        $('#ActionType').val('Add');
        submitForm();
    });

    // Handle Edit button click
    $('#btnEdit').click(function () {
        $('#ActionType').val('Edit');
        submitForm();
    });

    // Handle Delete button click
    $('#btnDelete').click(function () {
        $('#ActionType').val('Delete');
        submitForm();
    });

    // Function to submit the form via AJAX
    function submitForm() {
        var formData = $('#userForm').serialize();

        $.ajax({
            url: '/UserManagement/AddUser',  // The controller action URL
            method: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);  // Success message from backend
                } else {
                    alert('Something went wrong!');  // Handle error
                }
            },
            error: function () {
                alert('There was an error processing your request.');
            }
        });
    }
});
