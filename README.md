| Branch  | Status | 
|:--------|:-------|
| master-v8 | [![Build Status](https://dev.azure.com/bitflipping/Our.Umbraco.DataAnnotations/_apis/build/status/rasmuseeg.Our.Umbraco.DataAnnotations?branchName=master-v8)](https://dev.azure.com/bitflipping/Our.Umbraco.DataAnnotations/_build/latest?definitionId=9&branchName=master-v8) |
| dev-v8 | [![Build Status](https://dev.azure.com/bitflipping/Our.Umbraco.DataAnnotations/_apis/build/status/rasmuseeg.Our.Umbraco.DataAnnotations?branchName=dev-v8)](https://dev.azure.com/bitflipping/Our.Umbraco.DataAnnotations/_build/latest?definitionId=9&branchName=dev-v8) |

# Our.Umbraco.DataAnotations
Contains model validation attributes to for your properties, by using umbraco dictionary as the resource for error messages.

This branch is for Umbraco 8. [Looking for Umbraco 7?](https://github.com/rasmuseeg/Our.Umbraco.DataAnnotations/tree/dev-v7)

## Installation
During installation the keys will be created nested below `DataAnnotions` dictionary key.

NuGet:
```
PM > Install-Package Our.Umbraco.DataAnnotations
```

Build the project and start website.
On first run, a migration will check foreach dictionary key used in the application and added it to umbraco dictionary items.
Only default `en-US` keys and translations are added.

## Client Validation
Include the following scripts in your layout .cshtml file

```
<body>
    @RenderBody()

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.18.0/jquery.validate.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js"></script>
</body>
```

or using ClientDependency like so:
```
@using ClientDendency.Core.Mvc;

...

<body>
    @RenderBody()

    @{
        Html.RequiresJs("https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.18.0/jquery.validate.min.js")
        Html.RequiresJs("https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js")
    }
    @Html.RenderJsHere()
</body>

```

The above is just samples, you may use any method you like to include the scripts.

Then on each page with a form enable validation
```
Html.EnableClientValidation();
Html.EnableUnobtrusiveJavaScript();
```

The end result for a page with validation could look like:
```cshtml
@inherits UmbracoViewPage<LoginModel>
@using UmbracoBootstrap.Web.Models;
@using UmbracoBootstrap.Web.Controllers;
@{ 
    Html.EnableClientValidation();
    Html.EnableUnobtrusiveJavaScript();
}
@using (Html.BeginUmbracoForm<MemberController>("HandleLogin", null, new { @role="form", @class="" }, FormMethod.Post))
{
    @Html.ValidationSummary("loginModel", true)

    <div class="form-group">
        @Html.LabelFor(m=> m.Username, new { @class="control-label" })
        @Html.TextBoxFor(m => m.Username, new { @class = "form-control" })
        @Html.ValidationMessageFor(m => m.Username)
    </div>

    <div class="form-group">
        @Html.LabelFor(m=> m.Password, new { @class="control-label" })
        @Html.PasswordFor(m => m.Password, new {
            @class = "form-control form-control-appended",
            @placeholder = Umbraco.GetDictionaryValue("EnterYourPassword", "Enter your password")
        })
        @Html.ValidationMessageFor(m => m.Password)
    </div>

    @Html.HiddenFor(m=> m.RedirectUrl)

    <button type="submit" role="button">@Umbraco.GetDictionaryValue("SignIn", "Sign in")</button>
}
```

### 

## Attributes
Decorate your properties with the following attributes

 * UmbracoCompare
 * UmbracoDisplayName
 * UmbracoEmailAddress
 * UmbracoMinLength
 * UmbracoMaxLength
 * UmbracoStringLength
 * UmbracoMustBeTrue
 * UmbracoPassword
 * UmbracoRegularExpression
 * UmbracoRequired

**How to use:**
```C#
[UmbracoRequired]
public string MyProperty { get; set; } 
```

#### UmbracoCompareAttribute

| Key | Default |
| -- | -- |
| `EqualToError` | The '{0}' and '{1}' field fields must match. |


Example:
```c#
[UmbracoDisplayName(nameof(Password))]
[DataType(DataType.Password)]
public string Password { get; set; }

[UmbracoDisplayName(nameof(ConfirmPassword))]
[UmbracoRequired]
[UmbracoCompare(nameof(Password))]
[DataType(DataType.Password)]
public string ConfirmPassword { get; set; }
```

### UmbracoDisplayName

| Key | Default |
| -- | -- |
| Provied key | Must be created by your self. |

Example:
```c#
[UmbracoDisplayName(nameof(Username))]
[UmbracoRequired]
public string Username { get; set; }
```

### UmbracoEmailAddress

| Key | Default |
| -- | -- |
| EmailError | `Doesn't look like a valid e-mail.` |

Example:
```c#
[UmbracoEmailAddress(DictionaryKey = "MyCustomKey")]
public string Email { get; set; }
```

### UmbracoMinLength

| Key | Default |
| -- | -- |
| MinLengthError | `Must not exceed {1} characters.`

Example:
```C#
[UmbracoMinLength(20, DictionaryKey = "MyCustomKey")]
property string MyProperty { get; set; }
```

### UmbracoMaxLength

| Key | Default | Description |
| -- | -- |
| MaxLengthError | `Must not exceed {1} characters.`

Example:
```C#
[UmbracoMaxLength(120, DictionaryKey = "MyCustomKey")]
property string MyProperty { get; set; }
```

### UmbracoStringLength

| Key | Default | Description |
| -- | -- | -- |
| MaxLengthError | `Must not exceed {1} characters.` | Used when only `MaximumLength` is defined.
| MinMaxLengthError | `Must not be less than {1} and greather than {1} characters.` | Used when both `MaximumLength` and `MinimumLength` is defined.

Example:
```C#
[UmbracoStringLength(120, MinimumLength = 30, DictionaryKey = "MyCustomKey")]
property string Message { get; set; }
```

### UmbracoMustBeTrue
| Key | Default |
| -- | -- | -- |
| MustBeTrueError | `{0} is required.` |

Example:
```C#
[UmbracoMustBeTrue(DictionaryKey = "MyCustomKey")]
property boool Consent { get; set; }
```

### UmbracoPassword
| Key | Default | Description |
| -- | -- | -- |
| PasswordError | `Must be at least {1} characters long and contain {2} symbol (!, @, #, etc).` | Used when only `MaximumLength` is defined.
| MinPasswordLengthError | `Must be at least {1} characters.` | Used when only `MaximumLength` is defined.
| MinNonAlphanumericCharactersError | `Must contain at leat {2} symbol (!, @, #, etc).` | 
| PasswordStrengthError | `Must be at least {1} characters long and contain {2} symbol (!, @, #, etc).`

Example:
```C#
[UmbracoPassword(DictionaryKey = "CustomPasswordKey", 
    MinPasswordLengthDictionaryKey = "CustomMinPasswordLengthKey", 
    MinNonAlphanumericCharactersDictionaryKey = "MyCustomMinNonAlphanumericCharactersKey", 
    PasswordStrengthDictionaryKey = "MyCustomPasswordStrengtKey",
    PasswordStrengthRegexTimeout = 360)]
property string Password { get; set; }
```

### UmbracoRegularExpression

There are no default keys for this attribute, since each regex validation is unique

Example:
```C#
[UmbracoRegularExpression("^([0-9]{4})$", DictionaryKey = "MyCustomRegexKey")]
property string Password { get; set; }
```

### UmbracoRequired

Example:
```C#
[UmbracoRequired(DictionaryKey = "MyCustomRequiredKey")]
property string MyProperty { get; set; }
```

## Custom dictionary keys
Each Attribute, has a public property `DictionaryKey` which can be set like this:
```
[UmbracoReguired(DictionaryKey = "MyCustomKey")]
[UmbracoRegularExpression(DictionaryKey = "MyCustomRegexKey")]
[UmbracoRegularExpression(DictionaryKey = "MyCustomRegexKey")]
property string MyProperty { get; set; }
```

Not setting a custom key, will fallback to the default dictionary key.