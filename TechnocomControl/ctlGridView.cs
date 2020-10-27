using TechnocomShared.Constants;
using TechnocomShared.Entities;
using TechnocomShared.Enums;
using TechnocomShared.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TechnocomControl
{
    public class ctlGridView : GridView
    {

        /// <summary>
        /// Gets or sets the name of the meta source.
        /// </summary>
        /// <value>
        /// The name of the meta source.
        /// </value>
        [Bindable(true)]
        [Localizable(true)]
        public string MetaSourceName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [auto bind].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [auto bind]; otherwise, <c>false</c>.
        /// </value>
        [Bindable(true)]
        [Localizable(true)]
        [DefaultValue(true)]
        public bool AutoBind
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the meta sort.
        /// </summary>
        /// <value>
        /// The name of the meta sort.
        /// </value>
        [Bindable(true)]
        [Localizable(true)]
        public string MetaSortName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the meta command.
        /// </summary>
        /// <value>
        /// The type of the meta command.
        /// </value>
        [BrowsableAttribute(false)]
        [Bindable(true)]
        [Localizable(true)]
        public string MetaCommandType
        {
            get;
            set;
        }

        [BrowsableAttribute(false)]
        [Bindable(true)]
        [Localizable(true)]
        [DefaultValue(GridSelectMode.None)]
        public GridSelectMode GridMode
        {
            get;
            set;
        }

        [BrowsableAttribute(false)]
        [Bindable(true)]
        [Localizable(true)]
        [DefaultValue(15)]
        public int GridPageSize
        {
            get;
            set;
        }

        [BrowsableAttribute(false)]
        [Bindable(true)]
        [Localizable(true)]
        [DefaultValue(false)]
        public bool GridDisablePaging
        {
            get;
            set;
        }


        [BrowsableAttribute(true)]
        [DefaultValue("record")]
        public string NoCheckCaption
        {
            get;
            set;
        }
        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);
            HeaderStyle.CssClass = "gridHeader";
            AlternatingRowStyle.CssClass = "gridAlternatingRowStyle";
            RowStyle.CssClass = "gridRowStyle";
            EmptyDataText = "No Record Found!";
            EmptyDataRowStyle.CssClass = "info";

            //Code Added for Check box persistent          
            if (GridMode != GridSelectMode.MultiSelect) return;
            for (var i = 0; i < Rows.Count; i++)
            {
                var item = (OrderedDictionary)DataKeys[i].Values;
                var checkBox = (CheckBox)Rows[i].Cells[0].FindControl("selectCheckBox");
                int index;
                checkBox.Checked = Contains(CachedDataKey, item, out index);
            }
            //End:
        }
        private bool Contains(List<OrderedDictionary> list, OrderedDictionary item, out int index)
        {
            bool flag = true;
            index = -1;

            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    flag = CompareKeys(list[i], item);
                    if (flag)
                    {
                        index = i;
                        return true;
                    }
                }
            }
            else
                return false;

            return flag;
        }
        private bool CompareKeys(OrderedDictionary first, OrderedDictionary second)
        {
            bool flag = true;

            for (int i = 0; i < first.Count; i++)
            {
                if (first[i].ToString() != second[i].ToString())
                    return false;
            }

            return flag;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains event data.</param>
        protected override void OnInit(EventArgs e)
        {
            Page.RegisterRequiresControlState(this);
            base.OnInit(e);
            AllowPaging = GridDisablePaging ? false : true;
            AllowSorting = true;
            PageSize = (GridPageSize == 0) ? 15 : GridPageSize;
            PagerSettings.Position = PagerPosition.TopAndBottom;

            var page = Page as ctlPage;
            if (page != null && MetaSourceName != string.Empty && AutoBind)
                page.DataLoadHandler += page_DataLoadHandler;


            if (GridMode != GridSelectMode.None)
            {
                var bfield = new TemplateField
                                 {
                                     HeaderTemplate = new GridViewTemplate(ListItemType.Header, "Select", GridMode),
                                     ItemTemplate = new GridViewTemplate(ListItemType.Item, "Select", GridMode)
                                 };
                Columns.Insert(0, bfield);
            }

            //set caption default value
            if (NoCheckCaption == null)
                NoCheckCaption = "record";
        }
        
        /// <summary>
        /// Page_s the data load handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        void page_DataLoadHandler(ctlPage sender, CommandEventArgs e)
        {
            if (MetaSourceName == null) return;

            var data = ((IList<object>)e.CommandArgument).Where(obj => obj.GetType().FullName.Contains(MetaSourceName)).ToList();
            LoadData(data);
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void LoadData<T>(IList<T> data)
        {
            CachedDataKey = null;
            GridSortExpression = null;
            GridSortDirection = null;
            PageIndex = 0;
            var dataTable = ToDataTable(data);
            DataSource = new DataView(dataTable);
            DataBind();
            SelectedIndex = -1;
            GridDataSource = dataTable;
        }
        private List<OrderedDictionary> CachedDataKey
        {
            get
            {
                if (ViewState[ClientID + "DataKey"] == null)
                    ViewState[ClientID + "DataKey"] = new List<OrderedDictionary>();
                return ViewState[ClientID + "DataKey"] as List<OrderedDictionary>;
            }
            set { ViewState[ClientID + "DataKey"] = value; }
        }
        public DataTable GridDataSource
        {
            get { return (DataTable)ViewState[ClientID + "GridDataSource"]; }
            set { ViewState[ClientID + "GridDataSource"] = value; }
        }
        private string GridSortExpression
        {
            get { return (string)ViewState[ClientID + "SortExpression"]; }
            set { ViewState[ClientID + "SortExpression"] = value; }
        }
        private string GridSortDirection
        {
            get { return (string)ViewState[ClientID + "SortDirection"]; }
            set { ViewState[ClientID + "SortDirection"] = value; }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowCommand"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewCommandEventArgs"/> that contains event data.</param>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);
            // Use the RowType property to determine whether the 
            // row being created is the header row. 
            if (e.Row.RowType != DataControlRowType.Header) return;
            // Call the GetSortColumnIndex helper method to determine
            // the index of the column being sorted.
            var sortColumnIndex = GetSortColumnIndex();

            if (sortColumnIndex != -1)
            {
                // Call the AddSortImage helper method to add
                // a sort direction image to the appropriate
                // column header. 
                AddSortImage(sortColumnIndex, e.Row);
            }
        }

        // This is a helper method used to determine the index of the
        // column being sorted. If no column is being sorted, -1 is returned.
        int GetSortColumnIndex()
        {
            // Iterate through the Columns collection to determine the index
            // of the column being sorted.
            foreach (var field in
                Columns.Cast<DataControlField>().Where(field => field.SortExpression == GridSortExpression))
            {
                return Columns.IndexOf(field);
            }

            return -1;
        }

        // This is a helper method used to add a sort direction
        // image to the header of the column being sorted.
        void AddSortImage(int columnIndex, TableRow headerRow)
        {
            // Create the sorting image based on the sort direction.
            var sortImage = new Image();
            //sortImage.Attributes.Add("","");
            if (GridSortDirection == "ASC")
            {
                sortImage.ImageUrl = "~/Content/images/ascending.gif";
                sortImage.Attributes.Add("style", "padding-left:5px;");
                sortImage.AlternateText = "Ascending Order";
            }
            else
            {
                sortImage.ImageUrl = "~/Content/images/descending.gif";
                sortImage.Attributes.Add("style", "padding-left:5px;");
                sortImage.AlternateText = "Descending Order";
            }

            // Add the image to the appropriate header cell.
            headerRow.Cells[columnIndex].Controls.Add(sortImage);

        }

        /// <summary>
        /// Initializes the pager row displayed when the paging feature is enabled.
        /// </summary>
        /// <param name="row">A <see cref="T:System.Web.UI.WebControls.GridViewRow"/> that represents the pager row to initialize.</param>
        /// <param name="columnSpan">The number of columns the pager row should span.</param>
        /// <param name="pagedDataSource">A <see cref="T:System.Web.UI.WebControls.PagedDataSource"/> that represents the data source.</param>
        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            CreateCustomPager(row, columnSpan, pagedDataSource);
        }

        /// <summary>
        /// Creates the custom pager.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="columnSpan">The column span.</param>
        /// <param name="pagedDataSource">The paged data source.</param>
        protected virtual void CreateCustomPager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            var pageCount = pagedDataSource.PageCount;
            var pageIndex = pagedDataSource.CurrentPageIndex + 1;
            const int pageButtonCount = 1;

            var cell = new TableCell();
            row.Cells.Add(cell);
            if (columnSpan > 1) cell.ColumnSpan = columnSpan;

            if (pageCount <= 1) return;
            var pager = new Panel { HorizontalAlign = HorizontalAlign.Right, CssClass = "paging-box-small" };
            cell.Controls.Add(pager);
            var pagerCount = BuildSpan("Page " + pageIndex + " of " + pageCount, "paging-box");
            pager.Controls.Add(pagerCount);

            var min = pageIndex - pageButtonCount;
            var max = pageIndex + pageButtonCount;

            if (max > pageCount)
                min -= max - pageCount;
            else if (min < 1)
                max += 1 - min;


            var page = pageIndex > 1
                               ? BuildLinkButton(0, " |< ", "Page", "0")
                               : BuildSpan(" |< ", "disabled");
            pager.Controls.Add(page);

            // Create "previous" button
            page = pageIndex > 1
                       ? BuildLinkButton(pageIndex - 2, " << ", "Page", "Prev")
                       : BuildSpan(" << ", "disabled");
            pager.Controls.Add(page);

            // Create page buttons
            var needDiv = false;
            for (var i = 1; i <= pageCount; i++)
            {
                if (i <= 2 || i > pageCount - 2 || (min <= i && i <= max))
                {
                    var text = i.ToString(NumberFormatInfo.InvariantInfo);
                    page = i == pageIndex
                               ? BuildSpan(" " + text + " ", "current")
                               : BuildLinkButton(i - 1, " " + text + " ", "Page", text);
                    pager.Controls.Add(page);
                    needDiv = true;
                }
                else if (needDiv)
                {
                    page = BuildSpan("&hellip;", null);
                    pager.Controls.Add(page);
                    needDiv = false;
                }
            }

            // Create "next" button
            page = pageIndex < pageCount
                       ? BuildLinkButton(pageIndex, " >> ", "Page", "Next")
                       : BuildSpan(" >> ", "disabled");
            pager.Controls.Add(page);

            page = pageIndex < pageCount
                       ? BuildLinkButton(pageIndex, " >| ", "Page", pageCount.ToString())
                       : BuildSpan(" >| ", "disabled");
            pager.Controls.Add(page);
        }
        private Control BuildLinkButton(int pageIndex, string text, string commandName, string commandArgument)
        {
            var link = new PagerLinkButton(this) { Text = text };
            link.EnableCallback(ParentBuildCallbackArgument(pageIndex));
            link.CommandName = commandName;
            link.CommandArgument = commandArgument;
            return link;
        }
        private string ParentBuildCallbackArgument(int pageIndex)
        {
            var m =
                typeof(GridView).GetMethod("BuildCallbackArgument", BindingFlags.NonPublic | BindingFlags.Instance, null,
                                            new[] { typeof(int) }, null);
            return (string)m.Invoke(this, new object[] { pageIndex });
        }

        /// <summary>
        /// Builds the span.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns></returns>
        private Control BuildSpan(string text, string cssClass)
        {
            var span = new HtmlGenericControl("span");
            if (!String.IsNullOrEmpty(cssClass)) span.Attributes["class"] = cssClass;


            span.InnerHtml = text;
            return span;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.WebControls.GridView.Sorting"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewSortEventArgs"/> that contains event data.</param>
        /// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.GridView.Sorting"/> event.</exception>
        protected override void OnSorting(GridViewSortEventArgs e)
        {
            var dataView = new DataView(GridDataSource);
            if (dataView.Count <= 0) return;
            PageIndex = 0;
            dataView.Sort = GetSortExpression(e);
            DataSource = dataView;
            DataBind();
            SelectedIndex = -1;
        }

        /// <summary>
        /// GetSortExpression
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private string GetSortExpression(GridViewSortEventArgs e)
        {
            if (GridSortExpression != null)
                if (!GridSortExpression.Equals(e.SortExpression))
                    GridSortDirection = null;

            if (GridSortDirection != null)
            {
                GridSortDirection = GridSortDirection.Equals("ASC") ? "DESC" : "ASC";
            }
            else
            {
                GridSortDirection = "ASC";
            }

            GridSortExpression = e.SortExpression;

            return e.SortExpression + " " + GridSortDirection;
        }

        /// <summary>
        /// Toes the view.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        private DataTable ToDataTable<T>(IList<T> list)
        {
            if (list.Count < 1)
                return null;

            var table = new DataTable(list.GetType().Name);
            var properties = list[0].GetType().GetProperties();

            foreach (var info in properties)
            {
                try
                {
                    table.Columns.Add(new DataColumn(info.Name, info.PropertyType));
                }
                catch (NotSupportedException)
                {
                    table.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType)));
                }
                catch (Exception)
                {
                    table.Columns.Add(new DataColumn(info.Name, typeof(object)));
                }
            }

            foreach (object t in list)
            {
                var row = new object[properties.Length];
                for (var i = 0; i < row.Length; i++)
                    row[i] = properties[i].GetValue(t, null);

                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.WebControls.GridView.PageIndexChanging"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewPageEventArgs"/> that contains event data.</param>
        /// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.GridView.PageIndexChanging"/> event.</exception>
        protected override void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            var dataView = new DataView(GridDataSource);

            //Code Added for Check box persistent
            if (GridMode == GridSelectMode.MultiSelect)
            {
                if (GridMode == GridSelectMode.MultiSelect)
                {
                    for (var i = 0; i < Rows.Count; i++)
                    {
                        var item = (OrderedDictionary)DataKeys[i].Values;
                        var checkBox = (CheckBox)Rows[i].Cells[0].FindControl("selectCheckBox");
                        int index;
                        if (checkBox.Checked && !Contains(CachedDataKey, item, out index))
                            CachedDataKey.Add(item);
                        else if (!checkBox.Checked && Contains(CachedDataKey, item, out index))
                            CachedDataKey.RemoveAt(index);
                    }
                }
            }
            //End:

            if (GridSortExpression != null)
                dataView.Sort = GridSortExpression + " " + GridSortDirection;
            PageIndex = e.NewPageIndex > -1 ? e.NewPageIndex : 0;
            DataSource = dataView;
            DataBind();
        }

        public DataKey GetSelectedData()
        {
            SingleSelectData();
            return SelectedDataKey;
        }

        public IList<DataKey> GetMultiSelectedData()
        {
            return MultiSelectData();
        }


        private void SingleSelectData()
        {
            SelectedIndex = -1;
            for (var i = 0; i < Rows.Count; i++)
            {
                var radioButton = (RadioButton)Rows[i].Cells[0].FindControl("selectRadio");

                if (!radioButton.Checked) continue;
                SelectedIndex = i;
                break;
            }
            //added by PS - in case we haven't selected any radio button
            if (SelectedIndex == -1)
            {
                throw new ValidationException(string.Format(ErrorDescription.NoRowSelectedinGridView, NoCheckCaption));
            }
        }

        public void SelectAllCheckBoxes()
        {
            for (var i = 0; i < Rows.Count; i++)
            {
                ((CheckBox)HeaderRow.FindControl("selectAllCheckBox")).Checked = true;
                ((CheckBox)Rows[i].Cells[0].FindControl("selectCheckBox")).Checked = true;
            }
        }

        private IList<DataKey> MultiSelectData()
        {
            var response = new List<DataKey>();
            for (var i = 0; i < Rows.Count; i++)
            {
                var checkBox = (CheckBox)Rows[i].Cells[0].FindControl("selectCheckBox");
                // Added
                var item = (OrderedDictionary)DataKeys[i].Values;
                if (checkBox.Checked)
                {
                    SelectedIndex = i;
                    response.Add(SelectedDataKey);
                }
                //Code Added for Check box persistent
                else
                {
                    int index;
                    if (Contains(CachedDataKey, item, out index))
                    {
                        CachedDataKey.RemoveAt(index);
                    }
                }
                //End:
            }

            //Code Added for Check box persistent
            if (CachedDataKey != null)
            {
                List<OrderedDictionary> chkdatakey = CachedDataKey;
                DataKey selectedKey;
                foreach (var item in chkdatakey)
                {
                    selectedKey = GetDataKeyFromSelected(item);
                    if (!response.Contains(selectedKey))
                        response.Add(selectedKey);
                }
            }
            //End:

            if (response.Count == 0)
                throw new ValidationException(string.Format(ErrorDescription.NoRowSelectedinGridView, NoCheckCaption));

            CachedDataKey = null;
            return response;
        }

        public void SelectAllGridData(string keyField, string keyField2 = "", string keyField3 = "", string keyField4 = "", string keyField5 = "")
        {
            CachedDataKey.Clear();
            for (var i = 0; i < Rows.Count; i++)
            {
                ((CheckBox)Rows[i].Cells[0].FindControl("selectCheckBox")).Checked = true;
            }

            for (var i = 0; i < GridDataSource.Rows.Count; i++)
            {
                DataRow dr = GridDataSource.Rows[i];
                OrderedDictionary dic = new OrderedDictionary();
                dic.Add(keyField, dr[keyField]);
                if (!string.IsNullOrEmpty(keyField2))
                {
                    dic.Add(keyField2, dr[keyField2]);
                }
                if (!string.IsNullOrEmpty(keyField3))
                {
                    dic.Add(keyField3, dr[keyField3]);
                }
                if (!string.IsNullOrEmpty(keyField4))
                {
                    dic.Add(keyField4, dr[keyField4]);
                }
                if (!string.IsNullOrEmpty(keyField5))
                {
                    dic.Add(keyField5, dr[keyField5]);
                }
                CachedDataKey.Add(dic);
            }
        }

        private DataKey GetDataKeyFromSelected(IOrderedDictionary key)
        {
            return new DataKey(key);
        }

        public void ReportHeaderVisibility(IEnumerable<ReportColumnLists> unSelectedFieldList)
        {
            for (var i = 0; i < Columns.Count; i++)
            { Columns[i].Visible = true; }

            foreach (var unSelectedFields in unSelectedFieldList)
            {
                for (var i = 0; i < Columns.Count; i++)
                {
                    if (!unSelectedFields.ColumnName.Equals(Columns[i].HeaderText)) continue;
                    Columns[i].Visible = false;
                    i = Columns.Count;
                }
            }
        }

        public string[] GetSelectedDataFields()
        {
            var allDataFields = new List<string>();
            for (var i = 0; i < Columns.Count; i++)
            {
                if (Columns[i].Visible)
                    allDataFields.Add(((BoundField)Columns[i]).DataField);
            }
            var selectedDataFields = allDataFields.ToArray();
            return selectedDataFields;
        }
        public void ClearData()
        {
            CachedDataKey = null;
            GridSortExpression = null;
            GridSortDirection = null;
            PageIndex = 0;
            DataSource = null;
            DataBind();
            SelectedIndex = -1;
            GridDataSource = null;
        }
    }
    public class GridViewTemplate : ITemplate
    {
        readonly ListItemType _templateType;
        readonly string _columnName;
        private readonly GridSelectMode _gridMode;

        public GridViewTemplate(ListItemType type, string colname, GridSelectMode gridMode)
        {
            _templateType = type;
            _columnName = colname;
            _gridMode = gridMode;
        }

        void ITemplate.InstantiateIn(Control container)
        {
            switch (_templateType)
            {
                case ListItemType.Header:
                    switch (_gridMode)
                    {
                        case GridSelectMode.SingleSelect:
                            var lbl = new Label { Text = _columnName };
                            container.Controls.Add(lbl);

                            break;
                        case GridSelectMode.MultiSelect:
                            var checkBox = new CheckBox { ID = "selectAllCheckBox" };
                            checkBox.Attributes.Add("onclick", "CheckAll(this);");
                            container.Controls.Add(checkBox);
                            break;
                    }
                    break;
                case ListItemType.Item:
                    switch (_gridMode)
                    {
                        case GridSelectMode.SingleSelect:
                            var radioButton = new RadioButton { ID = "selectRadio" };
                            radioButton.Attributes.Add("onclick", "CheckRadioGridView(this,'" + container.ClientID + "');");
                            container.Controls.Add(radioButton);
                            break;
                        case GridSelectMode.MultiSelect:
                            var checkBox = new CheckBox { ID = "selectCheckBox" };
                            container.Controls.Add(checkBox);
                            break;
                    }
                    break;
            }
        }
    }
}

