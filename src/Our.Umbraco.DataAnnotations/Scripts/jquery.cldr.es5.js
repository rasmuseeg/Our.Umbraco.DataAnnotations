"use strict";

(function ($, Globalize)
{
    var twoLetterIsoLangugeCode = document.querySelector('html').getAttribute('lang');
    var CLDR_DATA_BASE = "/Assets/js/lib/cldr-data";

    // Fetch required cldr data for locale
    $.when(               
        $.get(CLDR_DATA_BASE + "/main/" + twoLetterIsoLangugeCode + "/ca-gregorian.json"),
        $.get(CLDR_DATA_BASE + "/main/" + twoLetterIsoLangugeCode + "/numbers.json"),
        $.get(CLDR_DATA_BASE + "/supplemental/likelySubtags.json"),
        $.get(CLDR_DATA_BASE + "/supplemental/numberingSystems.json"),
        $.get(CLDR_DATA_BASE + "/supplemental/timeData.json"),
        $.get(CLDR_DATA_BASE + "/supplemental/weekData.json")
    ).then(function ()
    {
        // Normalize $.get results, we only need the JSON, not the request statuses.
        return [].slice.apply(arguments, [0]).map(function (result)
        {
            return result[0];
        });

    }).then(Globalize.load).then(function ()
    {
        // Load locale 
        Globalize.locale(twoLetterIsoLangugeCode);
    });
}(jQuery, Globalize));