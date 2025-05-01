$(function () {


    $('#userForm input, #userForm select, #userForm textarea').prop('disabled', true);
          
    

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
    });

    // Handle Edit button click
    $('#btnEdit').click(function () {
        $('#ActionType').val('Edit');
        $('#userForm input, #userForm select, #userForm textarea').prop('disabled', false);
    });

    // Handle Delete button click
    $('#btnDelete').click(function () {
        $('#ActionType').val('Delete');
        submitForm();
    });

    // Function to submit the form via AJAX
    function submitForm() {

        var formData = $('#userForm').serialize();
        var action = $('#ActionType').val();
        if (action === 'Add') {
            $.ajax({
                url: '/UserManagement/AddUser',  // The controller action URL
                method: 'POST',
                data: $('#userForm').serialize(), // token is included automatically
                //data: formData,
                success: function (response) {
                    if (response.success) {

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

        // Populate fields
        $('#FullName').val(user.fullName);
        $('#Email').val(user.email);
        $('#PhoneNumber').val(user.phoneNumber);
        $('#UserID').val(user.userId);

        // ... add more fields as needed
    });

});



