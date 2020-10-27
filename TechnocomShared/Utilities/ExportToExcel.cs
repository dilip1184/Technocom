using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnocomShared.Utilities
{
    public class ExportToFile
    {
        public string GetExportToExcelString<T>(IList<T> itemsList, int[] strColumns)
        {
            string result = string.Empty;

            try
            {
                var stw = new StringWriter();
                var htextw = new HtmlTextWriter(stw);
                var gvData = new GridView();
                gvData.DataSource = itemsList;
                gvData.DataBind();

                string strStyle = @"<style>.text { mso-number-format:\@; } </style>";

                for (int i = 0; i < itemsList.Count; i++)
                {
                    for (int j = 0; j < strColumns.Length; j++)
                    {
                        gvData.Rows[i].Cells[j].Attributes.Add("class", "text");
                    }
                }
                gvData.RenderControl(htextw);
                gvData = null;

                result = strStyle;
                result += stw.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public string GetExportToCSVString<T>(IList<T> itemsList, string delimeter)
        {
            var response = new StringBuilder();

            //Add column header
            object itemFirst = itemsList[0];
            if (itemFirst != null)
            {
                Type type = itemFirst.GetType();
                PropertyInfo[] properties = type.GetProperties();
                for (int j = 0; j < properties.Length; j++)
                {
                    string value = properties[j].Name;
                    string r = value != null ? value : string.Empty;
                    response.Append(value + delimeter);
                }
            }
            response.Append(Environment.NewLine);

            //Add Data
            for (int i = 0; i < itemsList.Count; i++)
            {
                object item = itemsList[i];

                if (item != null)
                {
                    Type type = item.GetType();
                    PropertyInfo[] properties = type.GetProperties();
                    for (int j = 0; j < properties.Length; j++)
                    {
                        object value = properties[j].GetValue(item, null);
                        string r = value != null ? value.ToString() : string.Empty;
                        response.Append(value + delimeter);
                    }
                }
                response.Append(Environment.NewLine);
            }

            return response.ToString();
        }

        public string GetExportToCSVString(IList<object> itemsList, string delimeter)
        {
            var response = new StringBuilder();

            for (int i = 0; i < itemsList.Count; i++)
            {
                object item = itemsList[i];

                if (item != null)
                {
                    Type type = item.GetType();
                    PropertyInfo[] properties = type.GetProperties();
                    for (int j = 0; j < properties.Length; j++)
                    {
                        object value = properties[j].GetValue(item, null);
                        response.Append(value + delimeter);
                    }
                }
                response.Append(Environment.NewLine);
            }

            return response.ToString();
        }
    }
}