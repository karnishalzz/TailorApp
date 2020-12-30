#pragma checksum "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b02157c030ad542ad63eb3ba50bb00bc3a4643fd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Stocks_Index), @"mvc.1.0.view", @"/Views/Stocks/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b02157c030ad542ad63eb3ba50bb00bc3a4643fd", @"/Views/Stocks/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4441184388bfcc68b307595fd040aecb5fe2cbed", @"/Views/_ViewImports.cshtml")]
    public class Views_Stocks_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TailorManagementApp.Models.InventoryModel.Stock>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Popup.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""row"">
        <div class=""col-lg-12"">
            <div class=""card"">
                <div class=""card-block"">
                    <div id=""placeHolder""></div>

                    <div class=""col-lg-12"">
                        <b><i class=""ti-tag""></i> <span>Category List</span></b>
                        <a class=""btn btn-success"" style=""margin-bottom:12px;float:right;"" href=""/PurchaseEntries/Index""><i class=""ti-plus""></i>Stock IN</a>
                    </div>
                    <br />
                    <hr />
                    <div class=""dt-responsive table-responsive"">
                        <table class=""table table-striped table-bordered"" style=""width:100%"" id=""stockTable"">
                            <thead>
                                <tr>
                                    <th>
                                        ");
#nullable restore
#line 24 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.Item));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 27 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.Category));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 30 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.InitialQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 33 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 36 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.CostPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 39 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.SellingPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </th>\r\n                                    <th>\r\n                                        ");
#nullable restore
#line 42 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                   Write(Html.DisplayNameFor(model => model.Purchase.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                    </th>
                                    <th>
                                        Status
                                    </th>

                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 52 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 56 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.Item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <th>\r\n                                            ");
#nullable restore
#line 59 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.Category));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </th>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 62 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.InitialQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 65 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 68 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.CostPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 71 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.SellingPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 74 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                       Write(Html.DisplayFor(modelItem => item.Purchase.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td class=\"txtCheckBox\">\r\n");
#nullable restore
#line 77 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                              
                                                if (item.Quantity > 0)
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <a href=\"#\" style=\"padding: 3px 10px 3px 10px; background-color:green \" class=\"badge\" readonly>Available</a>\r\n");
#nullable restore
#line 81 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"

                                                }
                                                else
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <a href=\"#\" style=\"padding: 3px 10px 3px 10px; background-color:red \" class=\"badge\">Out of Stock</a>\r\n");
#nullable restore
#line 86 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                                }
                                            

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        </td>\r\n                                        <td>\r\n                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b02157c030ad542ad63eb3ba50bb00bc3a4643fd13474", async() => {
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
#line 90 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
                                                                      WriteLiteral(item.StockID);

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
            WriteLiteral("\r\n                                        </td>\r\n                                    </tr>\r\n");
#nullable restore
#line 93 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Stocks\Index.cshtml"
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
                WriteLiteral("\r\n\r\n                        <script type=\"text/javascript\" charset=\"utf8\" src=\"https://cdn.datatables.net/1.10.22/js/jquery.dataTables.js\"></script>\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b02157c030ad542ad63eb3ba50bb00bc3a4643fd16561", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                        <script>

                            $(document).ready(function () {
                                $('#stockTable').DataTable({
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TailorManagementApp.Models.InventoryModel.Stock>> Html { get; private set; }
    }
}
#pragma warning restore 1591
