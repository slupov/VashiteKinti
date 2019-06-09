using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastMember;
using Microsoft.EntityFrameworkCore;
using VashiteKinti.Data;
using VashiteKinti.Data.Enums;
using VashiteKinti.Data.Import.DbExtensions;
using VashiteKinti.Data.Models;
using VashiteKinti.Forms.Extensions;
using VashiteKinti.Services;

namespace VashiteKinti.Forms
{
    public partial class Form1 : Form
    {
        private readonly IGenericDataService<Deposit> _deposits;
        private readonly IGenericDataService<Bank> _banks;

        private readonly string INIT_SELECTED_DEPOSIT_NAME = "--Изберете име на депозит--";
        private readonly string INIT_SELECTED_BANK_NAME    = "--Изберете Банка--";

        private Deposit _lastClickedDeposit;

        public Form1(IGenericDataService<Deposit> deposits, IGenericDataService<Bank> banks, VashiteKintiDbContext db)
        {
            db.Database.Migrate();
            db.EnsureSeedData();

            _deposits = deposits;
            _banks    = banks;

            _lastClickedDeposit = new Deposit();;

            Init();
            InitializeComponent();
        }

        private void ResetFields()
        {
            //reset Bank combo box field
            this.bankComboBox.SelectedItem = null;
            bankComboBox.SelectedText = INIT_SELECTED_BANK_NAME;

            //reset deposit name field
            this.depositNameTextBox.SelectedText = INIT_SELECTED_DEPOSIT_NAME;
        }

        private async void Init()
        {
            var banks = await _banks.GetAllAsync();

            //initialize Bank combo box field
            this.bankComboBox.Items.AddRange(banks.ToArray());

            //initialize deposit interest type field
            interestTypeComboBox.DataSource = Enum.GetValues(typeof(InterestPaymentMethod));

            //initialize currency field
            currencyComboBox.DataSource = Enum.GetValues(typeof(Currency));

            ResetFields();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            ReloadDataGridViewData();
            MessageBox.Show("Database logs successfuly retrieved.");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This should open the app's website (if hosted) in the browser.");
        }

        private async Task<DataTable> getDepositsDT()
        {
            DataTable dtDeposits = new DataTable();
            IEnumerable<Deposit> data = await _deposits.GetAllAsync();

            using (var reader = ObjectReader.Create(data))
            {
                dtDeposits.Load(reader);
            }

            dtDeposits.SetColumnsOrder(nameof(Deposit.Bank), nameof(Deposit.Name), nameof(Deposit.MinAmount),
                nameof(Deposit.Interest), nameof(Deposit.PaymentMethod), nameof(Deposit.Currency));

            return dtDeposits;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Deposit newDeposit;

            if (IsConstructedObjectValid(out newDeposit))
            {
                bool isAdded = true;

                try
                {
                    _deposits.Add(newDeposit);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Failure! " + exception.Message);
                    isAdded = false;
                }

                if (isAdded)
                {

                    MessageBox.Show(string.Format(
                        "Successfully created a deposit with name {0} for " +
                        "bank {1}", newDeposit.Name, newDeposit.Bank.Name));

                    ResetFields();
                    ReloadDataGridViewData();
                }
            }
        }

        private bool IsConstructedObjectValid(out Deposit newDeposit)
        {
            bool isObjectValid = true;
            newDeposit = new Deposit();

            Bank selectedBank = (Bank)bankComboBox.SelectedItem;
            var selectedDepositName = depositNameTextBox.Text;
            var minSum = (int)minSumNumUpDown.Value;
            var interest = interestNumUpDown.Value;
            var interestPayment = (InterestPaymentMethod)interestTypeComboBox.SelectedValue;
            var currency = (Currency)currencyComboBox.SelectedValue;

            if (isObjectValid)
            {
                if (null == selectedBank)
                {
                    MessageBox.Show("You must selected a valid Bank first.");
                    isObjectValid = false;
                }
            }

            if (isObjectValid)
            {
                newDeposit.Bank = (Bank)selectedBank;

                if (selectedDepositName.Equals(INIT_SELECTED_DEPOSIT_NAME) ||
                    string.IsNullOrEmpty(selectedDepositName))
                {
                    MessageBox.Show("You must selected a valid Bank first.");
                    isObjectValid = false;
                }
            }

            if (isObjectValid)
            {
                newDeposit.Name = selectedDepositName;

                newDeposit.MinAmount = minSum;
                newDeposit.Interest = (double)interest;
                newDeposit.PaymentMethod = interestPayment;
                newDeposit.Currency = currency;     
            }

            return isObjectValid;
        }

        private void depositNameTextBox_Clicked(object sender, EventArgs e)
        {
            if (depositNameTextBox.Text.Equals(INIT_SELECTED_DEPOSIT_NAME) ||
                string.IsNullOrEmpty(depositNameTextBox.Text))
            {
                depositNameTextBox.Clear();
            }
        }

