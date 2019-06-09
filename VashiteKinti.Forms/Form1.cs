using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastMember;
using VashiteKinti.Data;
using VashiteKinti.Data.Models;
using VashiteKinti.Services;

namespace VashiteKinti.Forms
{
    public partial class Form1 : Form
    {
        private readonly IGenericDataService<Deposit> _deposits;
        private readonly IGenericDataService<Bank> _banks;

        public Form1(IGenericDataService<Deposit> deposits, IGenericDataService<Bank> banks)
        {
            _deposits = deposits;
            _banks    = banks;

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = await getDepositsDT();

            MessageBox.Show("Database logs successfuly retrieved.");
        }

        private async Task<DataTable> getDepositsDT()
        {
            DataTable dtDeposits = new DataTable();
            IEnumerable<Deposit> data = await _deposits.GetAllAsync();

            using (var reader = ObjectReader.Create(data))
            {
                dtDeposits.Load(reader);
            }

            var testB = data.First().Bank;
            var testB2 = _banks.GetSingleOrDefault(x => x.Id == data.First().BankId);
            var testB3 = (await _banks.GetAllAsync()).ToList();

            return dtDeposits;
        }
    }
}
