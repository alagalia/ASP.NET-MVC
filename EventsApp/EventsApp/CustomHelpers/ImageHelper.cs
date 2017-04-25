using System.Web.Mvc;

namespace EventsApp.CustomHelpers
{
    public static class ImageHelper
    {
        public static MvcHtmlString ImageThumbnail(this HtmlHelper htmlHelper, string src, string alt)
        {
            var imageTag = new TagBuilder("image");
            imageTag.MergeAttribute("src", src);
            imageTag.MergeAttribute("alt", alt);
            imageTag.MergeAttribute("position", "relative");
            imageTag.MergeAttribute("width", "100%");
            imageTag.MergeAttribute("height", "150px");
            imageTag.MergeAttribute("overflow", "hidden");
            imageTag.MergeAttribute("line-height", "50px");
            imageTag.MergeAttribute("object-fit", "cover");


            return MvcHtmlString.Create(imageTag.ToString(TagRenderMode.SelfClosing));
        }
    }
}