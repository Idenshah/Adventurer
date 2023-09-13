using Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final
{
    public partial class Details : System.Web.UI.Page
    {
        List<Adventurer> adventurers = new List<Adventurer>();
        List<Item> items = Helper.GetAvailableItems();

        int index = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            Get_Index();
            Get_Adventurers();
            Reset_ErrorMessages();

            if (!IsPostBack) { Populate_Items(); }

            if (adventurers.Count > 0 && index >= 0 && index < adventurers.Count)
            {
                Adventurer currentAdventurer = adventurers[index];
                txtName.InnerText = currentAdventurer.Name;
                txtType.InnerText = currentAdventurer.Type;
                lblStrength.Text = currentAdventurer.Strength.ToString();
                lblDexterity.Text = currentAdventurer.Dexterity.ToString();
                lblVitality.Text = currentAdventurer.Vitality.ToString();
                lblMana.Text = currentAdventurer.Mana.ToString();
                txtPhrase.InnerText = currentAdventurer.Greeting();  
            }

        }

        protected void Get_Index()
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            index = int.Parse(Request.QueryString["id"]);
        }

        protected void Get_Adventurers()
        {
            if (Session["adventurers"] != null)
            {
                adventurers = (List<Adventurer>)Session["adventurers"];
            }
        }

        protected void Reset_ErrorMessages()
        {
            lblErrorMessages.Visible = false;
            lblErrorMessages.Controls.Clear();
        }

        protected void Populate_Items()
        {
            int itemIndex = 0;
            foreach (Item item in items)
            {
                ListItem listItem = new ListItem($"{item.Name} ({item.StrengthRequirement}/{item.DexterityRequirement}/{item.ManaRequirement})", itemIndex.ToString());

                if (adventurers.Count > 0)
                {
                    if (adventurers[index].Item_Equiped(item))
                    {
                        listItem.Selected = true;
                    }
                }

                cblItems.Items.Add(listItem);

                itemIndex++;
            }
        }

        protected void btnEquipItems_Click(object sender, EventArgs e)
        {
            Reset_ErrorMessages();

            Adventurer currentAdventurer = adventurers[index];

            for (int i = 0; i < cblItems.Items.Count; i++)
            {
                ListItem listItem = cblItems.Items[i];
                Item currentItem = items[int.Parse(listItem.Value)];

                if (listItem.Selected)
                {
              
                    if (!currentAdventurer.Item_Equiped(currentItem))
                    {
                        try
                        {
                            currentAdventurer.Equip_Item(currentItem);
                        }
                        catch (Exception ex)
                        {
                            lblErrorMessages.Visible = true;
                            lblErrorMessages.Text += ex.Message + "<br/>";
                        }
                    }
                }
                else
                { 
                    if (currentAdventurer.Item_Equiped(currentItem))
                    {
                        currentAdventurer.Unequip_Item(currentItem);
                    }
                }
            }

            Session["adventurers"] = adventurers;
            Populate_Items();
        }

    }
}