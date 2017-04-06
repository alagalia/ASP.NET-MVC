using System.Web.Mvc;

namespace EventsApp.CustomHelpers
{
    public static class ImageHelper
    {
        public static MvcHtmlString ImageThumbnail(this HtmlHelper htmlHelper, string src, string alt, int size)
        {
            var imageTag = new TagBuilder("image");
            imageTag.MergeAttribute("src", src);
            imageTag.MergeAttribute("alt", alt);
            imageTag.MergeAttribute("position", "relative");
            imageTag.MergeAttribute("width", "200px");
            imageTag.MergeAttribute("height", "200px");
            imageTag.MergeAttribute("overflow", "hidden");

            return MvcHtmlString.Create(imageTag.ToString(TagRenderMode.SelfClosing));
        }
    }
}