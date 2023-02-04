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

CREATE TABLE [Pagamentos] (
    [Id] uniqueidentifier NOT NULL,
    [PedidoId] uniqueidentifier NOT NULL,
    [Status] varchar(100) NOT NULL,
    [Valor] decimal(18,2) NOT NULL,
    [NomeCartao] varchar(100) NOT NULL,
    [NumeroCartao] varchar(100) NOT NULL,
    [ExpiracaoCartao] varchar(100) NOT NULL,
    [CvvCartao] varchar(100) NOT NULL,
    CONSTRAINT [PK_Pagamentos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Transacoes] (
    [Id] uniqueidentifier NOT NULL,
    [PedidoId] uniqueidentifier NOT NULL,
    [PagamentoId] uniqueidentifier NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    [StatusTransacao] int NOT NULL,
    CONSTRAINT [PK_Transacoes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transacoes_Pagamentos_PagamentoId] FOREIGN KEY ([PagamentoId]) REFERENCES [Pagamentos] ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Transacoes_PagamentoId] ON [Transacoes] ([PagamentoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230204141618_DataBase', N'6.0.10');
GO

COMMIT;
GO

