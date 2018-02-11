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

                $('#addressList').removeClass('disp-none');
                $('.find-address').removeClass('disp-none');

                $('#BuildingNumber').val('');

                for (i = 0; i < data.length; i++) {
                    $('#addressList').prepend(prependAddress(i, data[i].Address, data[i].BuildingNumber));
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

    var retVal = '<option id="Address_' + index + '" value=' + buildingNumber + '>' + inputValue + '</option>';
    return (retVal);
}

function addressSelected(index) {
    var addressDropdown = document.getElementById("addressList");
    var buildingNumber = addressDropdown.options[index].value;

    $('#BuildingNumber').val(buildingNumber);

    $('#Address').val(addressDropdown[index].innerText);
}
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
/****************************************************************************************
 * Payments Page 
 ****************************************************************************************/



/*********************************************************************************
 * @param {} displayError 
 * @returns {} 
 * Displays the error on the view and returns the view state for the customer to re-try
 *********************************************************************************/
function manageError(displayError) {
    console.log('The error to display is ' + displayError);
    //Add the error to the view
    $('#errorText').text(displayError);
    //Enable the error panel
    $('#errorRow').show();
    //Change text from Submitting... to Pay with card
    $('#submit').text('Pay with Card');
    //Enable the submit button again
    $('#submit').prop('disabled', false);
}

/*********************************************************************************
 * @param {} err
 * @returns {errorTobeDisplayed} 
 * Analyses the braintree error and takes the necessary action
 *********************************************************************************/
function parseBraintreeError(err) {

    var errorTobeDisplayed = '';

    switch (err.type) {
        case 'CUSTOMER':
            errorTobeDisplayed = btCustomerError(err);
            break;
        case 'MERCHANT':
            //The merchant makde the error
            errorTobeDisplayed = 'Merchant error please try again';
            break;
        case 'NETWORK':
            //The error was caused by Braintree
            errorTobeDisplayed = 'Internal pay provider error please try again';
            break;
        case 'INTERNAL':
            //The error was caused by Braintree
            errorTobeDisplayed = 'Internal pay provider error please try again';
            break;
        case 'UNKNOWN':
            //The origin of the error is unknown
            errorTobeDisplayed = 'There has been an error please try again';
            break;
        default:
            //Make sure this is logged as it a new unexpected error
            errorTobeDisplayed = 'Unexpected erorr. Please try again';
    }

    return errorTobeDisplayed;
}

/*********************************************************************************
 * @param {} err
 * @returns {errorTobeDisplayed} 
 * Analyses the braintree customer error and takes the necessary action
 *********************************************************************************/
function btCustomerError(err) {

    var custErrorResponse = "";

    switch (err.code) {
        case 'HOSTED_FIELDS_FIELDS_EMPTY':
            custErrorResponse = 'You must enter your card details';
            break;
        case 'HOSTED_FIELDS_FIELDS_INVALID':
            var invalidFields = '';
            var seperator = '';

            if (err.details.invalidFieldKeys) {

                var invalidList = err.details.invalidFieldKeys;
                for (i = 0; i < invalidList.length; i++) {
                    if (invalidList[i] === 'number') {
                        seperator = (invalidFields.length > 0 ? ' ,' : '');
                        invalidFields = invalidFields + seperator + ' Card Number';
                    }

                    if (invalidList[i] === 'expirationMonth') {
                        seperator = (invalidFields.length > 0 ? ' ,' : '');
                        invalidFields = invalidFields + seperator + ' Expiry month';
                    }

                    if (invalidList[i] === 'expirationYear') {
                        seperator = (invalidFields.length > 0 ? ' ,' : '');
                        invalidFields = invalidFields + seperator + ' Expiry year';
                    }

                    if (invalidList[i] === 'cvv') {
                        seperator = (invalidFields.length > 0 ? ' ,' : '');
                        invalidFields = invalidFields + seperator + ' cvv';
                    }
                }
                custErrorResponse = 'Some of the fields are invalid - ' + invalidFields;
            } else {
                custErrorResponse = 'Some of the fields are invalid';
            }
            break;
        default:
            //Make sure this is logged as it a new unexpected error
            custErrorResponse = 'Unexpected braintree customer erorr. Please try again';
    }

    return custErrorResponse;
}

/*********************************************************************************
 * @param {} 
 * @returns {} 
 * Main Braintree API
 *********************************************************************************/
