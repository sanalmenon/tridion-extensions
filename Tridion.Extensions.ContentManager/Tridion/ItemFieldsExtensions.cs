using System;
using System.Collections.Generic;
using System.Linq;
using Tridion.ContentManager;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace Tridion.Extensions.ContentManager
{
    public static partial class ItemFieldsExtensions
    {
        public static double? Number(this ItemFields fields, string fieldName)
        {
            return fields.Numbers(fieldName).Cast<double?>().FirstOrDefault();
        }

        public static IEnumerable<double> Numbers(this ItemFields fields, string fieldName)
        {
            return fields.IfExists(fieldName, (NumberField field) => field.Values);
        }

        public static Keyword Keyword(this ItemFields fields, string fieldName)
        {
            return fields.Keywords(fieldName).FirstOrDefault();
        }

        public static IEnumerable<Keyword> Keywords(this ItemFields fields, string fieldName)
        {
            return fields.IfExists(fieldName, (KeywordField field) => field.Values);
        }

        public static Component Component(this ItemFields fields, string fieldName)
        {
            return fields.Components(fieldName).FirstOrDefault();
        }

        public static IEnumerable<Component> Components(this ItemFields fields, string fieldName)
        {
            return fields.IfExists(fieldName, (ComponentLinkField field) => field.Values);
        }

        public static string ExternalLink(this ItemFields fields, string fieldName)
        {
            return fields.ExternalLinks(fieldName).FirstOrDefault() ?? string.Empty;
        }

        public static IEnumerable<string> ExternalLinks(this ItemFields fields, string fieldName)
        {
            return fields.IfExists(fieldName, (ExternalLinkField field) => field.Values);
        }

        public static Component Multimedia(this ItemFields fields, string fieldName)
        {
            return fields.Multimedias(fieldName).FirstOrDefault();
        }

        public static IEnumerable<Component> Multimedias(this ItemFields fields, string fieldName)
        {
            return fields.IfExists(fieldName, (MultimediaLinkField field) => field.Values);
        }

        public static ItemFields Embedded(this ItemFields fields, string fieldName)
        {
            return fields.Embeddeds(fieldName).FirstOrDefault();
        }

        public static IEnumerable<ItemFields> Embeddeds(this ItemFields fields, string fieldName)
        {
            return fields.IfExists(fieldName, (EmbeddedSchemaField field) => field.Values);
        }

        public static string Text(this ItemFields fields, string fieldName)
        {
            return Texts(fields, fieldName).FirstOrDefault() ?? string.Empty;
        }

        public static IEnumerable<string> Texts(this ItemFields fields, string fieldName)
        {
            return fields.IfExists(fieldName, (TextField field) => field.Values);
        }

        public static string XHtml(this ItemFields fields, string fieldName)
        {
            return fields.XHtmls(fieldName).FirstOrDefault();
        }

        public static IEnumerable<string> XHtmls(this ItemFields fields, string fieldName)
        {
            return fields.IfExists<XhtmlField, string>(fieldName, field => field.Values);
        }

        static IEnumerable<OUT> IfExists<FIELDTYPE, OUT>(this ItemFields fields, string fieldName, Func<FIELDTYPE, IEnumerable<OUT>> f)
            where FIELDTYPE : ItemField
        {
            return
                null != fields && fields.Contains(fieldName)
                    ? f((FIELDTYPE)fields[fieldName])
                    : Enumerable.Empty<OUT>();
        }

        public static DateTime? Date(this ItemFields fields, string fieldName = "date")
        {
            return fields.Dates(fieldName)
              .Cast<DateTime?>()
              .DefaultIfEmpty(default(DateTime?))
              .FirstOrDefault();
        }

        public static IEnumerable<DateTime> Dates(this ItemFields fields, string fieldName = "date")
        {
            return fields.IfExists<DateField, DateTime>(fieldName, field => field.Values);
        }

        public static int GetFieldValueCount(this ItemFields fields, string fieldName)
        {
            if (null == fields)
            {
                return 0;
            }

            var field = fields[fieldName];

            return
                field is ComponentLinkField
                    ? ((ComponentLinkField)field).Values.Count
                    : field is TextField
                        ? ((TextField)field).Values.Count
                        : field is EmbeddedSchemaField
                            ? ((EmbeddedSchemaField)field).Values.Count
                            : 0;
        }

        /// <summary>
        /// Manual unification of different field types logic to overcome native tridion implementation shortcoming,
        /// which is not polymorphic.
        /// </summary>
        static readonly IDictionary<Type, Func<ItemFields, string, string>> valueResolver =
            new Dictionary<Type, Func<ItemFields, string, string>> {
                { typeof(KeywordField), (fields, name) => fields.Keywords(name).Select(k => k.Title).FirstOrDefault() },
                { typeof(ComponentLinkField), (fields, name) => fields.Components(name).Select<Component, TcmUri>(c => c.Id).FirstOrDefault() },
                { typeof(ExternalLinkField), (fields, name) => fields.ExternalLinks(name).FirstOrDefault() },
                { typeof(MultimediaLinkField), (fields, name) => fields.Multimedias(name).Select(mc => mc.Title).FirstOrDefault() },
                { typeof(DateField), (fields, name) => fields.Dates(name).FirstOrDefault().ToString("yyyy-MM-dd HH:mm:ss") }
            };

        /// <summary>
        /// Gets a sensible string represntation of a field.
        /// </summary>
        public static string AsText(this ItemFields fields, string fieldName)
        {
            ItemField field;
            Type fieldType;

            return
                null == fields
                || !fields.Contains(fieldName)
                    ? string.Empty
                    : valueResolver.ContainsKey((fieldType = (field = fields[fieldName]).GetType()))
                        ? valueResolver[fieldType](fields, fieldName) ?? string.Empty
                        : field.ToString();
        }

        public static string ToRFC822Date(this ItemField inputDate)
        {
            string formattedDate = inputDate.ToString();
            const string RFC822DateFormat = "yyyyMMddHHmmss";
            var dt = new DateTime();
            if (DateTime.TryParse(formattedDate, out dt))
            {
                formattedDate = dt.ToString(RFC822DateFormat);
            }
            return formattedDate;
        }
    }
}