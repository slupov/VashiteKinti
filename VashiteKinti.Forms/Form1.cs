using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastMember;
using VashiteKinti.Data.Enums;
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

        public Form1(IGenericDataService<Deposit> deposits, IGenericDataService<Bank> banks)
        {
            _deposits = deposits;
            _banks    = banks;

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
            dataGridView1.DataSource = await getDepositsDT();     
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
                MessageBox.Show(string.Format(
                    "Successfully created a deposit with name {0} for " +
                    "bank {1}", newDeposit.Name, newDeposit.Bank.Name));

                ResetFields();
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

                try
                {
                    _deposits.Add(newDeposit);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Failure! " + exception.Message);
                    isObjectValid = false;
                }
            }

            return isObjectValid;
        }

        private void depositNameTextBox_Clicked(object sender, EventArgs e)
        {
            depositNameTextBox.Clear();
        }

        private void depositNameTextBox_MouseLeave(object sender, EventArgs e)
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

                bankComboBox.SelectedItem = row.Cells[nameof(Deposit.Bank)].Value;
                depositNameTextBox.Text = row.Cells[nameof(Deposit.Name)].Value.ToString();
                minSumNumUpDown.Value = decimal.Parse(row.Cells[nameof(Deposit.MinAmount)].Value.ToString());
                interestNumUpDown.Value = decimal.Parse(row.Cells[nameof(Deposit.Interest)].Value.ToString());
                interestTypeComboBox.SelectedItem = row.Cells[nameof(Deposit.PaymentMethod)].Value;
                currencyComboBox.SelectedItem = row.Cells[nameof(Deposit.Currency)].Value;
            }
        }
    }
}
