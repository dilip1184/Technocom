using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnocomWeb.UI.UserControls
{
    public partial class ListBoxControl : System.Web.UI.UserControl
    {
        ArrayList arraylist1 = new ArrayList();
        ArrayList arraylist2 = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
        }
        public Label ViewLabel1
        {
            get
            {
                return this.Label1;
            }
            set
            {
                this.Label1 = value;
            }
        }

        public Label ViewLabel2
        {
            get
            {
                return this.Label2;
            }
            set
            {
                this.Label2 = value;
            }
        }
        public ListBox ViewListBox1
        {
            get
            {
                return this.listBox1;
            }
            set
            {
                this.listBox1 = value;
            }
        }

        public ListBox ViewListBox2
        {
            get
            {
                return this.listBox2;
            }
            set
            {
                this.listBox2 = value;
            }
        }

        protected void btnForward_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listBox1.Items[i].Selected)
                    {
                        if (!arraylist1.Contains(listBox1.Items[i]))
                        {
                            arraylist1.Add(listBox1.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arraylist1.Count; i++)
                {
                    if (!listBox2.Items.Contains(((ListItem)arraylist1[i])))
                    {
                        listBox2.Items.Add(((ListItem)arraylist1[i]));
                    }
                    listBox1.Items.Remove(((ListItem)arraylist1[i]));
                }
                listBox2.SelectedIndex = -1;
            }
        }

        protected void btnForwardAll_Click(object sender, EventArgs e)
        {
            while (listBox1.Items.Count != 0)
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    listBox2.Items.Add(listBox1.Items[i]);
                    listBox1.Items.Remove(listBox1.Items[i]);
                }
            }
        }
        protected void btnBackward_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    if (listBox2.Items[i].Selected)
                    {
                        if (!arraylist2.Contains(listBox2.Items[i]))
                        {
                            arraylist2.Add(listBox2.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arraylist2.Count; i++)
                {
                    if (!listBox1.Items.Contains(((ListItem)arraylist2[i])))
                    {
                        listBox1.Items.Add(((ListItem)arraylist2[i]));
                    }
                    listBox2.Items.Remove(((ListItem)arraylist2[i]));
                }
                listBox1.SelectedIndex = -1;
            }
        }

        protected void btnBackwardAll_Click(object sender, EventArgs e)
        {
            while (listBox2.Items.Count != 0)
            {
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    listBox1.Items.Add(listBox2.Items[i]);
                    listBox2.Items.Remove(listBox2.Items[i]);
                }
            }
        }
    }
}