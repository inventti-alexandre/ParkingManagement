﻿using ParkingMangement.DAO;
using ParkingMangement.DTO;
using ParkingMangement.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkingMangement.GUI
{
    public partial class FormQuanLy : Form
    {
        public FormQuanLy()
        {
            InitializeComponent();
        }

        private void FormQuanLy_Load(object sender, EventArgs e)
        {
            loadUserInfoTab();
        }

        /*
        User data
        */

        private void loadUserInfoTab()
        {
            loadUserList();
            loadSexDataToComboBox(cbUserSexCreate);
            loadSexDataToComboBox(cbUserSexEdit);
            loadFunctionalDataToComboBox(cbUserFunctionCreate);
            loadFunctionalDataToComboBox(cbUserFunctionEdit);

            int Index = dgvUserList.CurrentRow.Index;
            loadUserInfoFromDataGridViewRow(Index);
        }

        private void loadUserList()
        {
            dgvUserList.DataSource = UserDAO.GetAllData();
            dgvUserList2.DataSource = UserDAO.GetID_Name_Function();
        }

        private void loadSexDataToComboBox(ComboBox cb)
        {
            cb.DataSource = SexDAO.GetAllData();
            cb.DisplayMember = "SexName";
            cb.ValueMember = "SexID";
        }

        private void loadFunctionalDataToComboBox(ComboBox cb)
        {
            cb.DataSource = FunctionalDAO.GetAllData();
            cb.DisplayMember = "FunctionName";
            cb.ValueMember = "FunctionID";
        }

        private bool checkCreateUserData()
        {
            if (string.IsNullOrWhiteSpace(tbUserIDCreate.Text))
            {
                MessageBox.Show(Constant.sMessageUserIdNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbUserNameCreate.Text))
            {
                MessageBox.Show(Constant.sMessageUserNameNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbUserAccountCreate.Text))
            {
                MessageBox.Show(Constant.sMessageAccountNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbUserPassCreate.Text))
            {
                MessageBox.Show(Constant.sMessagePasswordNullError);
                return false;
            }
            return true;
        }

        private bool checkUpdateUserData()
        {
            if (string.IsNullOrWhiteSpace(tbUserNameEdit.Text))
            {
                MessageBox.Show(Constant.sMessageUserNameNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbUserAccountEdit.Text))
            {
                MessageBox.Show(Constant.sMessageAccountNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbUserPassEdit.Text))
            {
                MessageBox.Show(Constant.sMessagePasswordNullError);
                return false;
            }
            return true;
        }

        private void createUser()
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Id = tbUserIDCreate.Text.Trim();
            userDTO.Name = tbUserNameCreate.Text.Trim();
            userDTO.Account = tbUserAccountCreate.Text.Trim();
            userDTO.Password = tbUserPassCreate.Text.Trim();

            DataRow functionDataRow = ((DataRowView)cbUserFunctionCreate.SelectedItem).Row;
            userDTO.FunctionId = functionDataRow["FunctionID"].ToString();

            DataRow sexDataRow = ((DataRowView)cbUserSexCreate.SelectedItem).Row;
            userDTO.SexId = Convert.ToInt32(sexDataRow["SexID"]);

            UserDAO.Insert(userDTO);
            loadUserList();
        }

        private void updateUser()
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Id = tbUserIDEdit.Text.Trim();
            userDTO.Name = tbUserNameEdit.Text.Trim();
            userDTO.Account = tbUserAccountEdit.Text.Trim();
            userDTO.Password = tbUserPassEdit.Text.Trim();

            DataRow functionDataRow = ((DataRowView)cbUserFunctionEdit.SelectedItem).Row;
            userDTO.FunctionId = functionDataRow["FunctionID"].ToString();

            DataRow sexDataRow = ((DataRowView)cbUserSexEdit.SelectedItem).Row;
            userDTO.SexId = Convert.ToInt32(sexDataRow["SexID"]);

            UserDAO.Update(userDTO);
            loadUserList();
        }

        private void loadUserInfoFromDataGridViewRow(int Index)
        {
            string id = Convert.ToString(dgvUserList.Rows[Index].Cells["ID"].Value);
            tbUserIDEdit.Text = id;
            string account = Convert.ToString(dgvUserList.Rows[Index].Cells["Account"].Value);
            tbUserAccountEdit.Text = account;
            string pass = Convert.ToString(dgvUserList.Rows[Index].Cells["Pass"].Value);
            tbUserPassEdit.Text = pass;
            string nameUser = Convert.ToString(dgvUserList.Rows[Index].Cells["NameUser"].Value);
            tbUserNameEdit.Text = nameUser;
            string sexName = Convert.ToString(dgvUserList.Rows[Index].Cells["SexName"].Value);
            cbUserSexEdit.Text = sexName;
            string functionName = Convert.ToString(dgvUserList.Rows[Index].Cells["FunctionName"].Value);
            cbUserFunctionEdit.Text = functionName;
        }

        private void clearInputUserInfo()
        {
            tbUserIDCreate.Text = "";
            tbUserAccountCreate.Text = "";
            tbUserPassCreate.Text = "";
            tbUserNameCreate.Text = "";
            cbUserSexCreate.SelectedIndex = 0;
            cbUserFunctionCreate.SelectedIndex = 0;
        }

        private void loadWorkList()
        {
            dgvWorkList.DataSource = WorkDAO.GetAllData();
        }

        private void searchWork()
        {
            if (radioButtonWorkDate.Checked)
            {
                DateTime startDate = dateTimePickerWorkAssignDate.Value.Date;
                startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
                DateTime endDate = dateTimePickerWorkAssignDate.Value.Date;
                endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
                dgvWorkList.DataSource = WorkDAO.GetDataByMultiDate(startDate, endDate);
            }
            else if (radioButtonWorkMultiDate.Checked)
            {
                DateTime startDate = dateTimePickerWorkAssignStartDate.Value.Date;
                startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
                DateTime endDate = dateTimePickerWorkAssignEndDate.Value.Date;
                endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
                dgvWorkList.DataSource = WorkDAO.GetDataByMultiDate(startDate, endDate);
            }
        }

        private void btnWorkSearch_Click(object sender, EventArgs e)
        {
            searchWork();
        }

        private void btnWorkAll_Click(object sender, EventArgs e)
        {
            loadWorkList();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            if (checkCreateUserData())
            {
                createUser();
            }
        }

        private void dgvUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Index = e.RowIndex;
            int Count = dgvUserList.Rows.Count;
            if (Index < Count - 1)
            {
                loadUserInfoFromDataGridViewRow(Index);
            }
        }

        private void btnCancelCreateUser_Click(object sender, EventArgs e)
        {
            clearInputUserInfo();
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (checkUpdateUserData())
            {
                updateUser();
            }
        }

        private void btnCancelUpdateUser_Click(object sender, EventArgs e)
        {
            int Index = dgvUserList.CurrentRow.Index;
            loadUserInfoFromDataGridViewRow(Index);
        }

        private void tabQuanLyNhanSu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabQuanLyNhanSu.SelectedTab == tabQuanLyNhanSu.TabPages["tabPageDoBangChamCong"])
            {
                loadWorkList();
            }
        }

        /*
        Part data
        */

        private void loadPartList()
        {
            dgvPartList.DataSource = PartDAO.GetAllData();
        }

        private bool checkCreatePartData()
        {
            if (string.IsNullOrWhiteSpace(tbPartIdCreate.Text))
            {
                MessageBox.Show(Constant.sMessagePartIdNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbPartNameCreate.Text))
            {
                MessageBox.Show(Constant.sMessagePartNameNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbPartAmountCreate.Text))
            {
                MessageBox.Show(Constant.sMessagePartAmountNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbPartLimitCreate.Text))
            {
                MessageBox.Show(Constant.sMessagePartLimitNullError);
                return false;
            }
            return true;
        }

        private bool checkUpdatePartData()
        {
            if (string.IsNullOrWhiteSpace(tbPartNameEdit.Text))
            {
                MessageBox.Show(Constant.sMessagePartNameNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbPartAmountEdit.Text))
            {
                MessageBox.Show(Constant.sMessagePartAmountNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbPartLimitEdit.Text))
            {
                MessageBox.Show(Constant.sMessagePartLimitNullError);
                return false;
            }
            return true;
        }

        private void createPart()
        {
            PartDTO partDTO = new PartDTO();
            partDTO.Id = tbPartIdCreate.Text;
            partDTO.Name = tbPartNameCreate.Text;
            partDTO.Sign = tbPartSignCreate.Text;
            partDTO.Amount = int.Parse(tbPartAmountCreate.Text);
            partDTO.Limit = int.Parse(tbPartIdCreate.Text);

            PartDAO.Insert(partDTO);
            loadPartList();
        }

        private void updatePart()
        {
            PartDTO partDTO = new PartDTO();
            partDTO.Id = tbPartIdEdit.Text;
            partDTO.Name = tbPartNameEdit.Text;
            partDTO.Sign = tbPartSignEdit.Text;
            partDTO.Amount = int.Parse(tbPartAmountEdit.Text);
            partDTO.Limit = int.Parse(tbPartIdEdit.Text);

            PartDAO.Update(partDTO);
            loadPartList();
        }

        private void loadPartInfoFromDataGridViewRow(int Index)
        {
            string id = Convert.ToString(dgvPartList.Rows[Index].Cells["PartID"].Value);
            tbPartIdEdit.Text = id;
            string partName = Convert.ToString(dgvPartList.Rows[Index].Cells["PartName"].Value);
            tbPartNameEdit.Text = partName;
            string sign = Convert.ToString(dgvPartList.Rows[Index].Cells["Sign"].Value);
            tbPartSignEdit.Text = sign;
            string amount = Convert.ToString(dgvPartList.Rows[Index].Cells["Amount"].Value);
            tbPartAmountEdit.Text = amount;
            string limit = Convert.ToString(dgvPartList.Rows[Index].Cells["Limit"].Value);
            tbPartLimitEdit.Text = limit;
        }

        private void clearInputPartInfo()
        {
            tbPartIdCreate.Text = "";
            tbPartNameCreate.Text = "";
            tbPartSignCreate.Text = "";
            tbPartAmountCreate.Text = "";
            tbPartLimitCreate.Text = "";
        }

        private void tabCardManagement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabQuanLyThe_LoaiXe.SelectedTab == tabQuanLyThe_LoaiXe.TabPages["tabPageQuanLyLoaiXe"])
            {
                loadPartList();
            }
        }

        private void btnCreatePart_Click(object sender, EventArgs e)
        {
            if (checkCreatePartData())
            {
                createPart();
            }
        }

        private void btnCancelCreatePart_Click(object sender, EventArgs e)
        {
            clearInputPartInfo();
        }

        private void btnUpdatePart_Click(object sender, EventArgs e)
        {
            if (checkUpdatePartData())
            {
                updatePart();
            }
        }

        private void btnCancelUpdatePart_Click(object sender, EventArgs e)
        {
            int Index = dgvPartList.CurrentRow.Index;
            loadPartInfoFromDataGridViewRow(Index);
        }

        private void dgvPartList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Index = e.RowIndex;
            int Count = dgvPartList.Rows.Count;
            if (Index < Count - 1)
            {
                loadPartInfoFromDataGridViewRow(Index);
            }
        }

        /*
        Card data
        */

        private void loadCardList()
        {
            DataTable data = CardDAO.GetAllData();
            if (data != null)
            {
                for (int row = 0; row < data.Rows.Count; row++)
                {
                    if (data.Rows[row].Field<string>("IsUsing") == "1")
                    {
                        data.Rows[row].SetField("IsUsing", Constant.sLabelCardUsing);
                    }
                    else
                    {
                        data.Rows[row].SetField("IsUsing", Constant.sLabelCardNotUsing);
                    }

                }

                int lastIdentify = data.Rows[data.Rows.Count - 1].Field<int>("Identify");
                tbCardIdentifyCreate.Text = lastIdentify + 1 + "";

                dgvCardList.DataSource = data;
            }
        }

        private void loadCardStatistic()
        {
            DataTable data = CardDAO.GetDataGroupByType();

            DataRow rowAllCard = data.NewRow();
            rowAllCard.SetField("PartName", "Tổng thẻ");
            rowAllCard.SetField("IsUsing", "Dùng & Không");
            rowAllCard.SetField("SumCard", CardDAO.GetCardCount());
            data.Rows.InsertAt(rowAllCard, 0);

            DataRow rowNotUsingCard = data.NewRow();
            rowNotUsingCard.SetField("PartName", "Tổng thẻ không dùng");
            rowNotUsingCard.SetField("IsUsing", Constant.sLabelCardNotUsing);
            rowNotUsingCard.SetField("SumCard", CardDAO.GetNotUsingCardCount());
            data.Rows.InsertAt(rowNotUsingCard, 1);

            DataRow rowUsingCard = data.NewRow();
            rowUsingCard.SetField("PartName", "Tổng thẻ đang dùng");
            rowUsingCard.SetField("IsUsing", Constant.sLabelCardUsing);
            rowUsingCard.SetField("SumCard", CardDAO.GetUsingCardCount());
            data.Rows.InsertAt(rowUsingCard, 2);

            dgvCardStatistic.DataSource = data;
        }

        private void loadPartDataToComboBox(ComboBox cb)
        {
            DataTable dt = PartDAO.GetAllData();
            cb.DataSource = dt;
            cb.DisplayMember = "PartName";
            cb.ValueMember = "PartID";
        }

        private void loadPartDataWithFieldAllToComboBox(ComboBox cb)
        {
            DataTable dt = PartDAO.GetAllData();
            DataRow dr = dt.NewRow();
            dr["PartName"] = "Tất cả";
            dt.Rows.InsertAt(dr, 0);
            cb.DataSource = dt;
            cb.DisplayMember = "PartName";
            cb.ValueMember = "PartID";
        }

        private bool checkCreateCardData()
        {
            if (string.IsNullOrWhiteSpace(tbCardIDCreate.Text))
            {
                MessageBox.Show(Constant.sMessageCardIdNullError);
                return false;
            }
            return true;
        }

        private void createCard()
        {
            CardDTO cardDTO = new CardDTO();
            cardDTO.Id = tbCardIDCreate.Text.Trim();
            cardDTO.Id = tbCardIDCreate.Text.Trim();

            DataRow partDataRow = ((DataRowView)cbPartNameCreate.SelectedItem).Row;
            cardDTO.Type = Convert.ToInt32(partDataRow["PartID"]);

            string isUsing = "0";
            if (cbIsUsingCreate.Checked)
            {
                isUsing = "1";
            }
            cardDTO.IsUsing = isUsing;
            cardDTO.DayUnlimit = DateTime.Now;

            CardDAO.Insert(cardDTO);
            loadCardList();
        }

        private bool checkUpdateCardData()
        {
            if (string.IsNullOrWhiteSpace(tbCardIDEdit.Text))
            {
                MessageBox.Show(Constant.sMessageCardIdNullError);
                return false;
            }
            return true;
        }

        private void updateCard()
        {
            CardDTO cardDTO = new CardDTO();
            cardDTO.Identify = int.Parse(tbCardIdentifyEdit.Text.Trim());
            cardDTO.Id = tbCardIDEdit.Text.Trim();

            DataRow partDataRow = ((DataRowView)cbPartNameEdit.SelectedItem).Row;
            cardDTO.Type = Convert.ToInt32(partDataRow["PartID"]);

            string isUsing = "0";
            if (cbIsUsingEdit.Checked)
            {
                isUsing = "1";
            }
            cardDTO.IsUsing = isUsing;
            cardDTO.DayUnlimit = DateTime.Now;

            CardDAO.Update(cardDTO);
            loadCardList();
        }

        private void searchCard()
        {
            string key = tbCardSearch.Text;
            DataTable data = CardDAO.SearchData(key);
            for (int row = 0; row < data.Rows.Count; row++)
            {
                if (data.Rows[row].Field<string>("IsUsing") == "1")
                {
                    data.Rows[row].SetField("IsUsing", Constant.sLabelCardUsing);
                }
                else
                {
                    data.Rows[row].SetField("IsUsing", Constant.sLabelCardNotUsing);
                }

            }
            dgvCardList.DataSource = data;
        }

        private void loadCardInfoFromDataGridViewRow(int Index)
        {
            string identify = Convert.ToString(dgvCardList.Rows[Index].Cells["Identify"].Value);
            tbCardIdentifyEdit.Text = identify;
            string id = Convert.ToString(dgvCardList.Rows[Index].Cells["CardID"].Value);
            tbCardIDEdit.Text = id;
            string isUsing = Convert.ToString(dgvCardList.Rows[Index].Cells["IsUsing"].Value);
            if (isUsing == Constant.sLabelCardUsing)
            {
                cbIsUsingEdit.Checked = true;
            } else
            {
                cbIsUsingEdit.Checked = false;
            }
            string partName = Convert.ToString(dgvCardList.Rows[Index].Cells["CardPartName"].Value);
            cbPartNameEdit.Text = partName;
        }

        private void tabQuanLy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabQuanLy.SelectedTab == tabQuanLy.TabPages["tabPageQuanLyTheXeLoaiXe"])
            {
                loadCardList();
                loadPartDataToComboBox(cbPartNameCreate);
                loadPartDataToComboBox(cbPartNameEdit);

                loadCardStatistic();

                if (dgvCardList.DataSource != null)
                {
                    int Index = dgvCardList.CurrentRow.Index;
                    loadCardInfoFromDataGridViewRow(Index);
                }
            }
        }

        private void btnCardCreate_Click(object sender, EventArgs e)
        {
            if (checkCreateCardData())
            {
                createCard();
                loadCardStatistic();
            }
        }

        private void btnCardEdit_Click(object sender, EventArgs e)
        {
            if (checkUpdateCardData())
            {
                updateCard();
                loadCardStatistic();
            }
        }

        private void dgvCardList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Index = e.RowIndex;
            int Count = dgvCardList.Rows.Count;
            if (Index < Count - 1)
            {
                loadCardInfoFromDataGridViewRow(Index);
            }
        }

        private void tbCardSearch_TextChanged(object sender, EventArgs e)
        {
            searchCard();
        }

        /*
        Ticket Month data
        */

        private void loadTicketMonthData()
        {
            DataTable data = TicketMonthDAO.GetAllData();
            dgvTicketMonthList.DataSource = data;
            int lastIdentify = data.Rows[data.Rows.Count - 1].Field<int>("Identify");
            tbTicketMonthIdentifyCreate.Text = lastIdentify + 1 + "";
        }

        private void tabQuanLyVeThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabQuanLyVeThang.SelectedTab == tabQuanLyVeThang.TabPages["tabPageTaoMoiVeThang"])
            {
                loadTicketMonthData();
                loadPartDataToComboBox(cbTicketMonthPartCreate);
                loadPartDataToComboBox(cbTicketMonthPartEdit);
            }
        }

        private void addTicketMonth()
        {
            TicketMonthDTO ticketMonthDTO = new TicketMonthDTO();
            ticketMonthDTO.Id = tbTicketMonthIDCreate.Text;
            ticketMonthDTO.ProcessDate = DateTime.Now;
            ticketMonthDTO.Digit = tbTicketMonthDigitCreate.Text;
            ticketMonthDTO.CustomerName = tbTicketMonthCustomerNameCreate.Text;
            ticketMonthDTO.Cmnd = tbTicketMonthCMNDCreate.Text;
            ticketMonthDTO.Company = tbTicketMonthCompanyCreate.Text;
            ticketMonthDTO.Email = tbTicketMonthEmailCreate.Text;
            ticketMonthDTO.Address = tbTicketMonthAddressCreate.Text;
            ticketMonthDTO.CarKind = tbTicketMonthCarKindCreate.Text;

            DataRow dataRow = ((DataRowView)cbTicketMonthPartCreate.SelectedItem).Row;
            ticketMonthDTO.IdPart = Convert.ToString(dataRow["PartID"]);
           
            ticketMonthDTO.RegistrationDate = dateTimePickerTicketMonthRegistrationDateCreate.Value;
            ticketMonthDTO.ExpirationDate = dateTimePickerTicketMonthExpirationDateCreate.Value;
            ticketMonthDTO.ChargesAmount = tbTicketMonthChargesAmountCreate.Text;
            ticketMonthDTO.Status = 0;
            ticketMonthDTO.DayUnlimit = DateTime.Now;

            TicketMonthDAO.Insert(ticketMonthDTO);
            loadTicketMonthData();
        }

        private void updateTicketMonth()
        {
            TicketMonthDTO ticketMonthDTO = new TicketMonthDTO();
            ticketMonthDTO.Identify = Convert.ToInt32(tbTicketMonthIdentifyEdit.Text);
            ticketMonthDTO.ProcessDate = DateTime.Now;
            ticketMonthDTO.Digit = tbTicketMonthDigitEdit.Text;
            ticketMonthDTO.CustomerName = tbTicketMonthCustomerNameEdit.Text;
            ticketMonthDTO.Cmnd = tbTicketMonthCMNDEdit.Text;
            ticketMonthDTO.Company = tbTicketMonthCompanyEdit.Text;
            ticketMonthDTO.Email = tbTicketMonthEmailEdit.Text;
            ticketMonthDTO.Address = tbTicketMonthAddressEdit.Text;
            ticketMonthDTO.CarKind = tbTicketMonthCarKindEdit.Text;

            DataRow dataRow = ((DataRowView)cbTicketMonthPartEdit.SelectedItem).Row;
            ticketMonthDTO.IdPart = Convert.ToString(dataRow["PartID"]);

            ticketMonthDTO.RegistrationDate = dateTimePickerTicketMonthRegistrationDateEdit.Value;
            ticketMonthDTO.ExpirationDate = dateTimePickerTicketMonthExpirationDateEdit.Value;
            ticketMonthDTO.ChargesAmount = tbTicketMonthChargesAmountEdit.Text;
            ticketMonthDTO.Status = 0;
            ticketMonthDTO.DayUnlimit = DateTime.Now;

            TicketMonthDAO.Update(ticketMonthDTO);
            loadTicketMonthData();
        }

        private void clearInputTicketMonthInfo()
        {
            tbTicketMonthIDCreate.Text = "";
            tbTicketMonthDigitCreate.Text = "";
            tbTicketMonthCustomerNameCreate.Text = "";
            tbTicketMonthCMNDCreate.Text = "";
            tbTicketMonthCompanyCreate.Text = "";
            tbTicketMonthEmailCreate.Text = "";
            tbTicketMonthAddressCreate.Text = "";
            tbTicketMonthCarKindCreate.Text = "";

            cbTicketMonthPartCreate.SelectedIndex = 0;

            dateTimePickerTicketMonthRegistrationDateCreate.Value = DateTime.Now;
            dateTimePickerTicketMonthExpirationDateCreate.Value = DateTime.Now;
            tbTicketMonthChargesAmountCreate.Text = "";
        }

        private bool checkCreateTicketMonthData()
        {
            if (string.IsNullOrWhiteSpace(tbTicketMonthIDCreate.Text))
            {
                MessageBox.Show(Constant.sMessageTicketMonthIdNullError);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbTicketMonthDigitCreate.Text))
            {
                MessageBox.Show(Constant.sMessageTicketMonthDigitNullError);
                return false;
            }
            return true;
        }

        private bool checkUpdateTicketMonthData()
        {
            if (string.IsNullOrWhiteSpace(tbTicketMonthDigitEdit.Text))
            {
                MessageBox.Show(Constant.sMessageTicketMonthDigitNullError);
                return false;
            }
            return true;
        }

        private void loadTicketMonthInfoFromDataGridViewRow(int Index)
        {
            string identify = Convert.ToString(dgvTicketMonthList.Rows[Index].Cells["TicketMonthIdentify"].Value);
            tbTicketMonthIdentifyEdit.Text = identify;
            string digit = Convert.ToString(dgvTicketMonthList.Rows[Index].Cells["Digit"].Value);
            tbTicketMonthDigitEdit.Text = digit;
            string customerName = Convert.ToString(dgvTicketMonthList.Rows[Index].Cells["CustomerName"].Value);
            tbTicketMonthCustomerNameEdit.Text = customerName;
            string cmnd = Convert.ToString(dgvTicketMonthList.Rows[Index].Cells["CMND"].Value);
            tbTicketMonthCMNDEdit.Text = cmnd;
            string company = Convert.ToString(dgvTicketMonthList.Rows[Index].Cells["Company"].Value);
            tbTicketMonthCompanyEdit.Text = company;
            string email = Convert.ToString(dgvTicketMonthList.Rows[Index].Cells["Email"].Value);
            tbTicketMonthEmailEdit.Text = email;
            string address = Convert.ToString(dgvTicketMonthList.Rows[Index].Cells["Address"].Value);
            tbTicketMonthAddressEdit.Text = address;
            string carKind = Convert.ToString(dgvTicketMonthList.Rows[Index].Cells["CarKind"].Value);
            tbTicketMonthCarKindEdit.Text = carKind;
            string chargesAmount = Convert.ToString(dgvTicketMonthList.Rows[Index].Cells["ChargesAmount"].Value);
            tbTicketMonthChargesAmountEdit.Text = chargesAmount;
            string partName = Convert.ToString(dgvTicketMonthList.Rows[Index].Cells["TicketMonthPartName"].Value);
            cbTicketMonthPartEdit.Text = partName;
            if (!string.IsNullOrEmpty(dgvTicketMonthList.Rows[Index].Cells["RegistrationDate"].Value.ToString()))
            {
                DateTime registrationDate = Convert.ToDateTime(dgvTicketMonthList.Rows[Index].Cells["RegistrationDate"].Value);
                dateTimePickerTicketMonthRegistrationDateEdit.Value = registrationDate;
            }
            if (!string.IsNullOrEmpty(dgvTicketMonthList.Rows[Index].Cells["ExpirationDate"].Value.ToString()))
            {
                DateTime expirationDate = Convert.ToDateTime(dgvTicketMonthList.Rows[Index].Cells["ExpirationDate"].Value);
                dateTimePickerTicketMonthExpirationDateEdit.Value = expirationDate;
            }
        }

        private void btnTicketMonthCreate_Click(object sender, EventArgs e)
        {
            if (checkCreateTicketMonthData())
            {
                addTicketMonth();
            }
        }

        private void btnTicketMonthEdit_Click(object sender, EventArgs e)
        {
            if (checkUpdateTicketMonthData())
            {
                updateTicketMonth();
            }
        }

        private void btnTicketMonthCancelCreate_Click(object sender, EventArgs e)
        {
            clearInputTicketMonthInfo();
        }

        private void btnTicketMonthCancelEdit_Click(object sender, EventArgs e)
        {
            int Index = dgvTicketMonthList.CurrentRow.Index;
            loadTicketMonthInfoFromDataGridViewRow(Index);
        }

        private void dgvTicketMonthList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Index = e.RowIndex;
            int Count = dgvTicketMonthList.Rows.Count;
            if (Index < Count - 1)
            {
                loadTicketMonthInfoFromDataGridViewRow(Index);
            }
        }

        /*
        Car data
        */
        private void loadCarList()
        {
            DataTable data = CarDAO.GetAllTicketDayData();
            dgvCarList.DataSource = data;
        }

        private void loadCarTicketMonthList()
        {
            DataTable data = CarDAO.GetAllTicketMonthData();
            dgvCarTicketMonthList.DataSource = data;
        }

        private void searchCar()
        {
            CarDTO carDTO = new CarDTO();
            DateTime startDate = dateTimePickerCarDateIn.Value;
            DateTime startTime = dateTimePickerCarTimeIn.Value;
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, 0);
            carDTO.TimeStart = startDate;
            DateTime endDate = dateTimePickerCarDateOut.Value;
            DateTime endTime = dateTimePickerCarTimeOut.Value;
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, 0);
            carDTO.TimeEnd = endDate;
            if (comboBoxTruyVanLoaiXe.SelectedIndex > 0)
            {
                DataRow dataRow = ((DataRowView)comboBoxTruyVanLoaiXe.SelectedItem).Row;
                carDTO.IdPart = Convert.ToString(dataRow["PartID"]);
            }
            try
            {
                carDTO.Identify = Convert.ToInt32(tbCarIdentifySearch.Text);
            } catch (Exception e)
            {

            }
            carDTO.Digit = tbCarDigitSearch.Text;
            carDTO.Id = tbCarIDSearch.Text;
            if (comboBoxNhanVienVao.SelectedIndex > 0)
            {
                DataRow dataRow = ((DataRowView) comboBoxNhanVienVao.SelectedItem).Row;
                carDTO.IdIn = Convert.ToString(dataRow["UserID"]);
            }
            if (comboBoxNhanVienRa.SelectedIndex > 0)
            {
                DataRow dataRow = ((DataRowView)comboBoxNhanVienRa.SelectedItem).Row;
                carDTO.IdOut = Convert.ToString(dataRow["UserID"]);
            }

            DataTable data = CarDAO.searchTicketDayData(carDTO);
            dgvCarList.DataSource = data;
        }

        private void searchCarTicketMonth()
        {
            CarDTO carDTO = new CarDTO();
            DateTime startDate = dateTimePickerCarTicketMonthDateIn.Value;
            DateTime startTime = dateTimePickerCarTicketMonthTimeIn.Value;
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, 0);
            carDTO.TimeStart = startDate;
            DateTime endDate = dateTimePickerCarTicketMonthDateOut.Value;
            DateTime endTime = dateTimePickerCarTicketMonthTimeOut.Value;
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, 0);
            carDTO.TimeEnd = endDate;

            TicketMonthDTO ticketMonthDTO = new TicketMonthDTO();
            ticketMonthDTO.CustomerName = tbCustomerName.Text;
            ticketMonthDTO.Company = tbCompany.Text;

            DataTable data = CarDAO.searchTicketMonthData(carDTO, ticketMonthDTO);
            dgvCarTicketMonthList.DataSource = data;
        }

        private void loadUserDataToComboBox(ComboBox cb)
        {
            DataTable dt = UserDAO.GetAllData();
            DataRow dr = dt.NewRow();
            dr["NameUser"] = "Tất cả";
            dt.Rows.InsertAt(dr, 0);
            cb.DataSource = dt;
            cb.DisplayMember = "NameUser";
            cb.ValueMember = "UserID";
        }

        private void tabQuanLyXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabQuanLyXe.SelectedTab == tabQuanLyXe.TabPages["tabPageTraCuuVaoRa"])
            {
                loadCarList();
                loadUserDataToComboBox(comboBoxNhanVienVao);
                loadUserDataToComboBox(comboBoxNhanVienRa);
                loadPartDataWithFieldAllToComboBox(comboBoxTruyVanLoaiXe);

                dateTimePickerCarTimeIn.Format = DateTimePickerFormat.Custom;
                dateTimePickerCarTimeIn.CustomFormat = "HH:mm"; // Only use hours and minutes
                dateTimePickerCarTimeIn.ShowUpDown = true;
                dateTimePickerCarTimeOut.Format = DateTimePickerFormat.Custom;
                dateTimePickerCarTimeOut.CustomFormat = "HH:mm"; // Only use hours and minutes
                dateTimePickerCarTimeOut.ShowUpDown = true;
            } else if (tabQuanLyXe.SelectedTab == tabQuanLyXe.TabPages["tabPageTraCuuVaoRaVeThang"])
            {
                loadCarTicketMonthList();

                dateTimePickerCarTicketMonthTimeIn.Format = DateTimePickerFormat.Custom;
                dateTimePickerCarTicketMonthTimeIn.CustomFormat = "HH:mm"; // Only use hours and minutes
                dateTimePickerCarTicketMonthTimeIn.ShowUpDown = true;
                dateTimePickerCarTicketMonthTimeOut.Format = DateTimePickerFormat.Custom;
                dateTimePickerCarTicketMonthTimeOut.CustomFormat = "HH:mm"; // Only use hours and minutes
                dateTimePickerCarTicketMonthTimeOut.ShowUpDown = true;
            }
        }

        private void FormQuanLy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Util.showConfirmLogoutPopup(this);
            }
        }

        private void btnSearchCar_Click(object sender, EventArgs e)
        {
            searchCar();
        }

        private void btnSearchCarTicketMonth_Click(object sender, EventArgs e)
        {
            searchCarTicketMonth();
        }

        private void dgvTicketMonthList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Xóa"));

                int currentMouseOverRow = dgvTicketMonthList.HitTest(e.X, e.Y).RowIndex;

                m.Show(dgvTicketMonthList, new Point(e.X, e.Y));

            }
        }
    }
}
