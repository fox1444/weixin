﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


namespace QJY.Data
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class QJY_SaaSEntities : DbContext
{
    public QJY_SaaSEntities()
        : base("name=QJY_SaaSEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<FT_File> FT_File { get; set; }

    public virtual DbSet<FT_File_Downhistory> FT_File_Downhistory { get; set; }

    public virtual DbSet<FT_File_Share> FT_File_Share { get; set; }

    public virtual DbSet<FT_File_UserAuth> FT_File_UserAuth { get; set; }

    public virtual DbSet<FT_File_UserTag> FT_File_UserTag { get; set; }

    public virtual DbSet<FT_File_Vesion> FT_File_Vesion { get; set; }

    public virtual DbSet<FT_Folder> FT_Folder { get; set; }

    public virtual DbSet<JH_Auth_Branch> JH_Auth_Branch { get; set; }

    public virtual DbSet<JH_Auth_Collect> JH_Auth_Collect { get; set; }

    public virtual DbSet<JH_Auth_Common> JH_Auth_Common { get; set; }

    public virtual DbSet<JH_Auth_ExtendData> JH_Auth_ExtendData { get; set; }

    public virtual DbSet<JH_Auth_ExtendMode> JH_Auth_ExtendMode { get; set; }

    public virtual DbSet<JH_Auth_GUANZHU> JH_Auth_GUANZHU { get; set; }

    public virtual DbSet<JH_Auth_Log> JH_Auth_Log { get; set; }

    public virtual DbSet<JH_Auth_Model> JH_Auth_Model { get; set; }

    public virtual DbSet<JH_Auth_QY> JH_Auth_QY { get; set; }

    public virtual DbSet<JH_Auth_QY_Model> JH_Auth_QY_Model { get; set; }

    public virtual DbSet<JH_Auth_QY_WXSC> JH_Auth_QY_WXSC { get; set; }

    public virtual DbSet<JH_Auth_Role> JH_Auth_Role { get; set; }

    public virtual DbSet<JH_Auth_RoleAuth> JH_Auth_RoleAuth { get; set; }

    public virtual DbSet<JH_Auth_RoleFun> JH_Auth_RoleFun { get; set; }

    public virtual DbSet<JH_Auth_TL> JH_Auth_TL { get; set; }

    public virtual DbSet<JH_Auth_User> JH_Auth_User { get; set; }

    public virtual DbSet<JH_Auth_User_Center> JH_Auth_User_Center { get; set; }

    public virtual DbSet<JH_Auth_UserCustomData> JH_Auth_UserCustomData { get; set; }

    public virtual DbSet<JH_Auth_UserRole> JH_Auth_UserRole { get; set; }

    public virtual DbSet<JH_Auth_Version> JH_Auth_Version { get; set; }

    public virtual DbSet<JH_Auth_WXMSG> JH_Auth_WXMSG { get; set; }

    public virtual DbSet<JH_Auth_WXPJ> JH_Auth_WXPJ { get; set; }

    public virtual DbSet<JH_Auth_XTDT> JH_Auth_XTDT { get; set; }

    public virtual DbSet<JH_Auth_YYLog> JH_Auth_YYLog { get; set; }

    public virtual DbSet<JH_Auth_ZiDian> JH_Auth_ZiDian { get; set; }

    public virtual DbSet<SZHL_CCXJ> SZHL_CCXJ { get; set; }

    public virtual DbSet<SZHL_DXGL> SZHL_DXGL { get; set; }

    public virtual DbSet<SZHL_GZBG> SZHL_GZBG { get; set; }

    public virtual DbSet<SZHL_GZGL> SZHL_GZGL { get; set; }

    public virtual DbSet<SZHL_GZGL_FL> SZHL_GZGL_FL { get; set; }

    public virtual DbSet<SZHL_GZGL_JCSZ> SZHL_GZGL_JCSZ { get; set; }

    public virtual DbSet<SZHL_GZGL_WXYJ> SZHL_GZGL_WXYJ { get; set; }

    public virtual DbSet<SZHL_JFBX> SZHL_JFBX { get; set; }

    public virtual DbSet<SZHL_JFBXITEM> SZHL_JFBXITEM { get; set; }

    public virtual DbSet<SZHL_KQBC> SZHL_KQBC { get; set; }

    public virtual DbSet<SZHL_KQJL> SZHL_KQJL { get; set; }

    public virtual DbSet<SZHL_LCSP> SZHL_LCSP { get; set; }

    public virtual DbSet<SZHL_NOTE> SZHL_NOTE { get; set; }

    public virtual DbSet<SZHL_RWGL> SZHL_RWGL { get; set; }

    public virtual DbSet<SZHL_RWGL_ITEM> SZHL_RWGL_ITEM { get; set; }

    public virtual DbSet<SZHL_TSSQ> SZHL_TSSQ { get; set; }

    public virtual DbSet<SZHL_TXL> SZHL_TXL { get; set; }

    public virtual DbSet<SZHL_TXSX> SZHL_TXSX { get; set; }

    public virtual DbSet<SZHL_WQQD> SZHL_WQQD { get; set; }

    public virtual DbSet<SZHL_XMGL> SZHL_XMGL { get; set; }

    public virtual DbSet<SZHL_XXFB> SZHL_XXFB { get; set; }

    public virtual DbSet<SZHL_XXFB_ITEM> SZHL_XXFB_ITEM { get; set; }

    public virtual DbSet<SZHL_XXFB_SCK> SZHL_XXFB_SCK { get; set; }

    public virtual DbSet<SZHL_XXFBType> SZHL_XXFBType { get; set; }

    public virtual DbSet<SZHL_XZ_GZD> SZHL_XZ_GZD { get; set; }

    public virtual DbSet<SZHL_XZ_JL> SZHL_XZ_JL { get; set; }

    public virtual DbSet<Yan_WF_ChiTask> Yan_WF_ChiTask { get; set; }

    public virtual DbSet<Yan_WF_DaiLi> Yan_WF_DaiLi { get; set; }

    public virtual DbSet<Yan_WF_PI> Yan_WF_PI { get; set; }

    public virtual DbSet<Yan_WF_TD> Yan_WF_TD { get; set; }

    public virtual DbSet<Yan_WF_TI> Yan_WF_TI { get; set; }

    public virtual DbSet<SZHL_DBGL> SZHL_DBGL { get; set; }

    public virtual DbSet<Yan_WF_PD> Yan_WF_PD { get; set; }

    public virtual DbSet<JH_Auth_Function> JH_Auth_Function { get; set; }

    public virtual DbSet<SZHL_CRM_CONTACT> SZHL_CRM_CONTACT { get; set; }

    public virtual DbSet<SZHL_CRM_CPGL> SZHL_CRM_CPGL { get; set; }

    public virtual DbSet<SZHL_CRM_GJJL> SZHL_CRM_GJJL { get; set; }

    public virtual DbSet<SZHL_CRM_HTGL> SZHL_CRM_HTGL { get; set; }

    public virtual DbSet<SZHL_CRM_KHGL> SZHL_CRM_KHGL { get; set; }

    public virtual DbSet<SZHL_HYGL> SZHL_HYGL { get; set; }

    public virtual DbSet<SZHL_HYGL_QD> SZHL_HYGL_QD { get; set; }

    public virtual DbSet<SZHL_HYGL_QR> SZHL_HYGL_QR { get; set; }

    public virtual DbSet<SZHL_HYGL_ROOM> SZHL_HYGL_ROOM { get; set; }

    public virtual DbSet<SZHL_YCGL> SZHL_YCGL { get; set; }

    public virtual DbSet<SZHL_YCGL_CAR> SZHL_YCGL_CAR { get; set; }

    public virtual DbSet<SZHL_DRAFT> SZHL_DRAFT { get; set; }

    public virtual DbSet<APPConfig> APPConfigs { get; set; }

    public virtual DbSet<WX_User> WX_User { get; set; }

    public virtual DbSet<WX_Group> WX_Group { get; set; }

    public virtual DbSet<WX_UserGroup> WX_UserGroup { get; set; }

    public virtual DbSet<WX_RY> WX_RY { get; set; }

    public virtual DbSet<WX_HD> WX_HD { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<SZHL_ZCGL_Type> SZHL_ZCGL_Type { get; set; }

    public virtual DbSet<SZHL_ZCGL_Location> SZHL_ZCGL_Location { get; set; }

    public virtual DbSet<SZHL_ZCGL> SZHL_ZCGL { get; set; }

}

}

