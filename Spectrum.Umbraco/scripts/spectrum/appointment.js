/* Supporting code for insert and maintain appointments */

function checkAttendees() {

    var items = document.getElementById("attendeeList").getElementsByTagName("li");

    if (items.length === 0) {
        event.preventDefault();
        $('#attendeeInput').parents(".form-group").addClass('has-error');
        $('#attendeeInput').addClass('text-danger');
        $('span[data-valmsg-for="attendeeInput"]').text('Please enter at least one attendee');
        $('span[data-valmsg-for="attendeeInput"]').addClass('text-danger');
    }
}

function updateAttendees() {

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
    $('span[data-valmsg-for="attendeeInput"]').text('Please enter a valid Email Address');
    $('span[data-valmsg-for="attendeeInput"]').addClass('text-danger');
}

function addAttendee() {

    event.preventDefault();

    var inputValue = $('#attendeeInput').val();
    //This regex string stopped working when ported here from cshtml. But the replacement works fine.
    //var filter = /^([a-zA-Z0-9_\.\-])+\@@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (!filter.test(inputValue)) {
        addErrorFields();
    } else {

        var items = document.getElementById("attendeeList").getElementsByTagName("li");
        var index = items.length;

        $('#attendeeList').prepend(prependAttendee(index, inputValue));

        $('#attendeeInput').val('');
        $('span[data-valmsg-for="attendeeInput"]').text('');
    }

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