$(function () {


    $('#userForm input, #userForm select, #userForm textarea').prop('disabled', true);
          
    $('#btnEdit, #btnDelete, #btnSave').prop('disabled', true);

    // Handle Save button click
    $('#btnSave').click(function (e) {

        e.preventDefault(); // 🔥 Prevents the form from submitting right away

        let token = $('input[name="__RequestVerificationToken"]').val();

        if (!$('#userForm').valid()) {
            Swal.fire({
                icon: 'error',
                title: 'Validation Error',
                text: 'Please fill all required fields correctly.'
            });
            return;
        }


        submitForm();
    });

    // Handle Add button click
    $('#btnAdd').click(function (e) {
        $('#ActionType').val('Add');
        $('#userForm input, #userForm select, #userForm textarea').prop('disabled', false);
        $('#btnSave').prop('disabled', false);
        $('#btnAdd').prop('disabled', true);


    });

    // Handle Edit button click
    $('#btnEdit').click(function () {
        $('#ActionType').val('Edit');
        $('#btnSave').prop('disabled', false);
        $('#btnEdit').prop('disabled', true);
        $('#btnDelete').prop('disabled', true);

        $('#userForm input, #userForm select, #userForm textarea').prop('disabled', false);
    });

    // Handle Delete button click
    $('#btnDelete').click(function () {
        $('#ActionType').val('Delete');
        $('#btnSave').prop('disabled', true);
        $('#btnEdit').prop('disabled', true);


        Swal.fire({
            icon: 'warning',
            title: 'Warning',
            text: 'Are you sure you want to delete this user?',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No',
            reverseButtons: true // Optional: swaps the position of Yes and No
        }).then((result) => {
            if (result.isConfirmed) {
                // Code when "Yes" is clicked
                submitForm();
            } else if (result.dismiss === Swal.DismissReason.cancel) {
                // Code when "No" is clicked (optional)

                $('#btnSave').prop('disabled', true);
                $('#btnEdit, #btnDelete').prop('disabled', false);

            }
        });

       


    });


    $('#btnCancel').click(function () {

      
        $('#userForm')[0].reset(); // reset form fields
        $('#UserID').val(''); // clear hidden userId if you're using it to detect edit mode
        $('#userForm input, #userForm select, #userForm textarea').prop('disabled', true);
        $('#btnEdit, #btnDelete, #btnSave').prop('disabled', true);
        $('#btnAdd').prop('disabled', false);


    });


    // Function to submit the form via AJAX
    function submitForm() {

        var formData = $('#userForm').serialize();
        var action = $('#ActionType').val();
        if (action === 'Add') {

            alert("sada");
            var userid = $('#UserID').val('na');
            $.ajax({
                url: '/UserManagement/AddUser',  // The controller action URL
                method: 'POST',
                data: $('#userForm').serialize(), // token is included automatically
                //data: formData,
                success: function (response) {
                    if (response.success) {
                        $('#btnAdd').prop('disabled', false);
                        $('#btnEdit, #btnDelete, #btnSave').prop('disabled', true);



                        $('#UserID').val(response.userId);

                        // Add user to the modal table dynamically
                        const newRow = `
        <tr data-userid="${response.userId}">
            <td class="user-name">${response.fullName}</td>
            <td>${response.email}</td>
            <td>
                <button class="btn btn-sm btn-primary select-user" data-userid="${response.userId}" data-fullname="${response.fullName}" data-email="${response.email}">
                    Select
                </button>
            </td>
        </tr>
    `;
                        $('#userTable tbody').append(newRow);



                        $('#userForm input, #userForm select, #userForm textarea').prop('disabled', true);

                        Swal.fire({
                            icon: 'success',
                            title: 'Success',
                            text: response.message
                        }).then(() => {
                            $('#userForm')[0].reset(); // reset form if needed
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: response.message
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Server Error',
                        text: 'Something went wrong. Please try again later.'
                    });
                }
            });
        }
        else if (action === 'Edit') {
            $.ajax({
                url: '/UserManagement/EditUser',  // The controller action URL
                method: 'POST',
                data: $('#userForm').serialize(), // token is included automatically
                //data: formData,
                success: function (response) {
                    if (response.success) {
                        $('#btnAdd').prop('disabled', false);
                        $('#btnEdit, #btnDelete, #btnSave').prop('disabled', true);
                        $('#userForm input, #userForm select, #userForm textarea').prop('disabled', true);

                        const userId = $('#UserID').val();
                        const updatedName = $('#FullName').val(); // or whatever field you updated

                        // Update the row in the modal table
                        $(`#userTable tr[data-userid="${userId}"] .user-name`).text(updatedName);

                        Swal.fire({
                            icon: 'success',
                            title: 'Success',
                            text: response.message
                        }).then(() => {
                            $('#userForm')[0].reset(); // reset form if needed
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: response.message
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Server Error',
                        text: 'Something went wrong. Please try again later.'
                    });
                }
            });
        }
        else if (action === 'Delete') {

            var userid = $('#UserID').val();

            $.ajax({
                url: '/UserManagement/DeleteUser',  // The controller action URL
                method: 'POST',
                data: {
                    __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val(), // required for [ValidateAntiForgeryToken]
                    userId: userid
                },
                    success: function (response) {
                        if (response.success) {
                            // Remove the row from the table
                            $(`#userTable tr[data-userid="${userid}"]`).remove();

                            $('#btnAdd').prop('disabled', false);
                            $('#btnEdit, #btnDelete, #btnSave').prop('disabled', true);
                            const userId = $('#UserID').val();
                            const updatedName = $('#FullName').val(); // or whatever field you updated

                            // Update the row in the modal table
                            $(`#userTable tr[data-userid="${userId}"] .user-name`).text(updatedName);

                            Swal.fire({
                                icon: 'success',
                                title: 'Success',
                                text: response.message
                            }).then(() => {
                                $('#userForm')[0].reset(); // reset form if needed
                            });
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Error',
                                text: response.message
                            });
                        }
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Server Error',
                            text: 'Something went wrong. Please try again later.'
                        });
                    }
                });
        

        }
    }


    /////////////////


    function loadUsersIntoModal() {
        $.ajax({
            url: '/UserManagement/GetAllUsers', // replace with actual route
            method: 'GET',
            success: function (data) {
                const tbody = $("#userTable tbody");
                tbody.empty();

                data.forEach(user => {
                    const row = `
    <tr data-userid="${user.userId}">
        <td  class="user-name">${user.fullName}</td>
        <td>${user.email}</td>
        <td>
         <button class="btn btn-sm btn-primary select-user"
                        data-user='${JSON.stringify(user).replace(/'/g, "&apos;")}'
                        data-bs-dismiss="modal">
                    Select
                </button>
        </td>
    </tr>`;
                    tbody.append(row);
                });
            },
            error: function () {
                alert("Error loading users.");
            }
        });
    }

    // Optional: Load users when modal opens

    $('#userListModal').one('show.bs.modal', function () {
        loadUsersIntoModal();
    });

    function selectUser(user) {
        $('#FullName').val(user.fullName);
        $('#Email').val(user.email);
        $('#PhoneNumber').val(user.phoneNumber);
        $('#MaxRolesAllowed').val(user.maxRolesAllowed);
        $('#IsActive').prop('checked', user.isActive);
        $('#UserID').val(user.userId);
        $('#editMode').val("true");
    }

    ////////////


    $(document).on('click', '.select-user', function () {
        const userJson = $(this).data('user');
        const user = typeof userJson === 'string' ? JSON.parse(userJson.replace(/&apos;/g, "'")) : userJson;

        $('#btnAdd, #btnSave').prop('disabled', true);

        $('#btnEdit').prop('disabled', false);
        $('#btnDelete').prop('disabled', false);


        // Populate fields
        $('#FullName').val(user.fullName);
        $('#Email').val(user.email);
        $('#PhoneNumber').val(user.phoneNumber);
        $('#UserID').val(user.userId);

        // ... add more fields as needed
    });

});



