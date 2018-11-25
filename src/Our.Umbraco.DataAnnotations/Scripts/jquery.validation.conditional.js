$.each(['requiredif', 'regularexpressionif', 'rangeif'], function (index, validationName) {
    $.validator.addMethod(validationName, function (value, element, parameters) {
        // Get the name prefix for the target control, depending on the validated control name
        var prefix = "";
        var lastDot = element.name.lastIndexOf('.');
        if (lastDot !== -1) {
            prefix = element.name.substring(0, lastDot + 1).replace('.', '_');
        }
        var id = 'input[name="' + prefix + parameters['dependentproperty'] + '"]';
        // get the target value
        var targetvalue = parameters['targetvalue'];
        targetvalue = (targetvalue === null ? '' : targetvalue).toString();
        // get the actual value of the target control
        var control = $(id);
        if (control.length === 0 && prefix.length > 0) {
            // Target control not found, try without the prefix
            control = $('#' + parameters['dependentproperty']);
        }
        if (control.length > 0) {
            var actualvalue = getValue(control);
            // if the condition is true, reuse the existing validator functionality
            if (targetvalue.toLowerCase() === actualvalue.toLowerCase()) {
                var rule = parameters['rule'];
                var ruleparam = parameters['ruleparam'];
                return $.validator.methods[rule].call(this, value, element, ruleparam);
            }
        }
        return true;
    });
    $.validator.unobtrusive.adapters.add(validationName, ['dependentproperty', 'targetvalue', 'rule', 'ruleparam'], function (options) {
        var rp = options.params['ruleparam'];
        options.rules[validationName] = {
            dependentproperty: options.params['dependentproperty'],
            targetvalue: options.params['targetvalue'],
            rule: options.params['rule']
        };
        if (rp) {
            options.rules[validationName].ruleparam = rp.charAt(0) === '[' ? eval(rp) : rp;
        }
        options.messages[validationName] = options.message;
    });
    // Hide form group
    $('[data-val-' + validationName + '-dependentproperty]').each(function (index, element) {
        var prefix = "";
        var lastDot = element.name.lastIndexOf('.');
        if (lastDot !== -1) {
            prefix = element.name.substring(0, lastDot + 1);
        }
        var data = $(element).data();
        var dependentproperty = $(element).data('val-' + validationName + '-dependentproperty');
        var targetvalue = $(element).data('val-' + validationName + '-targetvalue') + "";
        var name = prefix + dependentproperty;
        // Select list dependent?
        var dependentControl = $('[name="' + name + '"]');
        var control = $(element);
        if (dependentControl.length > 0) {
            // if the condition is true, reuse the existing validator functionality
            dependentControl.change(function () {
                var $this = $(this);
                var actualvalue = getValue($this);
                if (actualvalue.length > 0 && targetvalue.toLowerCase() === actualvalue.toLowerCase()) {
                    control.closest('.form-group').removeClass('hidden');
                }
                else {
                    control.closest('.form-group').addClass('hidden');
                }
            });
            dependentControl.trigger('change');
        }
    });
});
function getValue(control) {
    var controltype = control.get(0).tagName.toLowerCase();
    if (controltype === "input") {
        controltype = control.attr('type').toLowerCase();
    }
    var name = control.attr('name');
    switch (controltype) {
        case 'radio':
        case 'checkbox':
            return $('[name="' + name + '"]:checked').val() || "";
        case 'select':
            return $('option:selected', control).val() + "";
        default:
            return control.val() + "";
    }
}
//# sourceMappingURL=jquery.validation.conditional.js.map