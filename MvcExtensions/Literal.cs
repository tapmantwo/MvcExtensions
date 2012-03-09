using System;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace MvcExtensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString LiteralFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string labelText)
        {
            string propertyName = ExpressionHelper.GetExpressionText((LambdaExpression)expression);
            propertyName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(propertyName);

            var builder = new TagBuilder("label");
            builder.MergeAttribute("for", propertyName);
            builder.InnerHtml = labelText;
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }
    }
}