using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncStreamForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void syncBut_Click(object sender, EventArgs e)
        {
            SyncToDo();

        }
        async void SyncToDo()
        {
            var orders = await GetOrdersAsync();
            dataGrid.DataSource = orders;
            this.Text = dataGrid.Rows.Count.ToString();
        }

        private void asyncBut_Click(object sender, EventArgs e)
        {
            AsyncToDo();
            this.Text = dataGrid.Rows.Count.ToString();
        }
        async void AsyncToDo()
        {         
            var table = new DataTable();
            table.Columns.Add("SalesOrderID", typeof(int));
            table.Columns.Add("CarrierTrackingNumber", typeof(string));
            table.Columns.Add("OrderQty", typeof(short));
            table.Columns.Add("ProductID", typeof(int));
            table.Columns.Add("SpecialOfferID", typeof(int));
            table.Columns.Add("UnitPrice", typeof(decimal));
            table.Columns.Add("UnitPriceDiscount", typeof(decimal));
            table.Columns.Add("rowguid", typeof(Guid));
            table.Columns.Add("ModifiedDate", typeof(DateTime));
            dataGrid.DataSource = table;
            await foreach (var order in EnumerateOrderAsync())
            {
                table.Rows.Add(order.SalesOrderID,order.CarrierTrackingNumber,order.OrderQty,order.ProductID,order.SpecialOfferID,order.UnitPrice,order.UnitPriceDiscount,order.rowguid,order.ModifiedDate);
            }        
        }
        public async Task<List<SalesOrderDetail>> GetOrdersAsync()
        {
            var orders = new List<SalesOrderDetail>();
            var offset = 0;
            while (true)
            {
                var list = await QueryOrdersAsync(offset);
                orders.AddRange(list);
                offset++;
                if (list.Count < 200)
                {
                    break;
                }
            }
            return orders;
        }

        public async IAsyncEnumerable<SalesOrderDetail> EnumerateOrderAsync()
        {
            var offset = 0;
            while (true)
            {
                var list = (await QueryOrdersAsync(offset)).ToList();
                foreach (var item in list)
                {
                    yield return item;
                }
                offset++;
                if (list.Count < 200)
                {
                    break;
                }
            }

        }
        public async Task<List<SalesOrderDetail>> QueryOrdersAsync(int offset)
        {
            using var con = new SqlConnection("server=.;database=AdventureWorks2016;uid=sa;pwd=sa;");
            var sql = @$"select * from Sales.SalesOrderDetail order by SalesOrderID,SalesOrderDetailID  offset {offset * 200} row fetch next 200 row only";
            //假如这里醒询有延时
            // await Task.Delay(100);
            return (await con.QueryAsync<SalesOrderDetail>(sql)).ToList();
        }
    }
    public class SalesOrderDetail
    {
        public int SalesOrderID { get; set; }
        public string CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public int ProductID { get; set; }
        public int SpecialOfferID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);

        }

    }
}
