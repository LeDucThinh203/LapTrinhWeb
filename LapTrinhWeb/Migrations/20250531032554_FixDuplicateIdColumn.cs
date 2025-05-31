using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LapTrinhWeb.Migrations
{
    /// <inheritdoc />
    public partial class FixDuplicateIdColumn : Migration
    {        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Tạo bảng tạm thời để lưu trữ dữ liệu
            migrationBuilder.Sql(@"
                SELECT 
                    ProductId,
                    Name,
                    Price,
                    CategoryId,
                    Image,
                    Description,
                    IsFeatured
                INTO #TempProducts
                FROM Products;
            ");

            // 2. Xóa các ràng buộc khóa ngoại
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_OrderDetails_Products_ProductId') AND parent_object_id = OBJECT_ID(N'OrderDetails'))
                BEGIN
                    ALTER TABLE OrderDetails DROP CONSTRAINT FK_OrderDetails_Products_ProductId;
                END
            ");

            // 3. Xóa bảng Products hiện tại
            migrationBuilder.Sql("DROP TABLE Products;");

            // 4. Tạo lại bảng Products với ProductId là khóa chính và identity
            migrationBuilder.Sql(@"
                CREATE TABLE Products (
                    ProductId INT IDENTITY(1,1) PRIMARY KEY,
                    Name NVARCHAR(MAX) NOT NULL,
                    Price DECIMAL(18,2) NOT NULL,
                    CategoryId INT NOT NULL,
                    Image NVARCHAR(MAX) NOT NULL,
                    Description NVARCHAR(MAX) NOT NULL,
                    IsFeatured BIT NOT NULL
                );
            ");            // 5. Chèn lại dữ liệu từ bảng tạm thời
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Products ON;
                INSERT INTO Products (ProductId, Name, Price, CategoryId, Image, Description, IsFeatured)
                SELECT ProductId, Name, Price, CategoryId, Image, Description, IsFeatured
                FROM #TempProducts;
                SET IDENTITY_INSERT Products OFF;

                DROP TABLE #TempProducts;
            ");

            // 6. Tạo lại khóa ngoại
            migrationBuilder.Sql(@"
                ALTER TABLE OrderDetails
                ADD CONSTRAINT FK_OrderDetails_Products_ProductId
                FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
                ON DELETE CASCADE;
            ");
        }        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Không hỗ trợ quay lại vì đây là migration thủ công
            migrationBuilder.Sql("-- Không hỗ trợ quay lại");
        }
    }
}
