$(() => {
    const addModal = new bootstrap.Modal($('#add-modal')[0]);
    const editModal = new bootstrap.Modal($('#edit-modal')[0]);

    const refreshPeople = (cb) => {
        $("tbody tr:gt(0)").remove();
        $("#spinner-row").show();
        $.get('/home/getpeople', function (people) {
            $("#spinner-row").hide();
            people.forEach(person => {
                $("tbody").append(`<tr>
                    <td>${person.firstName}</td>
                    <td>${person.lastName}</td>
                    <td>${person.age}</td>
                    <td>
                        <button class="btn btn-primary edit-btn" data-id="${person.id}">Edit</button>
                    </td>
                    <td>
                        <button class="btn btn-danger delete-btn" data-id="${person.id}">Delete</button>
                    </td>
                </tr>`);
            });
            if (cb) cb();
        });
    };

    $("#show-add").on('click', function () {
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
        addModal.show(); 
    });

    $("#save-person").on('click', function () {
        const firstName = $("#firstName").val();
        const lastName = $("#lastName").val();
        const age = $("#age").val();

        $.post('/home/addperson', {
            firstName: firstName,
            lastName: lastName,
            age: age
        }, function () {
            refreshPeople();
            addModal.hide(); 
        });
    });

    $(".table").on('click', ".edit-btn", function () {
        const row = $(this).closest("tr");
        const firstName = row.find("td:eq(0)").text();
        const lastName = row.find("td:eq(1)").text();
        const age = row.find("td:eq(2)").text();
        const id = $(this).data("id");

        $("#edit-id").val(id);
        $("#edit-firstName").val(firstName);
        $("#edit-lastName").val(lastName);
        $("#edit-age").val(age);
        editModal.show(); 
    });

    $("#update-person").on('click', function () {
        const id = $("#edit-id").val();
        const firstName = $("#edit-firstName").val();
        const lastName = $("#edit-lastName").val();
        const age = $("#edit-age").val();

        $.post('/home/editperson', {
            id,
            firstName,
            lastName,
            age
        }, function () {
            refreshPeople();
            editModal.hide()
        });
    });

    $(".table").on('click', ".delete-btn", function () {
        const id = $(this).data("id");
        $.post('/home/deletePerson', { id }, () => {
            refreshPeople();
        });
    });

    refreshPeople();
});
