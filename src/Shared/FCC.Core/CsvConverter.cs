using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCC.Core
{
    public static class CsvConverter<T>
    {
        public static byte[] ConvertToCsv(List<T> reportData)
        {
            StringBuilder lines = new StringBuilder();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.Select(x => x.Name));
            lines.AppendLine(header);
            var valueObj = reportData.Select(row => header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null) != null ? string.Format("\"{0}\"", row.GetType().GetProperty(a).GetValue(row, null)) : null).ToList()).ToList();
            var valueLines = valueObj.Select(row => string.Join(",", row)).ToList();
            foreach (var val in valueLines)
            {
                lines.AppendLine(val);
            }

            byte[] fileContent = Encoding.UTF8.GetBytes(lines.ToString());
            return fileContent;
        }

        public static string ConvertListToHtmlTable(List<T> list)
        {
            StringBuilder htmlTable = new StringBuilder();

            htmlTable.Append("<!DOCTYPE html>");
            htmlTable.Append("<html> <head>"); 
            htmlTable.Append("<style> table{width: 100%; background-color: #ffffff; border-collapse: collapse;" +
                "page-break-inside: auto;}" +
                "table td, table th { padding: 5px; margin: auto}" +
                "table thead {background-color: #dfdddd; display:table-header-group}" +
                "table tr, table td { page-break-inside: avoid; page-break-after: auto;} </style>");
            htmlTable.Append("</head>");
            htmlTable.Append("<body>");
            // Start the table
            htmlTable.Append("<table>");

            // Create the table header
            htmlTable.Append("<thead>");
            htmlTable.Append("<tr>");
            foreach (var property in typeof(T).GetProperties())
            {
                htmlTable.Append("<th>");
                htmlTable.Append(property.Name);
                htmlTable.Append("</th>");
            }
            htmlTable.Append("</tr>");
            htmlTable.Append("</thead>");

            // Create the table body
            htmlTable.Append("<tbody>");
            foreach (var item in list)
            {
                htmlTable.Append("<tr>");
                foreach (var property in typeof(T).GetProperties())
                {
                    htmlTable.Append("<td>");
                    htmlTable.Append(property.GetValue(item));
                    htmlTable.Append("</td>");
                }
                htmlTable.Append("</tr>");
            }
            htmlTable.Append("</tbody>");

            // End the table
            htmlTable.Append("</table>");
            htmlTable.Append("</body>");
            htmlTable.Append("</html>");

            return htmlTable.ToString();
        }
    }
}
