Create Function [dbo].[RemoveNonAlphaCharacters](@Temp VarChar(1000))
Returns VarChar(1000)
AS
Begin

    Declare @KeepValues as varchar(50)
    Set @KeepValues = '%[^a-z]%'
    While PatIndex(@KeepValues, @Temp) > 0
        Set @Temp = Stuff(@Temp, PatIndex(@KeepValues, @Temp), 1, '')

    Return @Temp
End;
GO

delete from PendidikanFormal
delete from SKPengangkatan
delete from PermissionPolicyUserUsers_PermissionPolicyRoleRoles
delete from Pegawai
delete from PermissionPolicyUser

--Excel 2007-2010
SELECT newid() As oid, * INTO #tmp
FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0',
    'Excel 12.0 Xml;HDR=YES;Database=e:\data\nonpns.xlsx',
    'SELECT * FROM [clean$]');


--select * from PermissionPolicyUser
insert into PermissionPolicyUser
select a.oid as Oid, null as StoredPassword, 0 as ChangePasswordOnFirstLogon, dbo.RemoveNonAlphaCharacters(a.Nama) as UserName, 0 as IsActive, null as OptimisticLockField, null as GCRecord, 15 as ObjectType from #tmp a


insert into Pegawai
select a.Oid, null as NomorInduk, null as Foto, a.Nama as NamaLengkap, 0 as StatusKepagawaian, a.TempatLahir, a.TanggalLahir, null as Email, null as Handphone, null as Telepon, null as Alamat, null as Propinsi, null as Kabupaten, null as Kecamatan, null as AlamatTinggal, null as PropinsiTinggal, null as KabupatenTinggal, null as KecamatanTinggal, 0 as StatusPernikahan, a.JenisKelamin as JenisKelamin, 0 as Agama, 0 as GolonganDarah, null as NomorKTP, null as NomorNPWP,null as KTP, null as NPWP, (select top 1 x.Oid from jabatan x where x.NamaJabatan = a.Jabatan) as Jabatan, (select top 1 x.Oid from UnitKerja x where x.NamaUnit= a.Unit) as UnitKerja, getdate() as TanggalPendaftaranSistem from #tmp a;
--drop table #tmp



--select * from PermissionPolicyUserUsers_PermissionPolicyRoleRoles
declare @Oid uniqueidentifier;
select @Oid = Oid from PermissionPolicyRole where [Name] = 'Default'
select @Oid as Roles, a.Oid as Users, newid() as OID, null as OptimisticLockField from #tmp a


select * from Pegawai
--insert into Pegawai
select a.Oid, null as NomorInduk, null as Foto, a.Nama as NamaLengkap, 0 as StatusKepagawaian, a.TempatLahir, a.TanggalLahir, null as Email, null as Handphone, null as Telepon, null as Alamat, null as Propinsi, null as Kabupaten, null as Kecamatan, null as AlamatTinggal, null as KabupatenTinggal, null as KecamatanTinggal, 0 as StatusPernikahan, 0 as JenisKelamin, 0 as Agama, 0 as GolonganDarah, null as NomorKTP, null as NomorNPWP, getdate() as TanggalPendaftaranSistem,null as NPWP, null as Jabatan from #tmp a

--select * from PendidikanFormal
insert into PendidikanFormal
select newid() as Oid, a.Oid as Pegawai, a.TahunLulus, (select top 1 z.Oid from Jurusan z where NamaJurusan = a.Jurusan) as Jurusan, a.JenjangPendidikan, 0 as IPK, null as Ijazah, null as Transkrip, null as TranskripNilai, null as OptimisticLockField, null as GCRecord from #tmp a where a.JenjangPendidikan is not null

--select * from PermissionPolicyUserUsers_PermissionPolicyRoleRoles
select * from SKPengangkatan
insert into SKPengangkatan
select newid() as Oid, a.SKAwal as NOSK, a.Oid as Pegawai, a.TahunSKAwal as Tahun, null as SuratSK, null as OptimisticLockField, null as GCRecord from #tmp a where a.SKawal is not null
insert into SKPengangkatan
select newid() as Oid, a.SkAkhir as NOSK, a.Oid as Pegawai, a.TahunSKAkhir as Tahun, null as SuratSK, null as OptimisticLockField, null as GCRecord from #tmp a where a.SKakhir is not null
	drop table #tmp

drop function [dbo].[RemoveNonAlphaCharacters]