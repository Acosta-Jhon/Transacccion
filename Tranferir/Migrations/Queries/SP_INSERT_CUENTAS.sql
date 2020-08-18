USE [DbTrasancciones]
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_CUENTAS]    Script Date: 17/8/2020 21:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SP_INSERT_CUENTAS]
@nombre varchar(max),
@dinero real,
@apellido nvarchar(max),
@cedula nvarchar(max),
@digitos int
as
Begin 
	insert into Cuentas values (@nombre,@dinero,@apellido,@cedula,@digitos)
End
