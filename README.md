# Aspose.HTML Converter 

Aspose.HTML Converter is built with [Aspose.HTML Cloud](https://products.aspose.cloud/html/family).
Please, follow to [Aspose.Cloud](https://purchase.aspose.cloud/) to get free API keys

Aspose.HTML Converter is a back-end API for editor extensions:

* Aspose.HTML Converter for [VS Code extension](https://github.com/asposecloudmarketplace/aspose-html-converter-for-vscode)
* Aspose.HTML Converter for [Atom.io extension](https://atom.io/packages/aspose-html-converter)

To run/debug app on local machine, please add Aspose.Cloud credential:

* in Visual Studio 2017/2019 use "Manage User Secrets" and add the following section

  ```
  "AsposeCloud": {
    "ApiKey": "<put api key here>",
    "AppSid": "<put app sid here>",
    "BasePath": "https://api.aspose.cloud"
  }
  ```

* in Visual Studio Code (or others editors) use terminal 
    * `dotnet user-secrets init`
    * `dotnet user-secrets set "AsposeCloud:ApiKey" "<put api key here>"`
    * `dotnet user-secrets set "AsposeCloud:AppSid" "<put app sid here>"`
    * `dotnet user-secrets set "AsposeCloud:BasePath" "https://api.aspose.cloud"`

## Workflow

In order to make changes in the repository, you need to:

1. Create a branch with the proposed changes whose name matches the feature/* pattern.
2. Create a pull request for this branch. It will be automatically assigned to a suitable reviewer.
3. Once the request is approved, it can be merged.
