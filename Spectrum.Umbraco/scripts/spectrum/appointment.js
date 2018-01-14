/* Supporting code for insert and maintain appointments */

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