using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VashiteKinti.Data;
using VashiteKinti.Data.Models;
using VashiteKinti.Services;

namespace VashiteKinti.Forms
{
    public partial class Form1 : Form
    {
        private readonly IGenericDataService<Deposit> _deposits;

        public Form1(IGenericDataService<Deposit> deposits)
        {
            _deposits = deposits;

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var res = await _deposits.GetAllAsync();

            MessageBox.Show("IBASI BA4KA!");
        }
    }
}
