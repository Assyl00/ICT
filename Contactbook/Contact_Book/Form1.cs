using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contact_Book
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadContacts();
            
        }

        BLL bll = default(BLL);
        CreateContactForm createContactForm = new CreateContactForm();
        DetailsForm detailsForm = new DetailsForm();
        private void LoadContacts()
        {
            //ContactDbMock contacts = new ContactDbMock();
            ContactDB contacts2 = new ContactDB();

            bll = new BLL(contacts2);

            bindingSource1.DataSource = bll.GetContacts();

            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = bll.GetContacts().Count.ToString();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            createContactForm.nameTxt.Text = "";
            createContactForm.phoneTxt.Text = "";
            createContactForm.addressTxt.Text = "";
            if (createContactForm.ShowDialog() == DialogResult.OK)
            {
                CreateContactCommand command = new CreateContactCommand();
                command.Name = createContactForm.nameTxt.Text;
                command.Phone = createContactForm.phoneTxt.Text;
                command.Address = createContactForm.addressTxt.Text;
                bll.CreateContact(command);
                bindingSource1.DataSource = bll.GetPaginationPrev();
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            createContactForm.nameTxt.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            createContactForm.phoneTxt.Text = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
            createContactForm.addressTxt.Text = dataGridView1.CurrentRow.Cells["Address"].Value.ToString();
            if (createContactForm.ShowDialog() == DialogResult.OK)
            {
                bll.UpdateContact(dataGridView1.CurrentRow.Cells["Id"].Value.ToString(),
                                            createContactForm.nameTxt.Text,
                                            createContactForm.phoneTxt.Text,
                                            createContactForm.addressTxt.Text);
                
                bindingSource1.DataSource = bll.GetCurrentCont();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = bll.SearchContact(textBox1.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bll.DeleteContact(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
            bindingSource1.DataSource = bll.GetCurrentCont();
        }

        int offset = 0;
        
        private void nextBtn_Click(object sender, EventArgs e)
        {

            bindingSource1.DataSource = bll.GetPaginationNext();

        }

        private void prevBtn_Click(object sender, EventArgs e)
        {

            bindingSource1.DataSource = bll.GetPaginationPrev();

        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            detailsForm.textBoxId.Text = bll.GetContactById(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()).Id;
            detailsForm.textBoxName.Text = bll.GetContactById(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()).Name;
            detailsForm.textBoxPhone.Text = bll.GetContactById(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()).Phone;
            detailsForm.textBoxAddr.Text = bll.GetContactById(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()).Address;
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                bindingSource1.DataSource = bll.GetCurrentCont();
            }
            
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = bll.GetOrderedName();
        }
    }
}
