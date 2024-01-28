using System;

namespace MigrationAndScaffold
{
    //Migration
    // migration: code => capnhat len database
    // migration: là 1 cái snapshot - ảnh chụp trong 1 thời điểm của database
    // dotnet ef migrations add <MigrationName> : Tao mot migration
    // dotnet ef migrations list : xem list migration
    // dotnet ef migrations remove: xoa 1 phien ban migration
    // dotnet ef database update <MigrationsName>: cap nhat migration len database, hoac quay lai phien ban cu
    // dotnet ef database drop -f

    // dotnet ef migrations script <Name 1> <Name 2>: hien thi tat ca cau lenh sql (tu Name1 sang Name2)
    // dotnet ef migrations script -o migrations.sql: Luu cac cau lenh tu migration 0 -> cuoi

  


    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
