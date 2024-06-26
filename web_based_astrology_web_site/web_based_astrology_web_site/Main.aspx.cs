using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace web_based_astrology_web_site
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnKoc_Click(object sender, EventArgs e)
        {
            Response.Redirect("Koc.aspx");
        }

        protected void BtnBoga_Click(object sender, EventArgs e)
        {
            Response.Redirect("Boga.aspx");
        }

        protected void BtnIkizler_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ikizler.aspx");
        }

        protected void BtnYengec_Click(object sender, EventArgs e)
        {
            Response.Redirect("Yengec.aspx");
        }

        protected void BtnAslan_Click(object sender, EventArgs e)
        {
            Response.Redirect("Aslan.aspx");
        }

        protected void BtnBasak_Click(object sender, EventArgs e)
        {
            Response.Redirect("Basak.aspx");
        }

        protected void BtnTerazi_Click(object sender, EventArgs e)
        {
            Response.Redirect("Terazi.aspx");
        }

        protected void BtnAkrep_Click(object sender, EventArgs e)
        {
            Response.Redirect("Akrep.aspx");
        }

        protected void BtnYay_Click(object sender, EventArgs e)
        {
            Response.Redirect("Yay.aspx");
        }

        protected void BtnOglak_Click(object sender, EventArgs e)
        {
            Response.Redirect("Oglak.aspx");
        }

        protected void BtnKova_Click(object sender, EventArgs e)
        {
            Response.Redirect("Kova.aspx");
        }

        protected void BtnBalik_Click(object sender, EventArgs e)
        {
            Response.Redirect("Balik.aspx");
        }
    }
}
