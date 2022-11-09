IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE SEQUENCE [MinhaSequencia] AS int START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE TABLE [Vouchers] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] varchar(100) NOT NULL,
    [Percentual] decimal(18,2) NULL,
    [ValorDesconto] decimal(18,2) NULL,
    [Quantidade] int NOT NULL,
    [TipoDescontoVoucher] int NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [DataUtilizacao] datetime2 NULL,
    [DataValidade] datetime2 NOT NULL,
    [Ativo] bit NOT NULL,
    [Utilizado] bit NOT NULL,
    CONSTRAINT [PK_Vouchers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pedidos] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] int NOT NULL DEFAULT (NEXT VALUE FOR MinhaSequencia),
    [ClienteId] uniqueidentifier NOT NULL,
    [VoucherId] uniqueidentifier NOT NULL,
    [VoucherUtilizado] bit NOT NULL,
    [Desconto] decimal(18,2) NOT NULL,
    [ValorTotal] decimal(18,2) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [PedidoStatus] int NOT NULL,
    CONSTRAINT [PK_Pedidos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pedidos_Vouchers_VoucherId] FOREIGN KEY ([VoucherId]) REFERENCES [Vouchers] ([Id])
);
GO

CREATE TABLE [PedidoItems] (
    [Id] uniqueidentifier NOT NULL,
    [PedidoId] uniqueidentifier NOT NULL,
    [ProdutoId] uniqueidentifier NOT NULL,
    [ProdutoNome] varchar(250) NOT NULL,
    [Quantidade] int NOT NULL,
    [ValorUnitario] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_PedidoItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PedidoItems_Pedidos_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedidos] ([Id])
);
GO

CREATE INDEX [IX_PedidoItems_PedidoId] ON [PedidoItems] ([PedidoId]);
GO

CREATE INDEX [IX_Pedidos_VoucherId] ON [Pedidos] ([VoucherId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221109170239_Teste', N'6.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Pedidos]') AND [c].[name] = N'VoucherId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Pedidos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Pedidos] ALTER COLUMN [VoucherId] uniqueidentifier NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221109180113_PedidoVoucher', N'6.0.4');
GO

COMMIT;
GO

