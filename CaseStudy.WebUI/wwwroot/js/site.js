$(document).ready(function () {
    var toggleData = {
        toggle: false,
        id: -1
    };
    $.fn.datepicker.dates["tr"] = {
        days: [
            "Pazar",
            "Pazartesi",
            "Salı",
            "Çarşamba",
            "Perşembe",
            "Cuma",
            "Cumartesi",
        ],
        daysShort: ["Paz", "Pzt", "Sal", "Çar", "Per", "Cum", "Cmt"],
        daysMin: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
        months: [
            "Ocak",
            "Şubat",
            "Mart",
            "Nisan",
            "Mayıs",
            "Haziran",
            "Temmuz",
            "Ağustos",
            "Eylül",
            "Ekim",
            "Kasım",
            "Aralık",
        ],
        monthsShort: [
            "Oca",
            "Şub",
            "Mar",
            "Nis",
            "May",
            "Haz",
            "Tem",
            "Ağu",
            "Eyl",
            "Eki",
            "Kas",
            "Ara",
        ],
        today: "Bugün",
        clear: "Temizle",
        dateFormat: "dd.mm.yyyy",
        timeFormat: "hh:ii",
        firstDay: 1,
    };
    $("#tasksForm .input-group.date").datepicker({
        autoclose: true,
        format: "dd.mm.yyyy",
        language: "tr",
        startDate: new Date(),
    });

    document.body.addEventListener("click", function (event) {
        if (event.target.classList.contains("edit")) {
            LoadinModal("Lütfen bekleyin...", "");

            toggleData.id = event.target.id;
            toggleData.toggle = true;
            var id = event.target.id;

            $.ajax({
                type: "POST",
                url: "Task/GetTaskDetail",
                data: { id }
            }).done(result => {
                LoadinModalClose();
                $("#inputTitle").val(result.title);
                $("#inputDescription").val(result.description);
                $("#date").val(result.startingDate);
                $("#dueDate").val(result.dueDate);
                $("#expectDue").val(result.expectedDueDate);

                $("#inputAssigned option[value=" + result.assignedUser.id + "]").attr('selected', 'selected');

                $("#inputAppointed option[value=" + result.appointedUser.id + "]").attr('selected', 'selected');

                $("#inputState option[value=" + result.status.id + "]").attr('selected', 'selected');

                $("#inputPriority").val(result.priority);
              


            }).catch(error => {
                Alert_Error("İşlem Başarısız", error.responseText);
            });
        }
    });
    document.body.addEventListener("click", function (event) {
        if (event.target.classList.contains("delete")) {
            var id = event.target.dataset.delete;  
            Alert_Delete("Silme İşlemi", "Bu görevi silmek istediğinize emin misiniz?", id, deleteTask);
        }
    });

    $("#saveTask").click(function () {
        LoadinModal("Lütfen Bekleyin...", "Ekleniyor");

        var formData = getSpecifiedFormData("#tasksForm");

        if (toggleData.toggle === true) {
            updateTask(toggleData.id, formData);
        }
        else {
            addTask(formData);
        }
    });
    $("#addTask").click(function () {
        toggleData.toggle = false;
    });
    $("#saveChangeStatus").click(function () {
        LoadinModal("Lütfen Bekleyin...", "Ekleniyor");

        var formData = getSpecifiedFormData("#settingModalForm");
        

        updateStatus(formData);
    });
    $("#updateUser").click(function () {
        LoadinModal("Lütfen Bekleyin...", "Ekleniyor");

        var formData = getSpecifiedFormData("#updateUserForm");

        updateUser(formData);

    });

    $("#settingUserSelect").on("change", function () {
        LoadinModal("Lütfen Bekleyin", "Yükleniyor...");
        var id = this.value;
        $.ajax({
            type: "POST",
            url: "User/GetUser",
            data: { id }
        }).done(result => {
            LoadinModalClose();
            $("#updateUserFirstName").val(result.firstName);
            $("#updateUserLastName").val(result.lastName);
            $("#updateUserEmail").val(result.email );

        }).catch(error => {
            Alert_Error("İşlem Başarısız", error.responseText);
        });
    });


    const updateUser = (formData) => {
        $.ajax({
            type: "POST",
            url: "User/UpdateUser",
            data: { resource: formData }
        }).done(result => {
            swal({
                title: "Güncelleme",
                text: "Kişi başarı ile güncellendi!",
                type: 'success',
                showConfirmButton: true,
                confirmButtonText: 'Harika',
                html: true
            });
            updateUserInfos(result);

        }).catch(error => {
            Alert_Error("İşlem Başarısız", error.responseText);
        });
    }
    const updateUserInfos = (result) => {

        var userOptions = $(".userSelect option[value='" + result.id + "']");
        console.log(userOptions);
        for (var i = 0; i < userOptions.length; i++) {
            userOptions[i].innerHTML = result.firstName + " " + result.lastName;
        }
        var taskUsers = $("td[data-taskUser='" + result.id + "']");
        for (var i = 0; i < taskUsers.length; i++) {
            taskUsers[i].innerHTML = result.firstName;
        }
    }
    
    const updateStatus = (formData) => {
        $.ajax({
            type: "POST",
            url: "Status/UpdateStatus",
            data: { resource: formData }
        }).done(result => {
            swal({
                title: "Güncelleme",
                text: "Durum başarı ile güncellendi!",
                type: 'success',
                showConfirmButton: true,
                confirmButtonText: 'Harika',
                html: true
            });
            updateStatusInfos(result);

        }).catch(error => {
            Alert_Error("İşlem Başarısız", error.responseText);
        });
    }

    const deleteTask = (id) => {
        LoadinModal("Lütfen bekleyin...", "");
        $.ajax({
            type: "POST",
            url: "Task/DeleteTask",
            data: { id }
        }).done(result => {
            swal({
                title: "Silme",
                text: "Görev başarı ile silindi!",
                type: 'success',
                showConfirmButton: true,
                confirmButtonText: 'Harika',
                html: true
            });
            deleteRow(id);

        }).catch(error => {
            Alert_Error("İşlem Başarısız", error.responseText);
        });

    };

    const deleteRow = (id) => {
        var deleteButton = document.querySelector(".delete[data-delete='" + id + "']");
        var tBody = deleteButton.parentNode.parentNode.parentNode;
        var tr = deleteButton.parentNode.parentNode;
        
        tBody.removeChild(tr);       
    };

    const updateTask = (id, formData) => {
        formData.id = parseInt(id);

        $.ajax({
            type: "POST",
            url: "Task/UpdateTask",
            data: { resource: formData }
        }).done(result => {
            swal({
                title: "Güncelleme",
                text: "Güncelleme başarı ile tamamlandı",
                type: 'success',
                showConfirmButton: true,
                confirmButtonText: 'Harika',
                html: true
            });
            updateRow(id, result);

        }).catch(error => {
            Alert_Error("İşlem Başarısız", error.responseText);
        });

    };

    const updateRow = (id, result) => {

        var button = document.getElementById(id);

        var updatedRow = createRow(result);

        var tbody = button.parentNode.parentNode.parentNode;

        tbody.replaceChild(updatedRow, button.parentNode.parentNode);
    };

    const createRow = (result) => {
        var td = "";

        td += "<td>" + result.title + "</td>";
        td += " <td data-taskUser='"+result.assignedUser.id+"'>" + result.assignedUser.firstName + "</td>";
        td += " <td><label class='badge badge-info statusDesc' data-statusId='"+result.status.id+"'>" + result.status.statusDesc + "</label></td>";
        td += " <td>" + result.dueDate + "</td>";
        td += " <td><strong>%" + result.priority + "</strong></td>";
        td += "<td>";
        td += "<button id='" + result.id + "'";
        td += "class='btn btn-warning edit'";
        td += "data-toggle='modal'";
        td += "data-target='#inputTaskModal'>";
        td += " Düzenle";
        td += "</button>";
        td += "</td>";
        td += "<td>";
        td += "<button class='btn btn-danger delete'";
        td += "data-delete='" + result.id + "'>";
        td += " Sil";
        td += "</button>";
        td += "</td>";


        var updatedRow = document.createElement("tr");
        updatedRow.innerHTML = td;

        return updatedRow;
    };

    const addTask = (formData) => {

        $.ajax({
            type: "POST",
            url: "Task/AddTask",
            data: { resource: formData }
        }).done(result => {
            swal({
                title: "Ekleme",
                text: "Yeni görev başarı ile eklendi!",
                type: 'success',
                showConfirmButton: true,
                confirmButtonText: 'Harika',
                html: true
            });
            appendRow(result);

        }).catch(error => {
            Alert_Error("İşlem Başarısız", error.responseText);
        });

    };

    const appendRow = (result) => {
        var row = createRow(result);
        $("#taskTable tbody").append(row);
    };

    const addUser = (formData) => {

        $.ajax({
            type: "POST",
            url: "User/AddUser",
            data: { resource: formData }
        }).done(result => {
            swal({
                title: "Ekleme",
                text: "Kullanıcı başarı ile eklendi!",
                type: 'success',
                showConfirmButton: true,
                confirmButtonText: 'Harika',
                html: true
            });
            
            appendUser(result);

        }).catch(error => {
            Alert_Error("İşlem Başarısız", error.responseText);
        });
    };

    const addStatus = (formData) => {
        $.ajax({
            type: "POST",
            url: "Status/AddStatus",
            data: { resource: formData }
        }).done(result => {
            swal({
                title: "Ekleme",
                text: "Durum bilgisi başarı ile eklendi!",
                type: 'success',
                showConfirmButton: true,
                confirmButtonText: 'Harika',
                html: true
            });
            appendStatus(result);

        }).catch(error => {
            Alert_Error("İşlem Başarısız", error.responseText);
        });
    };

    const getSpecifiedFormData = (formId) => {

        var formData = $(formId).serializeArray().reduce((obj, item) => {
            obj[item.name] = item.value;
            return obj
        }, {});

        return formData;
    };

    const appendUser = (result) => {
        var appointedSelect = document.getElementById("inputAppointed");
        appointedSelect.options[appointedSelect.options.length] = new Option(result.firstName + " " + result.lastName,
            String(result.id));

        var assignedSelect = document.getElementById("inputAssigned");
        assignedSelect.options[assignedSelect.options.length] = new Option(result.firstName + " " + result.lastName, String(result.id));
    };

    const appendStatus = (result) => {
      
        var statusOptions = $(".statusSelect");
        for (var i = 0; i < statusOptions.length; i++) {
            statusOptions[i].options[statusOptions[i].options.length] = new Option(result.statusDesc, String(result.id));
        }
    };

    const updateStatusInfos = (result) => {
        var statusOptions = $(".statusSelect option[value='" + result.id + "']");
        console.log(statusOptions);
        for (var i = 0; i < statusOptions.length; i++) {
            statusOptions[i].innerHTML = result.statusDesc;
        }
        var statusDescs = $(".statusDesc[data-statusId='" + result.id + "']");
        for (var i = 0; i < statusDescs.length; i++) {
            statusDescs[i].innerHTML = result.statusDesc;
        }
        
    };

    $("#saveUser").click(function () {
        LoadinModal("Lütfen Bekleyin...", "Ekleniyor");

        var formData = getSpecifiedFormData("#addUserForm");

        addUser(formData);

    });

    $('#saveStatus').click(function () {
        LoadinModal("Lütfen Bekleyin...", "Ekleniyor");
        var formData = getSpecifiedFormData("#inputStateForm");
        addStatus(formData);
    });

    $("#inputUserModal").on("hidden.bs.modal", function (e) {
        $("#addUserForm").trigger("reset");
    });

    $("#inputStateModal").on("hidden.bs.modal", function (e) {
        $("#inputStateForm").trigger("reset");
    });

    $("#inputTaskModal").on("hidden.bs.modal", function (e) {
        $("#tasksForm").trigger("reset");
    });



});