        private void depositNameTextBox_LostFocus(object sender, EventArgs e)
        {
            if (depositNameTextBox.Text.Equals(INIT_SELECTED_DEPOSIT_NAME) ||
                string.IsNullOrEmpty(depositNameTextBox.Text))
            {
                depositNameTextBox.Clear();
                depositNameTextBox.SelectedText = INIT_SELECTED_DEPOSIT_NAME;
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //change left sidebar data to match row data
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                bankComboBox.SelectedItem         = row.Cells[nameof(Deposit.Bank)].Value;
                depositNameTextBox.Text           = row.Cells[nameof(Deposit.Name)].Value.ToString();
                minSumNumUpDown.Value             = decimal.Parse(row.Cells[nameof(Deposit.MinAmount)].Value.ToString());
                interestNumUpDown.Value           = decimal.Parse(row.Cells[nameof(Deposit.Interest)].Value.ToString());
                interestTypeComboBox.SelectedItem = row.Cells[nameof(Deposit.PaymentMethod)].Value;
                currencyComboBox.SelectedItem     = row.Cells[nameof(Deposit.Currency)].Value;

                _lastClickedDeposit.Bank          = (Bank)row.Cells[nameof(Deposit.Bank)].Value;
                _lastClickedDeposit.Name          = row.Cells[nameof(Deposit.Name)].Value.ToString();
                _lastClickedDeposit.MinAmount     = double.Parse(row.Cells[nameof(Deposit.MinAmount)].Value.ToString());
                _lastClickedDeposit.Interest      = double.Parse(row.Cells[nameof(Deposit.Interest)].Value.ToString());
                _lastClickedDeposit.PaymentMethod = (InterestPaymentMethod)row.Cells[nameof(Deposit.PaymentMethod)].Value;
                _lastClickedDeposit.Currency      = (Currency)row.Cells[nameof(Deposit.Currency)].Value;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //reset and lock deposit name and Bank fields -> they must not be modified
            bankComboBox.SelectedItem = _lastClickedDeposit.Bank;
            depositNameTextBox.Text   = _lastClickedDeposit.Name;

            Deposit newDeposit;

            if (IsConstructedObjectValid(out newDeposit))
            {
                try
                {
                    Deposit trackedDeposit = _deposits.GetSingleOrDefault(x => x.Bank == _lastClickedDeposit.Bank &&
                                                                               x.Name.Equals(_lastClickedDeposit.Name));

                    if (null == trackedDeposit)
                    {
                        MessageBox.Show(string.Format("Error 404! No deposit with Bank = {0} and name {1} found !!!",
                            _lastClickedDeposit.Bank.Name, _lastClickedDeposit.Name));

                        return;
                    }

                    //copy new values into the tracked deposit
                    trackedDeposit.Bank          = newDeposit.Bank;
                    trackedDeposit.Name          = newDeposit.Name;
                    trackedDeposit.MinAmount     = newDeposit.MinAmount;
                    trackedDeposit.Interest      = newDeposit.Interest;
                    trackedDeposit.PaymentMethod = newDeposit.PaymentMethod;
                    trackedDeposit.Currency      = newDeposit.Currency;

                    //TODO STOYAN LUPOV: Catch exception "Cannot update identity column Id" ... 
                    _deposits.Update(trackedDeposit);

                    MessageBox.Show(string.Format(
                        "Successfully edited a deposit with name {0} for " +
                        "bank {1}", newDeposit.Name, newDeposit.Bank.Name));

                    ResetFields();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Failure! " + exception.Message);
                    throw;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //----------------- START USER ARE U SURE PROMPT ----------------- 

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            string message = string.Format("Are you sure you want to delete deposit with Bank = {0} and name {1}", 
                _lastClickedDeposit.Bank.Name, _lastClickedDeposit.Name);

            string caption = "Error Detected in Input";

            // Displays the MessageBox.
            var result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //----------------- END USER ARE U SURE PROMPT ----------------- 
            //delete last clicked entity
            var depositTracked = _deposits.GetSingleOrDefault(x => x.Name.Equals(_lastClickedDeposit.Name) &&
                                                                   x.Bank == _lastClickedDeposit.Bank);

            if (null == depositTracked)
            {
                MessageBox.Show(string.Format("Error 404! No deposit with Bank = {0} and name {1} found !!!",
                    _lastClickedDeposit.Bank.Name, _lastClickedDeposit.Name));

                return;;
            }

            bool isRemoved = true;

            try
            {
                _deposits.Remove(depositTracked);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failure! " + exception.Message);
                isRemoved = false;
            }

            if (isRemoved)
            {
                MessageBox.Show(string.Format("Successfully removed deposit with name {0} and bank {1}",
                    depositTracked.Name, depositTracked.Bank.Name));

                ReloadDataGridViewData();
            }
        }

        private async void ReloadDataGridViewData()
        {
            dataGridView1.DataSource = await getDepositsDT();
        }
    }
}
