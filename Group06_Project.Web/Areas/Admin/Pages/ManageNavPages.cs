using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group06_Project.Web.Areas.Admin.Pages;

public class ManageNavPages
{
    public static string Index => "Index";
    public static string User => "User";
    public static string Catalog => "Catalog";
    public static string Comment => "Comment";

    public static string? IndexNavClass(ViewContext viewContext)
    {
        return PageNavClass(viewContext, Index);
    }

    public static string? UserNavClass(ViewContext viewContext)
    {
        return PageNavClass(viewContext, User);
    }

    public static string? CatalogNavClass(ViewContext viewContext)
    {
        return PageNavClass(viewContext, Catalog);
    }

    public static string? CommentNavClass(ViewContext viewContext)
    {
        return PageNavClass(viewContext, Comment);
    }

    public static string? PageNavClass(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["ActivePage"] as string
                         ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "sidebar__nav-link--active" : null;
    }
}