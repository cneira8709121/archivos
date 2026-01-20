using System;
using System.Web.UI;
using Ruv.WebSite.Utilidades.Controles.GridCustomPager;

public partial class Utilidades_Controles_GridCustomPager : System.Web.UI.UserControl {

    public event CustomDelegateClass.PageChangedEventHandler PageChanged;

    private int? _currentPageNumber;
    public int CurrentPageNumber {
        get {
            _currentPageNumber = _currentPageNumber ?? 1;
            return _currentPageNumber.Value;
        }
        set { 
            _currentPageNumber = value;
        }
    }

    private int? _totalPages;
    public int TotalPages {
        get {
            _totalPages = _totalPages ?? 0;
            return _totalPages.Value;
        }
        set {
            _totalPages = value;
        }
    }

    private int? _currentPageSize;
    public int CurrentPageSize {
        get {
            _currentPageSize = _currentPageSize ?? 20;
            return _currentPageSize.Value;
        }
        set { 
            _currentPageSize = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        if (!Page.IsPostBack) {
            if (this.TotalPages > 0) {
                this.PagingDisabled.Visible = false;
                this.PagingEnabled.Visible = true;

                for (int count = 1; count <= this.TotalPages; ++count)
                    ddlPageNumber.Items.Add(count.ToString());
                ddlPageNumber.Items[0].Selected = true;

                lblShowRecords.Text = this.TotalPages.ToString();
            }
            else {
                this.PagingDisabled.Visible = true;
                this.PagingEnabled.Visible = false;
            }
        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPageChangeArgs args = new CustomPageChangeArgs();
        args.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value);
        args.CurrentPageNumber = 1;
        args.TotalPages = Convert.ToInt32(this.lblShowRecords.Text);
        Pager_PageChanged(this, args);

        ddlPageNumber.Items.Clear();
        for (int count = 1; count <= this.TotalPages; ++count)
            ddlPageNumber.Items.Add(count.ToString());
        ddlPageNumber.Items[0].Selected = true;
        lblShowRecords.Text = this.TotalPages.ToString();
    }

    void Pager_PageChanged(object sender, CustomPageChangeArgs e)
    {
        PageChanged(this, e);
        //throw new Exception("The method or operation is not implemented.");
    }

    protected void ddlPageNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPageChangeArgs args = new CustomPageChangeArgs();
        args.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value);
        args.CurrentPageNumber = Convert.ToInt32(this.ddlPageNumber.SelectedItem.Text);
        args.TotalPages = Convert.ToInt32(this.lblShowRecords.Text);
        Pager_PageChanged(this, args);

        lblShowRecords.Text = args.TotalPages.ToString();
    }

}