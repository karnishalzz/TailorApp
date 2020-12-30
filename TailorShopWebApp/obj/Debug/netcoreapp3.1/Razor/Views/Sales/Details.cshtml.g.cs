#pragma checksum "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6421c547482468bffff09864dec3c88f1298d283"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sales_Details), @"mvc.1.0.view", @"/Views/Sales/Details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6421c547482468bffff09864dec3c88f1298d283", @"/Views/Sales/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4441184388bfcc68b307595fd040aecb5fe2cbed", @"/Views/_ViewImports.cshtml")]
    public class Views_Sales_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TailorManagementApp.Models.SalesModule.Sales>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("form1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 4 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "~/Views/Shared/_Layout.cshtml";
    decimal amount = Model.Amount;
    decimal discount = Model.Discount;
    decimal discountPercent = (discount * 100) / amount;

    int count = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""row"">
        <div class=""col-lg-12"">
            <div class=""card"">
                <div class=""card-block"">
                    <h5 class=""m-b-10""><i class=""ti-tag""></i>Sales Summery</h5>
                    <hr />
                </div>
            </div>
        </div>
    </div>
    <div class=""row"">
        <div class=""col-lg-12"">
            <div class=""card"">
                <div class=""card-block"">
                    <div class=""box box-primary"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6421c547482468bffff09864dec3c88f1298d2834593", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        <div class=""row"">
                            <div class=""col-md-4"">
                               
                                <table class=""table table-condensed"">
                                    <tr>
                                        <td>Sale ID:</td>
                                        <td>");
#nullable restore
#line 35 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                       Write(Model.SalesID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    </tr>\r\n                                    <tr>\r\n                                        <td>Sale Date</td>\r\n                                        <td> ");
#nullable restore
#line 39 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                        Write(Model.Date);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                                    </tr>\r\n                                    <tr>\r\n                                        <td>Amount</td>\r\n                                        <td> ");
#nullable restore
#line 43 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                        Write(Model.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                                    </tr>\r\n                                    <tr>\r\n                                        <td>Discount % :</td>\r\n                                        <td><span id=\"disPer\">");
#nullable restore
#line 47 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                                         Write(discountPercent);

#line default
#line hidden
#nullable disable
            WriteLiteral(" (");
#nullable restore
#line 47 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                                                           Write(Model.Discount);

#line default
#line hidden
#nullable disable
            WriteLiteral(")</span> </td>\r\n                                    </tr>\r\n                                    <tr>\r\n                                        <td>Vat :</td>\r\n                                        <td> ");
#nullable restore
#line 51 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                        Write(Model.Tax);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                                    </tr>\r\n                                    <tr>\r\n                                        <td>Grand Total </td>\r\n                                        <td>");
#nullable restore
#line 55 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                       Write(Model.GrandTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
                                    </tr>
                                </table>

                            </div>
                            <div class=""col-md-8""></div>
                        </div>

                        <div class=""row"">
                            <div class=""col-md-12"">
                                <table class=""table table-striped"">
                                    <thead class=""dataTableHead"">
                                        <tr>
                                            <th>Item Name</th>
                                            <th>Stock ID</th>
                                            <th>Sale ID</th>
                                            <th>Quantity</th>
                                            <th>Rate</th>
                                            <th>Amount</th>
                                        </tr>
                                    </thead>

");
#nullable restore
#line 77 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                     foreach (var item in Model.SalesItems)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <tr>\r\n");
#nullable restore
#line 80 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                              count++;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <td>");
#nullable restore
#line 81 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                           Write(item.Stock.Item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                                            <td>");
#nullable restore
#line 82 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                           Write(item.StockID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 83 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                           Write(item.SalesDetailID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td><b>");
#nullable restore
#line 84 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                              Write(item.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></td>\r\n                                            <td>");
#nullable restore
#line 85 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                           Write(item.Rate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 86 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                           Write(item.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        </tr>\r\n");
#nullable restore
#line 88 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Sales\Details.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </table>

                            </div>
                            <div class=""form-group row"" style=""float:right"">
                                <div class=""col-sm-12"">
                                    <a href=""/Sales/Index/"" class=""btn btn-info"">Back To List</a>
                                </div>

                            </div>
                        </div>
                    </div>

</div></div></div></div>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TailorManagementApp.Models.SalesModule.Sales> Html { get; private set; }
    }
}
#pragma warning restore 1591
