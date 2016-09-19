using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjetOrion.Models
{
    public static class Helpers
    {
        public static MvcHtmlString ActionImage(this HtmlHelper html, string actionName, string controllerName, object routeValues, string imagePath, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.MergeAttribute("class", "img-thumbnail");
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");

            anchorBuilder.MergeAttribute("href", url.Action(actionName, controllerName, routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }
    }
}
