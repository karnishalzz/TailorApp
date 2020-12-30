#pragma checksum "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d86d616b641642822a5306ad33b18f5e5c9abb1d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Gallery_Index), @"mvc.1.0.view", @"/Views/Gallery/Index.cshtml")]
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
#nullable restore
#line 2 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
using TailorManagementApp.Models.InventoryModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d86d616b641642822a5306ad33b18f5e5c9abb1d", @"/Views/Gallery/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4441184388bfcc68b307595fd040aecb5fe2cbed", @"/Views/_ViewImports.cshtml")]
    public class Views_Gallery_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TailorManagementApp.ViewModels.GalleryViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""row"">
        <div class=""col-lg-12"">
            <div class=""card"">
                <div class=""card-block"">
                    <div class=""page-body gallery-page"">
                        <div class=""row"">
                            <div class=""col-sm-12"">
                                <div align=""center"">
                                    <button class=""btn btn-default filter-button"" data-filter=""All"">All</button>
                                    <button class=""btn btn-default filter-button"" data-filter=""Rent"">Rent Items</button>
                                    <button class=""btn btn-default filter-button"" data-filter=""Sale"">Sale items</button>
                                    <button class=""btn btn-default filter-button"" data-filter=""Other"">Other Items</button>
                                    <button class=""btn btn-default filter-button"" data-filter=""Product"">Catelog Items</button>
                                </div>

                                <d");
            WriteLiteral("iv class=\"card-block\">\r\n                                    <div class=\"row\">\r\n");
#nullable restore
#line 51 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                         foreach (var item in Model.Stocks)
                                        {
                                            if (item.Category == CategoryType.Rent)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                <div class=""col-lg-4 col-sm-6 filter Rent"">
                                                    <div class=""thumbnail "">
                                                        <div class=""thumb"">
                                                            <a href=""#"" class=""d-block mb-4 h-100"">
                                                                <img");
            BeginWriteAttribute("src", " src=\"", 3165, "\"", 3191, 1);
#nullable restore
#line 59 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
WriteAttributeValue("", 3171, item.Item.ImagePath, 3171, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 3192, "\"", 3213, 1);
#nullable restore
#line 59 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
WriteAttributeValue("", 3198, item.Item.Name, 3198, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid img-thumbnail\" height=\"500\" width=\"500\"><br />\r\n                                                                <label><b>Name: </b>");
#nullable restore
#line 60 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                                               Write(item.Item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label><br />\r\n                                                                <label><b>Price: </b>");
#nullable restore
#line 61 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                                                Write(item.SellingPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label><br />\r\n                                                                <label>");
#nullable restore
#line 62 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                                  Write(item.Item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</label>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
");
#nullable restore
#line 67 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                            }
                                            if (item.Category == CategoryType.Sale)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                <div class=""col-lg-4 col-sm-6 filter Sale "">
                                                    <div class=""thumbnail "">
                                                        <div class=""thumb"">
                                                            <a href=""#"" class=""d-block mb-4 h-100"">
                                                                <img");
            BeginWriteAttribute("src", " src=\"", 4460, "\"", 4486, 1);
#nullable restore
#line 74 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
WriteAttributeValue("", 4466, item.Item.ImagePath, 4466, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 4487, "\"", 4508, 1);
#nullable restore
#line 74 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
WriteAttributeValue("", 4493, item.Item.Name, 4493, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid img-thumbnail\" height=\"500\" width=\"500\"><br />\r\n                                                                <label><b>Name: </b>");
#nullable restore
#line 75 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                                               Write(item.Item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label><br />\r\n                                                                <label><b>Price: </b>");
#nullable restore
#line 76 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                                                Write(item.SellingPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label><br />\r\n                                                                <label>");
#nullable restore
#line 77 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                                  Write(item.Item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</label>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
");
#nullable restore
#line 82 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                            }
                                            if (item.Category == CategoryType.Others)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                <div class=""col-lg-4 col-sm-6 filter Other "">
                                                    <div class=""thumbnail "">
                                                        <div class=""thumb"">
                                                            <a href=""#"" class=""d-block mb-4 h-100"">
                                                                <img");
            BeginWriteAttribute("src", " src=\"", 5758, "\"", 5784, 1);
#nullable restore
#line 89 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
WriteAttributeValue("", 5764, item.Item.ImagePath, 5764, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 5785, "\"", 5806, 1);
#nullable restore
#line 89 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
WriteAttributeValue("", 5791, item.Item.Name, 5791, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid img-thumbnail\" height=\"500\" width=\"500\"><br />\r\n                                                                <label><b>Name: </b>");
#nullable restore
#line 90 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                                               Write(item.Item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label><br />\r\n                                                                <label><b>Price: </b>");
#nullable restore
#line 91 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                                                Write(item.SellingPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label><br />\r\n                                                                <label>");
#nullable restore
#line 92 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                                  Write(item.Item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</label>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
");
#nullable restore
#line 97 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                            }
                                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 99 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                         foreach (var item in Model.Products)
                                        {


#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <div class=\"col-lg-4 col-sm-6 filter Product\">\r\n\r\n                                                <a href=\"#\" class=\"d-block mb-4 h-100\">\r\n                                                    <img");
            BeginWriteAttribute("src", " src=\"", 6909, "\"", 6930, 1);
#nullable restore
#line 105 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
WriteAttributeValue("", 6915, item.ImagePath, 6915, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 6931, "\"", 6947, 1);
#nullable restore
#line 105 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
WriteAttributeValue("", 6937, item.Name, 6937, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid img-thumbnail\" height=\"500\" width=\"500\"><br />\r\n                                                    <label><b>Name: </b>");
#nullable restore
#line 106 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                                   Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label><br />\r\n                                                    <label>");
#nullable restore
#line 107 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"
                                                      Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                                                </a>\r\n\r\n                                            </div>\r\n");
#nullable restore
#line 111 "F:\DotNetProjects\TailorShopWebApp\TailorShopWebApp\Views\Gallery\Index.cshtml"

                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </div>\r\n                                </div>\r\n                            </div>\r\n\r\n                        </div>\r\n                    </div>\r\n\r\n\r\n\r\n                    </div></div></div></div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TailorManagementApp.ViewModels.GalleryViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
