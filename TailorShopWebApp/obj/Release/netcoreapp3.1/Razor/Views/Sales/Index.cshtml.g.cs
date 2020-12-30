#pragma checksum "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8d9cc0d1ab85244137645ddb6465f90cb600c1c6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sales_Index), @"mvc.1.0.view", @"/Views/Sales/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\_ViewImports.cshtml"
using TailorManagementApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\_ViewImports.cshtml"
using TailorManagementApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8d9cc0d1ab85244137645ddb6465f90cb600c1c6", @"/Views/Sales/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4441184388bfcc68b307595fd040aecb5fe2cbed", @"/Views/_ViewImports.cshtml")]
    public class Views_Sales_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TailorManagementApp.Models.SalesModule.Sales>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""row"">
        <div class=""col-lg-12"">
            <div class=""card"">
                <div class=""card-block"">
                    <div class=""col-lg-12"">
                        <b><i class=""ti-tag""></i> <span><b>Sales List</b></span></b>
                        <a class=""btn btn-success"" style=""margin-bottom:12px;float:right;"" href=""/SalesEntries/Index/""><i class=""ti-plus""></i>Create New</a>
                    </div>
                    <br />
                    <hr />
                    <div class=""dt-responsive table-responsive"">
                        <table class=""table table-striped table-bordered nowrap"" id=""salesTable"">
                            <thead>
                                <tr>
                                    <th>
                                        ");
#nullable restore
#line 22 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.SalesID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 25 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 28 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.Amount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 31 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.Discount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 34 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.Tax));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 37 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.GrandTotal));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 40 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.Remarks));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th></th>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n");
#nullable restore
#line 46 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 50 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.SalesID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 53 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 56 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Amount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 59 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Discount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 62 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Tax));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 65 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.GrandTotal));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 68 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Remarks));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n\r\n                                    <td>\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8d9cc0d1ab85244137645ddb6465f90cb600c1c610779", async() => {
                WriteLiteral("<i class=\"ti-view-list\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 72 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                                                  WriteLiteral(item.SalesID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\r\n\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 76 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            </tbody>
                        </table>
                    </div>
                    </div></div></div></div>
                    <link rel=""stylesheet"" type=""text/css"" href=""https://cdn.datatables.net/1.10.22/css/jquery.dataTables.css"">

");
            DefineSection("scripts", async() => {
                WriteLiteral(@"

                        <script type=""text/javascript"" charset=""utf8"" src=""https://cdn.datatables.net/1.10.22/js/jquery.dataTables.js""></script>

                        <script>

                            $(document).ready(function () {
                                $('#salesTable').DataTable({
                                    ""paging"": true,
                                    ""ordering"": true,
                                    ""info"": true
                                });
                            });


                        </script>

                    ");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TailorManagementApp.Models.SalesModule.Sales>> Html { get; private set; }
    }
}
#pragma warning restore 1591
