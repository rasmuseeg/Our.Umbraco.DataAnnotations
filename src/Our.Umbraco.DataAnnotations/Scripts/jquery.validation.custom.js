(function ($) {
    function setValidationValues(options, ruleName, value) {
        options.rules[ruleName] = value;
        if (options.message) {
            options.messages[ruleName] = options.message;
        }
    }

    //$.validator.addMethod("mustbetrue", ["required"], function (value, element, param) {
    //    // check if dependency is met
    //    if (!this.depend(param, element))
    //        return "dependency-mismatch";
    //    return element.checked;
    //});

    //$.validator.unobtrusive.adapters.add("mustbetrue", function (options) {
    //    setValidationValues(options, "mustbetrue", true);
    //});

    //$.validator.unobtrusive.adapters.addBool("mustbetrue", "required");
}(jQuery));

$.validator.unobtrusive.adapters.addBool("mustbetrue", "required");