function setupBraintree(context) {

    if (context.paymentAmount) {
        $('#amount').prop("readonly", true);
        $('#amount').val(context.paymentAmount);
    }

    braintree.client.create({
        authorization: context.authToken
    },
        function (err, clientInstance) {

            if (context.environment !== 'production') {
                $('#sandboxRibbon').text(context.environment);
                $('#sandboxRibbon').show();
                $('#mobileSandboxRibbon').text(context.environment);
                $('#mobileSandboxRibbon').show();
            }

            if (err) {
                console.error(err);
                return;
            }

            braintree.hostedFields.create({
                client: clientInstance,
                styles: {
                    'input': {
                        'font-size': '14px',
                        'font-family': 'helvetica, tahoma, calibri, sans-serif',
                        'color': '#3a3a3a'
                    },
                    ':focus': {
                        'color': 'black'
                    }
                },
                fields: {
                    number: {
                        selector: '#card-number',
                        placeholder: '4111 1111 1111 1111'
                    },
                    cvv: {
                        selector: '#cvv',
                        placeholder: '123'
                    },
                    expirationMonth: {
                        selector: '#expiration-month',
                        placeholder: 'MM'
                    },
                    expirationYear: {
                        selector: '#expiration-year',
                        placeholder: 'YY'
                    }
                }
            },
                function (err, hostedFieldsInstance) {
                    if (err) {
                        console.error(err);
                        return;
                    }

                    hostedFieldsInstance.on('validityChange',
                        function (event) {
                            var field = event.fields[event.emittedBy];

                            if (field.isValid) {
                                if (event.emittedBy === 'expirationMonth' ||
                                    event.emittedBy === 'expirationYear') {
                                    if (!event.fields.expirationMonth.isValid ||
                                        !event.fields.expirationYear.isValid) {
                                        return;
                                    }
                                } else if (event.emittedBy === 'number') {
                                    $('#card-number').next('span').text('');
                                }

                                $(field.container).parents('.form-group').addClass('has-success');
                            } else if (field.isPotentiallyValid) {
                                $(field.container).parents('.form-group').removeClass('has-warning');
                                $(field.container).parents('.form-group').removeClass('has-success');

                                if (event.emittedBy === 'number') {
                                    $('#card-number').next('span').text('');
                                }
                            } else {
                                $(field.container).parents('.form-group').addClass('has-warning');

                                if (event.emittedBy === 'number') {
                                    $('#card-number').next('span')
                                        .text('Looks like this card number has an error.');
                                }
                            }
                        });

                    hostedFieldsInstance.on('cardTypeChange',
                        function (event) {

                            if (event.cards.length === 1) {
                                $('#card-type').text(event.cards[0].niceType);
                            } else {
                                $('#card-type').text('Card');
                            }
                        });

                    $('form').submit(function (event) {
                        $('#submit').prop('disabled', true);

                        var displayError = '';
                        $('#submit').text('Submitting...');

                        event.preventDefault();

                        hostedFieldsInstance.tokenize(function (err, payload) {
                            if (err) {
                                console.error(JSON.stringify(err));

                                displayError = parseBraintreeError(err);
                                manageError(displayError);
                                return;
                            }

                            //Validate the amount
                            var amt = $('#amount').val();
                            var pattern = /^\d+(?:\.\d{0,2})$/;
                            if (!pattern.test(amt)) {
                                console.log('amount is invalid');
                                displayError = 'The amount is invalid. You must specify pounds and pence 0.0';
                                manageError(displayError);
                                return;
                            }

                            var code = $('#fklfkl').val();

                            var postData = {
                                code : code,
                                currentPageNodeId: context.nodeId,
                                nonce: payload.nonce,
                                amount: amt,
                                autoAllocate: context.autoAllocate,
                                invoiceId: context.invoiceId,
                                clientId: context.clientId,
                                customerId: context.customerId
                            };

                            $.ajax({
                                url: context.makePaymentUrl,
                                type: 'POST',
                                dataType: 'json',
                                data: JSON.stringify(postData),
                                contentType: 'application/json; charset=utf-8',
                                success: function (data) {
                                    console.log('Server success ' + data);
                                    window.location.href = data;
                                },
                                error: function (request, status, errorThrown) {
                                    console.log('Server error ' + errorThrown);
                                    window.location.href = "/error";
                                }
                            });
                        });
                    });
                });
        });
}