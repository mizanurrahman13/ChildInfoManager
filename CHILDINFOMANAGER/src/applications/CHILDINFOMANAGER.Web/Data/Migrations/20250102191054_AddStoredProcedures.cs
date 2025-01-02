using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CHILDINFOMANAGER.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE PROCEDURE SaveChildren
@Id NVARCHAR(40),
@FirstName NVARCHAR(250),
@LastName NVARCHAR(250),
@DateOfBirth DateTime,
@PhoneNumber NVARCHAR(20),
@HomeAddress NVARCHAR(250)
AS
BEGIN
    DECLARE @ExistingId NVARCHAR(40), @Message NVARCHAR(250);
    SET @ExistingId = (SELECT Id
                       FROM [ChildInfoDB].[dbo].[Childrens]
                       WHERE Id = @Id);

    IF(@ExistingId IS NULL)
    BEGIN
        INSERT INTO Childrens (Id, FirstName, LastName, DateOfBirth, PhoneNumber, HomeAddress)
        VALUES (@Id, @FirstName, @LastName, @DateOfBirth, @PhoneNumber, @HomeAddress);

        SET @Message = 'Children Added Successfully';
        SELECT @Message AS [Message];
    END
    ELSE
    BEGIN
        SET @Message = 'Children already exist with this ID: ' + CAST(@Id AS NVARCHAR(50));
        SELECT @Message AS [Message];
    END
END";
            migrationBuilder.Sql(sql);

            var createGetAllChildrenSP = @"
                            CREATE PROCEDURE GetAllChildren AS BEGIN SELECT * FROM Childrens; END"; 
            migrationBuilder.Sql(createGetAllChildrenSP);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            var dropSaveChildrenSP = "DROP PROCEDURE SaveChildren"; 
            migrationBuilder.Sql(dropSaveChildrenSP); 
            var dropGetAllChildrenSP = "DROP PROCEDURE GetAllChildren"; 
            migrationBuilder.Sql(dropGetAllChildrenSP); 
        }
    }
}
