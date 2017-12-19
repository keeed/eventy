var isSubmitting = false;

function addAttendance(eventId, familyMemberId) {
    if (isSubmitting) {
        return;
    }

    $(`#${eventId}---${familyMemberId}`).addClass("disabled");

    var myData = new Object();
    myData.EventId = eventId;
    myData.FamilyMemberId = familyMemberId;

    $.ajax({
        url: "/api/events/addAttendance",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(myData),
        success: function (data) {
            $(`#${eventId}---${familyMemberId}`).replaceWith(
                `<button id="${eventId}---${familyMemberId}" class="btn btn-danger" type="button" onclick="removeAttendance(${eventId}, ${familyMemberId})">Remove Attendance</button>`
            );
            isSubmitting = false;
        },
        error: function () {
            alert("error");
            isSubmitting = false;
            $(`#${eventId}---${familyMemberId}`).addClass("disabled");
            $(`#${eventId}---${familyMemberId}`).addClass("active");
        }
    });
}

function removeAttendance(eventId, familyMemberId) {
    if (isSubmitting) {
        return;
    }

    $(`#${eventId}---${familyMemberId}`).addClass("disabled");

    var myData = new Object();
    myData.EventId = eventId;
    myData.FamilyMemberId = familyMemberId;

    $.ajax({
        url: "/api/events/removeAttendance",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(myData),
        success: function (data) {
            $(`#${eventId}---${familyMemberId}`).replaceWith(
                `<button id="${eventId}---${familyMemberId}" class="btn btn-success" type="button" onclick="addAttendance(${eventId}, ${familyMemberId})">Add Attendance</button>`
            );
            isSubmitting = false;
        },
        error: function () {
            alert("error");
            isSubmitting = false;
            $(`#${eventId}---${familyMemberId}`).removeClass("disabled");
            $(`#${eventId}---${familyMemberId}`).addClass("active");
        }
    });
}