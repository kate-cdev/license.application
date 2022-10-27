using Microsoft.EntityFrameworkCore.Migrations;

namespace license.application.Data.Migrations
{
    public partial class create_admin_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'08020e81-ff8e-471c-a460-769353ab2a0c', N'admin@ls.com', N'ADMIN@LS.COM', N'admin@ls.com', N'ADMIN@LS.COM', 0, N'AQAAAAEAACcQAAAAELBa6L0EygX/WDs4t/tl0bCdBjXem0Rfq+imAWjYOa7AwHqqhA83TOvsk8uSXIrObA==', N'K2MFG6MBNP35NIDGS7IMXY4TLQOCUZUX', N'4c43bcdb-37fe-46ef-8d2f-3ccdb557fbc2', NULL, 0, 0, NULL, 1, 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
