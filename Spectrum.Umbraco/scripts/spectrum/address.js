/* Script to control address searching and DOM validation */

$(document).ready(function () {

    $("#findAddress").click(function () {

        var postCode = $('#PostCode').val();

        if (!postCode) {
            console.log('postcode is empty');

            //make the validation error span for postcode appear
            $('.find-address + span').addClass('field-validation-error');
            $('.find-address + span').removeClass('field-validation-valid');
            $('.find-address + span').text("Please enter a postcode");
            return;
        } else {
            //make the validation error span for postcode dissaapear
            $('.find-address + span').removeClass('field-validation-error');
            $('.find-address + span').addClass('field-validation-valid');
            $('.find-address + span').text("");
        }

        //Clear
        $("#addressList").empty();
        //$('#addressResult').text('Searching...');

        var url = "/umbraco/Surface/Search/GetAddresses";

        var postData = {
            buildingNumber: "",
            postCode: postCode
        };

        //Show the spinner
        $('#loading').removeClass('disp-none');
        $('#addressList').addClass('disp-none');
        $('.find-address').addClass('disp-none');

        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(postData),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                //console.log('Server success ', data);
                //Hide the spinner
                $('#loading').addClass('disp-none');

                //$('#addressResult').text('');
                $('#addressList').removeClass('disp-none');
                $('.find-address').removeClass('disp-none');

                //Empty the buildNumber field
                $('#BuildingNumber').val('');

                for (i = 0; i < data.length; i++) {
                    //console.log('order ' + data[i].FullAddress);
                    $('#addressList').prepend(prependAddress(i, ' ' + data[i].FullAddress, data[i].BuildingNumber));
                }

                //Reverse the list as prepend reverses it fpr some strange reason
                var list = $('#addressList');
                var listItems = list.children('option');
                list.append(listItems.get().reverse());

                $('#addressList').prepend('<option value="" disabled="disabled" selected="selected">Please select address</option>');

            },
            error: function (request, status, errorThrown) {
                //console.log('Server error ' + errorThrown);
                //Hide the spinner
                $('#loading').addClass('disp-none');
                $('.find-address').removeClass('disp-none');

                $('#addressList').removeClass('disp-none');
                window.location.href = "/error";
            }
        });
    });
});

function prependAddress(index, inputValue, buildingNumber) {

    //console.log('prepend input value = ' + inputValue);
    var retVal = '<option id="Address_' + index + '" value=' + buildingNumber + '>' + inputValue + '</option>';
    return (retVal);
}

function addressSelected(index) {
    var addressDropdown = document.getElementById("addressList");
    var buildingNumber = addressDropdown.options[index].value;

    //console.log('dropdown list changed. index ' + index + ' buildNumber ' + buildingNumber);
    $('#BuildingNumber').val(buildingNumber);
}