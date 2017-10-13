/* Supporting code for insert and maintain appointments */

function updateAttendees() {

    console.log('running update attendees');

    var items = document.getElementById("attendeeList").getElementsByTagName("li");

    for (var i = 0; i < items.length; i++) {
        var input = items[i].getElementsByTagName("input")[0];

        input.id = "Attendees_" + i;
        input.name = "Attendees[" + i + "]";
    }
}

function prependAttendee(index, inputValue) {

    var retVal = '<div class="calendar-position form-group">' +
    '<li style="padding-top:10px">' +
        '<input class="form-control" id="Attendees_' + index + '" name="Attendees[' + index + ']" value=' + inputValue + '>' +
        '</input>&nbsp;&nbsp;&nbsp;' +
        '<a href="#" onclick="$(this).parent().remove();updateAttendees();"><span class="glyphicon glyphicon-remove"></span></a>' +
    '</li>' +
    '</div>';

    return (retVal);
}



function addErrorFields() {

    $('#attendeeInput').parents(".form-group").addClass('has-error');
    $('#attendeeInput').addClass('text-danger');
    $('span[data-valmsg-for="attendeeInput"]').text('Please enter a valid xx Email Address');
    $('span[data-valmsg-for="attendeeInput"]').addClass('text-danger');
}

function initialiseDatePicker() {

    $('.date').datetimepicker(
    {
        dateFormat: 'dd-M-yy',
        controlType: 'select',
        hourMin: 8,
        hourMax: 20,
        stepMinute: 5,
        oneLine: true
    });
}