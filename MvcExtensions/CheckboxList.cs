using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcExtensions
{
    public static class CheckboxList
    {
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, MultiSelectList selectList)
        {
            return CheckBoxListFor<TModel, TProperty>(htmlHelper, expression, selectList, null, 1);
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, MultiSelectList selectList, int numberOfColumns)
        {
            return CheckBoxListFor<TModel, TProperty>(htmlHelper, expression, selectList, null, numberOfColumns);
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, MultiSelectList selectList,
            object htmlAttributes, int numberOfColumns)
        {
            return CheckBoxListFor<TModel, TProperty>(htmlHelper, expression, selectList, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)), numberOfColumns);
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, MultiSelectList selectList,
            IDictionary<string, object> htmlAttributes, int numberOfColumns)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            // Get the property (and assume IEnumerable)
            IEnumerable currentValues = htmlHelper.ViewData.Model != null
                                            ? (IEnumerable) expression.Compile().Invoke(htmlHelper.ViewData.Model)
                                            : null;

            int columnCount = 0;
            var sb = new StringBuilder();
            foreach (var option in selectList)
            {
                columnCount++;
                var builder = new TagBuilder("input");
                if (ShouldItemBeSelected(option, currentValues))
                    builder.MergeAttribute("checked", "checked");

                builder.MergeAttributes<string, object>(htmlAttributes);
                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", option.Value);
                builder.MergeAttribute("name", name);
                builder.InnerHtml = option.Text;
                sb.Append(builder.ToString(TagRenderMode.Normal));
                if (columnCount == numberOfColumns)
                {
                    columnCount = 0;
                    sb.Append("<br />");
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        private static bool ShouldItemBeSelected(SelectListItem item, IEnumerable selectedValues )
        {
            bool selected = false;
            if (null != selectedValues)
            {
                var enumerator = selectedValues.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var currentValueAsString = (string)Convert.ChangeType(enumerator.Current, typeof(string));
                    selected = string.Equals(currentValueAsString, item.Value);
                    if (selected)
                        break;
                }
            }
            return selected;
        }
    }
}