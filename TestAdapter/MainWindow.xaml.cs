using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestAdapter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable table = new DataTable("NhanVien");
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataSet dataSet = new DataSet();
        public MainWindow()
        {
            InitAdapter();
            InitializeComponent();
        }

        void InitAdapter()
        {
            var sqlStringBuilder = new SqlConnectionStringBuilder();
            sqlStringBuilder["Server"] = "localhost, 1433";
            sqlStringBuilder["Database"] = "xtlab";
            sqlStringBuilder["UID"] = "sa";
            sqlStringBuilder["PWD"] = "123456aA@";

            var sqlStringConnection = sqlStringBuilder.ToString();

            connection = new SqlConnection(sqlStringConnection);

            connection.Open();

            adapter = new SqlDataAdapter();

            adapter.TableMappings.Add("Table", "NhanVien"); // lay du lieu tu bang nhan vien

            adapter.SelectCommand = new SqlCommand("select NhanviennID, Ten, Ho from NhanVien", connection);

            //Thiet lap cho Insert Command
            adapter.InsertCommand = new SqlCommand("insert into NhanVien(Ho, Ten) values(@Ho, @Ten)", connection);
            adapter.InsertCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 255, "Ho");
            adapter.InsertCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 255, "Ten");
            //Thiet lap cho Delete Command

            adapter.DeleteCommand = new SqlCommand("Delete from NhanVien where NhanviennID = @NhanvienId", connection);
            var pr1 = adapter.DeleteCommand.Parameters.Add(new SqlParameter("@NhanvienId", SqlDbType.Int));
            pr1.SourceColumn = "NhanviennID"; // Nguon lay du lieu tu cot nao
            pr1.SourceVersion = DataRowVersion.Original; // Lay phien ban nao cua du lieu cap nhat

            adapter.UpdateCommand = new SqlCommand("Update NhanVien set Ho = @Ho, Ten = @Ten where NhanviennID = @NhanvienId", connection);
            var pr2 = adapter.UpdateCommand.Parameters.Add(new SqlParameter("@NhanvienId", SqlDbType.Int));
            adapter.UpdateCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 255, "Ho");
            adapter.UpdateCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 255, "Ten");

            pr2.SourceColumn = "NhanviennID"; // Nguon lay du lieu tu cot nao
            pr2.SourceVersion = DataRowVersion.Original;

            dataSet.Tables.Add(table);
            connection.Close();

        }

        void LoadDataTable()
        {
            table.Rows.Clear();
            adapter.Fill(dataSet);
            datagrid.DataContext = table.DefaultView;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataTable();

        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadDataTable();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            adapter.Update(dataSet);
            LoadDataTable();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedItem = (DataRowView)datagrid.SelectedItem;
            if (selectedItem != null)
            {
                selectedItem.Delete();
                adapter.Update(dataSet);
                LoadDataTable();
            }
        }
    }
}
